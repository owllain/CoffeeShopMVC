-- Verificar si la base de datos ya existe, y si es así, eliminarla
IF EXISTS (SELECT 1 FROM sys.databases WHERE name = 'BD_Proyecto3')
BEGIN
    ALTER DATABASE BD_Proyecto3 SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE BD_Proyecto3;
END

-- Crear la base de datos
CREATE DATABASE BD_Proyecto3;

-- Usar la base de datos recién creada
USE BD_Proyecto3;

-- Crear tabla "Clientes"
CREATE TABLE Clientes (
    ID INT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Privilegio VARCHAR(50) NOT NULL
);

-- Crear tabla "Platos" con la columna ImagenData para almacenar las imágenes
CREATE TABLE Platos (
    ID INT PRIMARY KEY,
    Nombre VARCHAR(100),
    Descripcion VARCHAR(100),
    ImagenData VARBINARY(MAX),
    Precio DECIMAL(10, 2),
    Categoria VARCHAR(50)
);

-- Crear tabla "Ventas"
CREATE TABLE Ventas (
    NumeroOrden INT PRIMARY KEY,
    FechaHoraVenta DATETIME NOT NULL,
    NombrePlatoVendido VARCHAR(100) NOT NULL,
    CantidadVendida INT NOT NULL
);

-- Crear tabla "Reservas"
CREATE TABLE Reservas (
    NumeroReserva INT PRIMARY KEY,
    FechaHoraReserva DATETIME NOT NULL,
    NombreCliente VARCHAR(100) NOT NULL,
    NumPersonas INT NOT NULL
);
