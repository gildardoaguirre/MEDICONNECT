namespace MediConnectPro.Core.Entidades
{
    public class MedicoProfesional
    {
        public Guid? Id { get; set; }
        public string? Documento { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Correo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public Guid? UsuarioCreacion { get; set; }
        public Guid? UsuarioModifica { get; set; }
        public bool? Estado { get; set; }
        public MedicoProfesional()
        {
            Id = Guid.Empty;

        }
    }
}
