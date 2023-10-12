using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class TipoPrueba
{
    public int IdTipoPrueba { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Prueba> Pruebas { get; set; } = new List<Prueba>();
}
