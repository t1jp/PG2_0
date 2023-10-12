using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class EncuestaAuditorium
{
    public int IdEncuestaAuditoria { get; set; }

    public int? IdEncuesta { get; set; }

    public int? IdAuditoria { get; set; }

    public virtual Auditoria? IdAuditoriaNavigation { get; set; }

    public virtual Encuesta? IdEncuestaNavigation { get; set; }
}
