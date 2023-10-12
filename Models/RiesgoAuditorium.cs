using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class RiesgoAuditorium
{
    public int IdRiesgoAuditoria { get; set; }

    public int? IdRiesgo { get; set; }

    public int? IdAuditoria { get; set; }

    public virtual Auditoria? IdAuditoriaNavigation { get; set; }

    public virtual Riesgo? IdRiesgoNavigation { get; set; }
}
