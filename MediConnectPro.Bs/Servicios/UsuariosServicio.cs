using AutoMapper;
using MediConnectPro.Core.Entidades;
using MediConnectPro.Core.Repo;
using System.Net;

namespace MediConnectPro.Bs.Servicios
{
    public class UsuariosServicio: IUsuariosServicio
    {
        private readonly RepoDB _repoDB;
        private readonly IMapper _mapper;
        public UsuariosServicio(RepoDB repoDB,IMapper mapper)
        {
            _repoDB = repoDB;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuariosDto>> TraerUsuarios(Usuarios usuarios)
        {
            var result = await _repoDB.TraerUsuarios(usuarios);
            return _mapper.Map<IEnumerable<UsuariosDto>>(result);
        }
        public async Task<int> GuardarActualizarUsuarios(Usuarios usuarios)
        {
            return await _repoDB.GuardarActualizarUsuarios(usuarios);
        }
    }
    public interface IUsuariosServicio
    {
        Task<IEnumerable<UsuariosDto>> TraerUsuarios(Usuarios usuarios);
        Task<int> GuardarActualizarUsuarios(Usuarios usuarios);

    }
}
