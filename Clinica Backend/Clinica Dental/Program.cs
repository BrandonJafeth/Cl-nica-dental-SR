using Application.GenericService;
using Application.JD_Services;
using Application.Services;
using Domain.Interfaces;
using Domain.Interfaces.Generic;
using Domain.Interfaces.JD_Interfaces;
using Infraestructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddScoped(typeof(ISvGeneric<>), typeof(SvGeneric<>));





#region JD inyeccion de dependencias
builder.Services.AddScoped<ISvFuncionario, SvFuncionario>();
builder.Services.AddScoped<ISvHistorialMedico, SvHistorialMedico>();
builder.Services.AddScoped<ISvHistorialTratamiento, SvHistorialTratamiento>();
builder.Services.AddScoped<ISvPaciente, SvPaciente>();
builder.Services.AddScoped<ISvPago, SvPago>();
builder.Services.AddScoped<ISvPermiso, SvPermiso>();
builder.Services.AddScoped<ISvProcedimiento, SvProcedimiento>();
builder.Services.AddScoped<ISvRole, SvRole>();
builder.Services.AddScoped<ISvRolesPermiso, SvRolesPermiso>();
builder.Services.AddScoped<ISvTipoPago, SvTipoPago>();
builder.Services.AddScoped<ISvTipoTratamiento, SvTipoTratamiento>();
builder.Services.AddScoped<ISvTratamiento, SvTratamiento>();
builder.Services.AddScoped<ISvUsuario, SvUsuario>();
builder.Services.AddScoped<ISvUsuarioRoles, SvUsuarioRoles>();

#endregion


builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
