using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class Informe
{
    public int IdInforme { get; set; }

    public int? IdAuditoria { get; set; }

    public DateTime? FechaHoraInforme { get; set; }

    public int? IdUsuario { get; set; }

    public string? DescripcionDocumento { get; set; }

    public virtual Auditoria? IdAuditoriaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
