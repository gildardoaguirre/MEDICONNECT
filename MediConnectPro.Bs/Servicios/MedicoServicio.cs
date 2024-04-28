using MediConnectPro.Core.Entidades;
using MediConnectPro.Core.Repo;

namespace MediConnectPro.Bs.Servicios
{
    public class MedicoServicio : IMedicoServicio
    {
        private readonly RepoDB _repoDB;
        public MedicoServicio(RepoDB repoDB)
        {
            _repoDB = repoDB;
        }

        public async Task<IEnumerable<MedicoProfesional>> TraerMedicoProfesional(MedicoProfesional medicoProfesional)
        {
            return await _repoDB.TraerMedicoProfesional(medicoProfesional);
        }

        public async Task<IEnumerable<MedicoEspecialidades>> TraerMedicoEspecialidades(MedicoEspecialidades medicoEspecialidades)
        {
            return await _repoDB.TraerMedicoEspecialidades(medicoEspecialidades);
        }
        public async Task<int> GuardarActualizarMedicoProfesional(MedicoProfesional medicoProfesional)
        {
            return await _repoDB.GuardarActualizarMedicoProfesional(medicoProfesional);
        }

        public async Task<int> GuardarActualizarMedicoEspecialidades(MedicoEspecialidades medicoEspecialidades)
        {
            return await _repoDB.GuardarActualizarMedicoEspecialidades(medicoEspecialidades);
        }
    }
    public interface IMedicoServicio
    {
        Task<IEnumerable<MedicoProfesional>> TraerMedicoProfesional(MedicoProfesional medicoProfesional);
        Task<IEnumerable<MedicoEspecialidades>> TraerMedicoEspecialidades(MedicoEspecialidades medicoEspecialidades);
        Task<int> GuardarActualizarMedicoProfesional(MedicoProfesional medicoProfesional);
        Task<int> GuardarActualizarMedicoEspecialidades(MedicoEspecialidades medicoEspecialidades);

    }
}
