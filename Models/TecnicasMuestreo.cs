using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class TecnicasMuestreo
{
    public int IdTecnicasMu { get; set; }

    public int? IdTipoTecnicas { get; set; }

    public string? Descripcion { get; set; }

    public virtual TipoTecniasMuestreo? IdTipoTecnicasNavigation { get; set; }

    public virtual ICollection<TecnicasAuditorium> TecnicasAuditoria { get; set; } = new List<TecnicasAuditorium>();
}
