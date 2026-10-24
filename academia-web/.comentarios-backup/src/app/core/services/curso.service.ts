import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { API_ROUTES } from '../constants/api-routes.constants';
import { ApiResult } from '../models/api-result.model';
import { CursoDetalle, CursoRequest } from '../models/curso.model';

// El listado paginado se hace con httpResource en la página (cursos-list.page.ts)
@Injectable({ providedIn: 'root' })
export class CursoService {
  private readonly http = inject(HttpClient);

  getById(id: number): Observable<CursoDetalle> {
    return this.http
      .get<ApiResult<CursoDetalle>>(API_ROUTES.CURSOS.BY_ID(id))
      .pipe(map((res) => res.value!)); // map: nos quedamos solo con value
  }

  create(body: CursoRequest): Observable<ApiResult> {
    return this.http.post<ApiResult>(API_ROUTES.CURSOS.BASE, body);
  }

  update(id: number, body: CursoRequest): Observable<ApiResult> {
    return this.http.put<ApiResult>(API_ROUTES.CURSOS.BY_ID(id), body);
  }

  delete(id: number): Observable<ApiResult> {
    return this.http.delete<ApiResult>(API_ROUTES.CURSOS.BY_ID(id));
  }
}
