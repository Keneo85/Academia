namespace Academia.Business.DTO.Request.Matricula
{
    public class CreateMatriculaRequest
    {
        public int AlumnoId { get; set; }
        public List<CreateDetalleMatriculaRequest> Detalles { get; set; } = new();
    }

    public class CreateDetalleMatriculaRequest
    {
        public int CursoId { get; set; }
        public int Cantidad { get; set; }
    }
}