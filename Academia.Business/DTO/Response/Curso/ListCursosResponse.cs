namespace Academia.Business.DTO.Response.Curso
{
    public class ListCursosResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int CuposDisponibles { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}