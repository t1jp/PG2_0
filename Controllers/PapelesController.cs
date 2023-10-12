using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Graduacion.Models;
using Utilities_Net_6_MVC;

namespace Proyecto_Graduacion.Controllers
{
    public class PapelesController : Controller
    {
        private readonly IRepositoryAsync<Papele> _papelesRepository;
        public PapelesController(IRepositoryAsync<Papele> papelesRepository)
        {
            _papelesRepository = papelesRepository;
        }
        // GET: PapelesController
        public async Task<ActionResult> Index()
        {
            var papeles = await _papelesRepository.GetAll();
            return View(papeles);
        }

        // GET: PapelesController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: PapelesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PapelesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(papeles papel)
        {
            if (papel.File != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Docs");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var rutaArchivo = Path.Combine(path, papel.File.FileName);
                using var fileStream=new FileStream(rutaArchivo,FileMode.Create);
                await papel.File.CopyToAsync(fileStream);
                var oPapel= new Papele();
                oPapel.RutaDoc = papel.File.FileName;
                oPapel.Descripcion = papel.descripcion;
                oPapel.TipoPapeles=papel.tipoPapeles;
                await _papelesRepository.Insert(oPapel);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Error");

        }

        // GET: PapelesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PapelesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PapelesController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {   
            var oPapel = await _papelesRepository.Delete(id);
            if (oPapel == null)
            { return RedirectToAction("Error"); }
            return RedirectToAction(nameof(Index));
        }

        // POST: PapelesController/Delete/5
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
