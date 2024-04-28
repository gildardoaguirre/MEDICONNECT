using AutoMapper;
using MediConnectPro.Core.Entidades;

namespace MediConnectPro.Bs.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<UsuariosDto, Usuarios>().ReverseMap();
        }
    }
}
