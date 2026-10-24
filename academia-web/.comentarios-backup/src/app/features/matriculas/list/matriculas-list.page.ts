import { Component, computed, inject, input, signal } from '@angular/core';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { httpResource } from '@angular/common/http';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { API_ROUTES } from '../../../core/constants/api-routes.constants';
import { ApiResult } from '../../../core/models/api-result.model';
import { MatriculaListItem } from '../../../core/models/matricula.model';
import { AuthService } from '../../../core/services/auth.service';
import { isNotFound } from '../../../core/utils/resource-error';
import { PageHeaderComponent } from '../../../shared/components/page-header/page-header';
import { EmptyStateComponent } from '../../../shared/components/empty-state/empty-state';
import { HasRoleDirective } from '../../../shared/directives/has-role.directive';

@Component({
  selector: 'app-matriculas-list-page',
  imports: [
    RouterLink, CurrencyPipe, DatePipe,
    MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatChipsModule,
    MatProgressBarModule, MatTooltipModule,
    PageHeaderComponent, EmptyStateComponent, HasRoleDirective,
  ],
  templateUrl: './matriculas-list.page.html',
  styleUrl: './matriculas-list.page.scss',
})
export class MatriculasListPage {
  protected readonly auth = inject(AuthService);

  // Query param ?alumnoId=5 (llega como input gracias a withComponentInputBinding)
  readonly alumnoId = input<string>();

  protected readonly page = signal(1);
  protected readonly pageSize = 10;

  // Una sola URL según el rol: Admin -> /matriculas (paginado) | Alumno -> /matriculas/mias
  // Devolver undefined = no hacer la petición (por ejemplo, justo al cerrar sesión)
  protected readonly matriculas = httpResource<ApiResult<MatriculaListItem[]>>(() =>
    !this.auth.isLoggedIn()
      ? undefined
      : this.auth.isAdmin()
      ? {
          url: API_ROUTES.MATRICULAS.BASE,
          params: {
            pageNumber: this.page(),
            pageSize: this.pageSize,
            ...(this.alumnoId() ? { alumnoId: this.alumnoId()! } : {}),
          },
        }
      : { url: API_ROUTES.MATRICULAS.MIAS },
  );

  protected readonly rows = computed(() => (this.matriculas.hasValue() ? (this.matriculas.value().value ?? []) : []));
  protected readonly notFound = computed(() => isNotFound(this.matriculas.error()));

  protected readonly columns = computed(() =>
    this.auth.isAdmin()
      ? ['id', 'alumno', 'cursos', 'total', 'fecha', 'acciones']
      : ['id', 'cursos', 'total', 'fecha', 'acciones'],
  );
}
