import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { API_ROUTES } from '../constants/api-routes.constants';
import { ApiResult } from '../models/api-result.model';
import { CreateMatriculaRequest, MatriculaDetalle } from '../models/matricula.model';

@Injectable({ providedIn: 'root' })
export class MatriculaService {
  private readonly http = inject(HttpClient);

  create(body: CreateMatriculaRequest): Observable<ApiResult> {
    return this.http.post<ApiResult>(API_ROUTES.MATRICULAS.BASE, body);
  }

  getById(id: number): Observable<MatriculaDetalle> {
    return this.http
      .get<ApiResult<MatriculaDetalle>>(API_ROUTES.MATRICULAS.BY_ID(id))
      .pipe(map((res) => res.value!));
  }
}
