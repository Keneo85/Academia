import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_ROUTES } from '../constants/api-routes.constants';
import { ApiResult } from '../models/api-result.model';
import { CreateAlumnoRequest } from '../models/alumno.model';

@Injectable({ providedIn: 'root' })
export class AlumnoService {
  private readonly http = inject(HttpClient);

  register(body: CreateAlumnoRequest): Observable<ApiResult> {
    return this.http.post<ApiResult>(API_ROUTES.ALUMNOS.BASE, body);
  }
}
