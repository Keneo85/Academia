import { Component, computed, inject, signal } from '@angular/core';
import { toObservable, toSignal } from '@angular/core/rxjs-interop';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { httpResource } from '@angular/common/http';
import { RouterLink } from '@angular/router';
import { debounceTime, distinctUntilChanged, firstValueFrom, map, tap } from 'rxjs';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDialog } from '@angular/material/dialog';
import { API_ROUTES } from '../../../core/constants/api-routes.constants';
import { ApiResult } from '../../../core/models/api-result.model';
import { CursoListItem } from '../../../core/models/curso.model';
import { AuthService } from '../../../core/services/auth.service';
import { CursoService } from '../../../core/services/curso.service';
import { NotificationService } from '../../../core/services/notification.service';
import { apiError } from '../../../core/utils/api-error';
import { PageHeaderComponent } from '../../../shared/components/page-header/page-header';
import { EmptyStateComponent } from '../../../shared/components/empty-state/empty-state';
import { ConfirmDialogComponent } from '../../../shared/components/confirm-dialog/confirm-dialog';
import { HasRoleDirective } from '../../../shared/directives/has-role.directive';

@Component({
  selector: 'app-cursos-list-page',
  imports: [
    RouterLink, CurrencyPipe, DatePipe,
    MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatFormFieldModule, MatInputModule,
    MatProgressBarModule, MatTooltipModule,
    PageHeaderComponent, EmptyStateComponent, HasRoleDirective,
  ],
  templateUrl: './cursos-list.page.html',
  styleUrl: './cursos-list.page.scss',
})
export class CursosListPage {
  private readonly auth = inject(AuthService);
  private readonly cursoService = inject(CursoService);
  private readonly notification = inject(NotificationService);
  private readonly dialog = inject(MatDialog);

  protected readonly page = signal(1);
  protected readonly pageSize = 4;

  protected readonly searchText = signal('');
  protected readonly search = toSignal(
    toObservable(this.searchText).pipe(
      map((t) => t.trim()),
      debounceTime(400),
      distinctUntilChanged(),
      tap(() => this.page.set(1)),
    ),
    { initialValue: '' },
  );

  protected readonly cursos = httpResource<ApiResult<CursoListItem[]>>(() => ({
    url: API_ROUTES.CURSOS.BASE,
    params: { pageNumber: this.page(), pageSize: this.pageSize, search: this.search() },
  }));

  protected readonly rows = computed(() => (this.cursos.hasValue() ? (this.cursos.value().value ?? []) : []));

  protected readonly columns = computed(() => {
    const base = ['id', 'nombre', 'especialidad', 'precio', 'cupos', 'creado'];
    return this.auth.isAdmin() ? [...base, 'acciones'] : base;
  });

  protected async eliminar(c: CursoListItem) {
    const ref = this.dialog.open(ConfirmDialogComponent, {
      data: { title: 'Eliminar curso', message: `¿Seguro que quieres eliminar "${c.nombre}"?`, confirmText: 'Eliminar' },
    });
    const confirmed = await firstValueFrom(ref.afterClosed());
    if (!confirmed) return;

    try {
      const res = await firstValueFrom(this.cursoService.delete(c.id));
      this.notification.success(res.message ?? 'Curso eliminado.');
      this.cursos.reload();
    } catch (err) {
      this.notification.error(apiError(err));
    }
  }
}
