using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class Encuesta
{
    public int IdEncuesta { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<EncuestaAuditorium> EncuestaAuditoria { get; set; } = new List<EncuestaAuditorium>();
}
