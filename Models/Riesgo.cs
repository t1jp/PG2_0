using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Graduacion.Models;

public partial class Riesgo
{
    public int IdRiesgo { get; set; }

    public int? IdTipoRiesgo { get; set; }

    public string? Descripcion { get; set; }

    public virtual TipoRiesgo? IdTipoRiesgoNavigation { get; set; }

    public virtual ICollection<RiesgoAuditorium> RiesgoAuditoria { get; set; } = new List<RiesgoAuditorium>();
}

public partial class riesgos
{
    [Key]
    public int IdRiesgo { get; set; }

    public int? IdTipoRiesgo { get; set; }

    public string? Descripcion { get; set; }

    public int? IdAuditoria { get; set; }
}

public class RiesgoViewModel
{
    [Key]
    public int IdRiesgo { get; set; }
    public string? TipoRiesgo { get; set; }
    public string? Descripcion { get; set; }
    public string? Auditoria { get; set; }
}

public class riesgoauditEdit
{
    [Key]
    public int idRiesgoAuditoria { get; set; }
    [DisplayName("Auditoria")]
    public int? IdAuditoria { get; set; }
    public int? IdTipoRiesgo { get; set; }
    public string? Descripcion { get; set; }
}