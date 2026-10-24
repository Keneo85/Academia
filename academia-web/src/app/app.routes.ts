import { Routes } from '@angular/router';
import { authGuard, guestGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: 'login',
    canActivate: [guestGuard],
    loadComponent: () => import('./features/auth/login/login.page').then((m) => m.LoginPage),
  },
  {
    path: 'registro',
    canActivate: [guestGuard],
    loadComponent: () => import('./features/auth/register/register.page').then((m) => m.RegisterPage),
  },
  {
    path: '403',
    loadComponent: () => import('./features/errors/forbidden/forbidden.page').then((m) => m.ForbiddenPage),
  },

  {
    path: '',
    loadComponent: () => import('./layout/shell/shell.component').then((m) => m.ShellComponent),
    canActivate: [authGuard],
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'cursos' },
      {
        path: 'cursos',
        loadChildren: () => import('./features/cursos/cursos.routes').then((m) => m.CURSOS_ROUTES),
      },
      {
        path: 'alumnos',
        loadChildren: () => import('./features/alumnos/alumnos.routes').then((m) => m.ALUMNOS_ROUTES),
      },
      {
        path: 'matriculas',
        loadChildren: () =>
          import('./features/matriculas/matriculas.routes').then((m) => m.MATRICULAS_ROUTES),
      },
    ],
  },

  {
    path: '**',
    loadComponent: () => import('./features/errors/notfound/notfound.page').then((m) => m.NotfoundPage),
  },
];
