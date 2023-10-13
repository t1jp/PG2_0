using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Graduacion.Models;
using Utilities_Net_6_MVC;

namespace Proyecto_Graduacion.Controllers
{
    public class EncuestaController : Controller
    {
        private readonly IRepositoryAsync<Auditoria> _auditoriaRepository;
        private readonly IRepositoryAsync<Encuesta> _encuestaRepository;
        private readonly IRepositoryAsync<EncuestaAuditorium> _encuestaAuditoriaRepository;

        public EncuestaController(IRepositoryAsync<Auditoria> auditoriaRepository,
            IRepositoryAsync<Encuesta> encuestaRepository,
            IRepositoryAsync<EncuestaAuditorium> encuestaAuditoriaRepository)
        {
            this._auditoriaRepository = auditoriaRepository;
            this._encuestaRepository = encuestaRepository;
            this._encuestaAuditoriaRepository = encuestaAuditoriaRepository;
        }
        // GET: EncuestaController
        public async Task<ActionResult> Index()
        {
            var listado = (List<EncuestaAuditorium>?)await _encuestaAuditoriaRepository
               .GetAllWithIncludes(a => a.IdAuditoriaNavigation,
               e => e.IdEncuestaNavigation,
               i => i.IdAuditoriaNavigation.IdInstitucionNavigation);
            var listadoViewModel = listado.Select(x => new EncuestaViewModel()
            {
                Auditoria = string.Concat(x.IdAuditoria, "-", x.IdAuditoriaNavigation.IdInstitucionNavigation.Nombre),
                Descripcion = x.IdEncuestaNavigation.Descripcion,
                IdEncuesta = (int)x.IdEncuestaAuditoria
            });
            return View(listadoViewModel);
        }

        // GET: EncuestaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EncuestaController/Create
        public async Task<ActionResult> Create()
        {
            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString()
                );
            return View();
        }

        // POST: EncuestaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(encuestaaudit model)
        {
            var oEncuesta = await _encuestaRepository.Insert(new Encuesta()
            {
                Descripcion = model.Descripcion
            });
            if (oEncuesta == null)
            {
                return RedirectToAction("Error");
            }
            var oEncuestaAuditorioa = await _encuestaAuditoriaRepository.Insert(new EncuestaAuditorium()
            {
                IdAuditoria = model.IdAuditoria,
                IdEncuesta = oEncuesta.IdEncuesta
            });
            if (oEncuestaAuditorioa == null)
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: EncuestaController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var oEncuestaAuditoria = await _encuestaAuditoriaRepository.GetByID(id);
            var Encuesta = await _encuestaRepository.GetByID(oEncuestaAuditoria.IdEncuesta);
            var Auditoria = await _auditoriaRepository.GetByID(oEncuestaAuditoria.IdAuditoria);
            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString(),
                Auditoria.IdAuditoria.ToString());
            var encuestaViewModel = new encuestaauditEdit()
            {
                IdEncuestaAuditoria = oEncuestaAuditoria.IdEncuestaAuditoria,
                IdAuditoria = Auditoria.IdAuditoria,
                Descripcion = Encuesta.Descripcion
            };
            return View(encuestaViewModel);
        }

        // POST: EncuestaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(encuestaauditEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var oEncuestaAuditoria = await _encuestaAuditoriaRepository.GetByID(model.IdEncuestaAuditoria);
                var Encuesta = await _encuestaRepository.GetByID(oEncuestaAuditoria.IdEncuesta);
                Encuesta.Descripcion = model.Descripcion;
                await _encuestaRepository.Update(Encuesta);
                oEncuestaAuditoria.IdAuditoria = model.IdAuditoria;
                await _encuestaAuditoriaRepository.Update(oEncuestaAuditoria);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EncuestaController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var oEncuestaAudit = await _encuestaAuditoriaRepository.Delete(id);
            if (oEncuestaAudit == null)
            { return RedirectToAction("Error"); }
            return RedirectToAction(nameof(Index));
        }

        // POST: EncuestaController/Delete/5
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
