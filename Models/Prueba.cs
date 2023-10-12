using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class Prueba
{
    public int IdPrueba { get; set; }

    public int? IdTipoPrueba { get; set; }

    public string? Descripcion { get; set; }

    public virtual TipoPrueba? IdTipoPruebaNavigation { get; set; }

    public virtual ICollection<PruebasAuditorium> PruebasAuditoria { get; set; } = new List<PruebasAuditorium>();
}
