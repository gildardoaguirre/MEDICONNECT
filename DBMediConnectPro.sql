--CREATE DATABASE MediConnectPro
--go
--USE MediConnectPro
--go

IF NOT EXISTS(SELECT * FROM sys.tables WHERE name='Perfil')
BEGIN
CREATE TABLE Perfil(
	Id uniqueidentifier primary key ,
	Nombre nvarchar(50),
	Estado bit default(0)
)
END
GO

IF NOT EXISTS(SELECT * FROM sys.tables WHERE name='Usuarios')
BEGIN
CREATE TABLE Usuarios(
Id uniqueidentifier primary key,
Documento nvarchar(20),
Nombre nvarchar(50),
Contrasena nvarchar(100),
PerfilId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Perfil(Id),
Ip nvarchar(50),
FechaCreacion smalldatetime,
Correo nvarchar(250),
Estado bit default(0)
)
END
go

IF NOT EXISTS(SELECT * FROM sys.tables WHERE name='Pacientes')
BEGIN
CREATE TABLE Pacientes(
	Id uniqueidentifier PRIMARY KEY,
	Documento nvarchar(20),
	Nombre nvarchar(50),
	Apellido nvarchar(50),
	FechaNacimiento smalldatetime,
	Telefono int,
	Direccion nvarchar(200),
	Correo nvarchar(250),
	FechaCreacion smalldatetime,
	UsuarioCreacion UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Usuarios(Id) NULL,
	UsuarioModifica UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Usuarios(Id) NULL,
	Estado bit default(0)
)
END
GO

IF NOT EXISTS(SELECT * FROM sys.tables WHERE name='MedicoProfesional')
BEGIN
CREATE TABLE MedicoProfesional(
	Id uniqueidentifier PRIMARY KEY,
	Documento nvarchar(20),
	Nombre nvarchar(50),
	Apellido nvarchar(50),
	FechaNacimiento smalldatetime,
	Telefono int,
	Direccion nvarchar(200),
	Correo nvarchar(250),
	FechaCreacion smalldatetime,
	UsuarioCreacion UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Usuarios(Id) NULL,
	UsuarioModifica UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Usuarios(Id) NULL,
	Estado bit default(0)
)
END
GO


IF NOT EXISTS(SELECT * FROM sys.tables WHERE name='MedicoEspecialidades')
BEGIN
CREATE TABLE MedicoEspecialidades(
Id uniqueidentifier primary key,
MedicoId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES MedicoProfesional(Id),
Nombre nvarchar(50),
FechaCreacion smalldatetime,
FechaModificacion smalldatetime null,
UsuarioCreacion  UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Usuarios(Id),
Estado bit default(0)
)
END
go

IF NOT EXISTS(SELECT * FROM sys.tables WHERE name='Cita')
BEGIN
CREATE TABLE Cita(
Id uniqueidentifier primary key,
MedicoId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES MedicoProfesional(Id) null,
PacienteId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Pacientes(Id) null,
Fecha smalldatetime,
Hora char(10),
Nombre nvarchar(100) null,
Descripcion nvarchar(300) null,
EstadoCita int,--1 -cita creada, 2-cita asignada,3- cita realizada , 4-cita cancelada
Estado bit default(0),
UsuarioCreacion UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Usuarios(Id) null,
FechaCreacion smalldatetime
)
END
go

--gets
if exists(SELECT *
FROM sys.sql_modules
WHERE OBJECT_NAME(OBJECT_ID) = 'SP_TRAER_USUARIOS')
BEGIN
	DROP PROC SP_TRAER_USUARIOS
END
GO

CREATE PROCEDURE SP_TRAER_USUARIOS
@Id UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000',
@Correo nvarchar(250) =null,
@Contrasena nvarchar(100)=null
AS
SELECT DISTINCT u.*,p.Nombre Perfil
FROM Usuarios u
INNER JOIN Perfil p on u.PerfilId = p.Id
WHERE (u.id = @Id or coalesce(@Id,'')='00000000-0000-0000-0000-000000000000')
AND (u.Correo = @Correo OR COALESCE(@Correo, '') = '')
AND (u.Contrasena = @Contrasena OR COALESCE(@Contrasena, '') = '')
go

if exists(SELECT *
FROM sys.sql_modules
WHERE OBJECT_NAME(OBJECT_ID) = 'SP_TRAER_PACIENTES')
BEGIN
	DROP PROC SP_TRAER_PACIENTES
END
GO

