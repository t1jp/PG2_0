using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Graduacion.Models;
using System.Data;
using System.Runtime.InteropServices;
using Utilities_Net_6_MVC;

namespace Proyecto_Graduacion.Controllers
{
    public class InstitucionController : Controller
    {
        private readonly IRepositoryAsync<Institucione> _institucionRepository;

        public InstitucionController(IRepositoryAsync<Institucione> institucionRepository)
        {
            _institucionRepository = institucionRepository;
        }
        public async  Task<ActionResult> Index()
        {
            var institucion = await _institucionRepository.GetAll();
            return View(institucion);
        }

        // GET: InstitucionController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: InstitucionController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: InstitucionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Institucione institucion)
        {
            if (!ModelState.IsValid)
            {
                return View(institucion);
            }
            try
            {
                var oInsti = await _institucionRepository.Insert(institucion);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(institucion);
            }
        }

        // GET: InstitucionController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var oInsti = await _institucionRepository.GetByID(id);
            if (oInsti == null)
            {
                return RedirectToAction("Error");
            }
            return View(oInsti);
        }

        // POST: InstitucionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Institucione institucion)
        {
            if (!ModelState.IsValid)
            {
                return View(institucion);
            }
            try
            {
                await _institucionRepository.Update(institucion);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InstitucionController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var oInsti = await _institucionRepository.Delete(id);
            if (oInsti == null)
            { return RedirectToAction("Error"); }
            return RedirectToAction(nameof(Index));
        }

        // POST: InstitucionController/Delete/5
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
