using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_Graduacion.Models;
using Utilities_Net_6_MVC;

namespace Proyecto_Graduacion.ContRolelers
{
    public class UsuariosController : Controller
    {
        private readonly IRepositoryAsync<Usuario> _usuariosRepository;
        private readonly IRepositoryAsync<Role> _RoleRepository;
        public UsuariosController(IRepositoryAsync<Usuario> usuariosRepository,
            IRepositoryAsync<Role> RoleRepository)
        {
            _usuariosRepository = usuariosRepository;
            _RoleRepository = RoleRepository;
        }

        // GET: UsuariosContRoleler
        public async Task<ActionResult> Index()
        {
            var usuarios = await _usuariosRepository.GetAllWithIncludes(u => u.IdRolNavigation);
            return View(usuarios);
        }

        // GET: UsuariosContRoleler/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: UsuariosContRoleler/Create
        public async Task<ActionResult> Create()
        {
            List<Role> Rolees = (List<Role>)await _RoleRepository.GetAll();
            ViewData["idRole"] = SelectListItemHelper.ToSelectListItems(Rolees,
                r=>r.Descripcion,
                r=>r.IdRol.ToString());
            return View();
        }

        // POST: UsuariosContRoleler/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Usuario usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(usuario);
                }
                var oUser = await _usuariosRepository.Insert(usuario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(usuario);
            }
        }

        
        public async Task<ActionResult> Edit(int id)
        {
            var oUser=await _usuariosRepository.GetByID(id);
            if (oUser == null)
            {
                return RedirectToAction("Error");
            }
            List<Role> Rolees = (List<Role>)await _RoleRepository.GetAll();
            ViewData["idRole"] = SelectListItemHelper.ToSelectListItems(Rolees,
                r => r.Descripcion,
                r => r.IdRol.ToString(),
                oUser.IdRol.ToString());

            return View(oUser);
        }

        // POST: UsuariosContRoleler/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }
            try
            {
                await _usuariosRepository.Update(usuario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosContRoleler/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var oUser = await _usuariosRepository.Delete(id);
            if (oUser == null)
            { return RedirectToAction("Error"); }
            return RedirectToAction(nameof(Index));
        }

        //// POST: UsuariosContRoleler/Delete/5
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