CREATE PROCEDURE SP_TRAER_PACIENTES
@Id UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000',
@Nombre nvarchar(250) =null
AS
SELECT DISTINCT p.*
FROM Pacientes p
WHERE (p.id = @Id or coalesce(@Id,'')='00000000-0000-0000-0000-000000000000')
AND (p.Nombre = @Nombre OR COALESCE(@Nombre, '') = '')
go

if exists(SELECT *
FROM sys.sql_modules
WHERE OBJECT_NAME(OBJECT_ID) = 'SP_TRAER_MEDICOS')
BEGIN
	DROP PROC SP_TRAER_MEDICOS
END
GO

CREATE PROCEDURE SP_TRAER_MEDICOS
@Id UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000',
@Nombre nvarchar(250) =null
AS
SELECT DISTINCT m.*
FROM MedicoProfesional m
WHERE (m.id = @Id or coalesce(@Id,'')='00000000-0000-0000-0000-000000000000')
AND (m.Nombre = @Nombre OR COALESCE(@Nombre, '') = '')
go

if exists(SELECT *
FROM sys.sql_modules
WHERE OBJECT_NAME(OBJECT_ID) = 'SP_TRAER_MEDICOS_ESPECIALIDADES')
BEGIN
	DROP PROC SP_TRAER_MEDICOS_ESPECIALIDADES
END
GO

CREATE PROCEDURE SP_TRAER_MEDICOS_ESPECIALIDADES
@Id UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000',
@Nombre nvarchar(250) =null
AS
SELECT DISTINCT m.*,(CONCAT(mp.Nombre,' ', mp.Apellido)) Medico
FROM MedicoEspecialidades m
INNER JOIN MedicoProfesional mp on m.MedicoId = mp.Id
WHERE (m.id = @Id or coalesce(@Id,'')='00000000-0000-0000-0000-000000000000')
AND (m.Nombre = @Nombre OR COALESCE(@Nombre, '') = '')
go


if exists(SELECT *
FROM sys.sql_modules
WHERE OBJECT_NAME(OBJECT_ID) = 'SP_TRAER_CITA')
BEGIN
	DROP PROC SP_TRAER_CITA
END
GO

CREATE PROCEDURE SP_TRAER_CITA
@Id UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000',
@PacienteId UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000',
@MedicoId UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000',
@Fecha smalldatetime= null
AS
SELECT DISTINCT c.*,(CONCAT(mp.Nombre,' ', mp.Apellido)) Medico,(CONCAT(p.Nombre,' ', p.Apellido)) Paciente
FROM Cita c
INNER JOIN MedicoProfesional mp on c.MedicoId = mp.Id
LEFT JOIN Pacientes p on c.PacienteId = p.Id
WHERE (c.id = @Id or coalesce(@Id,'')='00000000-0000-0000-0000-000000000000')
AND (c.PacienteId = @PacienteId or coalesce(@PacienteId,'')='00000000-0000-0000-0000-000000000000')
AND (c.MedicoId = @MedicoId or coalesce(@MedicoId,'')='00000000-0000-0000-0000-000000000000')
AND c.Fecha =(CASE WHEN COALESCE(@Fecha, '') = '' then c.Fecha ELSE @Fecha END)
go

if exists(SELECT *
FROM sys.sql_modules
WHERE OBJECT_NAME(OBJECT_ID) = 'SP_TRAER_PERFILES')
BEGIN
	DROP PROC SP_TRAER_PERFILES
END
GO

CREATE PROCEDURE SP_TRAER_PERFILES
@Id UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000'
AS
SELECT DISTINCT *
FROM Perfil p
WHERE (p.id = @Id or coalesce(@Id,'')='00000000-0000-0000-0000-000000000000')
go

--insert
if exists(SELECT *
FROM sys.sql_modules
WHERE OBJECT_NAME(OBJECT_ID) = 'SP_GuardarOActualiza_Perfil')
BEGIN
	DROP PROC SP_GuardarOActualiza_Perfil
END
GO

CREATE PROCEDURE SP_GuardarOActualiza_Perfil
	@Id UNIQUEIDENTIFIER,
	@Nombre NVARCHAR(50),
	@Estado BIT
AS
BEGIN
	IF NOT EXISTS(SELECT *
	FROM Perfil
	WHERE Id=@Id) 
		BEGIN
		INSERT INTO Perfil
			(Id,Nombre,Estado)
		VALUES
			(@Id,@Nombre,@Estado)
	END
ELSE
BEGIN
		UPDATE Perfil SET Estado = @Estado,Nombre=@Nombre WHERE Id=@Id
	END
END
go

