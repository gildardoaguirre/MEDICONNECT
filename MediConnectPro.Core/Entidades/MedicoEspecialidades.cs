using System.Text.Json.Serialization;

namespace MediConnectPro.Core.Entidades
{
    public class MedicoEspecialidades
    {
        public Guid? Id { get; set; }
        public Guid? MedicoId { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaCreacion { get; set; }
        [JsonIgnore]
        public DateTime? FechaModificacion { get; set; }
        [JsonIgnore]
        public Guid? UsuarioCreacion { get; set; }
        public bool? Estado { get; set; }
        public string? Medico { get; set; }
        public MedicoEspecialidades()
        {
            Id = Guid.Empty;

        }
    }
}
