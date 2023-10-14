using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Graduacion.Models;

public partial class Hallazgo
{
    public int IdHallazgo { get; set; }
    [Display(Name ="Tipo Hallazgo")]
    public int? IdTipoHallazgo { get; set; }

    public string? DescripcionHallazgo { get; set; }
    [Display(Name = "Auditoria")]
    public int? IdAuditoria { get; set; }

    public virtual Auditoria? IdAuditoriaNavigation { get; set; }

    public virtual TipoHallazgo? IdTipoHallazgoNavigation { get; set; }
}

public partial class hallazgos
{
    [Key]
    public int IdHallazgo { get; set; }

    public string? TipoHallazgo { get; set; }

    public string? DescripcionHallazgo { get; set; }

    public string? Auditoria { get; set; }
}
