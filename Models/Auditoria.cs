using System;
using System.Collections.Generic;

namespace Proyecto_Graduacion.Models;

public partial class Auditoria
{
    public int IdAuditoria { get; set; }

    public DateTime? FechaHora { get; set; }

    public string? Estado { get; set; }

    public int? IdInstitucion { get; set; }

    public int? IdUsuario { get; set; }

    public string? UsuarioMod { get; set; }

    public virtual ICollection<CausaEfecto> CausaEfectos { get; set; } = new List<CausaEfecto>();

    public virtual ICollection<DocumentosAuditorium> DocumentosAuditoria { get; set; } = new List<DocumentosAuditorium>();

    public virtual ICollection<EncuestaAuditorium> EncuestaAuditoria { get; set; } = new List<EncuestaAuditorium>();

    public virtual ICollection<EntrevistaAuditorium> EntrevistaAuditoria { get; set; } = new List<EntrevistaAuditorium>();

    public virtual ICollection<EvidenciaAuditorium> EvidenciaAuditoria { get; set; } = new List<EvidenciaAuditorium>();

    public virtual ICollection<Fodum> Foda { get; set; } = new List<Fodum>();

    public virtual ICollection<Hallazgo> Hallazgos { get; set; } = new List<Hallazgo>();

    public virtual Institucione? IdInstitucionNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Informe> Informes { get; set; } = new List<Informe>();

    public virtual ICollection<MateAuditorium> MateAuditoria { get; set; } = new List<MateAuditorium>();

    public virtual ICollection<ObjetivoAlcance> ObjetivoAlcances { get; set; } = new List<ObjetivoAlcance>();

    public virtual ICollection<PapelesAuditorium> PapelesAuditoria { get; set; } = new List<PapelesAuditorium>();

    public virtual ICollection<PlaneacionAudit> PlaneacionAudits { get; set; } = new List<PlaneacionAudit>();

    public virtual ICollection<PruebasAuditorium> PruebasAuditoria { get; set; } = new List<PruebasAuditorium>();

    public virtual ICollection<RiesgoAuditorium> RiesgoAuditoria { get; set; } = new List<RiesgoAuditorium>();

    public virtual ICollection<TecnicasAuditorium> TecnicasAuditoria { get; set; } = new List<TecnicasAuditorium>();
}
