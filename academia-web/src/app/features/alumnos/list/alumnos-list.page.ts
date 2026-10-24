import { Component, computed, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { httpResource } from '@angular/common/http';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { API_ROUTES } from '../../../core/constants/api-routes.constants';
import { ApiResult } from '../../../core/models/api-result.model';
import { AlumnoListItem } from '../../../core/models/alumno.model';
import { isNotFound } from '../../../core/utils/resource-error';
import { PageHeaderComponent } from '../../../shared/components/page-header/page-header';
import { EmptyStateComponent } from '../../../shared/components/empty-state/empty-state';

@Component({
  selector: 'app-alumnos-list-page',
  imports: [
    RouterLink, DatePipe,
    MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatFormFieldModule, MatInputModule,
    MatProgressBarModule, MatTooltipModule,
    PageHeaderComponent, EmptyStateComponent,
  ],
  templateUrl: './alumnos-list.page.html',
  styleUrl: './alumnos-list.page.scss',
})
export class AlumnosListPage {
  protected readonly page = signal(1);
  protected readonly pageSize = 4;
  protected readonly search = signal('');

  protected readonly alumnos = httpResource<ApiResult<AlumnoListItem[]>>(() => ({
    url: API_ROUTES.ALUMNOS.BASE,
    params: { pageNumber: this.page(), pageSize: this.pageSize, search: this.search() },
  }));

  protected readonly rows = computed(() => (this.alumnos.hasValue() ? (this.alumnos.value().value ?? []) : []));
  protected readonly notFound = computed(() => isNotFound(this.alumnos.error()));
  protected readonly columns = ['id', 'nombre', 'dni', 'email', 'telefono', 'usuario', 'creado', 'acciones'];

  protected buscar(texto: string) {
    this.page.set(1);
    this.search.set(texto.trim());
  }
}
