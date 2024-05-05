-- Creacion de la base de datos
CREATE DATABASE LABCE
go


-- Creacion de la tabla administrador
CREATE TABLE Administradores
(
Id INT UNIQUE NOT NULL,
Correo VARCHAR(100) UNIQUE NOT NULL,
Password VARCHAR(100) NOT NULL,
PRIMARY KEY(Id)
);

-- Creacion de la tabla operador
CREATE TABLE Operadores
(
Carnet INT UNIQUE NOT NULL,
Cedula INT UNIQUE NOT NULL,
Correo VARCHAR(100) UNIQUE NOT NULL,
Password VARCHAR(100) NOT NULL,
Nombre VARCHAR(100) NOT NULL,
Ap1 VARCHAR(100) NOT NULL,
Ap2 VARCHAR(100) NOT NULL,
Nacimiento DATE NOT NULL,
Edad INT NOT NULL,
Aprobado BIT NOT NULL,
Id_Admin INT,
PRIMARY KEY(Carnet),
CONSTRAINT "ADMIN_APRUEBA_FK" FOREIGN KEY(Id_Admin) REFERENCES Administradores(Id),

);

--Creacion de la tabla activo
CREATE TABLE Activos
(
Placa VARCHAR(50) UNIQUE NOT NULL,
Tipo VARCHAR(50) NOT NULL,
Marca VARCHAR(100) NOT NULL,
Fecha_Compra DATE,
Req_Aprobador BIT NOT NULL,
Id_Estado INT NOT NULL,
Nombre_Lab VARCHAR(50),
Ced_Prof INT,
Aprobado BIT,
PRIMARY KEY(Placa),
);

--Creacion tabla estado del activo
CREATE TABLE Estado_Activos
(
Id_Estado INT NOT NULL,
Estado VARCHAR(50) NOT NULL,
PRIMARY KEY(Id_Estado)
);

-- Creacion de la tabla laboratorio

CREATE TABLE Laboratorios
(
Nombre VARCHAR(50) UNIQUE NOT NULL,
Hora_Inicio TIME NOT NULL,
Hora_Final TIME NOT NULL,
Capacidad INT NOT NULL,
Computadores INT NOT NULL,
Facilidades VARCHAR(500),
PRIMARY KEY(Nombre)
);

-- Creacion de la tabla profesores

CREATE TABLE Profesores
(
Cedula INT UNIQUE NOT NULL,
Nombre VARCHAR(100) NOT NULL,
Ap1 VARCHAR(100) NOT NULL,
Ap2 VARCHAR(100) NOT NULL,
Correo VARCHAR(100) UNIQUE NOT NULL,
Password VARCHAR(100) NOT NULL,
Nacimiento DATE NOT NULL,
Edad INT NOT NULL,
PRIMARY KEY(Cedula)
);


-- Creacion de la tabla de reservaciones
CREATE TABLE Reservaciones
(
ID INT UNIQUE NOT NULL,
Fecha DATE NOT NULL,
HoraInicio TIME NOT NULL,
HoraFin TIME NOT NULL,
CantHoras INT NOT NULL,
NombreLab VARCHAR(50) NOT NULL,
CedProf INT,
CarnetOP INT,
NombreEstudiante VARCHAR(100),
AP1Estudiante VARCHAR(100),
AP2Estudiante VARCHAR(100),
CorreoEstudiante VARCHAR(100),
CarnetEstudiante INT,
PRIMARY KEY(ID,NombreLab),
CONSTRAINT "Nombre_Lab_Reservacion" FOREIGN KEY(NombreLab) REFERENCES Laboratorios(Nombre),
CONSTRAINT "Ced_Prof_Reservacion" FOREIGN KEY (CedProf) REFERENCES Profesores(Cedula),
CONSTRAINT "Carnet_OP_Reservacion" FOREIGN KEY (CarnetOP) REFERENCES Operadores(Carnet)
);



-- Creacion de la tabla de la solicitud de activos 

CREATE TABLE Prestamos
(
ID INT UNIQUE NOT NULL,
Fecha DATE NOT NULL,
Hora TIME NOT NULL,
PlacaActivo VARCHAR(50) NOT NULL,
CedProf INT,
CarnetOP INT,
NombreEstudiante VARCHAR(100),
AP1Estudiante VARCHAR(100),
AP2Estudiante VARCHAR(100),
CorreoEstudiante VARCHAR(100),
PRIMARY KEY (ID,PlacaActivo),
CONSTRAINT "Placa_Activo_Prestamo_Prof" FOREIGN KEY(PlacaActivo) REFERENCES Activos(Placa),
CONSTRAINT "Prestamo_Act_Ced_Prof" FOREIGN KEY(CedProf) REFERENCES PROFESORES(CEDULA),
CONSTRAINT "Prestamo_Act_Carnet_Op" FOREIGN KEY(CarnetOP) REFERENCES Operadores(Carnet)
);



-- Creacion tabla reportes
CREATE TABLE Reportes
(
ID INT UNIQUE NOT NULL,
Fecha_Trabajo DATE NOT NULL,
Hora_Inicio TIME NOT NULL,
Hora_Final TIME NOT NULL,
Horas_Totales INT NOT NULL,
Carnet_Op INT NOT NULL,
PRIMARY KEY(CARNET_OP, ID),
CONSTRAINT "REPORTES_CARNET_OP" FOREIGN KEY(CARNET_OP) REFERENCES OPERADORES(CARNET)
);

ALTER TABLE ACTIVOS
ADD CONSTRAINT "Activo_En_Lab_FK" FOREIGN KEY(NOMBRE_LAB) 
REFERENCES LABORATORIOS(NOMBRE);

ALTER TABLE ACTIVOS
ADD CONSTRAINT "Activo_Apr_Prof_FK" FOREIGN KEY(CED_PROF) 
REFERENCES PROFESORES(CEDULA);

ALTER TABLE ACTIVOS
ADD CONSTRAINT "Activo_Estado_FK" FOREIGN KEY(ID_ESTADO) 
REFERENCES ESTADO_ACTIVOS(ID_ESTADO);