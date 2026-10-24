import { Component, computed, inject, signal } from '@angular/core';
import { CurrencyPipe } from '@angular/common';
import { httpResource } from '@angular/common/http';
import { FormField, applyEach, form, min, submit } from '@angular/forms/signals';
import { firstValueFrom } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { API_ROUTES } from '../../../core/constants/api-routes.constants';
import { ApiResult } from '../../../core/models/api-result.model';
import { CursoListItem } from '../../../core/models/curso.model';
import { CreateMatriculaRequest } from '../../../core/models/matricula.model';
import { AlumnoListItem } from '../../../core/models/alumno.model';
import { MatriculaService } from '../../../core/services/matricula.service';
import { NotificationService } from '../../../core/services/notification.service';
import { apiError } from '../../../core/utils/api-error';
import { PageHeaderComponent } from '../../../shared/components/page-header/page-header';

const NUEVO_DETALLE = { cursoId: 0, cantidad: 1 };

@Component({
  selector: 'app-matriculas-form-page',
  imports: [
    FormField, CurrencyPipe,
    MatCardModule, MatFormFieldModule, MatInputModule, MatSelectModule,
    MatButtonModule, MatIconModule, MatTooltipModule,
    PageHeaderComponent,
  ],
  templateUrl: './matriculas-form.page.html',
  styleUrl: './matriculas-form.page.scss',
})
export class MatriculasFormPage {
  private readonly matriculaService = inject(MatriculaService);
  private readonly notification = inject(NotificationService);

  // Cursos para el mat-select (primera página de 100)
  private readonly cursosRes = httpResource<ApiResult<CursoListItem[]>>(() => ({
    url: API_ROUTES.CURSOS.BASE,
    params: { pageNumber: 1, pageSize: 100 },
  }));
  protected readonly cursos = computed(() => (this.cursosRes.hasValue() ? (this.cursosRes.value().value ?? []) : []));

  // Alumnos para el mat-select (primeros 100)
  protected readonly alumnosRes = httpResource<ApiResult<AlumnoListItem[]>>(() => ({
    url: API_ROUTES.ALUMNOS.BASE,
    params: { pageNumber: 1, pageSize: 100 },
  }));
  protected readonly alumnos = computed(() => (this.alumnosRes.hasValue() ? (this.alumnosRes.value().value ?? []) : []));

  protected readonly model = signal<CreateMatriculaRequest>({
    alumnoId: 0,
    detalles: [{ ...NUEVO_DETALLE }],
  });

  protected readonly f = form(this.model, (p) => {
    min(p.alumnoId, 1);
    // applyEach: aplica estas reglas a cada elemento del array
    applyEach(p.detalles, (d) => {
      min(d.cursoId, 1);
      min(d.cantidad, 1);
    });
  });

  // Total calculado en vivo con computed
  protected readonly total = computed(() =>
    this.model().detalles.reduce((acc, d) => acc + this.precioDe(d.cursoId) * d.cantidad, 0),
  );

  protected precioDe(cursoId: number): number {
    return this.cursos().find((c) => c.id === cursoId)?.precio ?? 0;
  }

  protected agregar() {
    // Signals son inmutables: update devuelve un objeto NUEVO
    this.model.update((m) => ({ ...m, detalles: [...m.detalles, { ...NUEVO_DETALLE }] }));
  }

  protected quitar(index: number) {
    this.model.update((m) => ({ ...m, detalles: m.detalles.filter((_, i) => i !== index) }));
  }

  protected onSubmit(event: Event) {
    event.preventDefault();
    submit(this.f, async () => {
      try {
        const res = await firstValueFrom(this.matriculaService.create(this.model()));
        this.notification.success(res.message ?? 'Matrícula registrada.');
        this.model.set({ alumnoId: 0, detalles: [{ ...NUEVO_DETALLE }] });
        this.f().reset();
      } catch (err) {
        this.notification.error(apiError(err));
      }
      return undefined;
    });
  }
}
