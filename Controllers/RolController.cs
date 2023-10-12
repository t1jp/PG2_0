using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Graduacion.Models;
using Utilities_Net_6_MVC;

namespace Proyecto_Graduacion.Controllers
{
    public class RolController : Controller
    {
        private readonly IRepositoryAsync<Role> _rolesRepository;

        public RolController(IRepositoryAsync<Role> rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }
        public async Task<ActionResult> Index()
        {
            var roles = await _rolesRepository.GetAll();
            return View(roles);
        }

        // GET: RolController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: RolController/Create
        public async  Task<ActionResult> Create()
        {
            return View();
        }

        // POST: RolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Role role)
        {
            if (!ModelState.IsValid)
            {
                return View(role);
            }
            try
            {
                var oRol = await _rolesRepository.Insert(role);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(role);
            }
        }

        // GET: RolController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var oRol = await _rolesRepository.GetByID(id);
            if (oRol == null)
            {
                return RedirectToAction("Error");
            }
            return View(oRol);
        }

        // POST: RolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Role role)
        {
            if (!ModelState.IsValid)
            {
                return View(role);
            }
            try
            {
                await _rolesRepository.Update(role);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RolController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var oRole = await _rolesRepository.Delete(id);
            if (oRole == null)
            {  return RedirectToAction("Error"); }
            return RedirectToAction(nameof(Index));
        }

        // POST: RolController/Delete/5
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
