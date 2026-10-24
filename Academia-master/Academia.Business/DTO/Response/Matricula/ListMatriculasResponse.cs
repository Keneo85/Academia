namespace Academia.Business.DTO.Response.Matricula
{
    public class ListMatriculasResponse
    {
        public int Id { get; set; }
        public int AlumnoId { get; set; }
        public string Alumno { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public int CantidadCursos { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
