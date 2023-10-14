using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Graduacion.Models;
using Utilities_Net_6_MVC;

namespace Proyecto_Graduacion.Controllers
{
    
    public class FodaController : Controller
    {
        private readonly IRepositoryAsync<Auditoria> _auditoriaRepository;
        private readonly IRepositoryAsync<Fodum> _fodaRepository;

        public FodaController(IRepositoryAsync<Auditoria> auditoriaRepository,
            IRepositoryAsync<Fodum> fodaRepository)
        {
            this._auditoriaRepository = auditoriaRepository;
            this._fodaRepository = fodaRepository;
        }
        // GET: FodaController
        public async Task<ActionResult> Index()
        {
            var FODA = await _fodaRepository.GetAllWithIncludes(a=>a.IdAuditoriaNavigation,i=>i.IdAuditoriaNavigation.IdInstitucionNavigation);
            var foda = FODA.Select(x=>new fodaaudit()
            {
                IdFoda = x.IdFoda,
                Auditoria= string.Concat(x.IdAuditoria.ToString(), "-", x.IdAuditoriaNavigation.IdInstitucionNavigation.Nombre),
                InfoAmenaza=x.InfoAmenaza,
                InfoDebilidad=x.InfoDebilidad,
                InfoFortaleza=x.InfoFortaleza,
                InfoOportunidad=x.InfoOportunidad
            });
            return View(foda);
        }

        // GET: FodaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FodaController/Create
        public async Task<ActionResult> Create()
        {
            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString()
                );
            return View();
        }

        // POST: FodaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Fodum foda)
        {
            var oFoda = await _fodaRepository.Insert(foda);
            if (oFoda == null)
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: FodaController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var oFoda = await _fodaRepository.GetByID(id);
            var Auditoria = await _auditoriaRepository.GetByID(oFoda.IdAuditoria);
            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString(),
                Auditoria.IdAuditoria.ToString());
            return View(oFoda);
        }

        // POST: FodaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Fodum model)
        {
            if (!ModelState.IsValid)
            {
                var oFoda = await _fodaRepository.GetByID(model.IdFoda);
                var Auditoria = await _auditoriaRepository.GetByID(oFoda.IdAuditoria);
                List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
                ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                    a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                    a => a.IdAuditoria.ToString(),
                    Auditoria.IdAuditoria.ToString());
                return View(model);
            }
            try
            {
                await _fodaRepository.Update(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FodaController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var oFoda = await _fodaRepository.Delete(id);
            if (oFoda == null)
            { return RedirectToAction("Error"); }
            return RedirectToAction(nameof(Index));
        }

        // POST: FodaController/Delete/5
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
