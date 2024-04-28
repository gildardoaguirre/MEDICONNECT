using System.Text.Json.Serialization;

namespace MediConnectPro.Core.Entidades
{
    public class Usuarios
    {
        public Guid? Id { get; set; }
        public string? Documento { get; set; }
        public string? Nombre { get; set; }
       
        public string? Contrasena { get; set; }
        public Guid? PerfilId { get; set; }
        [JsonIgnore]
        public string? Ip { get; set; }
        [JsonIgnore]
        public DateTime? FechaCreacion { get; set; }
        public string? Correo { get; set; }
        [JsonIgnore]
        public string? Perfil { get; set; }
        public bool? Estado { get; set; }

        public Usuarios()
        {
            Id = Guid.Empty;

        }

    }

    public class UsuariosDto
    {
        public Guid? Id { get; set; }
        public string? Documento { get; set; }
        public string? Nombre { get; set; }
        [JsonIgnore]
        public string? Contrasena { get; set; }
        public Guid? PerfilId { get; set; }
        public string? Ip { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? Correo { get; set; }
        public string? Perfil { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpirationToken { get; set; }
        public bool? Estado { get; set; }

        public UsuariosDto()
        {
            Id = Guid.Empty;

        }

    }
    public class UserClaims
    {
        public string? Documento { get; set; }
        public string? Correo { get; set; }
        public string? Id { get; set; }
    }
}
