using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Graduacion.Models;
using Utilities_Net_6_MVC;

namespace Proyecto_Graduacion.Controllers
{
    public class CausaEfectoController : Controller
    {
        private readonly IRepositoryAsync<Auditoria> _auditoriaRepository;
        private readonly IRepositoryAsync<CausaEfecto> _causaefectoRepository;

        public CausaEfectoController(IRepositoryAsync<Auditoria> auditoriaRepository,
            IRepositoryAsync<CausaEfecto> causaefectoRepository)
        {
            this._auditoriaRepository = auditoriaRepository;
            this._causaefectoRepository = causaefectoRepository;
        }
        // GET: CausaEfectoController
        public async Task<ActionResult> Index()
        {
            var CausaEfecto = await _causaefectoRepository.GetAllWithIncludes(a => a.IdAuditoriaNavigation, i => i.IdAuditoriaNavigation.IdInstitucionNavigation);
            var causaefecto = CausaEfecto.Select(x => new causaefectoaudit()
            {
                IdCausaEfecto = x.IdCausaEfecto,
                Auditoria = string.Concat(x.IdAuditoria.ToString(), "-", x.IdAuditoriaNavigation.IdInstitucionNavigation.Nombre),
                Descripcion = x.Descripcion
            });
            return View(causaefecto);
        }

        // GET: CausaEfectoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CausaEfectoController/Create
        public async Task<ActionResult> Create()
        {
            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString()
                );
            return View();
        }

        // POST: CausaEfectoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CausaEfecto causaefecto)
        {
            var oCausaEfecto = await _causaefectoRepository.Insert(causaefecto);
            if (oCausaEfecto == null)
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: CausaEfectoController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var oCausaEfecto = await _causaefectoRepository.GetByID(id);
            var Auditoria = await _auditoriaRepository.GetByID(oCausaEfecto.IdAuditoria);
            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString(),
                Auditoria.IdAuditoria.ToString());
            return View(oCausaEfecto);
        }

        // POST: CausaEfectoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CausaEfecto model)
        {
            if (!ModelState.IsValid)
            {
                var oCausaEfecto = await _causaefectoRepository.GetByID(model.IdCausaEfecto);
                var Auditoria = await _auditoriaRepository.GetByID(oCausaEfecto.IdAuditoria);
                List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
                ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                    a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                    a => a.IdAuditoria.ToString(),
                    Auditoria.IdAuditoria.ToString());
                return View(model);
            }
            try
            {
                await _causaefectoRepository.Update(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CausaEfectoController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var oCausaEfecto = await _causaefectoRepository.Delete(id);
            if (oCausaEfecto == null)
            { return RedirectToAction("Error"); }
            return RedirectToAction(nameof(Index));
        }

        // POST: CausaEfectoController/Delete/5
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
