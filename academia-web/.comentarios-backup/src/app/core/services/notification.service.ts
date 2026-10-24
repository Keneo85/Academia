import { Injectable, inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

// Envoltorio de MatSnackBar: mensajes flotantes abajo de la pantalla
@Injectable({ providedIn: 'root' })
export class NotificationService {
  private readonly snack = inject(MatSnackBar);

  success(message: string) {
    this.snack.open(message, 'OK', { duration: 3000, panelClass: 'snack-ok' });
  }

  error(message: string) {
    this.snack.open(message, 'Cerrar', { duration: 6000, panelClass: 'snack-error' });
  }
}
