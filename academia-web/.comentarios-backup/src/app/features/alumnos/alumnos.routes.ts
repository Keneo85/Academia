import { Routes } from '@angular/router';
import { roleGuard } from '../../core/guards/role.guard';

// Todo el módulo de alumnos es solo para Admin
export const ALUMNOS_ROUTES: Routes = [
  {
    path: '',
    canActivate: [roleGuard('Admin')],
    loadComponent: () => import('./list/alumnos-list.page').then((m) => m.AlumnosListPage),
  },
  {
    path: 'nuevo',
    canActivate: [roleGuard('Admin')],
    loadComponent: () => import('./form/alumnos-form.page').then((m) => m.AlumnosFormPage),
  },
];
