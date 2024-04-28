-- Creacion de la base de datos
CREATE DATABASE LABCE
go


-- Creacion de la tabla administrador
CREATE TABLE Administradores
(
Id INT NOT NULL,
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
Aprobado VARCHAR(10) NOT NULL,
Id_Admin INT,
PRIMARY KEY(Carnet),
CONSTRAINT "ADMIN_APRUEBA_FK" FOREIGN KEY(Id_Admin) REFERENCES Administradores(Id),

);



--Creacion de la tabla activo
CREATE TABLE Activos
(
Placa VARCHAR(10) UNIQUE NOT NULL,
Tipo VARCHAR(50) NOT NULL,
Marca VARCHAR(100) NOT NULL,
Fecha_Compra DATE,
Req_Aprobador VARCHAR(5) NOT NULL,
Id_Estado INT NOT NULL,
Nombre_Lab VARCHAR(50),
Ced_Prof INT,
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

-- Creacion de la tabla de reserva de profesores

CREATE TABLE Reserva_Lab_Profesor
(
Fecha DATE NOT NULL,
Hora_Inicio TIME NOT NULL,
Hora_Fin TIME NOT NULL,
Cant_Horas INT NOT NULL,
Ced_Prof INT NOT NULL,
Nombre_Lab VARCHAR(50) NOT NULL,
PRIMARY KEY(Ced_Prof, Nombre_Lab),
CONSTRAINT "RESERVA_LAB_CED_PROF" FOREIGN KEY(Ced_Prof) REFERENCES Profesores(Cedula),
CONSTRAINT "RESERVA_LAB_NOM_LAB" FOREIGN KEY(Nombre_Lab) REFERENCES Laboratorios(Nombre)
);

-- Creacion de la tabla relacion de la solicitud de activos de profesor

CREATE TABLE Solicitud_Act_Prof
(
Fecha DATE NOT NULL,
Hora TIME NOT NULL,
Ced_Prof INT NOT NULL,
Placa_Act VARCHAR(10) NOT NULL,
PRIMARY KEY (Ced_PROF,PLACA_ACT),
CONSTRAINT "SOLICITUD_ACT_CED_PROF" FOREIGN KEY(CED_PROF) REFERENCES PROFESORES(CEDULA),
CONSTRAINT "SOLICITUD_ACT_PLACA_ACT" FOREIGN KEY(PLACA_ACT) REFERENCES ACTIVOS(PLACA)
);

-- Creacion de la tabla relacion de la reservacion de labs por estudiantes

CREATE TABLE Reservacion_Lab_Est
(
Fecha DATE NOT NULL,
Hora_Inicio TIME NOT NULL,
Hora_Fin TIME NOT NULL,
Cant_Horas INT NOT NULL,
Nombre_Est VARCHAR(50) NOT NULL,
AP1_Est VARCHAR(50) NOT NULL,
AP2_Est VARCHAR(50) NOT NULL,
Carnet_Est INT NOT NULL,
Correo_Est VARCHAR(100) NOT NULL,
Carnet_Op INT NOT NULL,
NOM_LAB VARCHAR(50) NOT NULL,
PRIMARY KEY (CARNET_OP, NOM_LAB),
CONSTRAINT "RESERVACION_LAB_EST_CARNET_OP" FOREIGN KEY(CARNET_OP) REFERENCES OPERADORES(CARNET),
CONSTRAINT "RESERVACION_LAB_EST_NOM_LAB" FOREIGN KEY(NOM_LAB) REFERENCES LABORATORIOS(NOMBRE)
);

-- Creacion de la tabla relacion de solicitud de activos por parte de estudiantes
CREATE TABLE Solicitud_Act_Est
(
Fecha DATE NOT NULL,
Hora TIME NOT NULL,
Nombre_Est VARCHAR(50) NOT NULL,
AP1_Est VARCHAR(50) NOT NULL,
AP2_Est VARCHAR(50) NOT NULL,
Correo_Est VARCHAR(100) NOT NULL,
Carnet_Op INT NOT NULL,
Placa_Act VARCHAR(10) NOT NULL,
PRIMARY KEY(CARNET_OP, PLACA_ACT),
CONSTRAINT "SOLICITUD_ACT_EST_CARNET_OP" FOREIGN KEY(CARNET_OP) REFERENCES OPERADORES(CARNET),
CONSTRAINT "SOLICITUD_ACT_EST_PLACA_ACT" FOREIGN KEY (PLACA_ACT) REFERENCES ACTIVOS(PLACA)
);

-- Creacion tabla reportes
CREATE TABLE REPORTES
(
ID INT NOT NULL,
Fecha_Trabajo DATE NOT NULL,
Hora_Inicio TIME NOT NULL,
Hora_Final TIME NOT NULL,
Horas_Totalel INT NOT NULL,
Carnet_Op INT NOT NULL,
PRIMARY KEY(CARNET_OP, ID),
CONSTRAINT "REPORTES_CARNET_OP" FOREIGN KEY(CARNET_OP) REFERENCES OPERADORES(CARNET)
);

ALTER TABLE ACTIVOS
ADD CONSTRAINT "ACTIVO_EN_LAB_FK" FOREIGN KEY(NOMBRE_LAB) 
REFERENCES LABORATORIOS(NOMBRE);

ALTER TABLE ACTIVOS
ADD CONSTRAINT "ACTIVO_APR_PROF_FK" FOREIGN KEY(CED_PROF) 
REFERENCES PROFESORES(CEDULA);

ALTER TABLE ACTIVOS
ADD CONSTRAINT "ACTIVO_ESTADO_FK" FOREIGN KEY(ID_ESTADO) 
REFERENCES ESTADO_ACTIVOS(ID_ESTADO);