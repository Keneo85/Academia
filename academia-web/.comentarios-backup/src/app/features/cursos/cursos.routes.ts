import { Routes } from '@angular/router';
import { roleGuard } from '../../core/guards/role.guard';

// Rutas hijas de /cursos (se cargan con loadChildren desde app.routes.ts)
export const CURSOS_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () => import('./list/cursos-list.page').then((m) => m.CursosListPage),
  },
  {
    path: 'nuevo',
    canActivate: [roleGuard('Admin')],
    loadComponent: () => import('./form/cursos-form.page').then((m) => m.CursosFormPage),
  },
  {
    path: ':id/editar',
    canActivate: [roleGuard('Admin')],
    loadComponent: () => import('./form/cursos-form.page').then((m) => m.CursosFormPage),
  },
  {
    path: ':id',
    loadComponent: () => import('./detail/cursos-detail.page').then((m) => m.CursosDetailPage),
  },
];
