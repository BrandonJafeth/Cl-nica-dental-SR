


USE ClinicaDental;
GO

CREATE FUNCTION fn_ContarTratamientosPorPaciente (
    @ID_Paciente CHAR(8)
) 
RETURNS INT
AS
BEGIN
    RETURN (
        SELECT COUNT(DISTINCT PR.ID_Tratamiento)
        FROM Procedimiento PR
        INNER JOIN Tratamiento T ON PR.ID_Tratamiento = T.ID_Tratamiento
        WHERE PR.ID_Procedimiento IN (
            SELECT ID_Procedimiento 
            FROM Cita 
            WHERE ID_Paciente = @ID_Paciente
        )
    );
END;
GO







USE ClinicaDental;
GO

CREATE FUNCTION fn_SaldoPendiente (
    @ID_Paciente CHAR(8)
) 
RETURNS MONEY
AS
BEGIN
    DECLARE @TotalFacturado MONEY;
    DECLARE @TotalPagado MONEY;

    -- Suma todas las facturas del paciente
    SELECT @TotalFacturado = ISNULL(SUM(F.MontoTotal_Fa), 0)
    FROM Factura F
    INNER JOIN Cuenta C ON F.ID_Factura = C.ID_Factura
    WHERE C.ID_Paciente = @ID_Paciente;

    -- Suma todos los pagos realizados por el paciente
    SELECT @TotalPagado = ISNULL(SUM(P.Monto_Pago), 0)
    FROM Pago P
    INNER JOIN Factura F ON P.ID_Factura = F.ID_Factura
    INNER JOIN Cuenta C ON F.ID_Factura = C.ID_Factura
    WHERE C.ID_Paciente = @ID_Paciente;

    -- Retorna el saldo pendiente
    RETURN @TotalFacturado - @TotalPagado;
END;
GO



USE ClinicaDental;
GO
USE ClinicaDental;
GO

CREATE FUNCTION fn_TotalTratamientos (
    @ID_Paciente CHAR(8)
) 
RETURNS INT
AS
BEGIN
    RETURN (
        SELECT COUNT(DISTINCT T.ID_Tratamiento)
        FROM Procedimiento PR
        INNER JOIN Tratamiento T ON PR.ID_Tratamiento = T.ID_Tratamiento
        WHERE PR.ID_Procedimiento IN (
            SELECT ID_Procedimiento 
            FROM Cita 
            WHERE ID_Paciente = @ID_Paciente
        )
    );
END;
GO





Go
CREATE FUNCTION fn_CostoTotalTratamientos (
    @ID_Paciente CHAR(8)
) 
RETURNS MONEY
AS
BEGIN
    RETURN (
        SELECT ISNULL(SUM(F.MontoTotal_Fa), 0)
        FROM Factura_Tratamiento FT
        INNER JOIN Factura F ON FT.ID_Factura = F.ID_Factura
        INNER JOIN Cuenta C ON F.ID_Factura = C.ID_Factura
        WHERE C.ID_Paciente = @ID_Paciente
    );
END;
GO




Use ClinicaDental;
Go
CREATE FUNCTION fn_EdadPaciente (
    @FechaNacimiento DATE
) 
RETURNS INT
AS
BEGIN
    DECLARE @Edad INT;

    -- Calcula la edad restando el año actual con el año de nacimiento
    SET @Edad = DATEDIFF(YEAR, @FechaNacimiento, GETDATE());

    -- Ajusta si el cumpleaños aún no ha ocurrido en el año actual
    IF (MONTH(@FechaNacimiento) > MONTH(GETDATE())) OR 
       (MONTH(@FechaNacimiento) = MONTH(GETDATE()) AND DAY(@FechaNacimiento) > DAY(GETDATE()))
    BEGIN
        SET @Edad = @Edad - 1;
    END;

    RETURN @Edad;
END;
GO



-- Probar fn_ContarTratamientosPorPaciente
SELECT dbo.fn_ContarTratamientosPorPaciente('PAC00001') AS TotalTratamientos;

-- Probar fn_SaldoPendiente
SELECT dbo.fn_SaldoPendiente('PAC00001') AS SaldoPendiente;

-- Probar fn_TotalTratamientos

SELECT dbo.fn_ContarTratamientosPorPaciente('PAC00001') AS TotalTratamientos;

-- Probar fn_TotalTratamientos
SELECT dbo.fn_TotalTratamientos('PAC00001') AS TotalTratamientos;


-- Probar fn_CostoTotalTratamientos
SELECT dbo.fn_CostoTotalTratamientos('PAC00001') AS CostoTotalTratamientos;

-- Probar fn_EdadPaciente
SELECT dbo.fn_EdadPaciente('1990-05-15') AS EdadPaciente;












DROP FUNCTION IF EXISTS fn_ContarTratamientosPorPaciente;
GO



-- Eliminar función fn_TotalTratamientos
DROP FUNCTION IF EXISTS fn_TotalTratamientos;
GO