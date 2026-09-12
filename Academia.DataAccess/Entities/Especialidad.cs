namespace Academia.DataAccess.Entities
{
    public class Especialidad : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public List<Curso> Cursos { get; set; } = new();
    }
}