using System.Text.Json.Serialization;

namespace MediConnectPro.Core.Entidades
{
    public class Cita
    {
        public Guid? Id { get; set; }
        public string? Nombre { get; set; }
        public Guid? MedicoId { get; set; }
        public Guid? PacienteId { get; set; }
        [JsonIgnore]
        public Guid? UsuarioCreacion { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Hora { get; set; }
        public string? Descripcion { get; set; }
        public int? EstadoCita { get; set; }
        public bool? Estado { get; set; }
        public string? Medico { get; set; }
        public string? Paciente { get; set; }

        public Cita()
        {
            Id = Guid.Empty;
            MedicoId = Guid.Empty;
            PacienteId = Guid.Empty;
        }
    }
}
