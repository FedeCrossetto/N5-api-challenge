using System;
using System.Collections.Generic;

namespace N5.Api.Models;

public partial class Permiso
{
    public int Id { get; set; }

    public string NombreEmpleado { get; set; } = null!;

    public string ApellidoEmpleado { get; set; } = null!;

    public int TipoPermiso { get; set; }

    public DateTime FechaPermiso { get; set; }

    public virtual TipoPermiso TipoPermisoNavigation { get; set; } = null!;
}
