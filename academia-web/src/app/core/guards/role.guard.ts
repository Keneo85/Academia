import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Role } from '../models/auth.model';

export const roleGuard =
  (requiredRole: Role): CanActivateFn =>
  () => {
    const auth = inject(AuthService);
    return auth.hasRole(requiredRole) ? true : inject(Router).createUrlTree(['/403']);
  };
