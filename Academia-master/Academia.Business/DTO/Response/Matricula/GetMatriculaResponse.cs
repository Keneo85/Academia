namespace Academia.Business.DTO.Response.Matricula
{
    public class GetMatriculaResponse
    {
        public int Id { get; set; }
        public int AlumnoId { get; set; }
        public string Alumno { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<GetDetalleMatriculaResponse> Detalles { get; set; } = new();
    }

    public class GetDetalleMatriculaResponse
    {
        public int CursoId { get; set; }
        public string Curso { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
    }
}
