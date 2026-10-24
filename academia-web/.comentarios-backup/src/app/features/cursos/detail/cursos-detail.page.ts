import { Component, computed, inject, input, numberAttribute } from '@angular/core';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { rxResource } from '@angular/core/rxjs-interop';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { CursoService } from '../../../core/services/curso.service';
import { EspecialidadService } from '../../../core/services/especialidad.service';
import { PageHeaderComponent } from '../../../shared/components/page-header/page-header';
import { EmptyStateComponent } from '../../../shared/components/empty-state/empty-state';
import { HasRoleDirective } from '../../../shared/directives/has-role.directive';

@Component({
  selector: 'app-cursos-detail-page',
  imports: [
    RouterLink, CurrencyPipe, DatePipe,
    MatCardModule, MatButtonModule, MatIconModule, MatListModule, MatProgressBarModule,
    PageHeaderComponent, EmptyStateComponent, HasRoleDirective,
  ],
  templateUrl: './cursos-detail.page.html',
})
export class CursosDetailPage {
  private readonly cursoService = inject(CursoService);

  // :id de la ruta -> input. numberAttribute convierte "5" en 5
  readonly id = input.required({ transform: numberAttribute });

  // rxResource: como httpResource pero usando un método del servicio (Observable).
  // Se vuelve a ejecutar si cambia id()
  protected readonly curso = rxResource({
    params: () => this.id(),
    stream: ({ params: id }) => this.cursoService.getById(id),
  });

  // El detalle trae solo especialidadId: buscamos el nombre en la lista de especialidades
  private readonly especialidades = inject(EspecialidadService).listResource();
  protected readonly especialidad = computed(() => {
    const id = this.curso.hasValue() ? this.curso.value().especialidadId : undefined;
    // Si la API no tiene GET /api/especialidades, mostramos solo el ID
    const lista = this.especialidades.hasValue() ? (this.especialidades.value().value ?? []) : [];
    return lista.find((e) => e.id === id)?.nombre ?? `#${id}`;
  });
}
