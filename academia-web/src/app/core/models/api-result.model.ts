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
