using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Utilities_Net_6_MVC;

namespace Proyecto_Graduacion.Models;

public partial class Pg20Context : DbContext,IAppDbContext
{
    public Pg20Context()
    {
    }

    public Pg20Context(DbContextOptions<Pg20Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Auditoria> Auditorias { get; set; }

    public virtual DbSet<CausaEfecto> CausaEfectos { get; set; }

    public virtual DbSet<DocumentosAnalizar> DocumentosAnalizars { get; set; }

    public virtual DbSet<DocumentosAuditorium> DocumentosAuditoria { get; set; }

    public virtual DbSet<Encuesta> Encuestas { get; set; }

    public virtual DbSet<EncuestaAuditorium> EncuestaAuditoria { get; set; }

    public virtual DbSet<Entrevista> Entrevistas { get; set; }

    public virtual DbSet<EntrevistaAuditorium> EntrevistaAuditoria { get; set; }

    public virtual DbSet<Evidencia> Evidencias { get; set; }

    public virtual DbSet<EvidenciaAuditorium> EvidenciaAuditoria { get; set; }

    public virtual DbSet<Fodum> Foda { get; set; }

    public virtual DbSet<Hallazgo> Hallazgos { get; set; }

    public virtual DbSet<Informe> Informes { get; set; }

    public virtual DbSet<Institucione> Instituciones { get; set; }

    public virtual DbSet<MateAuditorium> MateAuditoria { get; set; }

    public virtual DbSet<Materialidad> Materialidads { get; set; }

    public virtual DbSet<ObjetivoAlcance> ObjetivoAlcances { get; set; }

    public virtual DbSet<Papele> Papeles { get; set; }

    public virtual DbSet<PapelesAuditorium> PapelesAuditoria { get; set; }

    public virtual DbSet<PlaneacionAudit> PlaneacionAudits { get; set; }

    public virtual DbSet<Prueba> Pruebas { get; set; }

    public virtual DbSet<PruebasAuditorium> PruebasAuditoria { get; set; }

    public virtual DbSet<Riesgo> Riesgos { get; set; }

    public virtual DbSet<RiesgoAuditorium> RiesgoAuditoria { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TecnicasAuditorium> TecnicasAuditoria { get; set; }

    public virtual DbSet<TecnicasMuestreo> TecnicasMuestreos { get; set; }

    public virtual DbSet<TipoEvidencium> TipoEvidencia { get; set; }

    public virtual DbSet<TipoHallazgo> TipoHallazgos { get; set; }

    public virtual DbSet<TipoPrueba> TipoPruebas { get; set; }

    public virtual DbSet<TipoRiesgo> TipoRiesgos { get; set; }

    public virtual DbSet<TipoTecniasMuestreo> TipoTecniasMuestreos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auditoria>(entity =>
        {
            entity.HasKey(e => e.IdAuditoria);

            entity.Property(e => e.IdAuditoria).HasColumnName("idAuditoria");
            entity.Property(e => e.Estado)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaHora)
                .HasColumnType("date")
                .HasColumnName("fecha_hora");
            entity.Property(e => e.IdInstitucion).HasColumnName("idInstitucion");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.UsuarioMod)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("usuarioMod");

            entity.HasOne(d => d.IdInstitucionNavigation).WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.IdInstitucion)
                .HasConstraintName("FK_Auditorias_Institucion");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Auditorias_Usuario");
        });

        modelBuilder.Entity<CausaEfecto>(entity =>
        {
            entity.HasKey(e => e.IdCausaEfecto);

            entity.ToTable("CausaEfecto");

            entity.Property(e => e.IdCausaEfecto).HasColumnName("idCausaEfecto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdAuditoria).HasColumnName("idAuditoria");

            entity.HasOne(d => d.IdAuditoriaNavigation).WithMany(p => p.CausaEfectos)
                .HasForeignKey(d => d.IdAuditoria)
                .HasConstraintName("FK_CausaEfecto_Auditoria");
        });

        modelBuilder.Entity<DocumentosAnalizar>(entity =>
        {
            entity.HasKey(e => e.IdDocumentos);

            entity.ToTable("DocumentosAnalizar");

            entity.Property(e => e.IdDocumentos).HasColumnName("idDocumentos");
            entity.Property(e => e.NombreDocumento)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("nombreDocumento");
            entity.Property(e => e.Url)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("url");
        });

        modelBuilder.Entity<DocumentosAuditorium>(entity =>
        {
            entity.HasKey(e => e.IdDocsAuditoria);

            entity.Property(e => e.IdDocsAuditoria).HasColumnName("idDocsAuditoria");
            entity.Property(e => e.IdAuditoria).HasColumnName("idAuditoria");
            entity.Property(e => e.IdDocumentos).HasColumnName("idDocumentos");

            entity.HasOne(d => d.IdAuditoriaNavigation).WithMany(p => p.DocumentosAuditoria)
                .HasForeignKey(d => d.IdAuditoria)
                .HasConstraintName("FK_DocumentosAuditoria_Auditoria");

            entity.HasOne(d => d.IdDocumentosNavigation).WithMany(p => p.DocumentosAuditoria)
                .HasForeignKey(d => d.IdDocumentos)
                .HasConstraintName("FK_DocumentosAuditoria_Documentos");
        });

        modelBuilder.Entity<Encuesta>(entity =>
        {
            entity.HasKey(e => e.IdEncuesta);

            entity.Property(e => e.IdEncuesta).HasColumnName("idEncuesta");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<EncuestaAuditorium>(entity =>
        {
            entity.HasKey(e => e.IdEncuestaAuditoria);

            entity.Property(e => e.IdEncuestaAuditoria).HasColumnName("idEncuestaAuditoria");
            entity.Property(e => e.IdAuditoria).HasColumnName("idAuditoria");
            entity.Property(e => e.IdEncuesta).HasColumnName("idEncuesta");

            entity.HasOne(d => d.IdAuditoriaNavigation).WithMany(p => p.EncuestaAuditoria)
                .HasForeignKey(d => d.IdAuditoria)
                .HasConstraintName("FK_EncuestaAuditoria_Auditoria");

            entity.HasOne(d => d.IdEncuestaNavigation).WithMany(p => p.EncuestaAuditoria)
                .HasForeignKey(d => d.IdEncuesta)
                .HasConstraintName("FK_EncuestaAuditoria_Encuesta");
        });

        modelBuilder.Entity<Entrevista>(entity =>
        {
            entity.HasKey(e => e.IdEntrevista);

            entity.Property(e => e.IdEntrevista).HasColumnName("idEntrevista");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<EntrevistaAuditorium>(entity =>
        {
            entity.HasKey(e => e.IdEntrevistaAuditoria);

            entity.Property(e => e.IdEntrevistaAuditoria).HasColumnName("idEntrevistaAuditoria");
            entity.Property(e => e.IdAuditoria).HasColumnName("idAuditoria");
            entity.Property(e => e.IdEntrevista).HasColumnName("idEntrevista");

            entity.HasOne(d => d.IdAuditoriaNavigation).WithMany(p => p.EntrevistaAuditoria)
                .HasForeignKey(d => d.IdAuditoria)
                .HasConstraintName("FK_EntrevistaAuditoria_Auditoria");

            entity.HasOne(d => d.IdEntrevistaNavigation).WithMany(p => p.EntrevistaAuditoria)
                .HasForeignKey(d => d.IdEntrevista)
                .HasConstraintName("FK_EntrevistaAuditoria_Entrevista");
        });

        modelBuilder.Entity<Evidencia>(entity =>
        {
            entity.HasKey(e => e.IdEvidencia);

            entity.Property(e => e.IdEvidencia).HasColumnName("idEvidencia");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdTipoEvidencia).HasColumnName("idTipoEvidencia");

            entity.HasOne(d => d.IdTipoEvidenciaNavigation).WithMany(p => p.Evidencia)
                .HasForeignKey(d => d.IdTipoEvidencia)
                .HasConstraintName("FK_Evidencias_TipoEvidencias");
        });

        modelBuilder.Entity<EvidenciaAuditorium>(entity =>
        {
            entity.HasKey(e => e.IdEvidenciaAuditoria);

            entity.Property(e => e.IdEvidenciaAuditoria).HasColumnName("idEvidenciaAuditoria");
            entity.Property(e => e.IdAuditoria).HasColumnName("idAuditoria");
            entity.Property(e => e.IdEvidencia).HasColumnName("idEvidencia");

            entity.HasOne(d => d.IdAuditoriaNavigation).WithMany(p => p.EvidenciaAuditoria)
                .HasForeignKey(d => d.IdAuditoria)
                .HasConstraintName("FK_EvidenciaAuditoria_Auditoria");

            entity.HasOne(d => d.IdEvidenciaNavigation).WithMany(p => p.EvidenciaAuditoria)
                .HasForeignKey(d => d.IdEvidencia)
                .HasConstraintName("FK_EvidenciaAuditoria_Evidencia");
        });

        modelBuilder.Entity<Fodum>(entity =>
        {
            entity.HasKey(e => e.IdFoda);

            entity.Property(e => e.IdFoda).HasColumnName("idFoda");
            entity.Property(e => e.IdAuditoria).HasColumnName("idAuditoria");
            entity.Property(e => e.InfoAmenaza)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("infoAmenaza");
            entity.Property(e => e.InfoDebilidad)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("infoDebilidad");
            entity.Property(e => e.InfoFortaleza)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("infoFortaleza");
            entity.Property(e => e.InfoOportunidad)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("infoOportunidad");

            entity.HasOne(d => d.IdAuditoriaNavigation).WithMany(p => p.Foda)
                .HasForeignKey(d => d.IdAuditoria)
                .HasConstraintName("FK_Foda_Auditoria");
        });

        modelBuilder.Entity<Hallazgo>(entity =>
        {
            entity.HasKey(e => e.IdHallazgo);

            entity.Property(e => e.IdHallazgo).HasColumnName("idHallazgo");
            entity.Property(e => e.DescripcionHallazgo)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("descripcionHallazgo");
            entity.Property(e => e.IdAuditoria).HasColumnName("idAuditoria");
            entity.Property(e => e.IdTipoHallazgo).HasColumnName("idTipoHallazgo");

            entity.HasOne(d => d.IdAuditoriaNavigation).WithMany(p => p.Hallazgos)
                .HasForeignKey(d => d.IdAuditoria)
                .HasConstraintName("FK_Hallazgos_Auditoria");

            entity.HasOne(d => d.IdTipoHallazgoNavigation).WithMany(p => p.Hallazgos)
                .HasForeignKey(d => d.IdTipoHallazgo)
                .HasConstraintName("FK_Hallazgos_TipoHallazgos");
        });

        modelBuilder.Entity<Informe>(entity =>
        {
            entity.HasKey(e => e.IdInforme);

            entity.Property(e => e.IdInforme).HasColumnName("idInforme");
            entity.Property(e => e.DescripcionDocumento)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("descripcionDocumento");
            entity.Property(e => e.FechaHoraInforme)
                .HasColumnType("date")
                .HasColumnName("fecha_hora_informe");
            entity.Property(e => e.IdAuditoria).HasColumnName("idAuditoria");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

            entity.HasOne(d => d.IdAuditoriaNavigation).WithMany(p => p.Informes)
                .HasForeignKey(d => d.IdAuditoria)
                .HasConstraintName("FK_Informes_Auditoria");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Informes)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Informes_Usuario");
        });

        modelBuilder.Entity<Institucione>(entity =>
        {
            entity.HasKey(e => e.IdInstitucion);

            entity.Property(e => e.IdInstitucion).HasColumnName("idInstitucion");
            entity.Property(e => e.Direccion)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.FechaFundacion)
                .HasColumnType("date")
                .HasColumnName("fecha_fundacion");
            entity.Property(e => e.Nif)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("nif");
            entity.Property(e => e.Nombre)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<MateAuditorium>(entity =>
        {
            entity.HasKey(e => e.IdMateAuditoria);

            entity.Property(e => e.IdMateAuditoria).HasColumnName("idMateAuditoria");
            entity.Property(e => e.IdAuditoria).HasColumnName("idAuditoria");
            entity.Property(e => e.IdMaterialidad).HasColumnName("idMaterialidad");

            entity.HasOne(d => d.IdAuditoriaNavigation).WithMany(p => p.MateAuditoria)
                .HasForeignKey(d => d.IdAuditoria)
                .HasConstraintName("FK_MateAuditoria_Auditoria");

            entity.HasOne(d => d.IdMaterialidadNavigation).WithMany(p => p.MateAuditoria)
                .HasForeignKey(d => d.IdMaterialidad)
                .HasConstraintName("FK_MateAuditoria_Mate");
        });

        modelBuilder.Entity<Materialidad>(entity =>
        {
            entity.HasKey(e => e.IdMaterialidad);

            entity.ToTable("Materialidad");

            entity.Property(e => e.IdMaterialidad).HasColumnName("idMaterialidad");
            entity.Property(e => e.Monto)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("monto");
        });

        modelBuilder.Entity<ObjetivoAlcance>(entity =>
        {
            entity.HasKey(e => e.IdObjetivoAlcance);

            entity.ToTable("ObjetivoAlcance");

            entity.Property(e => e.IdObjetivoAlcance).HasColumnName("idObjetivoAlcance");
            entity.Property(e => e.Alcance)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("alcance");
            entity.Property(e => e.IdAuditoria).HasColumnName("idAuditoria");
            entity.Property(e => e.Objetivo)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("objetivo");

            entity.HasOne(d => d.IdAuditoriaNavigation).WithMany(p => p.ObjetivoAlcances)
                .HasForeignKey(d => d.IdAuditoria)
                .HasConstraintName("FK_ObjetivoAlcance_Auditoria");
        });

        modelBuilder.Entity<Papele>(entity =>
        {
            entity.HasKey(e => e.IdPapeles);

            entity.Property(e => e.IdPapeles).HasColumnName("idPapeles");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.RutaDoc)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("rutaDoc");
            entity.Property(e => e.TipoPapeles)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("tipoPapeles");
        });

        modelBuilder.Entity<PapelesAuditorium>(entity =>
        {
            entity.HasKey(e => e.IdPapelesAuditoria);

            entity.Property(e => e.IdPapelesAuditoria).HasColumnName("idPapelesAuditoria");
            entity.Property(e => e.IdAuditoria).HasColumnName("idAuditoria");
            entity.Property(e => e.IdPapeles).HasColumnName("idPapeles");

            entity.HasOne(d => d.IdAuditoriaNavigation).WithMany(p => p.PapelesAuditoria)
                .HasForeignKey(d => d.IdAuditoria)
                .HasConstraintName("FK_PapelesAuditoria_Auditoria");

            entity.HasOne(d => d.IdPapelesNavigation).WithMany(p => p.PapelesAuditoria)
                .HasForeignKey(d => d.IdPapeles)
                .HasConstraintName("FK_PapelesAuditoria_Papeles");
        });

        modelBuilder.Entity<PlaneacionAudit>(entity =>
        {
            entity.HasKey(e => e.IdPlaneacion);

            entity.ToTable("PlaneacionAudit");

            entity.Property(e => e.IdPlaneacion).HasColumnName("idPlaneacion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdAuditoria).HasColumnName("idAuditoria");

            entity.HasOne(d => d.IdAuditoriaNavigation).WithMany(p => p.PlaneacionAudits)
                .HasForeignKey(d => d.IdAuditoria)
                .HasConstraintName("FK_PlaneacionAudit_Auditoria");
        });

        modelBuilder.Entity<Prueba>(entity =>
        {
            entity.HasKey(e => e.IdPrueba);

            entity.Property(e => e.IdPrueba).HasColumnName("idPrueba");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdTipoPrueba).HasColumnName("idTipoPrueba");

            entity.HasOne(d => d.IdTipoPruebaNavigation).WithMany(p => p.Pruebas)
                .HasForeignKey(d => d.IdTipoPrueba)
                .HasConstraintName("FK_Pruebas_TipoPruebas");
        });

        modelBuilder.Entity<PruebasAuditorium>(entity =>
        {
            entity.HasKey(e => e.IdPruebaAuditoria);

            entity.Property(e => e.IdPruebaAuditoria).HasColumnName("idPruebaAuditoria");
            entity.Property(e => e.IdAuditoria).HasColumnName("idAuditoria");
            entity.Property(e => e.IdPrueba).HasColumnName("idPrueba");

            entity.HasOne(d => d.IdAuditoriaNavigation).WithMany(p => p.PruebasAuditoria)
                .HasForeignKey(d => d.IdAuditoria)
                .HasConstraintName("FK_PruebasAuditoria_Auditoria");

            entity.HasOne(d => d.IdPruebaNavigation).WithMany(p => p.PruebasAuditoria)
                .HasForeignKey(d => d.IdPrueba)
                .HasConstraintName("FK_PruebasAuditoria_Pruebas");
        });

        modelBuilder.Entity<Riesgo>(entity =>
        {
            entity.HasKey(e => e.IdRiesgo);

            entity.Property(e => e.IdRiesgo).HasColumnName("idRiesgo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdTipoRiesgo).HasColumnName("idTipoRiesgo");

            entity.HasOne(d => d.IdTipoRiesgoNavigation).WithMany(p => p.Riesgos)
                .HasForeignKey(d => d.IdTipoRiesgo)
                .HasConstraintName("FK_Riesgos_TipoRiesgos");
        });

        modelBuilder.Entity<RiesgoAuditorium>(entity =>
        {
            entity.HasKey(e => e.IdRiesgoAuditoria);

            entity.Property(e => e.IdRiesgoAuditoria).HasColumnName("idRiesgoAuditoria");
            entity.Property(e => e.IdAuditoria).HasColumnName("idAuditoria");
            entity.Property(e => e.IdRiesgo).HasColumnName("idRiesgo");

            entity.HasOne(d => d.IdAuditoriaNavigation).WithMany(p => p.RiesgoAuditoria)
                .HasForeignKey(d => d.IdAuditoria)
                .HasConstraintName("FK_RiesgoAuditoria_Auditoria");

            entity.HasOne(d => d.IdRiesgoNavigation).WithMany(p => p.RiesgoAuditoria)
                .HasForeignKey(d => d.IdRiesgo)
                .HasConstraintName("FK_RiesgoAuditoria_Riesgo");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<TecnicasAuditorium>(entity =>
        {
            entity.HasKey(e => e.IdTecnicasAuditoria);

            entity.Property(e => e.IdTecnicasAuditoria).HasColumnName("idTecnicasAuditoria");
            entity.Property(e => e.IdAuditoria).HasColumnName("idAuditoria");
            entity.Property(e => e.IdTecnicasMu).HasColumnName("idTecnicasMu");

            entity.HasOne(d => d.IdAuditoriaNavigation).WithMany(p => p.TecnicasAuditoria)
                .HasForeignKey(d => d.IdAuditoria)
                .HasConstraintName("FK_TecnicasAuditoria_Auditoria");

            entity.HasOne(d => d.IdTecnicasMuNavigation).WithMany(p => p.TecnicasAuditoria)
                .HasForeignKey(d => d.IdTecnicasMu)
                .HasConstraintName("FK_TecnicasAuditoria_TecnicasMu");
        });

        modelBuilder.Entity<TecnicasMuestreo>(entity =>
        {
            entity.HasKey(e => e.IdTecnicasMu);

            entity.ToTable("TecnicasMuestreo");

            entity.Property(e => e.IdTecnicasMu).HasColumnName("idTecnicasMu");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdTipoTecnicas).HasColumnName("idTipoTecnicas");

            entity.HasOne(d => d.IdTipoTecnicasNavigation).WithMany(p => p.TecnicasMuestreos)
                .HasForeignKey(d => d.IdTipoTecnicas)
                .HasConstraintName("FK_TecnicasMuestreo_TipoTecnicas");
        });

        modelBuilder.Entity<TipoEvidencium>(entity =>
        {
            entity.HasKey(e => e.IdTipoEvidencia);

            entity.Property(e => e.IdTipoEvidencia).HasColumnName("idTipoEvidencia");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<TipoHallazgo>(entity =>
        {
            entity.HasKey(e => e.IdTipoHallazgo);

            entity.ToTable("TipoHallazgo");

            entity.Property(e => e.IdTipoHallazgo).HasColumnName("idTipoHallazgo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<TipoPrueba>(entity =>
        {
            entity.HasKey(e => e.IdTipoPrueba);

            entity.Property(e => e.IdTipoPrueba).HasColumnName("idTipoPrueba");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<TipoRiesgo>(entity =>
        {
            entity.HasKey(e => e.IdTipoRiesgo);

            entity.ToTable("TipoRiesgo");

            entity.Property(e => e.IdTipoRiesgo).HasColumnName("idTipoRiesgo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<TipoTecniasMuestreo>(entity =>
        {
            entity.HasKey(e => e.IdTipoTecnicas);

            entity.ToTable("TipoTecniasMuestreo");

            entity.Property(e => e.IdTipoTecnicas).HasColumnName("idTipoTecnicas");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Apellido)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Cargo)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("cargo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK_Usuarios_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
