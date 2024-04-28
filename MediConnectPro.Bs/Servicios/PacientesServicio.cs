using MediConnectPro.Core.Entidades;
using MediConnectPro.Core.Repo;

namespace MediConnectPro.Bs.Servicios
{
    public class PacientesServicio: IPacientesServicio
    {
        private readonly RepoDB _repoDB;
        public PacientesServicio(RepoDB repoDB)
        {
            _repoDB = repoDB;
        }

        public async Task<IEnumerable<Pacientes>> TraerPacientes(Pacientes pacientes)
        {
            return await _repoDB.TraerPacientes(pacientes);
        }

        public async Task<int> GuardarActualizarPacientes(Pacientes pacientes)
        {
            return await _repoDB.GuardarActualizarPacientes(pacientes);
        }
    }
    public interface IPacientesServicio
    {
        Task<IEnumerable<Pacientes>> TraerPacientes(Pacientes pacientes);
        Task<int> GuardarActualizarPacientes(Pacientes pacientes);

    }
}
