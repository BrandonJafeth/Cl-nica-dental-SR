-- Índice para mejorar las consultas por Fecha_Hora_Accion
CREATE INDEX IDX_Auditoria_Fecha_Hora_Accion
ON Auditoria (Fecha_Hora_Accion);
GO

-- Índice para optimizar búsquedas por Usuario
CREATE INDEX IDX_Auditoria_Usuario
ON Auditoria (Usuario);
GO

-- Índice para optimizar consultas que busquen por la acción realizada
CREATE INDEX IDX_Auditoria_Accion
ON Auditoria (Accion);
GO

-- Índice combinado en Usuario y Fecha_Hora_Accion, útil si buscas registros por usuario en un rango de fechas
CREATE INDEX IDX_Auditoria_Usuario_Fecha
ON Auditoria (Usuario, Fecha_Hora_Accion);
GO

-- Índice para mejorar la búsqueda de acciones según el dispositivo
CREATE INDEX IDX_Auditoria_Dispositivo
ON Auditoria (DispositivoQueRealizo);
GO
