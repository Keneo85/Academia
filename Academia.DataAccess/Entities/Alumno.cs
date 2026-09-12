namespace Academia.DataAccess.Entities
{
    public class Alumno : BaseEntity
    {
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
        public User? User { get; set; }
        public int UserId { get; set; }
        public List<Matricula> Matriculas { get; set; } = new();
    }
}