using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Graduacion.Models;

public partial class ObjetivoAlcance
{
    public int IdObjetivoAlcance { get; set; }

    public string? Objetivo { get; set; }

    public string? Alcance { get; set; }

    public int? IdAuditoria { get; set; }

    public virtual Auditoria? IdAuditoriaNavigation { get; set; }
}

public class objetivoalcanceaudit
{
    [Key]
    public int IdObjetivoAlcance { get; set; }
    [DisplayName("Auditoria")]
    public string? Auditoria { get; set; }
    public string? Objetivo { get; set; }

    public string? Alcance { get; set; }
}