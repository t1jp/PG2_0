using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class MateAuditorium
{
    public int IdMateAuditoria { get; set; }

    public int? IdMaterialidad { get; set; }

    public int? IdAuditoria { get; set; }

    public virtual Auditoria? IdAuditoriaNavigation { get; set; }

    public virtual Materialidad? IdMaterialidadNavigation { get; set; }
}
