// También se usa para el registro público (POST /api/auth/register)
export interface CreateAlumnoRequest {
  nombres: string;
  apellidos: string;
  email: string;
  telefono: string;
  dni: string;
  userName: string;
  password: string;
}

export interface AlumnoListItem {
  id: number;
  nombres: string;
  apellidos: string;
  email: string;
  telefono: string;
  dni: string;
  userName: string;
  createdAt: string;
}
