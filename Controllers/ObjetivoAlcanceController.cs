using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Graduacion.Models;
using Utilities_Net_6_MVC;

namespace Proyecto_Graduacion.Controllers
{
    public class ObjetivoAlcanceController : Controller
    {
        private readonly IRepositoryAsync<Auditoria> _auditoriaRepository;
        private readonly IRepositoryAsync<ObjetivoAlcance> _objetivoAlcanceRepository;

        public ObjetivoAlcanceController(IRepositoryAsync<Auditoria> auditoriaRepository,
            IRepositoryAsync<ObjetivoAlcance> objetivoAlcanceRepository)
        {
            this._auditoriaRepository = auditoriaRepository;
            this._objetivoAlcanceRepository = objetivoAlcanceRepository;
        }
        // GET: ObjetivoAlcanceController
        public async Task<ActionResult> Index()
        {
            var ObjetivoAlcance = await _objetivoAlcanceRepository.GetAllWithIncludes(a => a.IdAuditoriaNavigation, i => i.IdAuditoriaNavigation.IdInstitucionNavigation);
            var objetivoalcance = ObjetivoAlcance.Select(x => new objetivoalcanceaudit()
            {
                IdObjetivoAlcance = x.IdObjetivoAlcance,
                Auditoria = string.Concat(x.IdAuditoria.ToString(), "-", x.IdAuditoriaNavigation.IdInstitucionNavigation.Nombre),
                Alcance = x.Alcance,
                Objetivo = x.Objetivo
            });
            return View(objetivoalcance);
        }

        // GET: ObjetivoAlcanceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ObjetivoAlcanceController/Create
        public async Task<ActionResult> Create()
        {
            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString()
                );
            return View();
        }

        // POST: ObjetivoAlcanceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ObjetivoAlcance objetivoalcance)
        {
            var oObjetivoAlcance = await _objetivoAlcanceRepository.Insert(objetivoalcance);
            if (oObjetivoAlcance == null)
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ObjetivoAlcanceController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var oObjetivoAlcance = await _objetivoAlcanceRepository.GetByID(id);
            var Auditoria = await _auditoriaRepository.GetByID(oObjetivoAlcance.IdAuditoria);
            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString(),
                Auditoria.IdAuditoria.ToString());
            return View(oObjetivoAlcance);
        }

        // POST: ObjetivoAlcanceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ObjetivoAlcance model)
        {
            if (!ModelState.IsValid)
            {
                var oObjetivoAlcance = await _objetivoAlcanceRepository.GetByID(model.IdObjetivoAlcance);
                var Auditoria = await _auditoriaRepository.GetByID(oObjetivoAlcance.IdAuditoria);
                List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
                ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                    a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                    a => a.IdAuditoria.ToString(),
                    Auditoria.IdAuditoria.ToString());
                return View(model);
            }
            try
            {
                await _objetivoAlcanceRepository.Update(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ObjetivoAlcanceController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var oObjetivoAlcance = await _objetivoAlcanceRepository.Delete(id);
            if (oObjetivoAlcance == null)
            { return RedirectToAction("Error"); }
            return RedirectToAction(nameof(Index));
        }

        // POST: ObjetivoAlcanceController/Delete/5
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
