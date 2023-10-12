using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class Evidencia
{
    public int IdEvidencia { get; set; }

    public int? IdTipoEvidencia { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<EvidenciaAuditorium> EvidenciaAuditoria { get; set; } = new List<EvidenciaAuditorium>();

    public virtual TipoEvidencium? IdTipoEvidenciaNavigation { get; set; }
}
