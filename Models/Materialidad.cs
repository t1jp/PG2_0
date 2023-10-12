using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class Materialidad
{
    public int IdMaterialidad { get; set; }

    public string? Monto { get; set; }

    public virtual ICollection<MateAuditorium> MateAuditoria { get; set; } = new List<MateAuditorium>();
}
