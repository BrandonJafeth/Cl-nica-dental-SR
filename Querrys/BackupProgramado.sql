USE msdb;
GO

-- 1. Crear el trabajo de SQL Server Agent
EXEC sp_add_job 
    @job_name = N'Backup Diario ClinicaDental';

-- 2. Agregar un paso al trabajo para hacer el backup de la base de datos
EXEC sp_add_jobstep 
    @job_name = N'Backup Diario ClinicaDental',
    @step_name = N'Backup Completo',
    @subsystem = N'TSQL',
    @command = N'BACKUP DATABASE ClinicaDental 
                 TO DISK = ''C:\SQLBackup\ClinicaDental_Backup_'' + 
                 CONVERT(VARCHAR, GETDATE(), 112) + ''.bak''
                 WITH INIT;',
    @on_success_action = 1,  -- Ir al siguiente paso
    @on_fail_action = 2;     -- Detener el trabajo en caso de fallo

-- 3. Crear un horario para que el trabajo se ejecute a diario a las 12:00 AM
EXEC sp_add_schedule 
    @schedule_name = N'Horario Diario a Medianoche',
    @freq_type = 4,       -- Diario
    @freq_interval = 1,   -- Cada día
    @active_start_time = 0; -- Medianoche (00:00:00)

-- 4. Asociar el horario al trabajo de backup
EXEC sp_attach_schedule 
    @job_name = N'Backup Diario ClinicaDental', 
    @schedule_name = N'Horario Diario a Medianoche';

-- 5. Habilitar el trabajo
EXEC sp_add_jobserver 
    @job_name = N'Backup Diario ClinicaDental';
GO



USE msdb;
GO

-- 1. Eliminar el trabajo de SQL Server Agent
EXEC sp_delete_job 
    @job_name = N'Backup Diario ClinicaDental';
GO

-- 2. Eliminar el horario asociado (opcional si ya no se usa en otros trabajos)
EXEC sp_delete_schedule 
    @schedule_name = N'Horario Diario a Medianoche';
GO