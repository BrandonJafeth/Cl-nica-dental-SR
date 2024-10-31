using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class DB_User
{
    public string ID_DBUser { get; set; } = null!;

    public string? DBUserName { get; set; }

    public string? Contrasena { get; set; }

    public virtual ICollection<Auditorium> Auditoria { get; set; } = new List<Auditorium>();

    public virtual ICollection<Roles_DBUser> Roles_DBUsers { get; set; } = new List<Roles_DBUser>();
}
