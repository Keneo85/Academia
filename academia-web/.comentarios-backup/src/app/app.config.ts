import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter, withComponentInputBinding } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { routes } from './app.routes';
import { authInterceptor } from './core/interceptors/auth.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    // withComponentInputBinding: los parámetros de ruta (:id) llegan como input() del componente
    provideRouter(routes, withComponentInputBinding()),
    // HttpClient + nuestro interceptor JWT
    provideHttpClient(withInterceptors([authInterceptor])),
  ],
};
