using MediConecctPro.Utils;
using MediConnectPro.Bs.Servicios;
using MediConnectPro.Core.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediConecctPro.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MediConnectController : ControllerBase
    {
        private readonly ICitaServicio _citaServicio;
        private readonly IUsuariosServicio _usuariosServicio;
        private readonly IPacientesServicio _pacientesServicio;
        private readonly IMedicoServicio _medicoServicio;
        private readonly IPerfilServicio _perfilServicio;
        private readonly UtilsCls utls = new UtilsCls();
        public MediConnectController(ICitaServicio citaServicio, IUsuariosServicio usuariosServicio,
             IPacientesServicio pacientesServicio, IMedicoServicio medicoServicio, IPerfilServicio perfilServicio)
        {
            _citaServicio = citaServicio;
            _usuariosServicio = usuariosServicio;
            _pacientesServicio = pacientesServicio;
            _medicoServicio = medicoServicio;
            _perfilServicio = perfilServicio;
        }

        #region Usuarios
        [HttpPost]
        [Route("TraerUsuarios")]
        public async Task<IEnumerable<UsuariosDto>> TraerUsuarios(Usuarios usuarios)
        {
            var result = await _usuariosServicio.TraerUsuarios(usuarios);
            return result;
        }

        #endregion

        #region Cita
        [HttpPost]
        [Route("TraerCita")]
        public async Task<IEnumerable<Cita>> TraerCita(Cita cita)
        {
            return await _citaServicio.TraerCita(cita);
        }
        [HttpPost]
        [Route("GuardarActualizarCita")]
        public async Task<int> GuardarActualizarCita(Cita cita)
        {
            if (cita.Id == Guid.Empty)
            {
                cita.Id = Guid.NewGuid();
                cita.PacienteId = null;
                cita.UsuarioCreacion = Guid.Parse("EEC4333A-2ABD-4308-AF4E-495D375F8B33");
            }
            else
            {
                var lst = await _citaServicio.TraerCita(new() { Id = cita.Id });
                if (lst.Any())
                {
                    var res = lst.FirstOrDefault();
                    cita.Hora = (string.IsNullOrEmpty(cita.Hora) ? res!.Hora : cita.Hora);
                    cita.Nombre = (string.IsNullOrEmpty(cita.Nombre) ? res!.Nombre : cita.Nombre);
                    cita.Descripcion = (string.IsNullOrEmpty(cita.Descripcion) ? res!.Descripcion : cita.Descripcion);
                    cita.MedicoId = (cita.MedicoId == Guid.Empty || cita.MedicoId == null ? res!.MedicoId : cita.MedicoId);
                    cita.PacienteId = (cita.PacienteId == Guid.Empty || cita.PacienteId == null ? res!.PacienteId : null);
                    cita.EstadoCita = (cita.EstadoCita == 0 || cita.EstadoCita == null ? res!.EstadoCita : cita.EstadoCita);
                    cita.UsuarioCreacion = Guid.Parse("EEC4333A-2ABD-4308-AF4E-495D375F8B33");
                }
                else
                {
                    return 0;
                }
            }
            return await _citaServicio.GuardarActualizarCita(cita);
        }
        #endregion

        #region Pacientes
        [HttpPost]
        [Route("TraerPacientes")]
        public async Task<IEnumerable<Pacientes>> TraerPacientes(Pacientes pacientes)
        {
            return await _pacientesServicio.TraerPacientes(pacientes);
        }
        [HttpPost]
        [Route("GuardarActualizarPacientes")]
        public async Task<int> GuardarActualizarPacientes(Pacientes pacientes)
        {
            int usr = 0;

            if (pacientes.Id == Guid.Empty)
            {
                pacientes.Id = Guid.NewGuid();
                pacientes.UsuarioCreacion = Guid.Parse("EEC4333A-2ABD-4308-AF4E-495D375F8B33");
                Usuarios usuarios = new()
                {
                    Id = Guid.NewGuid(),
                    Estado = true,
                    Correo = pacientes.Correo,
                    Documento = pacientes.Documento,
                    Ip = HttpContext.Connection.RemoteIpAddress!.ToString(),
                    PerfilId = Guid.Parse("37058450-91ce-46d2-b090-decc916293cc"),
                    Nombre = pacientes.Nombre + " " + pacientes.Apellido,
                    Contrasena = utls.Encript(pacientes.Documento!),
                };
                usr = await _usuariosServicio.GuardarActualizarUsuarios(usuarios);
                if (usr > 0)
                {
                    return await _pacientesServicio.GuardarActualizarPacientes(pacientes);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                var lst = await _pacientesServicio.TraerPacientes(new() { Id = pacientes.Id });
                if (lst.Any())
                {
                    var res = lst.FirstOrDefault();
                    pacientes.Nombre = (string.IsNullOrEmpty(pacientes.Nombre) ? res!.Nombre : pacientes.Nombre);
                    pacientes.Apellido = (string.IsNullOrEmpty(pacientes.Apellido) ? res!.Apellido : pacientes.Apellido);
                    pacientes.Documento = (string.IsNullOrEmpty(pacientes.Documento) ? res!.Documento : pacientes.Documento);
                    pacientes.FechaNacimiento = (pacientes.FechaNacimiento.Value == null ? res!.FechaNacimiento : pacientes.FechaNacimiento);
                    pacientes.Telefono = pacientes.Telefono.Value == null ? res!.Telefono : pacientes.Telefono;
                    pacientes.Direccion = (string.IsNullOrEmpty(pacientes.Direccion) ? res!.Direccion : pacientes.Direccion);
                    pacientes.Correo = (string.IsNullOrEmpty(pacientes.Correo) ? res!.Direccion : pacientes.Correo);
                    pacientes.UsuarioCreacion = Guid.Parse("EEC4333A-2ABD-4308-AF4E-495D375F8B33");
                }
                else
                {
                    return 0;
                }
            }
            return await _pacientesServicio.GuardarActualizarPacientes(pacientes);

        }
        #endregion

        #region Medico
        [HttpPost]
        [Route("TraerMedicoProfesional")]
        public async Task<IEnumerable<MedicoProfesional>> TraerMedicoProfesional(MedicoProfesional medicoProfesional)
        {
            return await _medicoServicio.TraerMedicoProfesional(medicoProfesional);
        }

        [HttpPost]
        [Route("GuardarActualizarMedicoProfesional")]
        public async Task<int> GuardarActualizarMedicoProfesional(MedicoProfesional medicoProfesional)
        {
            int usr = 0;
            if (medicoProfesional.Id == Guid.Empty)
            {
                medicoProfesional.Id = Guid.NewGuid();
                medicoProfesional.UsuarioCreacion = Guid.Parse("EEC4333A-2ABD-4308-AF4E-495D375F8B33");
                Usuarios usuarios = new()
                {
                    Id = Guid.NewGuid(),
                    Estado = true,
                    Correo = medicoProfesional.Correo,
                    Documento = medicoProfesional.Documento,
                    Ip = HttpContext.Connection.RemoteIpAddress!.ToString(),
                    PerfilId = Guid.Parse("19dbfdd5-50ce-4ab4-bcfe-21118fbde58b"),
                    Nombre = medicoProfesional.Nombre + " " + medicoProfesional.Apellido,
                    Contrasena = utls.Encript(medicoProfesional.Documento!),
                };
                usr = await _usuariosServicio.GuardarActualizarUsuarios(usuarios);
                if (usr > 0)
                {
                    return await _medicoServicio.GuardarActualizarMedicoProfesional(medicoProfesional);

                }
                else
                {
                    return 0;
                }
            }
            else
            {
                var lst = await _medicoServicio.TraerMedicoProfesional(new() { Id = medicoProfesional.Id });
                if (lst.Any())
                {
                    var res = lst.FirstOrDefault();
                    medicoProfesional.Nombre = (string.IsNullOrEmpty(medicoProfesional.Nombre) ? res!.Nombre : medicoProfesional.Nombre);
                    medicoProfesional.Apellido = (string.IsNullOrEmpty(medicoProfesional.Apellido) ? res!.Apellido : medicoProfesional.Apellido);
                    medicoProfesional.Documento = (string.IsNullOrEmpty(medicoProfesional.Documento) ? res!.Documento : medicoProfesional.Documento);
                    medicoProfesional.FechaNacimiento = (medicoProfesional.FechaNacimiento.Value == null ? res!.FechaNacimiento : medicoProfesional.FechaNacimiento);
                    medicoProfesional.Telefono = medicoProfesional.Telefono.Value == null ? res!.Telefono : medicoProfesional.Telefono;
                    medicoProfesional.Direccion = (string.IsNullOrEmpty(medicoProfesional.Direccion) ? res!.Direccion : medicoProfesional.Direccion);
                    medicoProfesional.Correo = (string.IsNullOrEmpty(medicoProfesional.Correo) ? res!.Direccion : medicoProfesional.Correo);
                    medicoProfesional.UsuarioCreacion = Guid.Parse("EEC4333A-2ABD-4308-AF4E-495D375F8B33");
                }
                else
                {
                    return 0;
                }
            }

            return await _medicoServicio.GuardarActualizarMedicoProfesional(medicoProfesional);
        }


        [HttpPost]
        [Route("TraerMedicoEspecialidades")]
        public async Task<IEnumerable<MedicoEspecialidades>> TraerMedicoEspecialidades(MedicoEspecialidades medicoEspecialidades)
        {
            return await _medicoServicio.TraerMedicoEspecialidades(medicoEspecialidades);
        }


        [HttpPost]
        [Route("GuardarActualizarMedicoEspecialidades")]
        public async Task<int> GuardarActualizarMedicoEspecialidades(MedicoEspecialidades medicoEspecialidades)
        {
            if (medicoEspecialidades.Id == Guid.Empty)
            {
                medicoEspecialidades.Id = Guid.NewGuid();
                medicoEspecialidades.UsuarioCreacion = Guid.Parse("EEC4333A-2ABD-4308-AF4E-495D375F8B33");
            }
            else
            {
                var lst = await _medicoServicio.TraerMedicoEspecialidades(new() { Id = medicoEspecialidades.Id });
                if (lst.Any())
                {
                    var res = lst.FirstOrDefault();
                    medicoEspecialidades.Nombre = (string.IsNullOrEmpty(medicoEspecialidades.Nombre) ? res!.Nombre : medicoEspecialidades.Nombre);
                    medicoEspecialidades.MedicoId = (medicoEspecialidades.MedicoId == Guid.Empty || medicoEspecialidades.MedicoId == null ? res!.MedicoId : medicoEspecialidades.MedicoId);
                    medicoEspecialidades.UsuarioCreacion = Guid.Parse("EEC4333A-2ABD-4308-AF4E-495D375F8B33");
                }
                else
                {
                    return 0;
                }
            }
            return await _medicoServicio.GuardarActualizarMedicoEspecialidades(medicoEspecialidades);

        }
        #endregion


        #region Perfil
        [HttpPost]
        [Route("TraerPerfiles")]
        public async Task<IEnumerable<Perfil>> TraerPerfiles(Perfil perfil)
        {
            return await _perfilServicio.TraerPerfiles(perfil);
        }
        [HttpPost]
        [Route("GuardarActualizarPerfil")]
        public async Task<int> GuardarActualizarPerfil(Perfil perfil)
        {
            if (perfil.Id == Guid.Empty)
            {
                perfil.Id = Guid.NewGuid();
            }
            else
            {
                var lst = await _perfilServicio.TraerPerfiles(new() { Id = perfil.Id });
                if (lst.Any())
                {
                    var res = lst.FirstOrDefault();
                    perfil.Nombre = (string.IsNullOrEmpty(perfil.Nombre) ? res!.Nombre : perfil.Nombre);
                }
                else
                {
                    return 0;
                }
            }
            return await _perfilServicio.GuardarActualizarPerfil(perfil);
        }
        #endregion
    }
}
