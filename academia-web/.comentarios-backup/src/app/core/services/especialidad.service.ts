import { Injectable } from '@angular/core';
import { httpResource } from '@angular/common/http';
import { API_ROUTES } from '../constants/api-routes.constants';
import { ApiResult } from '../models/api-result.model';
import { Especialidad } from '../models/especialidad.model';

@Injectable({ providedIn: 'root' })
export class EspecialidadService {
  // httpResource dentro de un servicio: cada componente que lo llama obtiene su propio recurso.
  // Debe llamarse en un contexto de inyección (al declarar un campo del componente).
  listResource() {
    return httpResource<ApiResult<Especialidad[]>>(() => API_ROUTES.ESPECIALIDADES.BASE);
  }
}
