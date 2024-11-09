using System;
using System.Collections.Generic;
using Domain.Entities.Views;
using Microsoft.EntityFrameworkCore;

namespace Clinica_Dental;

public partial class MydDbContext : DbContext
{
    public MydDbContext()
    {
    }

    public MydDbContext(DbContextOptions<MydDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auditoria> Auditoria { get; set; }

    public virtual DbSet<Cita> Cita { get; set; }

    public virtual DbSet<Cuenta> Cuenta { get; set; }

    public virtual DbSet<Dentista_Especialidad> Dentista_Especialidads { get; set; }

    public virtual DbSet<Dentista> Dentista { get; set; }

    public virtual DbSet<Especialidad> Especialidads { get; set; }

    public virtual DbSet<Estado_Cita> Estado_Citas { get; set; }

    public virtual DbSet<Estado_Cuenta> Estado_Cuenta { get; set; }

    public virtual DbSet<Estado_Pago> Estado_Pagos { get; set; }

    public virtual DbSet<Estado_Tratamiento> Estado_Tratamientos { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Factura_Procedimiento> Factura_Procedimientos { get; set; }

    public virtual DbSet<Factura_Tratamiento> Factura_Tratamientos { get; set; }

    public virtual DbSet<Funcionario> Funcionarios { get; set; }

    public virtual DbSet<Historial_Medico> Historial_Medicos { get; set; }

    public virtual DbSet<Historial_Tratamiento> Historial_Tratamientos { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Procedimiento> Procedimientos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Roles_Permiso> Roles_Permisos { get; set; }

    public virtual DbSet<Tipo_Pago> Tipo_Pagos { get; set; }

    public virtual DbSet<Tipo_Tratamiento> Tipo_Tratamientos { get; set; }

    public virtual DbSet<Tratamiento> Tratamientos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Usuario_Role> Usuario_Roles { get; set; }

    public virtual DbSet<vw_CitasPorEstado> vw_CitasPorEstados { get; set; }

    public virtual DbSet<vw_DentistasConMasTratamiento> vw_DentistasConMasTratamientos { get; set; }

    public virtual DbSet<vw_FacturacionPorFecha> vw_FacturacionPorFechas { get; set; }

    public virtual DbSet<vw_FacturasPendiente> vw_FacturasPendientes { get; set; }

    public virtual DbSet<vw_HistorialPaciente> vw_HistorialPacientes { get; set; }

    public virtual DbSet<vw_PacientesConTratamientosActivo> vw_PacientesConTratamientosActivos { get; set; }

    public virtual DbSet<vw_ProximasCita> vw_ProximasCitas { get; set; }

    public virtual DbSet<vw_ResumenFinancieroPaciente> vw_ResumenFinancieroPacientes { get; set; }

    public virtual DbSet<vw_TratamientosPorDentista> vw_TratamientosPorDentista { get; set; }

    public virtual DbSet<vw_TratamientosPorPaciente> vw_TratamientosPorPacientes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=BRANDONCA;Initial Catalog=ClinicaDental;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;Connect Timeout=60;Command Timeout=300;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auditoria>(entity =>
        {
            entity.HasKey(e => e.ID_Auditoria).HasName("PK__Auditori__2C0A46DBE6581F43");

            entity.Property(e => e.ID_Auditoria).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Accion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DispositivoQueRealizo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fecha_Hora_Accion).HasColumnType("datetime");
            entity.Property(e => e.Usuario)
                .HasMaxLength(128)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.ID_Cita).HasName("PK__Cita__7C17FD160E32939C");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("tr_AuditarActualizacionCita");
                    tb.HasTrigger("tr_AuditarEliminacionCita");
                    tb.HasTrigger("tr_AuditarInsercionCita");
                    tb.HasTrigger("tr_EvitarDuplicadosCita");
                });

            entity.Property(e => e.ID_Cita)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Dentista)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_EstadoCita)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Funcionario)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Paciente)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Motivo)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.ID_DentistaNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.ID_Dentista)
                .HasConstraintName("FK__Cita__ID_Dentist__6B24EA82");

            entity.HasOne(d => d.ID_EstadoCitaNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.ID_EstadoCita)
                .HasConstraintName("FK__Cita__ID_EstadoC__6D0D32F4");

            entity.HasOne(d => d.ID_FuncionarioNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.ID_Funcionario)
                .HasConstraintName("FK__Cita__ID_Funcion__6C190EBB");

            entity.HasOne(d => d.ID_PacienteNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.ID_Paciente)
                .HasConstraintName("FK__Cita__ID_Pacient__6A30C649");
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.ID_Cuenta).HasName("PK__Cuenta__820D611FD52E8A69");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("tr_AuditarActualizacionCuenta");
                    tb.HasTrigger("tr_AuditarEliminacionCuenta");
                    tb.HasTrigger("tr_AuditarInsercionCuenta");
                });

            entity.Property(e => e.ID_Cuenta)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Estado_Cuenta)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Factura)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Paciente)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Observaciones)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Saldo_Total).HasColumnType("money");

            entity.HasOne(d => d.ID_Estado_CuentaNavigation).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.ID_Estado_Cuenta)
                .HasConstraintName("FK__Cuenta__ID_Estad__619B8048");

            entity.HasOne(d => d.ID_FacturaNavigation).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.ID_Factura)
                .HasConstraintName("FK__Cuenta__ID_Factu__628FA481");

            entity.HasOne(d => d.ID_PacienteNavigation).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.ID_Paciente)
                .HasConstraintName("FK__Cuenta__ID_Pacie__6383C8BA");
        });

        modelBuilder.Entity<Dentista_Especialidad>(entity =>
        {
            entity.HasKey(e => e.ID_Dentista_Especialidad).HasName("PK__Dentista__5A0D823E1000AB2E");

            entity.ToTable("Dentista_Especialidad", tb =>
                {
                    tb.HasTrigger("tr_AuditarActualizacionDentistaEspecialidad");
                    tb.HasTrigger("tr_AuditarEliminacionDentistaEspecialidad");
                    tb.HasTrigger("tr_AuditarInsercionDentistaEspecialidad");
                });

            entity.Property(e => e.ID_Dentista_Especialidad)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Dentista)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Especialidad)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.ID_DentistaNavigation).WithMany(p => p.Dentista_Especialidads)
                .HasForeignKey(d => d.ID_Dentista)
                .HasConstraintName("FK__Dentista___ID_De__5070F446");

            entity.HasOne(d => d.ID_EspecialidadNavigation).WithMany(p => p.Dentista_Especialidads)
                .HasForeignKey(d => d.ID_Especialidad)
                .HasConstraintName("FK__Dentista___ID_Es__5165187F");
        });

        modelBuilder.Entity<Dentista>(entity =>
        {
            entity.HasKey(e => e.ID_Dentista).HasName("PK__Dentista__99A0225AB60F7A5C");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("tr_AuditarActualizacionDentista");
                    tb.HasTrigger("tr_AuditarEliminacionDentista");
                    tb.HasTrigger("tr_AuditarInsercionDentista");
                });

            entity.Property(e => e.ID_Dentista)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Apellido1_Den)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2_Den)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Correo_Den)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Direccion_Den)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ID_Funcionario)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nombre_Den)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Telefono_Den)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.ID_FuncionarioNavigation).WithMany(p => p.Dentista)
                .HasForeignKey(d => d.ID_Funcionario)
                .HasConstraintName("FK__Dentista__ID_Fun__4BAC3F29");
        });

        modelBuilder.Entity<Especialidad>(entity =>
        {
            entity.HasKey(e => e.ID_Especialidad).HasName("PK__Especial__5D7732D7854F8876");

            entity.ToTable("Especialidad", tb =>
                {
                    tb.HasTrigger("tr_AuditarActualizacionEspecialidad");
                    tb.HasTrigger("tr_AuditarEliminacionEspecialidad");
                    tb.HasTrigger("tr_AuditarInsercionEspecialidad");
                });

            entity.Property(e => e.ID_Especialidad)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Descripcion_Esp)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_Esp)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Estado_Cita>(entity =>
        {
            entity.HasKey(e => e.ID_EstadoCita).HasName("PK__Estado_C__82FA13D96A36A238");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("tr_AuditarActualizacionEstadoCitas");
                    tb.HasTrigger("tr_AuditarEliminacionEstadoCitas");
                    tb.HasTrigger("tr_AuditarInsercionEstadoCitas");
                });

            entity.Property(e => e.ID_EstadoCita)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Descripcion_Estado)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Estado_Cuenta>(entity =>
        {
            entity.HasKey(e => e.ID_Estado_Cuenta).HasName("PK__Estado_C__127A61A58239AFAF");

            entity.Property(e => e.ID_Estado_Cuenta)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Descripcion_EC)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_EC)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Estado_Pago>(entity =>
        {
            entity.HasKey(e => e.ID_EstadoPago).HasName("PK__Estado_P__3ECA89D5AC5C9CC3");

            entity.ToTable("Estado_Pago", tb =>
                {
                    tb.HasTrigger("tr_AuditarActualizacionEstadoPago");
                    tb.HasTrigger("tr_AuditarEliminacionEstadoPago");
                    tb.HasTrigger("tr_AuditarInsercionEstadoPago");
                });

            entity.Property(e => e.ID_EstadoPago)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Descripcion_EP)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_EP)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Estado_Tratamiento>(entity =>
        {
            entity.HasKey(e => e.ID_EstadoTratamiento).HasName("PK__Estado_T__4372CBE5EE9BDA01");

            entity.ToTable("Estado_Tratamiento");

            entity.Property(e => e.ID_EstadoTratamiento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Descripcion_Estado)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.ID_Factura).HasName("PK__Factura__E9D586A865003965");

            entity.ToTable("Factura", tb =>
                {
                    tb.HasTrigger("tr_AuditarActualizacionFactura");
                    tb.HasTrigger("tr_AuditarEliminacionFactura");
                    tb.HasTrigger("tr_AuditarInsercionFactura");
                    tb.HasTrigger("tr_EvitarFacturaConMontoCero");
                });

            entity.Property(e => e.ID_Factura)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_EstadoPago)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MontoTotal_Fa).HasColumnType("money");

            entity.HasOne(d => d.ID_EstadoPagoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.ID_EstadoPago)
                .HasConstraintName("FK__Factura__ID_Esta__59FA5E80");
        });

        modelBuilder.Entity<Factura_Procedimiento>(entity =>
        {
            entity.HasKey(e => e.ID_Factura_Procedimiento).HasName("PK__Factura___3F2E92FB0601FDE3");

            entity.ToTable("Factura_Procedimiento", tb =>
                {
                    tb.HasTrigger("tr_AuditarActualizacionFacturaProcedimiento");
                    tb.HasTrigger("tr_AuditarEliminacionFacturaProcedimiento");
                    tb.HasTrigger("tr_AuditarInsercionFacturaProcedimiento");
                });

            entity.Property(e => e.ID_Factura_Procedimiento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Factura)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Procedimiento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.ID_FacturaNavigation).WithMany(p => p.Factura_Procedimientos)
                .HasForeignKey(d => d.ID_Factura)
                .HasConstraintName("FK__Factura_P__ID_Fa__7F2BE32F");

            entity.HasOne(d => d.ID_ProcedimientoNavigation).WithMany(p => p.Factura_Procedimientos)
                .HasForeignKey(d => d.ID_Procedimiento)
                .HasConstraintName("FK__Factura_P__ID_Pr__00200768");
        });

        modelBuilder.Entity<Factura_Tratamiento>(entity =>
        {
            entity.HasKey(e => e.ID_Factura_Tratamiento).HasName("PK__Factura___800ACAE12CEA5D81");

            entity.ToTable("Factura_Tratamiento", tb =>
                {
                    tb.HasTrigger("tr_AuditarActualizacionFacturaTratamiento");
                    tb.HasTrigger("tr_AuditarEliminacionFacturaTratamiento");
                    tb.HasTrigger("tr_AuditarInsercionFacturaTratamiento");
                });

            entity.Property(e => e.ID_Factura_Tratamiento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Factura)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Tratamiento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.ID_FacturaNavigation).WithMany(p => p.Factura_Tratamientos)
                .HasForeignKey(d => d.ID_Factura)
                .HasConstraintName("FK__Factura_T__ID_Fa__02FC7413");

            entity.HasOne(d => d.ID_TratamientoNavigation).WithMany(p => p.Factura_Tratamientos)
                .HasForeignKey(d => d.ID_Tratamiento)
                .HasConstraintName("FK__Factura_T__ID_Tr__03F0984C");
        });

        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.HasKey(e => e.ID_Funcionario).HasName("PK__Funciona__0AE977B95F5546F8");

            entity.ToTable("Funcionario", tb =>
                {
                    tb.HasTrigger("tr_AuditarActualizacionFuncionario");
                    tb.HasTrigger("tr_AuditarEliminacionFuncionario");
                    tb.HasTrigger("tr_AuditarInsercionFuncionario");
                });

            entity.Property(e => e.ID_Funcionario)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Apellido1)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Contraseña)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Historial_Medico>(entity =>
        {
            entity.HasKey(e => e.ID_HistorialMedico).HasName("PK__Historia__C9A115780EFAF8C4");

            entity.ToTable("Historial_Medico", tb =>
                {
                    tb.HasTrigger("tr_AuditarActualizacionHistorialMedico");
                    tb.HasTrigger("tr_AuditarEliminacionHistorialMedico");
                    tb.HasTrigger("tr_AuditarInsercionHistorialMedico");
                });

            entity.HasIndex(e => e.ID_Paciente, "UQ__Historia__5F3650604ECC0C4C").IsUnique();

            entity.Property(e => e.ID_HistorialMedico)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Diagnostico)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ID_Paciente)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.ID_PacienteNavigation).WithOne(p => p.Historial_Medico)
                .HasForeignKey<Historial_Medico>(d => d.ID_Paciente)
                .HasConstraintName("FK_HistorialMedico_Paciente");
        });

        modelBuilder.Entity<Historial_Tratamiento>(entity =>
        {
            entity.HasKey(e => e.ID_Historial_Tratamiento).HasName("PK__Historia__2611FDA2CC7604E3");

            entity.ToTable("Historial_Tratamiento");

            entity.Property(e => e.ID_Historial_Tratamiento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_HistorialMedico)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Tratamiento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.ID_HistorialMedicoNavigation).WithMany(p => p.Historial_Tratamientos)
                .HasForeignKey(d => d.ID_HistorialMedico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__ID_Hi__6FE99F9F");

            entity.HasOne(d => d.ID_TratamientoNavigation).WithMany(p => p.Historial_Tratamientos)
                .HasForeignKey(d => d.ID_Tratamiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__ID_Tr__70DDC3D8");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.ID_Paciente).HasName("PK__Paciente__5F365061CEFD25E1");

            entity.ToTable("Paciente", tb =>
                {
                    tb.HasTrigger("tr_AuditarActualizacionPaciente");
                    tb.HasTrigger("tr_AuditarEliminacionPaciente");
                    tb.HasTrigger("tr_AuditarInsercionPaciente");
                });

            entity.Property(e => e.ID_Paciente)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Apellido1_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Correo_Pac)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Direccion_Pac)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Telefono_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.ID_Pago).HasName("PK__Pago__AE88B4290CBA209A");

            entity.ToTable("Pago", tb =>
                {
                    tb.HasTrigger("tr_ActualizarEstadoFactura");
                    tb.HasTrigger("tr_AuditarInsercionPago");
                    tb.HasTrigger("tr_EvitarPagosNegativos");
                });

            entity.Property(e => e.ID_Pago).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ID_Factura)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Tipo_Pago)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Monto_Pago).HasColumnType("money");

            entity.HasOne(d => d.ID_FacturaNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.ID_Factura)
                .HasConstraintName("FK__Pago__ID_Factura__5DCAEF64");

            entity.HasOne(d => d.ID_Tipo_PagoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.ID_Tipo_Pago)
                .HasConstraintName("FK__Pago__ID_Tipo_Pa__5EBF139D");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.ID_Permisos).HasName("PK__Permisos__4AC6BD0AEA998186");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("tr_AuditarActualizacionPermisos");
                    tb.HasTrigger("tr_AuditarEliminacionPermisos");
                    tb.HasTrigger("tr_AuditarInsercionPermisos");
                });

            entity.Property(e => e.ID_Permisos)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Procedimiento>(entity =>
        {
            entity.HasKey(e => e.ID_Procedimiento).HasName("PK__Procedim__5A929191BCC5DF04");

            entity.ToTable("Procedimiento", tb =>
                {
                    tb.HasTrigger("tr_AuditarActualizacionProcedimiento");
                    tb.HasTrigger("tr_AuditarEliminacionProcedimiento");
                    tb.HasTrigger("tr_AuditarInsercionProcedimiento");
                });

            entity.Property(e => e.ID_Procedimiento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Detalles_Proc)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ID_Paciente)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Tratamiento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.ID_PacienteNavigation).WithMany(p => p.Procedimientos)
                .HasForeignKey(d => d.ID_Paciente)
                .HasConstraintName("FK__Procedimi__ID_Pa__6754599E");

            entity.HasOne(d => d.ID_TratamientoNavigation).WithMany(p => p.Procedimientos)
                .HasForeignKey(d => d.ID_Tratamiento)
                .HasConstraintName("FK__Procedimi__ID_Tr__66603565");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.ID_Roles).HasName("PK__Roles__30F629932BE7D4D5");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("tr_AuditarActualizacionRoles");
                    tb.HasTrigger("tr_AuditarEliminacionRoles");
                    tb.HasTrigger("tr_AuditarInsercionRoles");
                });

            entity.Property(e => e.ID_Roles)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Roles_Permiso>(entity =>
        {
            entity.HasKey(e => e.ID_Roles_Permisos).HasName("PK__Roles_Pe__84F39C2974AD3C8E");

            entity.Property(e => e.ID_Roles_Permisos)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Permisos)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Roles)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.ID_PermisosNavigation).WithMany(p => p.Roles_Permisos)
                .HasForeignKey(d => d.ID_Permisos)
                .HasConstraintName("FK__Roles_Per__ID_Pe__787EE5A0");

            entity.HasOne(d => d.ID_RolesNavigation).WithMany(p => p.Roles_Permisos)
                .HasForeignKey(d => d.ID_Roles)
                .HasConstraintName("FK__Roles_Per__ID_Ro__778AC167");
        });

        modelBuilder.Entity<Tipo_Pago>(entity =>
        {
            entity.HasKey(e => e.ID_Tipo_Pago).HasName("PK__Tipo_Pag__4792A1BED7A94554");

            entity.ToTable("Tipo_Pago");

            entity.Property(e => e.ID_Tipo_Pago)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Descripcion_TP)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_TP)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tipo_Tratamiento>(entity =>
        {
            entity.HasKey(e => e.ID_TipoTratamiento).HasName("PK__Tipo_Tra__7616399282F3BAEF");

            entity.ToTable("Tipo_Tratamiento", tb =>
                {
                    tb.HasTrigger("tr_AuditarActualizacionTipoTratamiento");
                    tb.HasTrigger("tr_AuditarEliminacionTipoTratamiento");
                    tb.HasTrigger("tr_AuditarInsercionTipoTratamiento");
                });

            entity.Property(e => e.ID_TipoTratamiento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Descripcion_Tipo_Tratamiento)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_Tipo_Tratamiento)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tratamiento>(entity =>
        {
            entity.HasKey(e => e.ID_Tratamiento).HasName("PK__Tratamie__37F4ED1580BD18FB");

            entity.ToTable("Tratamiento", tb =>
                {
                    tb.HasTrigger("tr_AuditarActualizacionTratamiento");
                    tb.HasTrigger("tr_AuditarEliminacionTratamiento");
                    tb.HasTrigger("tr_AuditarInsercionTratamiento");
                });

            entity.Property(e => e.ID_Tratamiento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Descripcion_Tra)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ID_EstadoTratamiento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_TipoTratamiento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nombre_Tra)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.ID_EstadoTratamientoNavigation).WithMany(p => p.Tratamientos)
                .HasForeignKey(d => d.ID_EstadoTratamiento)
                .HasConstraintName("FK__Tratamien__ID_Es__440B1D61");

            entity.HasOne(d => d.ID_TipoTratamientoNavigation).WithMany(p => p.Tratamientos)
                .HasForeignKey(d => d.ID_TipoTratamiento)
                .HasConstraintName("FK__Tratamien__ID_Ti__4316F928");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.ID_Usuario).HasName("PK__Usuarios__DE4431C503015969");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("tr_AuditarActualizacionUsuarios");
                    tb.HasTrigger("tr_AuditarEliminacionUsuarios");
                    tb.HasTrigger("tr_AuditarInsercionUsuarios");
                });

            entity.Property(e => e.ID_Usuario)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Apellido1)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Contraseña)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ID_Funcionario)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Token)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.ID_FuncionarioNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.ID_Funcionario)
                .HasConstraintName("FK__Usuarios__ID_Fun__48CFD27E");
        });

        modelBuilder.Entity<Usuario_Role>(entity =>
        {
            entity.HasKey(e => e.ID_Usuario_Roles).HasName("PK__Usuario___24287850EAA6203E");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("tr_AuditarActualizacionUsuarioRoles");
                    tb.HasTrigger("tr_AuditarEliminacionUsuarioRoles");
                    tb.HasTrigger("tr_AuditarInsercionUsuarioRoles");
                });

            entity.Property(e => e.ID_Usuario_Roles)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Roles)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Usuario)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.ID_RolesNavigation).WithMany(p => p.Usuario_Roles)
                .HasForeignKey(d => d.ID_Roles)
                .HasConstraintName("FK__Usuario_R__ID_Ro__7C4F7684");

            entity.HasOne(d => d.ID_UsuarioNavigation).WithMany(p => p.Usuario_Roles)
                .HasForeignKey(d => d.ID_Usuario)
                .HasConstraintName("FK__Usuario_R__ID_Us__7B5B524B");
        });

        modelBuilder.Entity<vw_CitasPorEstado>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_CitasPorEstado");

            entity.Property(e => e.Apellido1_Den)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido1_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2_Den)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ID_Cita)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Motivo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_Den)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<vw_DentistasConMasTratamiento>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_DentistasConMasTratamientos");

            entity.Property(e => e.Apellido1_Den)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2_Den)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ID_Dentista)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nombre_Den)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<vw_FacturacionPorFecha>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_FacturacionPorFecha");

            entity.Property(e => e.Apellido1_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Estado_Pago)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ID_Factura)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MontoTotal_Fa).HasColumnType("money");
            entity.Property(e => e.Nombre_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<vw_FacturasPendiente>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_FacturasPendientes");

            entity.Property(e => e.Apellido1_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Estado_Pago)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ID_Factura)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MontoTotal_Fa).HasColumnType("money");
            entity.Property(e => e.Nombre_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<vw_HistorialPaciente>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_HistorialPaciente");

            entity.Property(e => e.Apellido1_Den)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido1_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2_Den)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Correo_Pac)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion_Tra)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Direccion_Pac)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ID_Cita)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Historial_Tratamiento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Paciente)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Motivo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_Den)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_Tra)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Telefono_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<vw_PacientesConTratamientosActivo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_PacientesConTratamientosActivos");

            entity.Property(e => e.Apellido1_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion_Tra)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ID_Paciente)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nombre_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_Tra)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<vw_ProximasCita>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ProximasCitas");

            entity.Property(e => e.Apellido1_Den)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido1_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2_Den)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ID_Cita)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Motivo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_Den)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<vw_ResumenFinancieroPaciente>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ResumenFinancieroPaciente");

            entity.Property(e => e.Apellido1_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ID_Paciente)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nombre_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SaldoPendiente).HasColumnType("money");
            entity.Property(e => e.TotalFacturado).HasColumnType("money");
            entity.Property(e => e.TotalPagado).HasColumnType("money");
        });

        modelBuilder.Entity<vw_TratamientosPorDentista>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_TratamientosPorDentista");

            entity.Property(e => e.Apellido1_Den)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido1_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2_Den)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion_Tra)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ID_Dentista)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Historial_Tratamiento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nombre_Den)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_Tra)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<vw_TratamientosPorPaciente>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_TratamientosPorPaciente");

            entity.Property(e => e.Apellido1_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ID_Paciente)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nombre_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
