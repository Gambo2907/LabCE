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
Placa VARCHAR(50) UNIQUE NOT NULL,
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

-- Creacion tabla Reservacion

CREATE TABLE Reservaciones
(
ID INT UNIQUE NOT NULL,
Fecha DATE NOT NULL,
Hora_Inicio TIME NOT NULL,
Hora_Fin TIME NOT NULL,
Cant_Horas INT NOT NULL,
Nombre_Lab VARCHAR(50) NOT NULL,
PRIMARY KEY(ID,Nombre_Lab),
CONSTRAINT "Nombre_Lab_Reservacion" FOREIGN KEY(Nombre_Lab) REFERENCES Laboratorios(Nombre)
);

-- Creacion tabla Prestamo

CREATE TABLE Prestamos
(
ID INT UNIQUE NOT NULL,
Fecha DATE NOT NULL,
Hora TIME NOT NULL,
Placa_Activo VARCHAR(50) NOT NULL,
PRIMARY KEY (ID,Placa_Activo),
CONSTRAINT "Placa_Activo_Prestamo" FOREIGN KEY(Placa_Activo) REFERENCES Activos(Placa)
);


-- Creacion de la tabla de reserva de profesores

CREATE TABLE Reservacion_Lab_Profesores
(
ID_Reservacion INT NOT NULL,
Ced_Prof INT NOT NULL,
Nombre_Lab VARCHAR(50) NOT NULL,
PRIMARY KEY(Ced_Prof, ID_Reservacion, Nombre_Lab),
CONSTRAINT "Reserva_Lab_Ced_Prof" FOREIGN KEY(Ced_Prof) REFERENCES Profesores(Cedula),
CONSTRAINT "Reserva_Lab_Nom_Lab" FOREIGN KEY(Nombre_Lab) REFERENCES Laboratorios(Nombre),
CONSTRAINT "Reserva_Lab_ID_Reserva" FOREIGN KEY (ID_Reservacion) REFERENCES Reservaciones(ID)
);

-- Creacion de la tabla relacion de la solicitud de activos de profesor

CREATE TABLE Prestamo_Activo_Profesores
(
ID_Prestamo INT NOT NULL,
Ced_Prof INT NOT NULL,
Placa_Act VARCHAR(50) NOT NULL,
PRIMARY KEY (Ced_Prof, ID_Prestamo, Placa_Act),
CONSTRAINT "Prestamo_Act_Ced_Prof" FOREIGN KEY(CED_PROF) REFERENCES PROFESORES(CEDULA),
CONSTRAINT "Prestamo_Act_ID_Prestamo" FOREIGN KEY (ID_Prestamo) REFERENCES Prestamos(ID),
CONSTRAINT "Prestamo_Act_Placa_Act" FOREIGN KEY(PLACA_ACT) REFERENCES Activos(Placa)
);

-- Creacion de la tabla relacion de la reservacion de labs por estudiantes

CREATE TABLE Reservacion_Lab_Estudiantes
(
ID_Reservacion INT NOT NULL,
Nombre_Est VARCHAR(50) NOT NULL,
AP1_Est VARCHAR(50) NOT NULL,
AP2_Est VARCHAR(50) NOT NULL,
Carnet_Est INT NOT NULL,
Correo_Est VARCHAR(100) NOT NULL,
Carnet_Op INT NOT NULL,
Nom_Lab VARCHAR(50) NOT NULL,
PRIMARY KEY (Carnet_Op, ID_Reservacion, Nom_Lab),
CONSTRAINT "Reservacion_Lab_Est_Carnet_Op" FOREIGN KEY(Carnet_Op) REFERENCES OPERADORES(CARNET),
CONSTRAINT "Reservacion_Lab_Est_ID_Reservacion" FOREIGN KEY(ID_Reservacion) REFERENCES Reservaciones(ID),
CONSTRAINT "Reservacion_Lab_Est_Nom_Lab" FOREIGN KEY(NOM_LAB) REFERENCES Laboratorios(Nombre)
);

-- Creacion de la tabla relacion de solicitud de activos por parte de estudiantes
CREATE TABLE Prestamo_Activo_Estudiantes
(
ID_Prestamo INT NOT NULL,
Nombre_Est VARCHAR(50) NOT NULL,
AP1_Est VARCHAR(50) NOT NULL,
AP2_Est VARCHAR(50) NOT NULL,
Correo_Est VARCHAR(100) NOT NULL,
Carnet_OP INT NOT NULL,
Placa_Act VARCHAR(50) NOT NULL,
PRIMARY KEY(Carnet_Op, ID_Prestamo, Placa_Act),
CONSTRAINT "Prestamo_Act_Est_Carnet_Op" FOREIGN KEY(CARNET_OP) REFERENCES OPERADORES(CARNET),
CONSTRAINT "Prestamo_Act_Est_ID_Prestamo" FOREIGN KEY(ID_Prestamo) REFERENCES Prestamos(ID),
CONSTRAINT "Prestamo_Act_Est_Placa_Act" FOREIGN KEY (PLACA_ACT) REFERENCES ACTIVOS(PLACA)
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