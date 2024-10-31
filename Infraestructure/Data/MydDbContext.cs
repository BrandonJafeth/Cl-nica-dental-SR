using System;
using System.Collections.Generic;
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

    public virtual DbSet<Auditorium> Auditoria { get; set; }

    public virtual DbSet<Citum> Cita { get; set; }

    public virtual DbSet<Cuentum> Cuenta { get; set; }

    public virtual DbSet<DB_User> DB_Users { get; set; }

    public virtual DbSet<Dentista_Especialidad> Dentista_Especialidads { get; set; }

    public virtual DbSet<Dentistum> Dentista { get; set; }

    public virtual DbSet<Especialidad> Especialidads { get; set; }

    public virtual DbSet<Estado_Cita> Estado_Citas { get; set; }

    public virtual DbSet<Estado_Cuentum> Estado_Cuenta { get; set; }

    public virtual DbSet<Estado_Pago> Estado_Pagos { get; set; }

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

    public virtual DbSet<Roles_DBUser> Roles_DBUsers { get; set; }

    public virtual DbSet<Roles_Permiso> Roles_Permisos { get; set; }

    public virtual DbSet<Tipo_Accion> Tipo_Accions { get; set; }

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

    public virtual DbSet<vw_TratamientosPorDentistum> vw_TratamientosPorDentista { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=BRANDONCA;Initial Catalog=ClinicaDental;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;Connect Timeout=60;Command Timeout=300;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auditorium>(entity =>
        {
            entity.HasKey(e => e.ID_Auditoria).HasName("PK__Auditori__2C0A46DB02337ED3");

            entity.Property(e => e.ID_Auditoria)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Descripcion_Accion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DispositivoQueRealizo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fecha_Hora_Accion).HasColumnType("datetime");
            entity.Property(e => e.ID_DBUser)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_TipoAccion)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Usuario)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.ID_DBUserNavigation).WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.ID_DBUser)
                .HasConstraintName("FK_Auditoria_DBUser");

            entity.HasOne(d => d.ID_TipoAccionNavigation).WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.ID_TipoAccion)
                .HasConstraintName("FK_Auditoria_TipoAccion");

            entity.HasOne(d => d.ID_UsuarioNavigation).WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.ID_Usuario)
                .HasConstraintName("FK_Auditoria_Usuario");
        });

        modelBuilder.Entity<Citum>(entity =>
        {
            entity.HasKey(e => e.ID_Cita).HasName("PK__Cita__7C17FD16756DE5E1");

            entity.ToTable(tb => tb.HasTrigger("tr_EvitarDuplicadosCita"));

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
                .HasConstraintName("FK__Cita__ID_Dentist__73BA3083");

            entity.HasOne(d => d.ID_EstadoCitaNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.ID_EstadoCita)
                .HasConstraintName("FK_Cita_EstadoCita");

            entity.HasOne(d => d.ID_FuncionarioNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.ID_Funcionario)
                .HasConstraintName("FK__Cita__ID_Funcion__74AE54BC");

            entity.HasOne(d => d.ID_PacienteNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.ID_Paciente)
                .HasConstraintName("FK__Cita__ID_Pacient__72C60C4A");
        });

        modelBuilder.Entity<Cuentum>(entity =>
        {
            entity.HasKey(e => e.ID_Cuenta).HasName("PK__Cuenta__820D611F853F4593");

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
                .HasConstraintName("FK__Cuenta__ID_Estad__5070F446");

            entity.HasOne(d => d.ID_FacturaNavigation).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.ID_Factura)
                .HasConstraintName("FK__Cuenta__ID_Factu__5165187F");

            entity.HasOne(d => d.ID_PacienteNavigation).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.ID_Paciente)
                .HasConstraintName("FK__Cuenta__ID_Pacie__52593CB8");
        });

        modelBuilder.Entity<DB_User>(entity =>
        {
            entity.HasKey(e => e.ID_DBUser).HasName("PK__DB_User__060442FAD3942EBC");

            entity.ToTable("DB_User");

            entity.Property(e => e.ID_DBUser)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Contrasena)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DBUserName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Dentista_Especialidad>(entity =>
        {
            entity.HasKey(e => e.ID_Dentista_Especialidad).HasName("PK__Dentista__5A0D823EED2410DE");

            entity.ToTable("Dentista_Especialidad");

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
                .HasConstraintName("FK__Dentista___ID_De__6EF57B66");

            entity.HasOne(d => d.ID_EspecialidadNavigation).WithMany(p => p.Dentista_Especialidads)
                .HasForeignKey(d => d.ID_Especialidad)
                .HasConstraintName("FK__Dentista___ID_Es__6FE99F9F");
        });

        modelBuilder.Entity<Dentistum>(entity =>
        {
            entity.HasKey(e => e.ID_Dentista).HasName("PK__Dentista__99A0225A43514010");

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
                .HasConstraintName("FK__Dentista__ID_Fun__6A30C649");
        });

        modelBuilder.Entity<Especialidad>(entity =>
        {
            entity.HasKey(e => e.ID_Especialidad).HasName("PK__Especial__5D7732D710E7AB55");

            entity.ToTable("Especialidad");

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
            entity.HasKey(e => e.ID_EstadoCita).HasName("PK__Estado_C__82FA13D9B886AD60");

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

        modelBuilder.Entity<Estado_Cuentum>(entity =>
        {
            entity.HasKey(e => e.ID_Estado_Cuenta).HasName("PK__Estado_C__127A61A5CE2FFA04");

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
            entity.HasKey(e => e.ID_EstadoPago).HasName("PK__Estado_P__3ECA89D528AB7D5A");

            entity.ToTable("Estado_Pago");

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

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.ID_Factura).HasName("PK__Factura__E9D586A848B9863C");

            entity.ToTable("Factura", tb => tb.HasTrigger("tr_EvitarFacturaConMontoCero"));

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
                .HasConstraintName("FK__Factura__ID_Esta__412EB0B6");
        });

        modelBuilder.Entity<Factura_Procedimiento>(entity =>
        {
            entity.HasKey(e => e.ID_Factura_Procedimiento).HasName("PK__Factura___3F2E92FB1D93B1E3");

            entity.ToTable("Factura_Procedimiento");

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
                .HasConstraintName("FK__Factura_P__ID_Fa__5535A963");

            entity.HasOne(d => d.ID_ProcedimientoNavigation).WithMany(p => p.Factura_Procedimientos)
                .HasForeignKey(d => d.ID_Procedimiento)
                .HasConstraintName("FK__Factura_P__ID_Pr__5629CD9C");
        });

        modelBuilder.Entity<Factura_Tratamiento>(entity =>
        {
            entity.HasKey(e => e.ID_Factura_Tratamiento).HasName("PK__Factura___800ACAE1208E7607");

            entity.ToTable("Factura_Tratamiento");

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
                .HasConstraintName("FK__Factura_T__ID_Fa__59063A47");

            entity.HasOne(d => d.ID_TratamientoNavigation).WithMany(p => p.Factura_Tratamientos)
                .HasForeignKey(d => d.ID_Tratamiento)
                .HasConstraintName("FK__Factura_T__ID_Tr__59FA5E80");
        });

        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.HasKey(e => e.ID_Funcionario).HasName("PK__Funciona__0AE977B95B35B77C");

            entity.ToTable("Funcionario");

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
            entity.HasKey(e => e.ID_HistorialMedico).HasName("PK__Historia__C9A11578097ED88A");

            entity.ToTable("Historial_Medico");

            entity.Property(e => e.ID_HistorialMedico)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Diagnostico)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Tratamientos_Medicos)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Historial_Tratamiento>(entity =>
        {
            entity.HasKey(e => e.ID_Historial_Tratamiento).HasName("PK__Historia__2611FDA2BDAE7E27");

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
                .HasConstraintName("FK__Historial__ID_Hi__7A672E12");

            entity.HasOne(d => d.ID_TratamientoNavigation).WithMany(p => p.Historial_Tratamientos)
                .HasForeignKey(d => d.ID_Tratamiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__ID_Tr__7B5B524B");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.ID_Paciente).HasName("PK__Paciente__5F365061F844DDCF");

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
            entity.Property(e => e.ID_HistorialMedico)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nombre_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Telefono_Pac)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.ID_HistorialMedicoNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.ID_HistorialMedico)
                .HasConstraintName("FK_Paciente_HistorialMedico");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.ID_Pago).HasName("PK__Pago__AE88B42941498393");

            entity.ToTable("Pago", tb =>
                {
                    tb.HasTrigger("tr_ActualizarEstadoFactura");
                    tb.HasTrigger("tr_EvitarPagosNegativos");
                });

            entity.Property(e => e.ID_Pago).ValueGeneratedNever();
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
                .HasConstraintName("FK__Pago__ID_Factura__45F365D3");

            entity.HasOne(d => d.ID_Tipo_PagoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.ID_Tipo_Pago)
                .HasConstraintName("FK__Pago__ID_Tipo_Pa__46E78A0C");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.ID_Permisos).HasName("PK__Permisos__4AC6BD0ABD69BDCD");

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
            entity.HasKey(e => e.ID_Procedimiento).HasName("PK__Procedim__5A9291917D208C40");

            entity.ToTable("Procedimiento");

            entity.Property(e => e.ID_Procedimiento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Detalles_Proc)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ID_Tratamiento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.ID_TratamientoNavigation).WithMany(p => p.Procedimientos)
                .HasForeignKey(d => d.ID_Tratamiento)
                .HasConstraintName("FK__Procedimi__ID_Tr__3E52440B");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.ID_Roles).HasName("PK__Roles__30F62993B0ED3947");

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

        modelBuilder.Entity<Roles_DBUser>(entity =>
        {
            entity.HasKey(e => e.ID_Roles_DBUser).HasName("PK__Roles_DB__1EED7FF95F04823E");

            entity.ToTable("Roles_DBUser");

            entity.Property(e => e.ID_Roles_DBUser)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_DBUser)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_Roles)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.ID_DBUserNavigation).WithMany(p => p.Roles_DBUsers)
                .HasForeignKey(d => d.ID_DBUser)
                .HasConstraintName("FK__Roles_DBU__ID_DB__02FC7413");

            entity.HasOne(d => d.ID_RolesNavigation).WithMany(p => p.Roles_DBUsers)
                .HasForeignKey(d => d.ID_Roles)
                .HasConstraintName("FK__Roles_DBU__ID_Ro__02084FDA");
        });

        modelBuilder.Entity<Roles_Permiso>(entity =>
        {
            entity.HasKey(e => e.ID_Roles_Permisos).HasName("PK__Roles_Pe__84F39C29DD0E1EC4");

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
                .HasConstraintName("FK__Roles_Per__ID_Pe__06CD04F7");

            entity.HasOne(d => d.ID_RolesNavigation).WithMany(p => p.Roles_Permisos)
                .HasForeignKey(d => d.ID_Roles)
                .HasConstraintName("FK__Roles_Per__ID_Ro__05D8E0BE");
        });

        modelBuilder.Entity<Tipo_Accion>(entity =>
        {
            entity.HasKey(e => e.ID_TipoAccion).HasName("PK__Tipo_Acc__9738184D6B9A0619");

            entity.ToTable("Tipo_Accion");

            entity.Property(e => e.ID_TipoAccion)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Descripcion_Tipo_Accion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_Accion)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tipo_Pago>(entity =>
        {
            entity.HasKey(e => e.ID_Tipo_Pago).HasName("PK__Tipo_Pag__4792A1BE59E50B5D");

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
            entity.HasKey(e => e.ID_TipoTratamiento).HasName("PK__Tipo_Tra__76163992FC5A5185");

            entity.ToTable("Tipo_Tratamiento");

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
            entity.HasKey(e => e.ID_Tratamiento).HasName("PK__Tratamie__37F4ED1557360D5F");

            entity.ToTable("Tratamiento");

            entity.Property(e => e.ID_Tratamiento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Descripcion_Tra)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ID_TipoTratamiento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nombre_Tra)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.ID_TipoTratamientoNavigation).WithMany(p => p.Tratamientos)
                .HasForeignKey(d => d.ID_TipoTratamiento)
                .HasConstraintName("FK__Tratamien__ID_Ti__3B75D760");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.ID_Usuario).HasName("PK__Usuarios__DE4431C5817812C7");

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
                .HasConstraintName("FK__Usuarios__ID_Fun__628FA481");
        });

        modelBuilder.Entity<Usuario_Role>(entity =>
        {
            entity.HasKey(e => e.ID_Usuario_Roles).HasName("PK__Usuario___242878505845573F");

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
                .HasConstraintName("FK__Usuario_R__ID_Ro__0A9D95DB");

            entity.HasOne(d => d.ID_UsuarioNavigation).WithMany(p => p.Usuario_Roles)
                .HasForeignKey(d => d.ID_Usuario)
                .HasConstraintName("FK__Usuario_R__ID_Us__09A971A2");
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

        modelBuilder.Entity<vw_TratamientosPorDentistum>(entity =>
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
