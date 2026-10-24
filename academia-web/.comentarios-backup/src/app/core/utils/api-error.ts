import { HttpErrorResponse } from '@angular/common/http';

// Convierte cualquier error HTTP en un texto legible para mostrar en pantalla.
export function apiError(err: unknown): string {
  if (err instanceof HttpErrorResponse) {
    if (err.status === 0) return 'No hay conexión con la API (¿está levantada? ¿CORS habilitado?).';
    if (err.status === 403) return 'No tienes permisos para esta acción (requiere rol Admin).';
    const body = err.error;
    if (body?.message) return body.message;
    if (body?.errors?.length) return body.errors.join(' · ');
    if (body?.error) return body.error;
    return `Error ${err.status}`;
  }
  return 'Error inesperado.';
}
