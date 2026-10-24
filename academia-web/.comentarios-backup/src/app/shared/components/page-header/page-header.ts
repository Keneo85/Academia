import { Component, input } from '@angular/core';

// Título de página + subtítulo opcional + botones a la derecha (via <ng-content>)
// Uso: <app-page-header title="Cursos" subtitle="..."> <button>...</button> </app-page-header>
@Component({
  selector: 'app-page-header',
  templateUrl: './page-header.html',
  styleUrl: './page-header.scss',
})
export class PageHeaderComponent {
  readonly title = input.required<string>();
  readonly subtitle = input<string>();
}
