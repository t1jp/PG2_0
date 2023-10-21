using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Graduacion.Models;
using Utilities_Net_6_MVC;

namespace Proyecto_Graduacion.Controllers
{
    public class RiesgoController : Controller
    {
        private readonly IRepositoryAsync<Auditoria> _auditoriaRepository;
        private readonly IRepositoryAsync<Riesgo> _riesgoRepository;
        private readonly IRepositoryAsync<RiesgoAuditorium> _riesgoAuditoriaRepository;
        private readonly IRepositoryAsync<TipoRiesgo> _tipoRiesgoRepository;
        public RiesgoController(IRepositoryAsync<Riesgo> riesgoRepository,
            IRepositoryAsync<TipoRiesgo> tipoRiesgoRepository,
            IRepositoryAsync<Auditoria> auditoriaRepository,
            IRepositoryAsync<RiesgoAuditorium> riesgoAuditoriaReporitory)
        {
            this._riesgoRepository = riesgoRepository;
            _tipoRiesgoRepository = tipoRiesgoRepository;
            _auditoriaRepository = auditoriaRepository;
            _riesgoAuditoriaRepository = riesgoAuditoriaReporitory;
        }
        // GET: RiesgoController
        public async Task<ActionResult> Index()
        {
            var listado = (List<RiesgoAuditorium>?)await _riesgoAuditoriaRepository
                .GetAllWithIncludes(a => a.IdAuditoriaNavigation,
                i => i.IdAuditoriaNavigation.IdInstitucionNavigation,
                e => e.IdRiesgoNavigation,
                tp => tp.IdRiesgoNavigation.IdTipoRiesgoNavigation);
            var listadoViewModel = listado.Select(x => new RiesgoViewModel()
            {
                Auditoria = string.Concat(x.IdAuditoria, "-", x.IdAuditoriaNavigation.IdInstitucionNavigation.Nombre),
                Descripcion = x.IdRiesgoNavigation.Descripcion,
                IdRiesgo = (int)x.IdRiesgoAuditoria,
                TipoRiesgo = x.IdRiesgoNavigation.IdTipoRiesgoNavigation.Descripcion
            });
            return View(listadoViewModel);
        }

        // GET: RiesgoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RiesgoController/Create
        public async Task<ActionResult> Create()
        {
            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString()
                );
            List<TipoRiesgo> TipoRiesgo = (List<TipoRiesgo>)await _tipoRiesgoRepository.GetAll();
            ViewData["TipoRiesgo"] = SelectListItemHelper.ToSelectListItems(TipoRiesgo,
                r => r.Descripcion,
                r => r.IdTipoRiesgo.ToString());
            return View();
        }

        // POST: RiesgoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(riesgos model)
        {
            if (!ModelState.IsValid)
            {
                List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
                ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                    a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                    a => a.IdAuditoria.ToString()
                    );
                var tipos = (List<TipoRiesgo>)await _tipoRiesgoRepository.GetAll();
                ViewData["TipoRiesgo"] = SelectListItemHelper.ToSelectListItems(tipos,
                    a => a.Descripcion,
                    a => a.IdTipoRiesgo.ToString()
                    );
                return View(model);
            }
            var oRiesgo = await _riesgoRepository.Insert(new Riesgo()
            {
                IdTipoRiesgo = model.IdTipoRiesgo,
                Descripcion = model.Descripcion
            });
            if (oRiesgo == null)
            {
                return RedirectToAction("Error");
            }
            var oRiesgoAuditoria = await _riesgoAuditoriaRepository.Insert(new RiesgoAuditorium()
            {
                IdAuditoria = model.IdAuditoria,
                IdRiesgo = oRiesgo.IdRiesgo
            });
            if (oRiesgoAuditoria == null)
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: RiesgoController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var oRiesgoAuditoria = await _riesgoAuditoriaRepository.GetByID(id);
            var Riesgo = await _riesgoRepository.GetByID(oRiesgoAuditoria.IdRiesgo);
            var Auditoria = await _auditoriaRepository.GetByID(oRiesgoAuditoria.IdAuditoria);
            var TipoRiesgo = await _tipoRiesgoRepository.GetByID(Riesgo.IdTipoRiesgo);

            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString(),
                Auditoria.IdAuditoria.ToString());
            var tipos = (List<TipoRiesgo>)await _tipoRiesgoRepository.GetAll();
            ViewData["TipoRiesgo"] = SelectListItemHelper.ToSelectListItems(tipos,
                a => a.Descripcion,
                a => a.IdTipoRiesgo.ToString()
                );
            var riesgoViewModel = new riesgoauditEdit()
            {
                idRiesgoAuditoria = oRiesgoAuditoria.IdRiesgoAuditoria,
                IdAuditoria = Auditoria.IdAuditoria,
                Descripcion = Riesgo.Descripcion,
                IdTipoRiesgo = Riesgo.IdTipoRiesgo
            };
            return View(riesgoViewModel);
        }

        // POST: RiesgoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(riesgoauditEdit model)
        {
            if (!ModelState.IsValid)
            {
                var oRiesgoAuditoria = await _riesgoAuditoriaRepository.GetByID(model.idRiesgoAuditoria);
                var Riesgo = await _riesgoRepository.GetByID(oRiesgoAuditoria.IdRiesgo);
                var Auditoria = await _auditoriaRepository.GetByID(oRiesgoAuditoria.IdAuditoria);
                var TipoRiesgo = await _tipoRiesgoRepository.GetByID(Riesgo.IdTipoRiesgo);
                List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
                ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                    a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                    a => a.IdAuditoria.ToString(),
                    Auditoria.IdAuditoria.ToString());
                var tipos = (List<TipoRiesgo>)await _tipoRiesgoRepository.GetAll();
                ViewData["TipoRiesgo"] = SelectListItemHelper.ToSelectListItems(tipos,
                    a => a.Descripcion,
                    a => a.IdTipoRiesgo.ToString()
                    );
                return View(model);
            }
            try
            {
                var oRiesgoAuditoria = await _riesgoAuditoriaRepository.GetByID(model.idRiesgoAuditoria);
                var Riesgo = await _riesgoRepository.GetByID(oRiesgoAuditoria.IdRiesgo);
                Riesgo.Descripcion = model.Descripcion;
                Riesgo.IdTipoRiesgo = model.IdTipoRiesgo;
                await _riesgoRepository.Update(Riesgo);
                oRiesgoAuditoria.IdAuditoria = model.IdAuditoria;
                await _riesgoAuditoriaRepository.Update(oRiesgoAuditoria);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RiesgoController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var oRiesgoAudit = await _riesgoAuditoriaRepository.Delete(id);
            if (oRiesgoAudit == null)
            { return RedirectToAction("Error"); }
            return RedirectToAction(nameof(Index));
        }

        // POST: RiesgoController/Delete/5
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
