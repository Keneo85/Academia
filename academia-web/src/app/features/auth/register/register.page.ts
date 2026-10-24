import { Component, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormField, email, form, maxLength, minLength, required, submit, validate } from '@angular/forms/signals';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { AuthService } from '../../../core/services/auth.service';
import { CreateAlumnoRequest } from '../../../core/models/alumno.model';
import { apiError } from '../../../core/utils/api-error';

type RegisterModel = CreateAlumnoRequest & { confirmPassword: string };

@Component({
  selector: 'app-register-page',
  imports: [
    FormField, RouterLink,
    MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatProgressBarModule,
  ],
  templateUrl: './register.page.html',
  styleUrl: '../auth.scss',
})
export class RegisterPage {
  private readonly auth = inject(AuthService);
  private readonly router = inject(Router);

  protected readonly model = signal<RegisterModel>({
    nombres: '',
    apellidos: '',
    email: '',
    telefono: '',
    dni: '',
    userName: '',
    password: '',
    confirmPassword: '',
  });

  protected readonly f = form(this.model, (p) => {
    required(p.nombres);
    required(p.apellidos);
    required(p.email);
    email(p.email);
    required(p.telefono);
    required(p.dni);
    minLength(p.dni, 8);
    maxLength(p.dni, 8);
    required(p.userName);
    required(p.password);
    minLength(p.password, 6);
    required(p.confirmPassword);
    validate(p.confirmPassword, ({ value, valueOf }) =>
      value() === valueOf(p.password)
        ? undefined
        : { kind: 'mismatch', message: 'Las contraseñas no coinciden' },
    );
  });

  protected readonly error = signal<string | null>(null);

  protected onSubmit(event: Event) {
    event.preventDefault();
    submit(this.f, async () => {
      this.error.set(null);
      const { confirmPassword, ...body } = this.model();
      try {
        await this.auth.register(body);
        this.router.navigate(['/']);
      } catch (err) {
        this.error.set(apiError(err));
      }
      return undefined;
    });
  }
}
