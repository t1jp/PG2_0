using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class TecnicasAuditorium
{
    public int IdTecnicasAuditoria { get; set; }

    public int? IdTecnicasMu { get; set; }

    public int? IdAuditoria { get; set; }

    public virtual Auditoria? IdAuditoriaNavigation { get; set; }

    public virtual TecnicasMuestreo? IdTecnicasMuNavigation { get; set; }
}
