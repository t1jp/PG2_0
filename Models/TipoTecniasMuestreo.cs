using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class TipoTecniasMuestreo
{
    public int IdTipoTecnicas { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<TecnicasMuestreo> TecnicasMuestreos { get; set; } = new List<TecnicasMuestreo>();
}
