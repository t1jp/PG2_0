using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class PlaneacionAudit
{
    public int IdPlaneacion { get; set; }

    public string? Descripcion { get; set; }

    public int? IdAuditoria { get; set; }

    public virtual Auditoria? IdAuditoriaNavigation { get; set; }
}
