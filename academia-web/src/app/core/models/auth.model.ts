export type Role = 'Admin' | 'Alumno';

export interface LoginRequest {
  userName: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  expirationDate: string;
  role: Role;
}
