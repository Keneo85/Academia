namespace Academia.DataAccess.Entities
{
    public class DetalleMatricula : BaseEntity
    {
        public int MatriculaId { get; set; }
        public Matricula? Matricula { get; set; }
        public int CursoId { get; set; }
        public Curso? Curso { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal => Cantidad * PrecioUnitario;
    }
}