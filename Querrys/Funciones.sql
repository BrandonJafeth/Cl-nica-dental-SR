-- Función: fn_ContarTratamientosPorPaciente
-- Descripción: Cuenta el número total de tratamientos distintos realizados a un paciente.
DROP FUNCTION IF EXISTS fn_ContarTratamientosPorPaciente;
GO
CREATE FUNCTION fn_ContarTratamientosPorPaciente (
    @ID_Paciente CHAR(8)
)
RETURNS INT
AS
BEGIN
    RETURN (
        SELECT COUNT(DISTINCT HT.ID_Tratamiento)
        FROM Historial_Tratamiento HT
        INNER JOIN Historial_Medico HM ON HT.ID_HistorialMedico = HM.ID_HistorialMedico
        INNER JOIN Paciente P ON HM.ID_HistorialMedico = P.ID_HistorialMedico
        WHERE P.ID_Paciente = @ID_Paciente
    );
END;
GO

DROP FUNCTION IF EXISTS fn_SaldoPendiente;
GO
CREATE FUNCTION fn_SaldoPendiente (
    @ID_Paciente CHAR(8)
) 
RETURNS MONEY
AS
BEGIN
    DECLARE @TotalFacturado MONEY = 0;
    DECLARE @TotalPagado MONEY = 0;

    -- Suma todas las facturas del paciente
    SELECT @TotalFacturado = ISNULL(SUM(F.MontoTotal_Fa), 0)
    FROM Factura F
    INNER JOIN Cuenta C ON F.ID_Factura = C.ID_Factura
    WHERE C.ID_Paciente = @ID_Paciente;

    -- Suma todos los pagos realizados por el paciente
    SELECT @TotalPagado = ISNULL(SUM(P.Monto_Pago), 0)
    FROM Pago P
    INNER JOIN Factura F ON P.ID_Factura = F.ID_Factura
    WHERE F.ID_Factura IN (SELECT ID_Factura FROM Cuenta WHERE ID_Paciente = @ID_Paciente);

    -- Retorna el saldo pendiente
    RETURN @TotalFacturado - @TotalPagado;
END;
GO

-- Función: fn_TotalTratamientos
-- Descripción: Calcula el número total de tratamientos realizados a un paciente, incluyendo repeticiones.
DROP FUNCTION IF EXISTS fn_TotalTratamientos;
GO
CREATE FUNCTION fn_TotalTratamientos (
    @ID_Paciente CHAR(8)
)
RETURNS INT
AS
BEGIN
    RETURN (
        SELECT COUNT(HT.ID_Tratamiento)
        FROM Historial_Tratamiento HT
        INNER JOIN Historial_Medico HM ON HT.ID_HistorialMedico = HM.ID_HistorialMedico
        INNER JOIN Paciente P ON HM.ID_HistorialMedico = P.ID_HistorialMedico
        WHERE P.ID_Paciente = @ID_Paciente
    );
END;
GO

-- Función: fn_CostoTotalTratamientos
-- Descripción: Calcula el costo total de los tratamientos realizados a un paciente.
DROP FUNCTION IF EXISTS fn_CostoTotalTratamientos;
GO
CREATE FUNCTION fn_CostoTotalTratamientos (
    @ID_Paciente CHAR(8)
)
RETURNS MONEY
AS
BEGIN
    DECLARE @CostoTotal MONEY = 0;

    SELECT @CostoTotal = ISNULL(SUM(F.MontoTotal_Fa), 0)
    FROM Factura F
    INNER JOIN Cuenta C ON F.ID_Factura = C.ID_Factura
    WHERE C.ID_Paciente = @ID_Paciente;

    RETURN @CostoTotal;
END;
GO

-- Función: fn_EdadPaciente
-- Descripción: Calcula la edad de un paciente en función de su fecha de nacimiento.
DROP FUNCTION IF EXISTS fn_EdadPacientePorID;
GO
CREATE FUNCTION fn_EdadPacientePorID (
    @ID_Paciente CHAR(8)
)
RETURNS INT
AS
BEGIN
    DECLARE @FechaNacimiento DATE;
    DECLARE @Edad INT;

    -- Obtener la fecha de nacimiento del paciente
    SELECT @FechaNacimiento = Fecha_Nacimiento_Pac
    FROM Paciente
    WHERE ID_Paciente = @ID_Paciente;

    -- Calcular la edad
    SET @Edad = DATEDIFF(YEAR, @FechaNacimiento, GETDATE());

    -- Ajustar la edad si la fecha actual es anterior al cumpleaños de este año
    IF (MONTH(GETDATE()) < MONTH(@FechaNacimiento)) OR 
       (MONTH(GETDATE()) = MONTH(@FechaNacimiento) AND DAY(GETDATE()) < DAY(@FechaNacimiento))
    BEGIN
        SET @Edad = @Edad - 1;
    END;

    RETURN @Edad;
