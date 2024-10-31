using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Roles_DBUser
{
    public string ID_Roles_DBUser { get; set; } = null!;

    public string? ID_Roles { get; set; }

    public string? ID_DBUser { get; set; }

    public virtual DB_User? ID_DBUserNavigation { get; set; }

    public virtual Role? ID_RolesNavigation { get; set; }
}
