using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class EvidenciaAuditorium
{
    public int IdEvidenciaAuditoria { get; set; }

    public int? IdEvidencia { get; set; }

    public int? IdAuditoria { get; set; }

    public virtual Auditoria? IdAuditoriaNavigation { get; set; }

    public virtual Evidencia? IdEvidenciaNavigation { get; set; }
}
