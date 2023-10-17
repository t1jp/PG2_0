using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Graduacion.Models;

public partial class Prueba
{
    public int IdPrueba { get; set; }
    public int? IdTipoPrueba { get; set; }

    public string? Descripcion { get; set; }
    [Display(Name = "Auditoria")]
    public virtual TipoPrueba? IdTipoPruebaNavigation { get; set; }

    public virtual ICollection<PruebasAuditorium> PruebasAuditoria { get; set; } = new List<PruebasAuditorium>();
}

public partial class pruebas
{
    [Key]
    public int IdPrueba { get; set; }

    public int? IdTipoPrueba { get; set; }

    public string? Descripcion { get; set; }

    public int? IdAuditoria { get; set; }
}

public class PruebaViewModel
{
    [Key]
    public int IdPrueba { get; set; }
    public string? TipoPrueba { get; set; }
    public string? Descripcion { get; set; }
    public string? Auditoria { get; set; }
}

public class pruebaauditEdit
{
    [Key]
    public int idPruebaAuditoria { get; set; }
    [DisplayName("Auditoria")]
    public int? IdAuditoria { get; set; }
    public int? IdTipoPrueba { get; set; }
    public string? Descripcion { get; set; }
}