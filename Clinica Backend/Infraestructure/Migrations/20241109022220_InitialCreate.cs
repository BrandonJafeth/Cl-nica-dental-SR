using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auditoria",
                columns: table => new
                {
                    ID_Auditoria = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Fecha_Hora_Accion = table.Column<DateTime>(type: "datetime", nullable: false),
                    Accion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    DispositivoQueRealizo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Usuario = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Auditori__2C0A46DBE6581F43", x => x.ID_Auditoria);
                });

            migrationBuilder.CreateTable(
                name: "Especialidad",
                columns: table => new
                {
                    ID_Especialidad = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    Nombre_Esp = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Descripcion_Esp = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Especial__5D7732D7854F8876", x => x.ID_Especialidad);
                });

            migrationBuilder.CreateTable(
                name: "Estado_Citas",
                columns: table => new
                {
                    ID_EstadoCita = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    Nombre_Estado = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Descripcion_Estado = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Estado_C__82FA13D96A36A238", x => x.ID_EstadoCita);
                });

            migrationBuilder.CreateTable(
                name: "Estado_Cuenta",
                columns: table => new
                {
                    ID_Estado_Cuenta = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    Nombre_EC = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Descripcion_EC = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Estado_C__127A61A58239AFAF", x => x.ID_Estado_Cuenta);
                });

            migrationBuilder.CreateTable(
                name: "Estado_Pago",
                columns: table => new
                {
                    ID_EstadoPago = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    Nombre_EP = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Descripcion_EP = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Estado_P__3ECA89D5AC5C9CC3", x => x.ID_EstadoPago);
                });

            migrationBuilder.CreateTable(
                name: "Estado_Tratamiento",
                columns: table => new
                {
                    ID_EstadoTratamiento = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    Nombre_Estado = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Descripcion_Estado = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Estado_T__4372CBE5EE9BDA01", x => x.ID_EstadoTratamiento);
                });

            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    ID_Funcionario = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    Nombre = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Apellido1 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Apellido2 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Contraseña = table.Column<string>(type: "char(12)", unicode: false, fixedLength: true, maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Funciona__0AE977B95F5546F8", x => x.ID_Funcionario);
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    ID_Paciente = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    Nombre_Pac = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Apellido1_Pac = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Apellido2_Pac = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Fecha_Nacimiento_Pac = table.Column<DateOnly>(type: "date", nullable: true),
                    Telefono_Pac = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Correo_Pac = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Direccion_Pac = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Paciente__5F365061CEFD25E1", x => x.ID_Paciente);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    ID_Permisos = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    Nombre = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Descripcion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Permisos__4AC6BD0AEA998186", x => x.ID_Permisos);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID_Roles = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    Nombre = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Descripcion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__30F629932BE7D4D5", x => x.ID_Roles);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Pago",
                columns: table => new
                {
                    ID_Tipo_Pago = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    Nombre_TP = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Descripcion_TP = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tipo_Pag__4792A1BED7A94554", x => x.ID_Tipo_Pago);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Tratamiento",
                columns: table => new
                {
                    ID_TipoTratamiento = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    Nombre_Tipo_Tratamiento = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Descripcion_Tipo_Tratamiento = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tipo_Tra__7616399282F3BAEF", x => x.ID_TipoTratamiento);
                });

            migrationBuilder.CreateTable(
                name: "Factura",
                columns: table => new
                {
                    ID_Factura = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    MontoTotal_Fa = table.Column<decimal>(type: "money", nullable: true),
                    FechaEmision_Fa = table.Column<DateOnly>(type: "date", nullable: true),
                    ID_EstadoPago = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Factura__E9D586A865003965", x => x.ID_Factura);
                    table.ForeignKey(
                        name: "FK__Factura__ID_Esta__59FA5E80",
                        column: x => x.ID_EstadoPago,
                        principalTable: "Estado_Pago",
                        principalColumn: "ID_EstadoPago");
                });

            migrationBuilder.CreateTable(
                name: "Dentista",
                columns: table => new
                {
                    ID_Dentista = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    Nombre_Den = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Apellido1_Den = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Apellido2_Den = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Direccion_Den = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    FechaNacimiento_Den = table.Column<DateOnly>(type: "date", nullable: true),
                    Telefono_Den = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Correo_Den = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    ID_Funcionario = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Dentista__99A0225AB60F7A5C", x => x.ID_Dentista);
                    table.ForeignKey(
                        name: "FK__Dentista__ID_Fun__4BAC3F29",
                        column: x => x.ID_Funcionario,
                        principalTable: "Funcionario",
                        principalColumn: "ID_Funcionario");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID_Usuario = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    Nombre = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Apellido1 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Apellido2 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Contraseña = table.Column<string>(type: "char(12)", unicode: false, fixedLength: true, maxLength: 12, nullable: true),
                    Token = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ID_Funcionario = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuarios__DE4431C503015969", x => x.ID_Usuario);
                    table.ForeignKey(
                        name: "FK__Usuarios__ID_Fun__48CFD27E",
                        column: x => x.ID_Funcionario,
                        principalTable: "Funcionario",
                        principalColumn: "ID_Funcionario");
                });

            migrationBuilder.CreateTable(
                name: "Historial_Medico",
                columns: table => new
                {
                    ID_HistorialMedico = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    Fecha_Historial = table.Column<DateOnly>(type: "date", nullable: true),
                    Diagnostico = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ID_Paciente = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Historia__C9A115780EFAF8C4", x => x.ID_HistorialMedico);
                    table.ForeignKey(
                        name: "FK_HistorialMedico_Paciente",
                        column: x => x.ID_Paciente,
                        principalTable: "Paciente",
                        principalColumn: "ID_Paciente");
                });

            migrationBuilder.CreateTable(
                name: "Roles_Permisos",
                columns: table => new
                {
                    ID_Roles_Permisos = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    ID_Roles = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true),
                    ID_Permisos = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles_Pe__84F39C2974AD3C8E", x => x.ID_Roles_Permisos);
                    table.ForeignKey(
                        name: "FK__Roles_Per__ID_Pe__787EE5A0",
                        column: x => x.ID_Permisos,
                        principalTable: "Permisos",
                        principalColumn: "ID_Permisos");
                    table.ForeignKey(
                        name: "FK__Roles_Per__ID_Ro__778AC167",
                        column: x => x.ID_Roles,
                        principalTable: "Roles",
                        principalColumn: "ID_Roles");
                });

            migrationBuilder.CreateTable(
                name: "Tratamiento",
                columns: table => new
                {
                    ID_Tratamiento = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    Nombre_Tra = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Descripcion_Tra = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    ID_TipoTratamiento = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true),
                    ID_EstadoTratamiento = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tratamie__37F4ED1580BD18FB", x => x.ID_Tratamiento);
                    table.ForeignKey(
                        name: "FK__Tratamien__ID_Es__440B1D61",
                        column: x => x.ID_EstadoTratamiento,
                        principalTable: "Estado_Tratamiento",
                        principalColumn: "ID_EstadoTratamiento");
                    table.ForeignKey(
                        name: "FK__Tratamien__ID_Ti__4316F928",
                        column: x => x.ID_TipoTratamiento,
                        principalTable: "Tipo_Tratamiento",
                        principalColumn: "ID_TipoTratamiento");
                });

            migrationBuilder.CreateTable(
                name: "Cuenta",
                columns: table => new
                {
                    ID_Cuenta = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    Saldo_Total = table.Column<decimal>(type: "money", nullable: true),
                    Fecha_Apertura = table.Column<DateOnly>(type: "date", nullable: true),
                    Fecha_Cierre = table.Column<DateOnly>(type: "date", nullable: true),
                    Fecha_Ultima_Actualizacion = table.Column<DateOnly>(type: "date", nullable: true),
                    Observaciones = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ID_Estado_Cuenta = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true),
                    ID_Factura = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true),
                    ID_Paciente = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cuenta__820D611FD52E8A69", x => x.ID_Cuenta);
                    table.ForeignKey(
                        name: "FK__Cuenta__ID_Estad__619B8048",
                        column: x => x.ID_Estado_Cuenta,
                        principalTable: "Estado_Cuenta",
                        principalColumn: "ID_Estado_Cuenta");
                    table.ForeignKey(
                        name: "FK__Cuenta__ID_Factu__628FA481",
                        column: x => x.ID_Factura,
                        principalTable: "Factura",
                        principalColumn: "ID_Factura");
                    table.ForeignKey(
                        name: "FK__Cuenta__ID_Pacie__6383C8BA",
                        column: x => x.ID_Paciente,
                        principalTable: "Paciente",
                        principalColumn: "ID_Paciente");
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    ID_Pago = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Monto_Pago = table.Column<decimal>(type: "money", nullable: true),
                    Fecha_Pago = table.Column<DateOnly>(type: "date", nullable: true),
                    ID_Factura = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true),
                    ID_Tipo_Pago = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pago__AE88B4290CBA209A", x => x.ID_Pago);
                    table.ForeignKey(
                        name: "FK__Pago__ID_Factura__5DCAEF64",
                        column: x => x.ID_Factura,
                        principalTable: "Factura",
                        principalColumn: "ID_Factura");
                    table.ForeignKey(
                        name: "FK__Pago__ID_Tipo_Pa__5EBF139D",
                        column: x => x.ID_Tipo_Pago,
                        principalTable: "Tipo_Pago",
                        principalColumn: "ID_Tipo_Pago");
                });

            migrationBuilder.CreateTable(
                name: "Cita",
                columns: table => new
                {
                    ID_Cita = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    Fecha_Cita = table.Column<DateOnly>(type: "date", nullable: true),
                    Motivo = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Hora_Inicio = table.Column<TimeOnly>(type: "time", nullable: true),
                    Hora_Fin = table.Column<TimeOnly>(type: "time", nullable: true),
                    ID_Paciente = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true),
                    ID_Dentista = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true),
                    ID_Funcionario = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true),
                    ID_EstadoCita = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cita__7C17FD160E32939C", x => x.ID_Cita);
                    table.ForeignKey(
                        name: "FK__Cita__ID_Dentist__6B24EA82",
                        column: x => x.ID_Dentista,
                        principalTable: "Dentista",
                        principalColumn: "ID_Dentista");
                    table.ForeignKey(
                        name: "FK__Cita__ID_EstadoC__6D0D32F4",
                        column: x => x.ID_EstadoCita,
                        principalTable: "Estado_Citas",
                        principalColumn: "ID_EstadoCita");
                    table.ForeignKey(
                        name: "FK__Cita__ID_Funcion__6C190EBB",
                        column: x => x.ID_Funcionario,
                        principalTable: "Funcionario",
                        principalColumn: "ID_Funcionario");
                    table.ForeignKey(
                        name: "FK__Cita__ID_Pacient__6A30C649",
                        column: x => x.ID_Paciente,
                        principalTable: "Paciente",
                        principalColumn: "ID_Paciente");
                });

            migrationBuilder.CreateTable(
                name: "Dentista_Especialidad",
                columns: table => new
                {
                    ID_Dentista_Especialidad = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    ID_Dentista = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true),
                    ID_Especialidad = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Dentista__5A0D823E1000AB2E", x => x.ID_Dentista_Especialidad);
                    table.ForeignKey(
                        name: "FK__Dentista___ID_De__5070F446",
                        column: x => x.ID_Dentista,
                        principalTable: "Dentista",
                        principalColumn: "ID_Dentista");
                    table.ForeignKey(
                        name: "FK__Dentista___ID_Es__5165187F",
                        column: x => x.ID_Especialidad,
                        principalTable: "Especialidad",
                        principalColumn: "ID_Especialidad");
                });

            migrationBuilder.CreateTable(
                name: "Usuario_Roles",
                columns: table => new
                {
                    ID_Usuario_Roles = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    ID_Usuario = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true),
                    ID_Roles = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario___24287850EAA6203E", x => x.ID_Usuario_Roles);
                    table.ForeignKey(
                        name: "FK__Usuario_R__ID_Ro__7C4F7684",
                        column: x => x.ID_Roles,
                        principalTable: "Roles",
                        principalColumn: "ID_Roles");
                    table.ForeignKey(
                        name: "FK__Usuario_R__ID_Us__7B5B524B",
                        column: x => x.ID_Usuario,
                        principalTable: "Usuarios",
                        principalColumn: "ID_Usuario");
                });

            migrationBuilder.CreateTable(
                name: "Factura_Tratamiento",
                columns: table => new
                {
                    ID_Factura_Tratamiento = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    ID_Factura = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true),
                    ID_Tratamiento = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Factura___800ACAE12CEA5D81", x => x.ID_Factura_Tratamiento);
                    table.ForeignKey(
                        name: "FK__Factura_T__ID_Fa__02FC7413",
                        column: x => x.ID_Factura,
                        principalTable: "Factura",
                        principalColumn: "ID_Factura");
                    table.ForeignKey(
                        name: "FK__Factura_T__ID_Tr__03F0984C",
                        column: x => x.ID_Tratamiento,
                        principalTable: "Tratamiento",
                        principalColumn: "ID_Tratamiento");
                });

            migrationBuilder.CreateTable(
                name: "Historial_Tratamiento",
                columns: table => new
                {
                    ID_Historial_Tratamiento = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    ID_HistorialMedico = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    ID_Tratamiento = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    Fecha_Tratamiento = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Historia__2611FDA2CC7604E3", x => x.ID_Historial_Tratamiento);
                    table.ForeignKey(
                        name: "FK__Historial__ID_Hi__6FE99F9F",
                        column: x => x.ID_HistorialMedico,
                        principalTable: "Historial_Medico",
                        principalColumn: "ID_HistorialMedico");
                    table.ForeignKey(
                        name: "FK__Historial__ID_Tr__70DDC3D8",
                        column: x => x.ID_Tratamiento,
                        principalTable: "Tratamiento",
                        principalColumn: "ID_Tratamiento");
                });

            migrationBuilder.CreateTable(
                name: "Procedimiento",
                columns: table => new
                {
                    ID_Procedimiento = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    Fecha_Proc = table.Column<DateOnly>(type: "date", nullable: true),
                    Detalles_Proc = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Hora_Inicio_Proc = table.Column<TimeOnly>(type: "time", nullable: true),
                    Hora_Fin_Proc = table.Column<TimeOnly>(type: "time", nullable: true),
                    ID_Tratamiento = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true),
                    ID_Paciente = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Procedim__5A929191BCC5DF04", x => x.ID_Procedimiento);
                    table.ForeignKey(
                        name: "FK__Procedimi__ID_Pa__6754599E",
                        column: x => x.ID_Paciente,
                        principalTable: "Paciente",
                        principalColumn: "ID_Paciente");
                    table.ForeignKey(
                        name: "FK__Procedimi__ID_Tr__66603565",
                        column: x => x.ID_Tratamiento,
                        principalTable: "Tratamiento",
                        principalColumn: "ID_Tratamiento");
                });

            migrationBuilder.CreateTable(
                name: "Factura_Procedimiento",
                columns: table => new
                {
                    ID_Factura_Procedimiento = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    ID_Factura = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true),
                    ID_Procedimiento = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Factura___3F2E92FB0601FDE3", x => x.ID_Factura_Procedimiento);
                    table.ForeignKey(
                        name: "FK__Factura_P__ID_Fa__7F2BE32F",
                        column: x => x.ID_Factura,
                        principalTable: "Factura",
                        principalColumn: "ID_Factura");
                    table.ForeignKey(
                        name: "FK__Factura_P__ID_Pr__00200768",
                        column: x => x.ID_Procedimiento,
                        principalTable: "Procedimiento",
                        principalColumn: "ID_Procedimiento");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cita_ID_Dentista",
                table: "Cita",
                column: "ID_Dentista");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_ID_EstadoCita",
                table: "Cita",
                column: "ID_EstadoCita");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_ID_Funcionario",
                table: "Cita",
                column: "ID_Funcionario");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_ID_Paciente",
                table: "Cita",
                column: "ID_Paciente");

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_ID_Estado_Cuenta",
                table: "Cuenta",
                column: "ID_Estado_Cuenta");

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_ID_Factura",
                table: "Cuenta",
                column: "ID_Factura");

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_ID_Paciente",
                table: "Cuenta",
                column: "ID_Paciente");

            migrationBuilder.CreateIndex(
                name: "IX_Dentista_ID_Funcionario",
                table: "Dentista",
                column: "ID_Funcionario");

            migrationBuilder.CreateIndex(
                name: "IX_Dentista_Especialidad_ID_Dentista",
                table: "Dentista_Especialidad",
                column: "ID_Dentista");

            migrationBuilder.CreateIndex(
                name: "IX_Dentista_Especialidad_ID_Especialidad",
                table: "Dentista_Especialidad",
                column: "ID_Especialidad");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_ID_EstadoPago",
                table: "Factura",
                column: "ID_EstadoPago");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_Procedimiento_ID_Factura",
                table: "Factura_Procedimiento",
                column: "ID_Factura");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_Procedimiento_ID_Procedimiento",
                table: "Factura_Procedimiento",
                column: "ID_Procedimiento");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_Tratamiento_ID_Factura",
                table: "Factura_Tratamiento",
                column: "ID_Factura");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_Tratamiento_ID_Tratamiento",
                table: "Factura_Tratamiento",
                column: "ID_Tratamiento");

            migrationBuilder.CreateIndex(
                name: "UQ__Historia__5F3650604ECC0C4C",
                table: "Historial_Medico",
                column: "ID_Paciente",
                unique: true,
                filter: "[ID_Paciente] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Historial_Tratamiento_ID_HistorialMedico",
                table: "Historial_Tratamiento",
                column: "ID_HistorialMedico");

            migrationBuilder.CreateIndex(
                name: "IX_Historial_Tratamiento_ID_Tratamiento",
                table: "Historial_Tratamiento",
                column: "ID_Tratamiento");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_ID_Factura",
                table: "Pago",
                column: "ID_Factura");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_ID_Tipo_Pago",
                table: "Pago",
                column: "ID_Tipo_Pago");

            migrationBuilder.CreateIndex(
                name: "IX_Procedimiento_ID_Paciente",
                table: "Procedimiento",
                column: "ID_Paciente");

            migrationBuilder.CreateIndex(
                name: "IX_Procedimiento_ID_Tratamiento",
                table: "Procedimiento",
                column: "ID_Tratamiento");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Permisos_ID_Permisos",
                table: "Roles_Permisos",
                column: "ID_Permisos");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Permisos_ID_Roles",
                table: "Roles_Permisos",
                column: "ID_Roles");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamiento_ID_EstadoTratamiento",
                table: "Tratamiento",
                column: "ID_EstadoTratamiento");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamiento_ID_TipoTratamiento",
                table: "Tratamiento",
                column: "ID_TipoTratamiento");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Roles_ID_Roles",
                table: "Usuario_Roles",
                column: "ID_Roles");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Roles_ID_Usuario",
                table: "Usuario_Roles",
                column: "ID_Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ID_Funcionario",
                table: "Usuarios",
                column: "ID_Funcionario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditoria");

            migrationBuilder.DropTable(
                name: "Cita");

            migrationBuilder.DropTable(
                name: "Cuenta");

            migrationBuilder.DropTable(
                name: "Dentista_Especialidad");

            migrationBuilder.DropTable(
                name: "Factura_Procedimiento");

            migrationBuilder.DropTable(
                name: "Factura_Tratamiento");

            migrationBuilder.DropTable(
                name: "Historial_Tratamiento");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "Roles_Permisos");

            migrationBuilder.DropTable(
                name: "Usuario_Roles");

            migrationBuilder.DropTable(
                name: "Estado_Citas");

            migrationBuilder.DropTable(
                name: "Estado_Cuenta");

            migrationBuilder.DropTable(
                name: "Dentista");

            migrationBuilder.DropTable(
                name: "Especialidad");

            migrationBuilder.DropTable(
                name: "Procedimiento");

            migrationBuilder.DropTable(
                name: "Historial_Medico");

            migrationBuilder.DropTable(
                name: "Factura");

            migrationBuilder.DropTable(
                name: "Tipo_Pago");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Tratamiento");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "Estado_Pago");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "Estado_Tratamiento");

            migrationBuilder.DropTable(
                name: "Tipo_Tratamiento");
        }
    }
}
