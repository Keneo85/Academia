namespace Academia.Business.DTO.Request.Alumno
{
    public class CreateAlumnoRequest
    {
        public string Nombres { get; set; } = default!;
        public string Apellidos { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Telefono { get; set; } = default!;
        public string DNI { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}