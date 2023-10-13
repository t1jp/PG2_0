using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Graduacion.Models;
using Utilities_Net_6_MVC;

namespace Proyecto_Graduacion.Controllers
{
    public class AuditoriaController : Controller
    {
        private readonly IRepositoryAsync<Auditoria> _auditoriaRepository;
        private readonly IRepositoryAsync<Usuario> _usuariosRepository;
        private readonly IRepositoryAsync<Institucione> _institucionRepository;

        public AuditoriaController(IRepositoryAsync<Auditoria>  auditoriaRepository,
            IRepositoryAsync<Usuario> usuariosRepository,
            IRepositoryAsync<Institucione> institucionRepository)
        {
            _auditoriaRepository = auditoriaRepository;
            _usuariosRepository = usuariosRepository;
            _institucionRepository = institucionRepository;
        }
        public async Task<ActionResult> Index()
        {
            var auditoria = await _auditoriaRepository.GetAllWithIncludes(u => u.IdUsuarioNavigation, i => i.IdInstitucionNavigation);
            return View(auditoria);
        }

        // GET: AuditoriaController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: AuditoriaController/Create
        public async Task<ActionResult> Create()
        {
            List<Institucione> Insti = (List<Institucione>)await _institucionRepository.GetAll();
            ViewData["idInsti"] = SelectListItemHelper.ToSelectListItems(Insti,
                i => i.Nombre,
                i => i.IdInstitucion.ToString());
            List<Usuario> Usua = (List<Usuario>)await _usuariosRepository.GetAll();
            ViewData["idUsua"] = SelectListItemHelper.ToSelectListItems(Usua,
                u => u.Nombre,
                u => u.IdUsuario.ToString());
            return View();
        }

        // POST: AuditoriaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Auditoria auditoria)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(auditoria);
                }
                var oAudit = await _auditoriaRepository.Insert(auditoria);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(auditoria);
            }
        }

        // GET: AuditoriaController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var oAudit = await _auditoriaRepository.GetByID(id);
            if (oAudit == null)
            {
                return RedirectToAction("Error");
            }
            List<Institucione> Insti = (List<Institucione>)await _institucionRepository.GetAll();
            ViewData["idInsti"] = SelectListItemHelper.ToSelectListItems(Insti,
                i => i.Nombre,
                i => i.IdInstitucion.ToString());
            List<Usuario> Usua = (List<Usuario>)await _usuariosRepository.GetAll();
            ViewData["idUsua"] = SelectListItemHelper.ToSelectListItems(Usua,
                u => u.Nombre,
                u => u.IdUsuario.ToString());

            return View(oAudit);
        }

        // POST: AuditoriaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Auditoria auditoria)
        {
            if (!ModelState.IsValid)
            {
                return View(auditoria);
            }
            try
            {
                await _auditoriaRepository.Update(auditoria);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuditoriaController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var oAudit = await _auditoriaRepository.Delete(id);
            if (oAudit == null)
            { return RedirectToAction("Error"); }
            return RedirectToAction(nameof(Index));
        }

        // POST: AuditoriaController/Delete/5
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
