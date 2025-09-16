
-- Crear la base de datos
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'DBClientes')
BEGIN
    CREATE DATABASE DBClientes;
END
GO

USE DBClientes;
GO
-- Crear tabla Clientes
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Clientes' AND xtype='U')
BEGIN
    CREATE TABLE Clientes (
        IdCliente INT IDENTITY(1,1) PRIMARY KEY,
        Identificacion NVARCHAR(20) NOT NULL UNIQUE,
        Nombre NVARCHAR(100) NOT NULL,
        Apellido NVARCHAR(100) NOT NULL,
        Email NVARCHAR(255) NOT NULL,
        FechaCreacion DATETIME2 DEFAULT GETDATE(),
        FechaActualizacion DATETIME2 DEFAULT GETDATE()
    );

    -- Crear índice en Identificacion
    CREATE NONCLUSTERED INDEX IX_Clientes_Identificacion 
    ON Clientes (Identificacion);
END
GO

-- Crear Stored Procedure
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'SP_ObtenerClientePorIdentificacion')
BEGIN
    DROP PROCEDURE SP_ObtenerClientePorIdentificacion;
END
GO

CREATE PROCEDURE SP_ObtenerClientePorIdentificacion
    @Identificacion NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        IdCliente,
        Identificacion,
        Nombre,
        Apellido,
        Email,
        FechaCreacion,
        FechaActualizacion
    FROM Clientes 
    WHERE Identificacion = @Identificacion;
END
GO

-- Insertar datos de prueba
IF NOT EXISTS (SELECT 1 FROM Clientes)
BEGIN
    INSERT INTO Clientes (Identificacion, Nombre, Apellido, Email)
    VALUES 
        ('12345678', 'Juan', 'Pérez', 'juan.perez@email.com'),
        ('87654321', 'María', 'González', 'maria.gonzalez@email.com'),
        ('11223344', 'Carlos', 'Rodríguez', 'carlos.rodriguez@email.com'),
        ('55667788', 'Ana', 'Martínez', 'ana.martinez@email.com'),
        ('99887766', 'Pedro', 'López', 'pedro.lopez@email.com');
END
GO

SELECT 'Base de datos configurada correctamente' AS Resultado;
