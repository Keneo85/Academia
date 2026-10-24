import { Routes } from '@angular/router';
import { authGuard, guestGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  // Rutas públicas (sin sesión)
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

  // Rutas protegidas: todas dentro del Shell (toolbar + sidenav)
  {
    path: '',
    // Lazy también: así Material del layout no pesa en la carga inicial (login)
    loadComponent: () => import('./layout/shell/shell.component').then((m) => m.ShellComponent),
    canActivate: [authGuard],
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'cursos' },
      // loadChildren: cada feature trae su propio archivo de rutas
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

  // Cualquier otra URL
  {
    path: '**',
    loadComponent: () => import('./features/errors/notfound/notfound.page').then((m) => m.NotfoundPage),
  },
];
