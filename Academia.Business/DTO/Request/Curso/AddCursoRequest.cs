namespace Academia.Business.DTO.Request.Curso
{
    public class AddCursoRequest
    {
        public string Nombre { get; set; } = default!;
        public string Descripcion { get; set; } = default!;
        public int EspecialidadId { get; set; }
        public decimal Precio { get; set; }
        public int CuposDisponibles { get; set; }
    }
}