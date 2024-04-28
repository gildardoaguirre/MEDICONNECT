using MediConnectPro.Core.Entidades;
using MediConnectPro.Core.Repo;

namespace MediConnectPro.Bs.Servicios
{
    public class CitaServicio : ICitaServicio
    {
        private readonly RepoDB _repoDB;
        public CitaServicio(RepoDB repoDB)
        {
            _repoDB = repoDB;
        }

        public async Task<IEnumerable<Cita>> TraerCita(Cita cita)
        {
            return await _repoDB.TraerCita(cita);
        }

        public async Task<int> GuardarActualizarCita(Cita cita)
        {
            return await _repoDB.GuardarActualizarCita(cita);
        }
    }
    public interface ICitaServicio
    {
        Task<IEnumerable<Cita>> TraerCita(Cita cita);
        Task<int> GuardarActualizarCita(Cita cita);
    }
}
