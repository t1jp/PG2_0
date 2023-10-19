using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Graduacion.Models;

public partial class Evidencia
{
    public int IdEvidencia { get; set; }

    public int? IdTipoEvidencia { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<EvidenciaAuditorium> EvidenciaAuditoria { get; set; } = new List<EvidenciaAuditorium>();

    public virtual TipoEvidencium? IdTipoEvidenciaNavigation { get; set; }
}

public partial class evidencias
{
    [Key]
    public int IdEvidencia { get; set; }

    public int? IdTipoEvidencia { get; set; }

    public string? Descripcion { get; set; }

    public int? IdAuditoria { get; set; }
}

public class EvidenciaViewModel
{
    [Key]
    public int IdEvidencia { get; set; }
    public string? TipoEvidencia { get; set; }
    public string? Descripcion { get; set; }
    public string? Auditoria { get; set; }
}

public class evidenciaauditEdit
{
    [Key]
    public int idEvidenciaAuditoria { get; set; }
    [DisplayName("Auditoria")]
    public int? IdAuditoria { get; set; }
    public int? IdTipoEvidencia { get; set; }
    public string? Descripcion { get; set; }
}
