using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Graduacion.Models;
using Utilities_Net_6_MVC;

namespace Proyecto_Graduacion.Controllers
{
    public class MaterialidadController : Controller
    {
        private readonly IRepositoryAsync<Auditoria> _auditoriaRepository;
        private readonly IRepositoryAsync<Materialidad> _materialidadRepository;
        private readonly IRepositoryAsync<MateAuditorium> _mateAuditoriaRepository;
        public MaterialidadController(IRepositoryAsync<Auditoria> auditoriaRepository,
            IRepositoryAsync<Materialidad> materialidadRepository,
            IRepositoryAsync<MateAuditorium> mateAuditoriaRepository)
        {
            this._auditoriaRepository = auditoriaRepository;
            this._materialidadRepository = materialidadRepository;
            this._mateAuditoriaRepository = mateAuditoriaRepository;
        }
        // GET: MaterialidadController
        public async Task<ActionResult> Index()
        {
            var listado = (List<MateAuditorium>?)await _mateAuditoriaRepository
                .GetAllWithIncludes(a => a.IdAuditoriaNavigation,
                e => e.IdMaterialidadNavigation,
                i => i.IdAuditoriaNavigation.IdInstitucionNavigation);
            var listadoViewModel = listado.Select(x => new MaterialidadViewModel()
            {
                Auditoria = string.Concat(x.IdAuditoria, "-", x.IdAuditoriaNavigation.IdInstitucionNavigation.Nombre),
                Monto = x.IdMaterialidadNavigation.Monto,
                IdMaterialidad = (int)x.IdMateAuditoria
            });
            return View(listadoViewModel);
        }

        // GET: MaterialidadController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MaterialidadController/Create
        public async Task<ActionResult> Create()
        {
            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString()
                );
            return View();
        }

        // POST: MaterialidadController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(materialidadaudit model)
        {
            var oMaterialidad = await _materialidadRepository.Insert(new Materialidad()
            {
                Monto = model.Monto
            });
            if (oMaterialidad == null)
            {
                return RedirectToAction("Error");
            }
            var oMaterialidadAuditorioa = await _mateAuditoriaRepository.Insert(new MateAuditorium()
            {
                IdAuditoria = model.IdAuditoria,
                IdMaterialidad = oMaterialidad.IdMaterialidad
            });
            if (oMaterialidadAuditorioa == null)
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: MaterialidadController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var oMaterialidadAuditoria = await _mateAuditoriaRepository.GetByID(id);
            var Materialidad = await _materialidadRepository.GetByID(oMaterialidadAuditoria.IdMaterialidad);
            var Auditoria = await _auditoriaRepository.GetByID(oMaterialidadAuditoria.IdAuditoria);

            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString(),
                Auditoria.IdAuditoria.ToString());
            var materialidadViewModel = new materialidadauditEdit()
            {
                idMaterialidadAuditoria = oMaterialidadAuditoria.IdMateAuditoria,
                IdAuditoria = Auditoria.IdAuditoria,
                Monto = Materialidad.Monto
            };
            return View(materialidadViewModel);
        }

        // POST: MaterialidadController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(materialidadauditEdit model)
        {
            if (!ModelState.IsValid)
            {
                var oMaterialidadAuditoria = await _mateAuditoriaRepository.GetByID(model.idMaterialidadAuditoria);
                var Materialidad = await _materialidadRepository.GetByID(oMaterialidadAuditoria.IdMaterialidad);
                var Auditoria = await _auditoriaRepository.GetByID(oMaterialidadAuditoria.IdAuditoria);
                List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
                ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                    a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                    a => a.IdAuditoria.ToString(),
                    Auditoria.IdAuditoria.ToString());
                return View(model);
            }
            try
            {
                var oMaterialidadAuditoria = await _mateAuditoriaRepository.GetByID(model.idMaterialidadAuditoria);
                var Materialidad = await _materialidadRepository.GetByID(oMaterialidadAuditoria.IdMaterialidad);
                Materialidad.Monto = model.Monto;
                await _materialidadRepository.Update(Materialidad);
                oMaterialidadAuditoria.IdAuditoria = model.IdAuditoria;
                await _mateAuditoriaRepository.Update(oMaterialidadAuditoria);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MaterialidadController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var oMaterialidadAudit = await _mateAuditoriaRepository.Delete(id);
            if (oMaterialidadAudit == null)
            { return RedirectToAction("Error"); }
            return RedirectToAction(nameof(Index));
        }

        // POST: MaterialidadController/Delete/5
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
