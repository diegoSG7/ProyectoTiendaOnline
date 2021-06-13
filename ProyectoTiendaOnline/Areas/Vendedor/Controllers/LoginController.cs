using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoTiendaOnline.Contenedor;
using ProyectoTiendaOnline.Models;
using ProyectoTiendaOnline.Security;

namespace ProyectoTiendaOnline.Areas.Vendedor.Controllers
{
    [Area("vendedor")]
    [Route("vendedor/login")]
    public class LoginController : Controller
    {
        private DataBaseContext _db;
        private IUsuarioContenedor IUsuario;
        public LoginController(DataBaseContext db, IUsuarioContenedor IUsuario)
        {
            _db = db;
            this.IUsuario = IUsuario;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("process")]
        public IActionResult Process(string email, string password)
        {

            var user = IUsuario.usuarioLogin(email, password);
            if (user == null)
            {
                ViewBag.error = "Invalid";
                return View("Index");
            }
            else
            {
                var securityManager = new SecurityManager();
                securityManager.SingIn(this.HttpContext, user, "Schema_Vendor");
                return RedirectToAction("index", "dashboard", new { area = "vendedor" });
            }
        }

        [Authorize(Roles = "Cliente", AuthenticationSchemes = "Schema_Vendor")]
        [HttpGet]
        [Route("index")]
        public IActionResult Logout()
        {
            var securityManager = new SecurityManager();
            securityManager.SingOut(this.HttpContext, "Schema_Vendor");
            return RedirectToAction("index", "login", new { area = "vendedor" });
        }
    }
}
