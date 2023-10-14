using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Graduacion.Models;

public partial class Fodum
{
    public int IdFoda { get; set; }

    public string? InfoFortaleza { get; set; }

    public string? InfoOportunidad { get; set; }

    public string? InfoDebilidad { get; set; }

    public string? InfoAmenaza { get; set; }
    [DisplayName("Auditoria")]
    public int? IdAuditoria { get; set; }

    public virtual Auditoria? IdAuditoriaNavigation { get; set; }
}

public class fodaaudit
{
    [Key]
    public int IdFoda { get; set; }
    [DisplayName("Auditoria")]
    public string? Auditoria { get; set; }
    public string? InfoFortaleza { get; set; }

    public string? InfoOportunidad { get; set; }

    public string? InfoDebilidad { get; set; }

    public string? InfoAmenaza { get; set; }
}
