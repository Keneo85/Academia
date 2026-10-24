import { Component, input } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';

// Mensaje para listas vacías o errores de carga
@Component({
  selector: 'app-empty-state',
  imports: [MatIconModule],
  templateUrl: './empty-state.html',
  styleUrl: './empty-state.scss',
})
export class EmptyStateComponent {
  readonly icon = input('inbox');
  readonly message = input.required<string>();
}
