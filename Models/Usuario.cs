using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int? IdRol { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Cargo { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<Auditoria> Auditoria { get; set; } = new List<Auditoria>();

    public virtual Role? IdRolNavigation { get; set; }

    public virtual ICollection<Informe> Informes { get; set; } = new List<Informe>();
}
