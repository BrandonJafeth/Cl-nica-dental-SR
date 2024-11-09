-- �ndice para mejorar las consultas por Fecha_Hora_Accion
CREATE INDEX IDX_Auditoria_Fecha_Hora_Accion
ON Auditoria (Fecha_Hora_Accion);
GO

-- �ndice para optimizar b�squedas por Usuario
CREATE INDEX IDX_Auditoria_Usuario
ON Auditoria (Usuario);
GO

-- �ndice para optimizar consultas que busquen por la acci�n realizada
CREATE INDEX IDX_Auditoria_Accion
ON Auditoria (Accion);
GO

-- �ndice combinado en Usuario y Fecha_Hora_Accion, �til si buscas registros por usuario en un rango de fechas
CREATE INDEX IDX_Auditoria_Usuario_Fecha
ON Auditoria (Usuario, Fecha_Hora_Accion);
GO

-- �ndice para mejorar la b�squeda de acciones seg�n el dispositivo
CREATE INDEX IDX_Auditoria_Dispositivo
ON Auditoria (DispositivoQueRealizo);
GO
