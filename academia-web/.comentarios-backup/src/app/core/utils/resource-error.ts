import { HttpErrorResponse } from '@angular/common/http';

// true si el endpoint todavía no existe en el backend:
//   404 = la ruta no existe | 405 = la ruta existe pero solo para otro verbo (p. ej. solo POST)
// Sirve para que el front siga funcionando aunque falten los GET opcionales.
export function isNotFound(error: unknown): boolean {
  return error instanceof HttpErrorResponse && (error.status === 404 || error.status === 405);
}
