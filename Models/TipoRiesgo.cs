using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class TipoRiesgo
{
    public int IdTipoRiesgo { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Riesgo> Riesgos { get; set; } = new List<Riesgo>();
}
