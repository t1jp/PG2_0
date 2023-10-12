using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class PruebasAuditorium
{
    public int IdPruebaAuditoria { get; set; }

    public int? IdPrueba { get; set; }

    public int? IdAuditoria { get; set; }

    public virtual Auditoria? IdAuditoriaNavigation { get; set; }

    public virtual Prueba? IdPruebaNavigation { get; set; }
}
