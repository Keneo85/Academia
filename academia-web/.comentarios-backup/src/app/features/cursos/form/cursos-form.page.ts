import { Component, OnInit, inject, input, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormField, form, min, required, submit } from '@angular/forms/signals';
import { firstValueFrom } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { CursoRequest } from '../../../core/models/curso.model';
import { CursoService } from '../../../core/services/curso.service';
import { EspecialidadService } from '../../../core/services/especialidad.service';
import { NotificationService } from '../../../core/services/notification.service';
import { apiError } from '../../../core/utils/api-error';
import { PageHeaderComponent } from '../../../shared/components/page-header/page-header';

@Component({
  selector: 'app-cursos-form-page',
  imports: [
    FormField, RouterLink,
    MatCardModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatButtonModule, MatProgressBarModule,
    PageHeaderComponent,
  ],
  templateUrl: './cursos-form.page.html',
})
export class CursosFormPage implements OnInit {
  private readonly cursoService = inject(CursoService);
  private readonly notification = inject(NotificationService);
  private readonly router = inject(Router);

  // Opciones del mat-select de especialidad (GET /api/especialidades)
  protected readonly especialidades = inject(EspecialidadService).listResource();

  // Viene de la ruta "cursos/:id/editar" gracias a withComponentInputBinding()
  // En "cursos/nuevo" no hay :id -> undefined
  readonly id = input<string>();

  protected readonly model = signal<CursoRequest>({
    nombre: '',
    descripcion: '',
    especialidadId: 0,
    precio: 0,
    cuposDisponibles: 0,
  });

  // Reglas de validación del formulario (schema)
  protected readonly f = form(this.model, (p) => {
    required(p.nombre);
    min(p.especialidadId, 1);
    min(p.precio, 0);
    min(p.cuposDisponibles, 0);
  });

  async ngOnInit() {
    const id = this.id();
    if (!id) return;
    try {
      const c = await firstValueFrom(this.cursoService.getById(+id));
      // Cargamos los datos del curso en el modelo -> el form se actualiza solo
      this.model.set({
        nombre: c.nombre,
        descripcion: c.descripcion,
        especialidadId: c.especialidadId,
        precio: c.precio,
        cuposDisponibles: c.cuposDisponibles,
      });
    } catch (err) {
      this.notification.error(apiError(err));
    }
  }

  protected onSubmit(event: Event) {
    event.preventDefault();
    submit(this.f, async () => {
      try {
        const id = this.id();
        const body = this.model();
        const res = await firstValueFrom(
          id ? this.cursoService.update(+id, body) : this.cursoService.create(body),
        );
        this.notification.success(res.message ?? 'Curso guardado.');
        this.router.navigate(['/cursos']);
      } catch (err) {
        this.notification.error(apiError(err));
      }
      return undefined;
    });
  }
}
