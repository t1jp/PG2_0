using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Graduacion.Models;

public partial class CausaEfecto
{
    public int IdCausaEfecto { get; set; }

    public string? Descripcion { get; set; }

    public int? IdAuditoria { get; set; }

    public virtual Auditoria? IdAuditoriaNavigation { get; set; }
}

public class causaefectoaudit
{
    [Key]
    public int IdCausaEfecto { get; set; }
    [DisplayName("Auditoria")]
    public string? Auditoria { get; set; }
    public string? Descripcion { get; set; }
}