using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class TipoEvidencium
{
    public int IdTipoEvidencia { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Evidencia> Evidencia { get; set; } = new List<Evidencia>();
}
