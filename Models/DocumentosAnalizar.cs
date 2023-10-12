using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class DocumentosAnalizar
{
    public int IdDocumentos { get; set; }

    public string? Url { get; set; }

    public string? NombreDocumento { get; set; }

    public virtual ICollection<DocumentosAuditorium> DocumentosAuditoria { get; set; } = new List<DocumentosAuditorium>();
}
