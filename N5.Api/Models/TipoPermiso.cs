﻿using System;
using System.Collections.Generic;

namespace N5.Api.Models;

public partial class TipoPermiso
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();
}