if exists(SELECT *
FROM sys.sql_modules
WHERE OBJECT_NAME(OBJECT_ID) = 'SP_GuardarOActualiza_Cita')
BEGIN
	DROP PROC SP_GuardarOActualiza_Cita
END
GO

CREATE PROCEDURE SP_GuardarOActualiza_Cita
	@Id UNIQUEIDENTIFIER,
	@MedicoId UNIQUEIDENTIFIER,
	@PacienteId UNIQUEIDENTIFIER,
	@UsuarioCreacion UNIQUEIDENTIFIER,
	@Fecha smalldatetime,
	@Hora NVARCHAR(10),
	@Descripcion NVARCHAR(300),
	@Nombre NVARCHAR(100),
	@EstadoCita int,
	@Estado BIT
AS
BEGIN
	IF NOT EXISTS(SELECT *
	FROM Cita
	WHERE Id=@Id) 
		BEGIN
		INSERT INTO Cita
			(Id,MedicoId,Nombre,PacienteId,Fecha,Hora,Descripcion,EstadoCita,Estado,FechaCreacion,UsuarioCreacion)
		VALUES
			(@Id,@MedicoId,@Nombre,@PacienteId,@Fecha,@Hora,@Descripcion,@EstadoCita,@Estado,GETDATE(),@UsuarioCreacion)
	END
ELSE
BEGIN
		UPDATE Cita SET Nombre=@Nombre,Estado = @Estado,PacienteId=@PacienteId WHERE Id=@Id
	END
END
go

if exists(SELECT *
FROM sys.sql_modules
WHERE OBJECT_NAME(OBJECT_ID) = 'SP_GuardarOActualiza_Pacientes')
BEGIN
	DROP PROC SP_GuardarOActualiza_Pacientes
END
GO

CREATE PROCEDURE SP_GuardarOActualiza_Pacientes
	@Id uniqueidentifier,
	@Nombre nvarchar(50),
	@Documento nvarchar(20),
	@Apellido nvarchar(50),
	@FechaNacimiento smalldatetime,
	@Telefono int,
	@Direccion nvarchar(200),
	@Correo nvarchar(250),
	@UsuarioCreacion UNIQUEIDENTIFIER,
	@Estado bit
AS
BEGIN
	IF NOT EXISTS(SELECT *
	FROM Pacientes
	WHERE Id=@Id) 
		BEGIN
		INSERT INTO Pacientes
			(Id,Documento,Nombre,Apellido,FechaNacimiento,Telefono,Direccion,Correo,FechaCreacion,UsuarioCreacion,Estado)
		VALUES
			(@Id,@Documento,@Nombre,@Apellido,@FechaNacimiento,@Telefono,@Direccion,@Correo,GETDATE(),@UsuarioCreacion,@Estado)
	END
ELSE
BEGIN
		UPDATE Pacientes SET Nombre= @Nombre,Apellido=@Apellido,FechaNacimiento=@FechaNacimiento,Telefono=@Telefono,Direccion=@Direccion,Correo = @Correo,UsuarioModifica=@UsuarioCreacion,Estado=@Estado WHERE Id=@Id
	END
END
go

if exists(SELECT *
FROM sys.sql_modules
WHERE OBJECT_NAME(OBJECT_ID) = 'SP_GuardarOActualiza_Medicos')
BEGIN
	DROP PROC SP_GuardarOActualiza_Medicos
END
GO

CREATE PROCEDURE SP_GuardarOActualiza_Medicos
	@Id uniqueidentifier,
	@Documento nvarchar(20),
	@Nombre nvarchar(50),
	@Apellido nvarchar(50),
	@FechaNacimiento smalldatetime,
	@Telefono int,
	@Direccion nvarchar(200),
	@Correo nvarchar(250),
	@UsuarioCreacion UNIQUEIDENTIFIER,
	@Estado bit
AS
BEGIN
	IF NOT EXISTS(SELECT *
	FROM MedicoProfesional
	WHERE Id=@Id) 
		BEGIN
		INSERT INTO MedicoProfesional
			(Id,Documento,Nombre,Apellido,FechaNacimiento,Telefono,Direccion,Correo,FechaCreacion,UsuarioCreacion,Estado)
		VALUES
			(@Id,@Documento,@Nombre,@Apellido,@FechaNacimiento,@Telefono,@Direccion,@Correo,GETDATE(),@UsuarioCreacion,@Estado)
	END
