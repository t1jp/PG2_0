using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Graduacion.Models;
using Utilities_Net_6_MVC;

namespace Proyecto_Graduacion.Controllers
{
    public class TecnicasMuController : Controller
    {
        private readonly IRepositoryAsync<Auditoria> _auditoriaRepository;
        private readonly IRepositoryAsync<TecnicasMuestreo> _tecnicasMuRepository;
        private readonly IRepositoryAsync<TecnicasAuditorium> _tecnicasAuditoriaRepository;
        private readonly IRepositoryAsync<TipoTecniasMuestreo> _tipoTecnicasRepository;
        public TecnicasMuController(IRepositoryAsync<TecnicasMuestreo> tecnicasMuRepository,
            IRepositoryAsync<TipoTecniasMuestreo> tipoTenicasRepository,
            IRepositoryAsync<Auditoria> auditoriaRepository,
            IRepositoryAsync<TecnicasAuditorium> tecnicasAuditoriaReporitory)
        {
            this._tecnicasMuRepository = tecnicasMuRepository;
            _tipoTecnicasRepository = tipoTenicasRepository;
            _auditoriaRepository = auditoriaRepository;
            _tecnicasAuditoriaRepository = tecnicasAuditoriaReporitory;
        }
        // GET: TecnicasMuController
        public async Task<ActionResult> Index()
        {
            var listado = (List<TecnicasAuditorium>?)await _tecnicasAuditoriaRepository
                .GetAllWithIncludes(a => a.IdAuditoriaNavigation,
                i => i.IdAuditoriaNavigation.IdInstitucionNavigation,
                e => e.IdTecnicasMuNavigation,
                tp => tp.IdTecnicasMuNavigation.IdTipoTecnicasNavigation);
            var listadoViewModel = listado.Select(x => new TecnicaViewModel()
            {
                Auditoria = string.Concat(x.IdAuditoria, "-", x.IdAuditoriaNavigation.IdInstitucionNavigation.Nombre),
                Descripcion = x.IdTecnicasMuNavigation.Descripcion,
                IdTecnicasMu = (int)x.IdTecnicasAuditoria,
                TipoTecnica = x.IdTecnicasMuNavigation.IdTipoTecnicasNavigation.Descripcion
            });
            return View(listadoViewModel);
        }

        // GET: TecnicasMuController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TecnicasMuController/Create
        public async Task<ActionResult> Create()
        {
            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString()
                );
            List<TipoTecniasMuestreo> TipoTecnica = (List<TipoTecniasMuestreo>)await _tipoTecnicasRepository.GetAll();
            ViewData["TipoTecnica"] = SelectListItemHelper.ToSelectListItems(TipoTecnica,
                r => r.Descripcion,
                r => r.IdTipoTecnicas.ToString());
            return View();
        }

        // POST: TecnicasMuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(tecnicas model)
        {
            if (!ModelState.IsValid)
            {
                List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
                ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                    a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                    a => a.IdAuditoria.ToString()
                    );
                var tipos = (List<TipoTecniasMuestreo>)await _tipoTecnicasRepository.GetAll();
                ViewData["TipoTenica"] = SelectListItemHelper.ToSelectListItems(tipos,
                    a => a.Descripcion,
                    a => a.IdTipoTecnicas.ToString()
                    );
                return View(model);
            }
            var oTecnica = await _tecnicasMuRepository.Insert(new TecnicasMuestreo()
            {
                IdTipoTecnicas = model.IdTipoTecnicas,
                Descripcion = model.Descripcion
            });
            if (oTecnica == null)
            {
                return RedirectToAction("Error");
            }
            var oTecnicaAuditoria = await _tecnicasAuditoriaRepository.Insert(new TecnicasAuditorium()
            {
                IdAuditoria = model.IdAuditoria,
                IdTecnicasMu = oTecnica.IdTecnicasMu
            });
            if (oTecnicaAuditoria == null)
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: TecnicasMuController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var oTenicaAuditoria = await _tecnicasAuditoriaRepository.GetByID(id);
            var Tecnica = await _tecnicasMuRepository.GetByID(oTenicaAuditoria.IdTecnicasMu);
            var Auditoria = await _auditoriaRepository.GetByID(oTenicaAuditoria.IdAuditoria);
            var TipoTecnica = await _tipoTecnicasRepository.GetByID(Tecnica.IdTipoTecnicas);

            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString(),
                Auditoria.IdAuditoria.ToString());
            var tipos = (List<TipoTecniasMuestreo>)await _tipoTecnicasRepository.GetAll();
            ViewData["TipoTecnica"] = SelectListItemHelper.ToSelectListItems(tipos,
                a => a.Descripcion,
                a => a.IdTipoTecnicas.ToString()
                );
            var tecnicaViewModel = new tecnicaauditEdit()
            {
                idTecnicaAuditoria = oTenicaAuditoria.IdTecnicasAuditoria,
                IdAuditoria = Auditoria.IdAuditoria,
                Descripcion = Tecnica.Descripcion,
                IdTipoTecnicas = Tecnica.IdTipoTecnicas
            };
            return View(tecnicaViewModel);
        }

        // POST: TecnicasMuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(tecnicaauditEdit model)
        {
            if (!ModelState.IsValid)
            {
                var oTenicaAuditoria = await _tecnicasAuditoriaRepository.GetByID(model.idTecnicaAuditoria);
                var Tecnica = await _tecnicasMuRepository.GetByID(oTenicaAuditoria.IdTecnicasMu);
                var Auditoria = await _auditoriaRepository.GetByID(oTenicaAuditoria.IdAuditoria);
                var TipoTecnica = await _tipoTecnicasRepository.GetByID(Tecnica.IdTipoTecnicas);
                List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
                ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                    a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                    a => a.IdAuditoria.ToString(),
                    Auditoria.IdAuditoria.ToString());
                var tipos = (List<TipoTecniasMuestreo>)await _tipoTecnicasRepository.GetAll();
                ViewData["TipoTenica"] = SelectListItemHelper.ToSelectListItems(tipos,
                    a => a.Descripcion,
                    a => a.IdTipoTecnicas.ToString()
                    );
                return View(model);
            }
            try
            {
                var oTecnicaAuditoria = await _tecnicasAuditoriaRepository.GetByID(model.idTecnicaAuditoria);
                var Tecnica = await _tecnicasMuRepository.GetByID(oTecnicaAuditoria.IdTecnicasMu);
                Tecnica.Descripcion = model.Descripcion;
                Tecnica.IdTipoTecnicas = model.IdTipoTecnicas;
                await _tecnicasMuRepository.Update(Tecnica);
                oTecnicaAuditoria.IdAuditoria = model.IdAuditoria;
                await _tecnicasAuditoriaRepository.Update(oTecnicaAuditoria);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TecnicasMuController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var oTecnicaAudit = await _tecnicasAuditoriaRepository.Delete(id);
            if (oTecnicaAudit == null)
            { return RedirectToAction("Error"); }
            return RedirectToAction(nameof(Index));
        }

        // POST: TecnicasMuController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
