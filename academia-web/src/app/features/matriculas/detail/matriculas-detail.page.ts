import { Component, inject, input, numberAttribute } from '@angular/core';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { rxResource } from '@angular/core/rxjs-interop';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatriculaService } from '../../../core/services/matricula.service';
import { PageHeaderComponent } from '../../../shared/components/page-header/page-header';
import { EmptyStateComponent } from '../../../shared/components/empty-state/empty-state';

@Component({
  selector: 'app-matriculas-detail-page',
  imports: [
    RouterLink, CurrencyPipe, DatePipe,
    MatCardModule, MatTableModule, MatButtonModule, MatIconModule, MatProgressBarModule,
    PageHeaderComponent, EmptyStateComponent,
  ],
  templateUrl: './matriculas-detail.page.html',
  styleUrl: './matriculas-detail.page.scss',
})
export class MatriculasDetailPage {
  private readonly matriculaService = inject(MatriculaService);

  readonly id = input.required({ transform: numberAttribute });

  protected readonly matricula = rxResource({
    params: () => this.id(),
    stream: ({ params: id }) => this.matriculaService.getById(id),
  });

  protected readonly columns = ['curso', 'cantidad', 'precio', 'subtotal'];
}
