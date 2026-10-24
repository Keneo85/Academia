import { Component, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormField, form, required, submit } from '@angular/forms/signals';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { AuthService } from '../../../core/services/auth.service';
import { LoginRequest } from '../../../core/models/auth.model';
import { apiError } from '../../../core/utils/api-error';

@Component({
  selector: 'app-login-page',
  imports: [
    FormField, RouterLink,
    MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule, MatProgressBarModule,
  ],
  templateUrl: './login.page.html',
  styleUrl: '../auth.scss',
})
export class LoginPage {
  private readonly auth = inject(AuthService);
  private readonly router = inject(Router);

  protected readonly model = signal<LoginRequest>({ userName: '', password: '' });
  protected readonly f = form(this.model, (p) => {
    required(p.userName);
    required(p.password);
  });

  protected readonly error = signal<string | null>(null);
  protected readonly showPass = signal(false);

  protected onSubmit(event: Event) {
    event.preventDefault();
    submit(this.f, async () => {
      this.error.set(null);
      try {
        await this.auth.login(this.model());
        this.router.navigate(['/']);
      } catch (err) {
        this.error.set(apiError(err));
      }
      return undefined;
    });
  }
}
