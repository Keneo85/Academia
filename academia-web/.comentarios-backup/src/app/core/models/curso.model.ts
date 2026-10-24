export interface CursoListItem {
  id: number;
  nombre: string;
  descripcion: string;
  especialidad: string;
  precio: number;
  cuposDisponibles: number;
  createdAt: string;
  createdBy: string;
}

export interface CursoDetalle {
  id: number;
  nombre: string;
  descripcion: string;
  especialidadId: number;
  precio: number;
  cuposDisponibles: number;
  createdAt: string;
  createdBy: string;
}

// Sirve para crear (POST) y actualizar (PUT)
export interface CursoRequest {
  nombre: string;
  descripcion: string;
  especialidadId: number;
  precio: number;
  cuposDisponibles: number;
}
