using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Graduacion.Models;
using Utilities_Net_6_MVC;

namespace Proyecto_Graduacion.Controllers
{
    public class PruebaController : Controller
    {
        private readonly IRepositoryAsync<Auditoria> _auditoriaRepository;
        private readonly IRepositoryAsync<Prueba> _pruebaRepository;
        private readonly IRepositoryAsync<PruebasAuditorium> _pruebaAuditoriaRepository;
        private readonly IRepositoryAsync<TipoPrueba> _tipoPruebaRepository;
        public PruebaController(IRepositoryAsync<Prueba> pruebaRepository,
            IRepositoryAsync<TipoPrueba> tipoPruebaRepository,
            IRepositoryAsync<Auditoria> auditoriaRepository,
            IRepositoryAsync<PruebasAuditorium> pruebaAuditoriaReporitory)
        {
            this._pruebaRepository = pruebaRepository;
            _tipoPruebaRepository = tipoPruebaRepository;
            _auditoriaRepository = auditoriaRepository;
            _pruebaAuditoriaRepository = pruebaAuditoriaReporitory;
        }
        // GET: PruebaController
        public async Task<ActionResult> Index()
        {
            var listado = (List<PruebasAuditorium>?)await _pruebaAuditoriaRepository
                .GetAllWithIncludes(a => a.IdAuditoriaNavigation,
                e => e.IdPruebaNavigation,
                i => i.IdAuditoriaNavigation.IdInstitucionNavigation);
            var listadoViewModel = listado.Select(x => new PruebaViewModel()
            {
                Auditoria = string.Concat(x.IdAuditoria, "-", x.IdAuditoriaNavigation.IdInstitucionNavigation.Nombre),
                Descripcion = x.IdPruebaNavigation.Descripcion,
                IdPrueba = (int)x.IdPruebaAuditoria,
                /*TipoPrueba = x.IdPruebaNavigation.IdTipoPruebaNavigation.Descripcion
                 Aqui es donde esta el error al momento de buscar el index*/
            });
            return View(listadoViewModel);
        }

        // GET: PruebaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PruebaController/Create
        public async  Task<ActionResult> Create()
        {
            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString()
                );
            List<TipoPrueba> TipoPrueba = (List<TipoPrueba>)await _tipoPruebaRepository.GetAll();
            ViewData["TipoPrueba"] = SelectListItemHelper.ToSelectListItems(TipoPrueba,
                r => r.Descripcion,
                r => r.IdTipoPrueba.ToString());
            return View();
        }

        // POST: PruebaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(pruebas model)
        {
            if (!ModelState.IsValid)
            {
                List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
                ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                    a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                    a => a.IdAuditoria.ToString()
                    );
                var tipos = (List<TipoPrueba>)await _tipoPruebaRepository.GetAll();
                ViewData["TipoPrueba"] = SelectListItemHelper.ToSelectListItems(tipos,
                    a => a.Descripcion,
                    a => a.IdTipoPrueba.ToString()
                    );
                return View(model);
            }
            var oPrueba = await _pruebaRepository.Insert(new Prueba()
            {
                IdTipoPrueba = model.IdTipoPrueba,
                Descripcion = model.Descripcion
            });
            if (oPrueba == null)
            {
                return RedirectToAction("Error");
            }
            var oPruebaAuditoria = await _pruebaAuditoriaRepository.Insert(new PruebasAuditorium()
            {
                IdAuditoria = model.IdAuditoria,
                IdPrueba = oPrueba.IdPrueba
            });
            if (oPruebaAuditoria == null)
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: PruebaController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var oPruebaAuditoria = await _pruebaAuditoriaRepository.GetByID(id);
            var Prueba = await _pruebaRepository.GetByID(oPruebaAuditoria.IdPrueba);
            var Auditoria = await _auditoriaRepository.GetByID(oPruebaAuditoria.IdAuditoria);
            var TipoPrueba = await _tipoPruebaRepository.GetByID(Prueba.IdTipoPrueba);

            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString(),
                Auditoria.IdAuditoria.ToString());
            var tipos = (List<TipoPrueba>)await _tipoPruebaRepository.GetAll();
            ViewData["TipoPrueba"] = SelectListItemHelper.ToSelectListItems(tipos,
                a => a.Descripcion,
                a => a.IdTipoPrueba.ToString()
                );
            var pruebaViewModel = new pruebaauditEdit()
            {
                idPruebaAuditoria = oPruebaAuditoria.IdPruebaAuditoria,
                IdAuditoria = Auditoria.IdAuditoria,
                Descripcion = Prueba.Descripcion,
                IdTipoPrueba = Prueba.IdTipoPrueba
            };
            return View(pruebaViewModel);
        }

        // POST: PruebaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(pruebaauditEdit model)
        {
            if (!ModelState.IsValid)
            {
                var oPruebaAuditoria = await _pruebaAuditoriaRepository.GetByID(model.idPruebaAuditoria);
                var Prueba = await _pruebaRepository.GetByID(oPruebaAuditoria.IdPrueba);
                var Auditoria = await _auditoriaRepository.GetByID(oPruebaAuditoria.IdAuditoria);
                var TipoPrueba = await _tipoPruebaRepository.GetByID(Prueba.IdTipoPrueba);
                List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
                ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                    a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                    a => a.IdAuditoria.ToString(),
                    Auditoria.IdAuditoria.ToString());
                var tipos = (List<TipoPrueba>)await _tipoPruebaRepository.GetAll();
                ViewData["TipoPrueba"] = SelectListItemHelper.ToSelectListItems(tipos,
                    a => a.Descripcion,
                    a => a.IdTipoPrueba.ToString()
                    );
                return View(model);
            }
            try
            {
                var oPruebaAuditoria = await _pruebaAuditoriaRepository.GetByID(model.idPruebaAuditoria);
                var Prueba = await _pruebaRepository.GetByID(oPruebaAuditoria.IdPrueba);
                Prueba.Descripcion = model.Descripcion;
                Prueba.IdTipoPrueba = model.IdTipoPrueba;
                await _pruebaRepository.Update(Prueba);
                oPruebaAuditoria.IdAuditoria = model.IdAuditoria;
                await _pruebaAuditoriaRepository.Update(oPruebaAuditoria);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PruebaController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var oPruebaAudit = await _pruebaAuditoriaRepository.Delete(id);
            if (oPruebaAudit == null)
            { return RedirectToAction("Error"); }
            return RedirectToAction(nameof(Index));
        }

        // POST: PruebaController/Delete/5
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
