using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Graduacion.Models;

public partial class TecnicasMuestreo
{
    public int IdTecnicasMu { get; set; }

    public int? IdTipoTecnicas { get; set; }

    public string? Descripcion { get; set; }

    public virtual TipoTecniasMuestreo? IdTipoTecnicasNavigation { get; set; }

    public virtual ICollection<TecnicasAuditorium> TecnicasAuditoria { get; set; } = new List<TecnicasAuditorium>();
}

public partial class tecnicas
{
    [Key]
    public int IdTecnicasMu { get; set; }

    public int? IdTipoTecnicas { get; set; }

    public string? Descripcion { get; set; }

    public int? IdAuditoria { get; set; }
}

public class TecnicaViewModel
{
    [Key]
    public int IdTecnicasMu { get; set; }
    public string? TipoTecnica { get; set; }
    public string? Descripcion { get; set; }
    public string? Auditoria { get; set; }
}

public class tecnicaauditEdit
{
    [Key]
    public int idTecnicaAuditoria { get; set; }
    [DisplayName("Auditoria")]
    public int? IdAuditoria { get; set; }
    public int? IdTipoTecnicas { get; set; }
    public string? Descripcion { get; set; }
}
