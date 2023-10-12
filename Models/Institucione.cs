using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class Institucione
{
    public int IdInstitucion { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? Nif { get; set; }

    public DateTime? FechaFundacion { get; set; }

    public virtual ICollection<Auditoria> Auditoria { get; set; } = new List<Auditoria>();
}
