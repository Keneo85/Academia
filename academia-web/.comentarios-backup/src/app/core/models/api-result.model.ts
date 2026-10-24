// Respuesta genérica de la API (Academia.Common.Helpers.Result).
// .NET serializa en camelCase: IsSuccess -> isSuccess, Value -> value...
export interface ApiResult<T = void> {
  isSuccess: boolean;
  isFailure: boolean;
  message: string | null;
  errors: string[];
  value?: T;
}

export interface PagedRequest {
  pageNumber: number;
  pageSize: number;
}
