import { Directive, TemplateRef, ViewContainerRef, effect, inject, input } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { Role } from '../../core/models/auth.model';

// Directiva estructural: muestra el bloque solo si el usuario tiene el rol.
// Uso: <button *appHasRole="'Admin'">Eliminar</button>
// Con input() + effect() reacciona sola si cambia la sesión (logout, otro login...)
@Directive({ selector: '[appHasRole]' })
export class HasRoleDirective {
  private readonly auth = inject(AuthService);
  private readonly templateRef = inject(TemplateRef<unknown>);
  private readonly viewContainer = inject(ViewContainerRef);

  readonly appHasRole = input.required<Role>();

  constructor() {
    effect(() => {
      this.viewContainer.clear();
      if (this.auth.hasRole(this.appHasRole())) {
        this.viewContainer.createEmbeddedView(this.templateRef);
      }
    });
  }
}