ELSE
BEGIN
		UPDATE MedicoProfesional SET Nombre= @Nombre,Apellido=@Apellido,FechaNacimiento=@FechaNacimiento,Telefono=@Telefono,Direccion=@Direccion,Correo = @Correo,UsuarioModifica=@UsuarioCreacion,Estado=@Estado WHERE Id=@Id
	END
END
go

if exists(SELECT *
FROM sys.sql_modules
WHERE OBJECT_NAME(OBJECT_ID) = 'SP_GuardarOActualiza_MedicoEspecialidad')
BEGIN
	DROP PROC SP_GuardarOActualiza_MedicoEspecialidad
END
GO

CREATE PROCEDURE SP_GuardarOActualiza_MedicoEspecialidad
	@Id uniqueidentifier,
	@MedicoId uniqueidentifier,
	@Nombre nvarchar(50),
	@UsuarioCreacion UNIQUEIDENTIFIER,
	@Estado bit
AS
BEGIN
	IF NOT EXISTS(SELECT *
	FROM MedicoEspecialidades
	WHERE Id=@Id) 
		BEGIN
		INSERT INTO MedicoEspecialidades
			(Id,Nombre,MedicoId,UsuarioCreacion,FechaCreacion,Estado)
		VALUES
			(@Id,@Nombre,@MedicoId,@UsuarioCreacion,GETDATE(),@Estado)
	END
ELSE
BEGIN
		UPDATE MedicoEspecialidades SET Nombre= @Nombre,MedicoId=@MedicoId,FechaModificacion=GETDATE(),Estado=@Estado WHERE Id=@Id
	END
END
go

if exists(SELECT *
FROM sys.sql_modules
WHERE OBJECT_NAME(OBJECT_ID) = 'SP_GuardarOActualiza_Usuarios')
BEGIN
	DROP PROC SP_GuardarOActualiza_Usuarios
END
GO

CREATE PROCEDURE SP_GuardarOActualiza_Usuarios
	@Id uniqueidentifier,
	@PerfilId uniqueidentifier,
	@Documento nvarchar(20),
	@Nombre nvarchar(50),
	@Contrasena nvarchar(100),
	@Ip nvarchar(100),
	@Correo nvarchar(250),
	@Estado bit
AS
BEGIN
	IF NOT EXISTS(SELECT *
	FROM Usuarios
	WHERE Id=@Id) 
		BEGIN
		INSERT INTO Usuarios
			(Id,Nombre,Documento,Contrasena,PerfilId,Ip,Correo,FechaCreacion,Estado)
		VALUES
			(@Id,@Nombre,@Documento,@Contrasena,@PerfilId,@Ip,@Correo,GETDATE(),@Estado)
	END
ELSE
BEGIN
		UPDATE Usuarios SET Nombre= @Nombre,Contrasena=@Contrasena,PerfilId=@PerfilId,Correo=@Correo
		,Estado=@Estado WHERE Id=@Id
	END
END
go

IF NOT EXISTS(SELECT * FROM Perfil WHERE Id = '19DBFDD5-50CE-4AB4-BCFE-21118FBDE58B')
BEGIN
	INSERT INTO Perfil(Estado, Id, Nombre) VALUES( '19DBFDD5-50CE-4AB4-BCFE-21118FBDE58B', 'Medico', 1); 
END

GO

IF NOT EXISTS(SELECT * FROM Perfil WHERE Id = '98F182C6-CC66-4C25-BBCC-B4075EE16538')
BEGIN
	INSERT INTO Perfil(Estado, Id, Nombre) VALUES( '98F182C6-CC66-4C25-BBCC-B4075EE16538', 'Administrador', 1); 
END

GO

IF NOT EXISTS(SELECT * FROM Perfil WHERE Id = '37058450-91CE-46D2-B090-DECC916293CC')
BEGIN
	INSERT INTO Perfil(Estado, Id, Nombre) VALUES( '37058450-91CE-46D2-B090-DECC916293CC', 'Paciente', 1); 
END

GO

IF NOT EXISTS(SELECT * FROM Usuarios WHERE Id = 'EEC4333A-2ABD-4308-AF4E-495D375F8B33')
BEGIN
	INSERT INTO Usuarios(Contrasena, Correo, Documento, Estado, FechaCreacion, Id, Ip, Nombre, PerfilId) VALUES( 'EEC4333A-2ABD-4308-AF4E-495D375F8B33', 'Usuario Admin', '5072756562612E323032342A', '98F182C6-CC66-4C25-BBCC-B4075EE16538', NULL, '2024-04-28 13:13:00', 'restgomez@poligran.edu.co', 1, '1023970'); 
END

GO

