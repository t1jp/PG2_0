using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Graduacion.Models;

public partial class Entrevista
{
    public int IdEntrevista { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<EntrevistaAuditorium> EntrevistaAuditoria { get; set; } = new List<EntrevistaAuditorium>();
}

public class entrevistaaudit
{
    [DisplayName("Auditoria")]
    public int? IdAuditoria { get; set; }
    public string? Descripcion { get; set; }
}

public class EntrevistaViewModel
{
    [Key]
    public int IdEntrevista { get; set; }
    public string? Descripcion { get; set; }
    public string Auditoria { get; set; }
}
public class entrevistaauditEdit
{
    [Key]
    public int idEntrevistaAuditoria { get; set; }
    [DisplayName("Auditoria")]
    public int? IdAuditoria { get; set; }
    public string? Descripcion { get; set; }
}