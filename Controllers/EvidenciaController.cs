using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Graduacion.Models;
using Utilities_Net_6_MVC;

namespace Proyecto_Graduacion.Controllers
{
    public class EvidenciaController : Controller
    {
        private readonly IRepositoryAsync<Auditoria> _auditoriaRepository;
        private readonly IRepositoryAsync<Evidencia> _evidenciaRepository;
        private readonly IRepositoryAsync<EvidenciaAuditorium> _evidenciaAuditoriaRepository;
        private readonly IRepositoryAsync<TipoEvidencium> _tipoEvidenciaRepository;
        public EvidenciaController(IRepositoryAsync<Evidencia> evidenciaRepository,
            IRepositoryAsync<TipoEvidencium> tipoEvidenciaRepository,
            IRepositoryAsync<Auditoria> auditoriaRepository,
            IRepositoryAsync<EvidenciaAuditorium> evidenciaAuditoriaReporitory)
        {
            this._evidenciaRepository = evidenciaRepository;
            _tipoEvidenciaRepository = tipoEvidenciaRepository;
            _auditoriaRepository = auditoriaRepository;
            _evidenciaAuditoriaRepository = evidenciaAuditoriaReporitory;
        }
        // GET: EvidenciaController
        public async Task<ActionResult> Index()
        {
            var listado = (List<EvidenciaAuditorium>?)await _evidenciaAuditoriaRepository
                .GetAllWithIncludes(a => a.IdAuditoriaNavigation,
                i => i.IdAuditoriaNavigation.IdInstitucionNavigation,
                e => e.IdEvidenciaNavigation,
                tp => tp.IdEvidenciaNavigation.IdTipoEvidenciaNavigation);
            var listadoViewModel = listado.Select(x => new EvidenciaViewModel()
            {
                Auditoria = string.Concat(x.IdAuditoria, "-", x.IdAuditoriaNavigation.IdInstitucionNavigation.Nombre),
                Descripcion = x.IdEvidenciaNavigation.Descripcion,
                IdEvidencia = (int)x.IdEvidenciaAuditoria,
                TipoEvidencia = x.IdEvidenciaNavigation.IdTipoEvidenciaNavigation.Descripcion
            });
            return View(listadoViewModel);
        }

        // GET: EvidenciaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EvidenciaController/Create
        public async Task<ActionResult> Create()
        {
            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString()
                );
            List<TipoEvidencium> TipoEvidencia = (List<TipoEvidencium>)await _tipoEvidenciaRepository.GetAll();
            ViewData["TipoEvidencia"] = SelectListItemHelper.ToSelectListItems(TipoEvidencia,
                r => r.Descripcion,
                r => r.IdTipoEvidencia.ToString());
            return View();
        }

        // POST: EvidenciaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(evidencias model)
        {
            if (!ModelState.IsValid)
            {
                List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
                ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                    a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                    a => a.IdAuditoria.ToString()
                    );
                var tipos = (List<TipoEvidencium>)await _tipoEvidenciaRepository.GetAll();
                ViewData["TipoEvidencia"] = SelectListItemHelper.ToSelectListItems(tipos,
                    a => a.Descripcion,
                    a => a.IdTipoEvidencia.ToString()
                    );
                return View(model);
            }
            var oEvidencia = await _evidenciaRepository.Insert(new Evidencia()
            {
                IdTipoEvidencia = model.IdTipoEvidencia,
                Descripcion = model.Descripcion
            });
            if (oEvidencia == null)
            {
                return RedirectToAction("Error");
            }
            var oPruebaAuditoria = await _evidenciaAuditoriaRepository.Insert(new EvidenciaAuditorium()
            {
                IdAuditoria = model.IdAuditoria,
                IdEvidencia = oEvidencia.IdEvidencia
            });
            if (oPruebaAuditoria == null)
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: EvidenciaController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var oEvidenciaAuditoria = await _evidenciaAuditoriaRepository.GetByID(id);
            var Evidencia = await _evidenciaRepository.GetByID(oEvidenciaAuditoria.IdEvidencia);
            var Auditoria = await _auditoriaRepository.GetByID(oEvidenciaAuditoria.IdAuditoria);
            var TipoEvidencia = await _tipoEvidenciaRepository.GetByID(Evidencia.IdTipoEvidencia);

            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString(),
                Auditoria.IdAuditoria.ToString());
            var tipos = (List<TipoEvidencium>)await _tipoEvidenciaRepository.GetAll();
            ViewData["TipoEvidencia"] = SelectListItemHelper.ToSelectListItems(tipos,
                a => a.Descripcion,
                a => a.IdTipoEvidencia.ToString()
                );
            var EvidenciaViewModel = new evidenciaauditEdit()
            {
                idEvidenciaAuditoria = oEvidenciaAuditoria.IdEvidenciaAuditoria,
                IdAuditoria = Auditoria.IdAuditoria,
                Descripcion = Evidencia.Descripcion,
                IdTipoEvidencia = Evidencia.IdTipoEvidencia
            };
            return View(EvidenciaViewModel);
        }

        // POST: EvidenciaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(evidenciaauditEdit model)
        {
            if (!ModelState.IsValid)
            {
                var oEvidenciaAuditoria = await _evidenciaAuditoriaRepository.GetByID(model.idEvidenciaAuditoria);
                var Evidencia = await _evidenciaRepository.GetByID(oEvidenciaAuditoria.IdEvidencia);
                var Auditoria = await _auditoriaRepository.GetByID(oEvidenciaAuditoria.IdAuditoria);
                var TipoEvidencia = await _tipoEvidenciaRepository.GetByID(Evidencia.IdTipoEvidencia);
                List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
                ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                    a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                    a => a.IdAuditoria.ToString(),
                    Auditoria.IdAuditoria.ToString());
                var tipos = (List<TipoEvidencium>)await _tipoEvidenciaRepository.GetAll();
                ViewData["TipoEvidencia"] = SelectListItemHelper.ToSelectListItems(tipos,
                    a => a.Descripcion,
                    a => a.IdTipoEvidencia.ToString()
                    );
                return View(model);
            }
            try
            {
                var oEvidenciaAuditoria = await _evidenciaAuditoriaRepository.GetByID(model.idEvidenciaAuditoria);
                var Evidencia = await _evidenciaRepository.GetByID(oEvidenciaAuditoria.IdEvidencia);
                Evidencia.Descripcion = model.Descripcion;
                Evidencia.IdTipoEvidencia = model.IdTipoEvidencia;
                await _evidenciaRepository.Update(Evidencia);
                oEvidenciaAuditoria.IdAuditoria = model.IdAuditoria;
                await _evidenciaAuditoriaRepository.Update(oEvidenciaAuditoria);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EvidenciaController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var oEvidenciaAudit = await _evidenciaAuditoriaRepository.Delete(id);
            if (oEvidenciaAudit == null)
            { return RedirectToAction("Error"); }
            return RedirectToAction(nameof(Index));
        }

        // POST: EvidenciaController/Delete/5
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
