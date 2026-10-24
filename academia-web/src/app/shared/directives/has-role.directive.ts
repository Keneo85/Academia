import { Directive, TemplateRef, ViewContainerRef, effect, inject, input } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { Role } from '../../core/models/auth.model';

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
