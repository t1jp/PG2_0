using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class PapelesAuditorium
{
    public int IdPapelesAuditoria { get; set; }

    public int? IdPapeles { get; set; }

    public int? IdAuditoria { get; set; }

    public virtual Auditoria? IdAuditoriaNavigation { get; set; }

    public virtual Papele? IdPapelesNavigation { get; set; }
}
