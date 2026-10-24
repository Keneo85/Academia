import { Component, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FormField, email, form, maxLength, minLength, required, submit } from '@angular/forms/signals';
import { firstValueFrom } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { CreateAlumnoRequest } from '../../../core/models/alumno.model';
import { AlumnoService } from '../../../core/services/alumno.service';
import { NotificationService } from '../../../core/services/notification.service';
import { apiError } from '../../../core/utils/api-error';
import { PageHeaderComponent } from '../../../shared/components/page-header/page-header';

const EMPTY: CreateAlumnoRequest = {
  nombres: '',
  apellidos: '',
  email: '',
  telefono: '',
  dni: '',
  userName: '',
  password: '',
};

@Component({
  selector: 'app-alumnos-form-page',
  imports: [
    FormField, RouterLink,
    MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule, MatDividerModule,
    PageHeaderComponent,
  ],
  templateUrl: './alumnos-form.page.html',
})
export class AlumnosFormPage {
  private readonly alumnoService = inject(AlumnoService);
  private readonly notification = inject(NotificationService);

  protected readonly model = signal<CreateAlumnoRequest>({ ...EMPTY });
  protected readonly f = form(this.model, (p) => {
    required(p.nombres);
    required(p.apellidos);
    required(p.email);
    email(p.email);
    required(p.telefono);
    required(p.dni);
    minLength(p.dni, 8);
    maxLength(p.dni, 8); // también pone maxlength en el <input>
    required(p.userName);
    required(p.password);
    minLength(p.password, 6);
  });

  protected onSubmit(event: Event) {
    event.preventDefault();
    submit(this.f, async () => {
      try {
        const res = await firstValueFrom(this.alumnoService.register(this.model()));
        this.notification.success(res.message ?? 'Alumno registrado.');
        this.model.set({ ...EMPTY });
        this.f().reset(); // limpia touched/dirty
      } catch (err) {
        this.notification.error(apiError(err));
      }
      return undefined;
    });
  }
}
