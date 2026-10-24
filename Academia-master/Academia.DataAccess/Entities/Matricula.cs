namespace Academia.DataAccess.Entities
{
    public class Matricula : BaseEntity
    {
        public int AlumnoId { get; set; }
        public Alumno? Alumno { get; set; }
        public decimal Total { get; set; }
        public List<DetalleMatricula> Detalles { get; set; } = new();
    }
}