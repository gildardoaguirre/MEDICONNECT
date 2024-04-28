namespace MediConnectPro.Core.Entidades
{
    public class Perfil
    {
        public Guid? Id { get; set; }
        public string? Nombre { get; set; }
        public bool? Estado { get; set; }
        public Perfil()
        {
            Id = Guid.Empty;

        }
    }
}
