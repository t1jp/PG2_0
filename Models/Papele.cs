using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class Papele
{
    public int IdPapeles { get; set; }

    public string? TipoPapeles { get; set; }

    public string? Descripcion { get; set; }

    public string? RutaDoc { get; set; }

    public virtual ICollection<PapelesAuditorium> PapelesAuditoria { get; set; } = new List<PapelesAuditorium>();
}

public class papeles
{
    public int? idPapeles { get; set; }
    public string? tipoPapeles { get; set; }
    public string? descripcion { get; set; }
    public string? rutaDoc { get; set; }
    public IFormFile? File { get; set; }
}
