export interface DetalleMatriculaRequest {
  cursoId: number;
  cantidad: number;
}

export interface CreateMatriculaRequest {
  alumnoId: number;
  detalles: DetalleMatriculaRequest[];
}

export interface MatriculaListItem {
  id: number;
  alumnoId: number;
  alumno: string;
  total: number;
  cantidadCursos: number;
  createdAt: string;
}

export interface MatriculaDetalle {
  id: number;
  alumnoId: number;
  alumno: string;
  total: number;
  createdAt: string;
  detalles: {
    cursoId: number;
    curso: string;
    cantidad: number;
    precioUnitario: number;
    subTotal: number;
  }[];
}
