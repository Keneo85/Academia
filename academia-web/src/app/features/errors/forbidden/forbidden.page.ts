import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { EmptyStateComponent } from '../../../shared/components/empty-state/empty-state';

@Component({
  selector: 'app-forbidden-page',
  imports: [RouterLink, MatButtonModule, EmptyStateComponent],
  templateUrl: './forbidden.page.html',
  styleUrl: '../errors.scss',
})
export class ForbiddenPage {}
