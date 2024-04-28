using Dapper;
using MediConnectPro.Core.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;
using static Dapper.SqlMapper;

namespace MediConnectPro.Core.Repo
{
    public class RepoDB
    {

        private readonly IDbConnection _connection;
        public RepoDB(IDbConnection dbConnection)
        {
            _connection = dbConnection;
        }
        #region Perfil
        public async Task<IEnumerable<Perfil>> TraerPerfiles(Perfil perfil)
        {
            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("Id", perfil.Id);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.QueryAsync<Perfil>("SP_TRAER_PERFILES", dynamicParameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<int> GuardarActualizarPerfil(Perfil perfil)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            var save = 0;
            using (var transaction = conn.BeginTransaction())
            {
                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("Id", perfil.Id);
                dynamicParameters.Add("Nombre", perfil.Nombre);
                dynamicParameters.Add("Estado", perfil.Estado);
                save = await conn.ExecuteAsync("SP_GuardarOActualiza_Perfil", dynamicParameters, transaction, commandType: CommandType.StoredProcedure);
                if (save > 0)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
            }
            conn.Close();
            return save;
        }

        #endregion

        #region Usuarios
        public async Task<IEnumerable<Usuarios>> TraerUsuarios(Usuarios usuarios)
        {
            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("Id", usuarios.Id);
            dynamicParameters.Add("Correo", usuarios.Correo);
            dynamicParameters.Add("Contrasena", usuarios.Contrasena);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.QueryAsync<Usuarios>("SP_TRAER_USUARIOS", dynamicParameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<int> GuardarActualizarUsuarios(Usuarios usuarios)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            var save = 0;
            using (var transaction = conn.BeginTransaction())
            {
                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("Id", usuarios.Id);
                dynamicParameters.Add("Documento", usuarios.Documento);
                dynamicParameters.Add("PerfilId", usuarios.PerfilId);
                dynamicParameters.Add("Nombre", usuarios.Nombre);
                dynamicParameters.Add("Contrasena", usuarios.Contrasena);
                dynamicParameters.Add("Ip", usuarios.Ip);
                dynamicParameters.Add("Correo", usuarios.Correo);
                dynamicParameters.Add("Estado", usuarios.Estado);
                save = await conn.ExecuteAsync("SP_GuardarOActualiza_Usuarios", dynamicParameters, transaction, commandType: CommandType.StoredProcedure);
                if (save > 0)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
            }
            conn.Close();
            return save;
        }
        #endregion

        #region Pacientes
        public async Task<IEnumerable<Pacientes>> TraerPacientes(Pacientes pacientes)
        {
            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("Id", pacientes.Id);
            dynamicParameters.Add("Nombre", pacientes.Nombre);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.QueryAsync<Pacientes>("SP_TRAER_PACIENTES", dynamicParameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<int> GuardarActualizarPacientes(Pacientes pacientes)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            var save = 0;
            using (var transaction = conn.BeginTransaction())
            {
                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("Id", pacientes.Id);
                dynamicParameters.Add("Nombre", pacientes.Nombre);
                dynamicParameters.Add("Apellido", pacientes.Apellido);
                dynamicParameters.Add("Documento", pacientes.Documento);
                dynamicParameters.Add("FechaNacimiento", pacientes.FechaNacimiento);
                dynamicParameters.Add("Telefono", pacientes.Telefono);
                dynamicParameters.Add("Direccion", pacientes.Direccion);
                dynamicParameters.Add("Correo", pacientes.Correo);
                dynamicParameters.Add("UsuarioCreacion", pacientes.UsuarioCreacion);
                dynamicParameters.Add("Estado", pacientes.Estado);
                save = await conn.ExecuteAsync("SP_GuardarOActualiza_Pacientes", dynamicParameters, transaction, commandType: CommandType.StoredProcedure);
                if (save > 0)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
            }
            conn.Close();
            return save;
        }
        #endregion

        #region MedicoProfesional
        public async Task<IEnumerable<MedicoProfesional>> TraerMedicoProfesional(MedicoProfesional medicoProfesional)
        {
            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("Id", medicoProfesional.Id);
            dynamicParameters.Add("Nombre", medicoProfesional.Nombre);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.QueryAsync<MedicoProfesional>("SP_TRAER_MEDICOS", dynamicParameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<int> GuardarActualizarMedicoProfesional(MedicoProfesional medicoProfesional)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            var save = 0;
            using (var transaction = conn.BeginTransaction())
            {
                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("Id", medicoProfesional.Id);
                dynamicParameters.Add("Nombre", medicoProfesional.Nombre);
                dynamicParameters.Add("Apellido", medicoProfesional.Apellido);
                dynamicParameters.Add("FechaNacimiento", medicoProfesional.FechaNacimiento);
                dynamicParameters.Add("Documento", medicoProfesional.Documento);
                dynamicParameters.Add("Telefono", medicoProfesional.Telefono);
                dynamicParameters.Add("Direccion", medicoProfesional.Direccion);
                dynamicParameters.Add("Correo", medicoProfesional.Correo);
                dynamicParameters.Add("UsuarioCreacion", medicoProfesional.UsuarioCreacion);
                dynamicParameters.Add("Estado", medicoProfesional.Estado);
                save = await conn.ExecuteAsync("SP_GuardarOActualiza_Medicos", dynamicParameters, transaction, commandType: CommandType.StoredProcedure);
                if (save > 0)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
            }
            conn.Close();
            return save;
        }
        #endregion

        #region MedicoEspecialidades
        public async Task<IEnumerable<MedicoEspecialidades>> TraerMedicoEspecialidades(MedicoEspecialidades medicoEspecialidades)
        {
            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("Id", medicoEspecialidades.Id);
            dynamicParameters.Add("Nombre", medicoEspecialidades.Nombre);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.QueryAsync<MedicoEspecialidades>("SP_TRAER_MEDICOS_ESPECIALIDADES", dynamicParameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<int> GuardarActualizarMedicoEspecialidades(MedicoEspecialidades medicoEspecialidades)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            var save = 0;
            using (var transaction = conn.BeginTransaction())
            {
                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("Id", medicoEspecialidades.Id);
                dynamicParameters.Add("Nombre", medicoEspecialidades.Nombre);
                dynamicParameters.Add("MedicoId", medicoEspecialidades.MedicoId);
                dynamicParameters.Add("UsuarioCreacion", medicoEspecialidades.UsuarioCreacion);
                dynamicParameters.Add("Estado", medicoEspecialidades.Estado);
                save = await conn.ExecuteAsync("SP_GuardarOActualiza_MedicoEspecialidad", dynamicParameters, transaction, commandType: CommandType.StoredProcedure);
                if (save > 0)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
            }
            conn.Close();
            return save;
        }
        #endregion

        #region Cita
        public async Task<IEnumerable<Cita>> TraerCita(Cita cita)
        {
            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("Id", cita.Id);
            dynamicParameters.Add("Fecha", cita.Fecha);
            dynamicParameters.Add("PacienteId", cita.PacienteId);
            dynamicParameters.Add("MedicoId", cita.MedicoId);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.QueryAsync<Cita>("SP_TRAER_CITA", dynamicParameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<int> GuardarActualizarCita(Cita cita)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            var save = 0;
            using (var transaction = conn.BeginTransaction())
            {
                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("Id", cita.Id);
                dynamicParameters.Add("Nombre", cita.Nombre);
                dynamicParameters.Add("MedicoId", cita.MedicoId);
                dynamicParameters.Add("PacienteId", cita.PacienteId == Guid.Empty ? null : cita.PacienteId);
                dynamicParameters.Add("Fecha", cita.Fecha);
                dynamicParameters.Add("Hora", cita.Hora);
                dynamicParameters.Add("Descripcion", cita.Descripcion);
                dynamicParameters.Add("EstadoCita", cita.EstadoCita);
                dynamicParameters.Add("Estado", cita.Estado);
                dynamicParameters.Add("UsuarioCreacion", cita.UsuarioCreacion);
                save = await conn.ExecuteAsync("SP_GuardarOActualiza_Cita", dynamicParameters, transaction, commandType: CommandType.StoredProcedure);
                if (save > 0)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
            }
            conn.Close();
            return save;
        }
        #endregion
    }
}
