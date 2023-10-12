using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class DocumentosAuditorium
{
    public int IdDocsAuditoria { get; set; }

    public int? IdDocumentos { get; set; }

    public int? IdAuditoria { get; set; }

    public virtual Auditoria? IdAuditoriaNavigation { get; set; }

    public virtual DocumentosAnalizar? IdDocumentosNavigation { get; set; }
}
