using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class EntrevistaAuditorium
{
    public int IdEntrevistaAuditoria { get; set; }

    public int? IdEntrevista { get; set; }

    public int? IdAuditoria { get; set; }

    public virtual Auditoria? IdAuditoriaNavigation { get; set; }

    public virtual Entrevista? IdEntrevistaNavigation { get; set; }
}
