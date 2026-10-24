import { Routes } from '@angular/router';
import { roleGuard } from '../../core/guards/role.guard';

export const MATRICULAS_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () => import('./list/matriculas-list.page').then((m) => m.MatriculasListPage),
  },
  {
    path: 'nueva',
    canActivate: [roleGuard('Admin')],
    loadComponent: () => import('./form/matriculas-form.page').then((m) => m.MatriculasFormPage),
  },
  {
    path: ':id',
    loadComponent: () => import('./detail/matriculas-detail.page').then((m) => m.MatriculasDetailPage),
  },
];
