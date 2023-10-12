using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class TipoHallazgo
{
    public int IdTipoHallazgo { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Hallazgo> Hallazgos { get; set; } = new List<Hallazgo>();
}
