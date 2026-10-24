import { Injectable, computed, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { API_ROUTES } from '../constants/api-routes.constants';
import { ApiResult } from '../models/api-result.model';
import { LoginRequest, LoginResponse, Role } from '../models/auth.model';
import { CreateAlumnoRequest } from '../models/alumno.model';

const STORAGE_KEY = 'academia.session';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly router = inject(Router);

  private readonly session = signal<LoginResponse | null>(this.loadSession());

  readonly token = computed(() => this.session()?.token ?? null);
  readonly role = computed(() => this.session()?.role ?? null);
  readonly isLoggedIn = computed(() => this.token() !== null);
  readonly isAdmin = computed(() => this.role() === 'Admin');

  hasRole(role: Role): boolean {
    return this.role() === role;
  }

  async login(request: LoginRequest): Promise<void> {
    const res = await firstValueFrom(
      this.http.post<ApiResult<LoginResponse>>(API_ROUTES.AUTH.LOGIN, request),
    );
    this.session.set(res.value!);
    localStorage.setItem(STORAGE_KEY, JSON.stringify(res.value));
  }

  async register(request: CreateAlumnoRequest): Promise<void> {
    await firstValueFrom(this.http.post<ApiResult>(API_ROUTES.AUTH.REGISTER, request));
    await this.login({ userName: request.userName, password: request.password });
  }

  logout(): void {
    this.session.set(null);
    localStorage.removeItem(STORAGE_KEY);
    this.router.navigate(['/login']);
  }

  private loadSession(): LoginResponse | null {
    const raw = localStorage.getItem(STORAGE_KEY);
    if (!raw) return null;
    const s = JSON.parse(raw) as LoginResponse;
    if (new Date(s.expirationDate) <= new Date()) {
      localStorage.removeItem(STORAGE_KEY);
      return null;
    }
    return s;
  }
}
