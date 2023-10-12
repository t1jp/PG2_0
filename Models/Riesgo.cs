using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class Riesgo
{
    public int IdRiesgo { get; set; }

    public int? IdTipoRiesgo { get; set; }

    public string? Descripcion { get; set; }

    public virtual TipoRiesgo? IdTipoRiesgoNavigation { get; set; }

    public virtual ICollection<RiesgoAuditorium> RiesgoAuditoria { get; set; } = new List<RiesgoAuditorium>();
}
