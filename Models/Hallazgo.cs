using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class Hallazgo
{
    public int IdHallazgo { get; set; }

    public int? IdTipoHallazgo { get; set; }

    public string? DescripcionHallazgo { get; set; }

    public int? IdAuditoria { get; set; }

    public virtual Auditoria? IdAuditoriaNavigation { get; set; }

    public virtual TipoHallazgo? IdTipoHallazgoNavigation { get; set; }
}
