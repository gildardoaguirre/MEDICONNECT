using MediConnectPro.Core.Entidades;
using MediConnectPro.Core.Repo;

namespace MediConnectPro.Bs.Servicios
{
    public class PerfilServicio: IPerfilServicio
    {
        private readonly RepoDB _repoDB;
        public PerfilServicio(RepoDB repoDB)
        {
            _repoDB = repoDB;
        }

        public async Task<IEnumerable<Perfil>> TraerPerfiles(Perfil perfil)
        {
            return await _repoDB.TraerPerfiles(perfil);
        }

        public async Task<int> GuardarActualizarPerfil(Perfil perfil)
        {
            return await _repoDB.GuardarActualizarPerfil(perfil);
        }
    }
    public interface IPerfilServicio
    {
        Task<IEnumerable<Perfil>> TraerPerfiles(Perfil perfil);
        Task<int> GuardarActualizarPerfil(Perfil perfil);
    }
}
