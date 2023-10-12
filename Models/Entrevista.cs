using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class Entrevista
{
    public int IdEntrevista { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<EntrevistaAuditorium> EntrevistaAuditoria { get; set; } = new List<EntrevistaAuditorium>();
}
