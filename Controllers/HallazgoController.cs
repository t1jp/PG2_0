using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Graduacion.Models;
using Utilities_Net_6_MVC;

namespace Proyecto_Graduacion.Controllers
{
    public class HallazgoController : Controller
    {
        private readonly IRepositoryAsync<Hallazgo> _hallazgoRepository;
        private readonly IRepositoryAsync<TipoHallazgo> _tipoHallazgoRepository;
        private readonly IRepositoryAsync<Auditoria> _auditoriaRepository;
        public HallazgoController(IRepositoryAsync<Hallazgo> hallazgoRepository,
            IRepositoryAsync<TipoHallazgo> tipoHallazgoRepository,
            IRepositoryAsync<Auditoria> auditoriaRepository)
        {
            this._hallazgoRepository = hallazgoRepository;
            _tipoHallazgoRepository = tipoHallazgoRepository;
            _auditoriaRepository = auditoriaRepository;
        }
        // GET: HallazgoController
        public async Task<ActionResult> Index()
        {
            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString()
                );
            var oHallazgos = await _hallazgoRepository.GetAllWithIncludes(a=>a.IdAuditoriaNavigation,
                i=>i.IdAuditoriaNavigation.IdInstitucionNavigation,
                th=>th.IdTipoHallazgoNavigation);
            var listadoHallazgos = oHallazgos.Select(x => new hallazgos()
            {
                IdHallazgo = x.IdHallazgo,
                Auditoria= string.Concat(x.IdAuditoria.ToString(), "-", x.IdAuditoriaNavigation.IdInstitucionNavigation.Nombre),
                DescripcionHallazgo=x.DescripcionHallazgo,
                TipoHallazgo=x.IdTipoHallazgoNavigation.Descripcion
            });
            return View(listadoHallazgos);
        }

        // GET: HallazgoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HallazgoController/Create
        public async Task<ActionResult> Create()
        {
            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString()
                );
            var tipos = (List<TipoHallazgo>)await _tipoHallazgoRepository.GetAll();
            ViewData["TipoHallazgo"] = SelectListItemHelper.ToSelectListItems(tipos,
                a => a.Descripcion,
                a => a.IdTipoHallazgo.ToString()
                );
            return View();
        }

        // POST: HallazgoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Hallazgo model)
        {
            if (!ModelState.IsValid)
            {
                List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
                ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                    a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                    a => a.IdAuditoria.ToString()
                    );
                var tipos = (List<TipoHallazgo>)await _tipoHallazgoRepository.GetAll();
                ViewData["TipoHallazgo"] = SelectListItemHelper.ToSelectListItems(tipos,
                    a => a.Descripcion,
                    a => a.IdTipoHallazgo.ToString()
                    );
                return View(model);
            }
            try
            {
                var oHallazgo = await _hallazgoRepository.Insert(model);
                if( oHallazgo == null )
                {
                    return RedirectToAction("Error");
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HallazgoController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString()
                );
            var tipos = (List<TipoHallazgo>)await _tipoHallazgoRepository.GetAll();
            ViewData["TipoHallazgo"] = SelectListItemHelper.ToSelectListItems(tipos,
                a => a.Descripcion,
                a => a.IdTipoHallazgo.ToString()
                );
            var oHallazgo = await _hallazgoRepository.GetByID(id);
            return View(oHallazgo);
        }

        // POST: HallazgoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Hallazgo model)
        {
            if (!ModelState.IsValid)
            {
                List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
                ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                    a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                    a => a.IdAuditoria.ToString()
                    );
                var tipos = (List<TipoHallazgo>)await _tipoHallazgoRepository.GetAll();
                ViewData["TipoHallazgo"] = SelectListItemHelper.ToSelectListItems(tipos,
                    a => a.Descripcion,
                    a => a.IdTipoHallazgo.ToString()
                    );
                return View(model);
            }
            try
            {
                await _hallazgoRepository.Update(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HallazgoController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            var oHallazgo = await _hallazgoRepository.Delete(id);
            if (oHallazgo == null)
            { return RedirectToAction("Error"); }
            return RedirectToAction(nameof(Index));
        }

        // POST: HallazgoController/Delete/5
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
