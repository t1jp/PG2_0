using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Proyecto_Graduacion.Models;
using Utilities_Net_6_MVC;

namespace Proyecto_Graduacion.Controllers
{
    public class EntrevistaController : Controller
    {
        private readonly IRepositoryAsync<Auditoria> _auditoriaRepository;
        private readonly IRepositoryAsync<Entrevista> _entrevistaRepository;
        private readonly IRepositoryAsync<EntrevistaAuditorium> _entrevistaAuditoriaRepository;
        public EntrevistaController(IRepositoryAsync<Auditoria> auditoriaRepository,
            IRepositoryAsync<Entrevista> entrevistaRepository,
            IRepositoryAsync<EntrevistaAuditorium> entrevistaAuditoriaRepository) 
        {
            this._auditoriaRepository = auditoriaRepository;
            this._entrevistaRepository=entrevistaRepository;
            this._entrevistaAuditoriaRepository = entrevistaAuditoriaRepository;
        }

        // GET: EntrevistaController
        public async Task<ActionResult> Index()
        {
            var listado=(List<EntrevistaAuditorium>?)await _entrevistaAuditoriaRepository
                .GetAllWithIncludes(a=>a.IdAuditoriaNavigation,
                e=>e.IdEntrevistaNavigation,
                i=>i.IdAuditoriaNavigation.IdInstitucionNavigation);
            var listadoViewModel = listado.Select(x=>new EntrevistaViewModel()
            {
                Auditoria=string.Concat(x.IdAuditoria,"-",x.IdAuditoriaNavigation.IdInstitucionNavigation.Nombre),
                Descripcion=x.IdEntrevistaNavigation.Descripcion,
                IdEntrevista= (int)x.IdEntrevistaAuditoria
            });
            return View(listadoViewModel);
        }

        // GET: EntrevistaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EntrevistaController/Create
        public async Task<ActionResult> Create()
        {
            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a=>a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a=>string.Concat(a.IdAuditoria.ToString(),"-",a.IdInstitucionNavigation.Nombre),
                a=>a.IdAuditoria.ToString()
                );
            return View();
        }

        // POST: EntrevistaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(entrevistaaudit model)
        {
            var oEntrevista = await _entrevistaRepository.Insert(new Entrevista()
            {
                Descripcion = model.Descripcion
            });
            if(oEntrevista==null)
            {
                return RedirectToAction("Error");
            }
            var oEntrevistaAuditorioa = await _entrevistaAuditoriaRepository.Insert(new EntrevistaAuditorium()
            {
                IdAuditoria = model.IdAuditoria,
                IdEntrevista=oEntrevista.IdEntrevista
            });
            if (oEntrevistaAuditorioa == null)
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: EntrevistaController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var oEntrevistaAuditoria = await _entrevistaAuditoriaRepository.GetByID(id);
            var Entrevista = await _entrevistaRepository.GetByID(oEntrevistaAuditoria.IdEntrevista);
            var Auditoria = await _auditoriaRepository.GetByID(oEntrevistaAuditoria.IdAuditoria);

            List<Auditoria> Auditorias = (List<Auditoria>)await _auditoriaRepository.GetAllWithIncludes(a => a.IdInstitucionNavigation);
            ViewData["Auditorias"] = SelectListItemHelper.ToSelectListItems(Auditorias,
                a => string.Concat(a.IdAuditoria.ToString(), "-", a.IdInstitucionNavigation.Nombre),
                a => a.IdAuditoria.ToString(),
                Auditoria.IdAuditoria.ToString());
            var entrevistaViewModel = new entrevistaauditEdit()
            {
                idEntrevistaAuditoria = oEntrevistaAuditoria.IdEntrevistaAuditoria,
                IdAuditoria = Auditoria.IdAuditoria,
                Descripcion = Entrevista.Descripcion
            };
            return View(entrevistaViewModel);
        }

        // POST: EntrevistaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(entrevistaauditEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var oEntrevistaAuditoria = await _entrevistaAuditoriaRepository.GetByID(model.idEntrevistaAuditoria);
                var Entrevista = await _entrevistaRepository.GetByID(oEntrevistaAuditoria.IdEntrevista);
                Entrevista.Descripcion = model.Descripcion;
                await _entrevistaRepository.Update(Entrevista);
                oEntrevistaAuditoria.IdAuditoria= model.IdAuditoria;
                await _entrevistaAuditoriaRepository.Update(oEntrevistaAuditoria);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EntrevistaController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var oEntrevistaAudit = await _entrevistaAuditoriaRepository.Delete(id);
            if (oEntrevistaAudit == null)
            { return RedirectToAction("Error"); }
            return RedirectToAction(nameof(Index));
        }

        // POST: EntrevistaController/Delete/5
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
