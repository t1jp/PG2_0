using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class Fodum
{
    public int IdFoda { get; set; }

    public string? InfoFortaleza { get; set; }

    public string? InfoOportunidad { get; set; }

    public string? InfoDebilidad { get; set; }

    public string? InfoAmenaza { get; set; }

    public int? IdAuditoria { get; set; }

    public virtual Auditoria? IdAuditoriaNavigation { get; set; }
}
