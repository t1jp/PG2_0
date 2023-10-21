using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Graduacion.Models;

public partial class Materialidad
{
    public int IdMaterialidad { get; set; }

    public string? Monto { get; set; }

    public virtual ICollection<MateAuditorium> MateAuditoria { get; set; } = new List<MateAuditorium>();
}

public class materialidadaudit
{
    [DisplayName("Auditoria")]
    public int? IdAuditoria { get; set; }
    public string? Monto { get; set; }
}

public class MaterialidadViewModel
{
    [Key]
    public int IdMaterialidad { get; set; }
    public string? Monto { get; set; }
    public string Auditoria { get; set; }
}
public class materialidadauditEdit
{
    [Key]
    public int idMaterialidadAuditoria { get; set; }
    [DisplayName("Auditoria")]
    public int? IdAuditoria { get; set; }
    public string? Monto { get; set; }
}
