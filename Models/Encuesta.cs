using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Graduacion.Models;

public partial class Encuesta
{
    public int IdEncuesta { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<EncuestaAuditorium> EncuestaAuditoria { get; set; } = new List<EncuestaAuditorium>();
}

public class encuestaaudit
{
    [DisplayName("Auditoria")]
    public int? IdAuditoria { get; set; }
    public string? Descripcion { get; set; }
}

public class EncuestaViewModel
{
    [Key]
    public int IdEncuesta { get; set; }
    public string? Descripcion { get; set; }
    public string Auditoria { get; set; }
}

public class encuestaauditEdit
{
    [Key]
    public int IdEncuestaAuditoria { get; set; }
    [DisplayName("Auditoria")]
    public int? IdAuditoria { get; set; }
    public string? Descripcion { get; set; }
}


