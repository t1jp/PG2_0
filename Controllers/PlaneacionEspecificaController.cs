using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Graduacion.Models;
using Utilities_Net_6_MVC;

namespace Proyecto_Graduacion.Controllers
{
    public class PlaneacionEspecificaController : Controller
    {
        private readonly IRepositoryAsync<Auditoria> _auditoriaRepository;
        private readonly IRepositoryAsync<PlaneacionAudit> _planeacionRepository;

        public PlaneacionEspecificaController(IRepositoryAsync<Auditoria> auditoriaRepository,
            IRepositoryAsync<PlaneacionAudit> planeacionRepository)
        {
            this._auditoriaRepository = auditoriaRepository;
            this._planeacionRepository = planeacionRepository;
        }
        // GET: PlaneacionEspecificaController
        public async Task<ActionResult> Index()
        {
            var PlaneacionEs = await _planeacionRepository.GetAllWithIncludes(a => a.IdAuditoriaNavigation, i => i.IdAuditoriaNavigation.IdInstitucionNavigation);
            var planeacion = PlaneacionEs.Select(x => new planeacionaudit()
            {
                IdPlaneacion = x.IdPlaneacion,
                Auditoria = string.Concat(x.IdAuditoria.ToString(), "-", x.IdAuditoriaNavigation.IdInstitucionNavigation.Nombre),
                Descripcion = x.Descripcion
            });
            return View(planeacion);
        }

        // GET: PlaneacionEspecificaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PlaneacionEspecificaController/Create
        public async Task<ActionResult> Create()
        {
            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString()
                );
            return View();
        }

        // POST: PlaneacionEspecificaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PlaneacionAudit planAudit)
        {
            var oPlaneacion = await _planeacionRepository.Insert(planAudit);
            if (oPlaneacion == null)
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: PlaneacionEspecificaController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var oPlaneacion = await _planeacionRepository.GetByID(id);
            var Auditoria = await _auditoriaRepository.GetByID(oPlaneacion.IdAuditoria);
            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString(),
                Auditoria.IdAuditoria.ToString());
            return View(oPlaneacion);
        }

        // POST: PlaneacionEspecificaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PlaneacionAudit model)
        {
            if (!ModelState.IsValid)
            {
                var oPlaneacion = await _planeacionRepository.GetByID(model.IdPlaneacion);
                var Auditoria = await _auditoriaRepository.GetByID(oPlaneacion.IdAuditoria);
                List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
                ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                    a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                    a => a.IdAuditoria.ToString(),
                    Auditoria.IdAuditoria.ToString());
                return View(model);
            }
            try
            {
                await _planeacionRepository.Update(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlaneacionEspecificaController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var oPlaneacion = await _planeacionRepository.Delete(id);
            if (oPlaneacion == null)
            { return RedirectToAction("Error"); }
            return RedirectToAction(nameof(Index));
        }

        // POST: PlaneacionEspecificaController/Delete/5
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