END;
GO

-- Función: fn_UsuarioActual
-- Descripción: Devuelve el nombre del usuario actual conectado a la base de datos.
DROP FUNCTION IF EXISTS fn_UsuarioActual;
GO
CREATE FUNCTION fn_UsuarioActual()
RETURNS VARCHAR(128)
AS
BEGIN
    RETURN SYSTEM_USER;
END;
GO

-- Función: fn_UltimaAccionUsuario
-- Descripción: Consulta la última acción registrada por un usuario específico en la tabla de auditoría.
-- Función para Usuarios del Sistema
DROP FUNCTION IF EXISTS fn_UltimaAccionUsuario;
GO
CREATE FUNCTION fn_UltimaAccionUsuario (
    @ID_Usuario CHAR(8)
)
RETURNS TABLE
AS
RETURN
(
    SELECT TOP 1 *
    FROM Auditoria
    WHERE ID_Usuario = @ID_Usuario
    ORDER BY Fecha_Hora_Accion DESC
);
GO

-- Función para Usuarios de la Base de Datos
DROP FUNCTION IF EXISTS fn_UltimaAccionDBUser;
GO
CREATE FUNCTION fn_UltimaAccionDBUser (
    @ID_DBUser CHAR(8)
)
RETURNS TABLE
AS
RETURN
(
    SELECT TOP 1 *
    FROM Auditoria
    WHERE ID_DBUser = @ID_DBUser
    ORDER BY Fecha_Hora_Accion DESC
);
GO

-- Función: fn_NombreCompletoPaciente
-- Descripción: Devuelve el nombre completo de un paciente (incluyendo nombre y apellidos).
DROP FUNCTION IF EXISTS fn_NombreCompletoPaciente;
GO
CREATE FUNCTION fn_NombreCompletoPaciente (
    @ID_Paciente CHAR(8)
)
RETURNS VARCHAR(255)
AS
BEGIN
    DECLARE @NombreCompleto VARCHAR(255);

    SELECT @NombreCompleto = CONCAT(Nombre_Pac, ' ', Apellido1_Pac, ' ', Apellido2_Pac)
    FROM Paciente
    WHERE ID_Paciente = @ID_Paciente;

    RETURN @NombreCompleto;
END;
GO

-- Función: fn_FormatoFecha
-- Descripción: Convierte una fecha al formato dd/mm/yyyy.
DROP FUNCTION IF EXISTS fn_FormatoFecha;
GO
CREATE FUNCTION fn_FormatoFecha (
    @Fecha DATE
)
RETURNS VARCHAR(10)
AS
BEGIN
    RETURN CONVERT(VARCHAR(10), @Fecha, 103); -- Formato dd/mm/yyyy
END;
GO

-- Función: fn_EstadoCita
-- Descripción: Devuelve el estado de una cita como texto legible en función de su código.
DROP FUNCTION IF EXISTS fn_EstadoCita;
GO
CREATE FUNCTION fn_EstadoCita (
    @ID_Cita CHAR(8)
)
RETURNS VARCHAR(50)
AS
BEGIN
    DECLARE @NombreEstado VARCHAR(50);

    SELECT @NombreEstado = EC.Nombre_Estado
    FROM Cita C
    INNER JOIN Estado_Citas EC ON C.ID_EstadoCita = EC.ID_EstadoCita
    WHERE C.ID_Cita = @ID_Cita;

    RETURN @NombreEstado;
END;
GO

-- Función: fn_ProximaCitaPaciente
-- Descripción: Devuelve la próxima cita programada para un paciente.
DROP FUNCTION IF EXISTS fn_ProximaCitaPaciente;
GO
CREATE FUNCTION fn_ProximaCitaPaciente (
    @ID_Paciente CHAR(8)
)
RETURNS TABLE
AS
RETURN
(
    SELECT TOP 1
        C.ID_Cita,
        C.Fecha_Cita,
        C.Hora_Inicio,
        C.Hora_Fin,
        C.Motivo,
        C.ID_Dentista,
        D.Nombre_Den,
        D.Apellido1_Den,
        D.Apellido2_Den
    FROM Cita C
    INNER JOIN Dentista D ON C.ID_Dentista = D.ID_Dentista
    WHERE C.ID_Paciente = @ID_Paciente
      AND C.Fecha_Cita >= CONVERT(DATE, GETDATE())
    ORDER BY C.Fecha_Cita ASC, C.Hora_Inicio ASC
);
GO

-- Función: fn_DentistaMasCitas
-- Descripción: Devuelve el dentista que ha tenido más citas en un periodo de tiempo específico.
DROP FUNCTION IF EXISTS fn_DentistaMasCitas;
GO
CREATE FUNCTION fn_DentistaMasCitas (
    @Fecha_Inicio DATE,
    @Fecha_Fin DATE
)
RETURNS TABLE
AS
RETURN
(
    SELECT TOP 1
        C.ID_Dentista,
        D.Nombre_Den,
        D.Apellido1_Den,
        D.Apellido2_Den,
        COUNT(*) AS TotalCitas
    FROM Cita C
    INNER JOIN Dentista D ON C.ID_Dentista = D.ID_Dentista
    WHERE C.Fecha_Cita BETWEEN @Fecha_Inicio AND @Fecha_Fin
    GROUP BY C.ID_Dentista, D.Nombre_Den, D.Apellido1_Den, D.Apellido2_Den
    ORDER BY COUNT(*) DESC
);
GO

-- Función: fn_ResumenFacturacion
-- Descripción: Devuelve un resumen de facturación (monto total facturado y monto pagado) para un paciente.
DROP FUNCTION IF EXISTS fn_ResumenFacturacion;
GO
CREATE FUNCTION fn_ResumenFacturacion (
    @ID_Paciente CHAR(8)
)
RETURNS TABLE
AS
RETURN
(
    SELECT
        P.ID_Paciente,
        P.Nombre_Pac,
        P.Apellido1_Pac,
        P.Apellido2_Pac,
        (SELECT SUM(F.MontoTotal_Fa)
         FROM Factura F
         INNER JOIN Cuenta C ON F.ID_Factura = C.ID_Factura
         WHERE C.ID_Paciente = @ID_Paciente) AS TotalFacturado,
        (SELECT SUM(PG.Monto_Pago)
         FROM Pago PG
         INNER JOIN Factura F ON PG.ID_Factura = F.ID_Factura
         INNER JOIN Cuenta C ON F.ID_Factura = C.ID_Factura
         WHERE C.ID_Paciente = @ID_Paciente) AS TotalPagado
    FROM Paciente P
    WHERE P.ID_Paciente = @ID_Paciente
);
GO

-- Consultas para probar las funciones

-- Probar fn_ContarTratamientosPorPaciente
SELECT dbo.fn_ContarTratamientosPorPaciente('PAC00001') AS TotalTratamientosPorPaciente;
GO

-- Probar fn_SaldoPendiente
SELECT dbo.fn_SaldoPendiente('PAC00001') AS SaldoPendiente;
GO

-- Probar fn_TotalTratamientos
SELECT dbo.fn_TotalTratamientos('PAC00001') AS TotalTratamientos;
GO

-- Probar fn_CostoTotalTratamientos
SELECT dbo.fn_CostoTotalTratamientos('PAC00001') AS CostoTotalTratamientos;
GO

-- Calcular la edad del paciente con ID 'PAC00001'
SELECT dbo.fn_EdadPacientePorID('PAC00001') AS EdadPaciente;
GO

-- Probar fn_UsuarioActual
SELECT dbo.fn_UsuarioActual() AS UsuarioActual;
GO

-- Probar fn_UltimaAccionUsuario
-- Probar fn_UltimaAccionUsuario para un usuario del sistema
SELECT * FROM dbo.fn_UltimaAccionUsuario('USER0001');
GO

-- Probar fn_UltimaAccionDBUser para un usuario de la base de datos
SELECT * FROM dbo.fn_UltimaAccionDBUser('DBUSER01');
GO

-- Probar fn_NombreCompletoPaciente
SELECT dbo.fn_NombreCompletoPaciente('PAC00001') AS NombreCompletoPaciente;
GO

-- Probar fn_FormatoFecha
SELECT dbo.fn_FormatoFecha('2024-10-31') AS FechaFormateada;
GO

-- Probar fn_EstadoCita
SELECT dbo.fn_EstadoCita('CITA0001') AS EstadoCita;
GO

-- Probar fn_ProximaCitaPaciente
SELECT * FROM fn_ProximaCitaPaciente('PAC00001');
GO

-- Probar fn_DentistaMasCitas
SELECT * FROM fn_DentistaMasCitas('2024-11-01', '2024-11-30');
GO

-- Probar fn_ResumenFacturacion
SELECT * FROM fn_ResumenFacturacion('PAC00001');
GO