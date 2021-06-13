using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoTiendaOnline.Contenedor;
using ProyectoTiendaOnline.Models;
using ProyectoTiendaOnline.Security;

namespace ProyectoTiendaOnline.Controllers
{
    [Route("cliente")]
    public class ClienteController : Controller
    {
        private DataBaseContext _db;
        private IUsuarioContenedor iusuario;
        private IVentaContenedor Iventa;
        public ClienteController(DataBaseContext db, IUsuarioContenedor iusuario, IVentaContenedor Iventa)
        {
            _db = db;
            this.iusuario = iusuario;
            this.Iventa = Iventa;
        }


        [HttpGet]
        [Route("registrar")]
        public IActionResult Registrar()
        {
            // var usuario = new Usuario();
            return View("Registrar", new Usuario());

        }

        [HttpPost]
        [Route("registrar")]
        public IActionResult Registrar(Usuario usuario)
        {
            // var existe = _db.Usuarios.Count(a => a.Email.Equals(usuario.Email)) > 0;

            if (iusuario.usuarioExiste(usuario.Email))
            {
                ViewBag.error = "Email existe";
                //usuario = new Usuario();
                return View("Registrar", new Usuario());
            }
            else
            {
                iusuario.guardarUsuario(usuario);
                //_db.Usuarios.Add(usuario);
                //usuario.RolId = 2;
                //_db.SaveChanges();

                return RedirectToAction("Login", "Cliente");
            }

        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {
            //var user = _db.Usuarios.Include(c => c.Rol).Where(o => o.Email == email && o.Password == password &&
            ////o.RolId == 2).FirstOrDefault();
            //if (email == null)
            //    ModelState.AddModelError("email","campo obligatorio");

            var user = iusuario.usuarioLogin(email, password);

            if (email == null || password == null)
            {
                ViewBag.error = "Invalid";
                return View("Login");
            }
            else
            {

                var securityManager = new SecurityManager();
                securityManager.SingIn(this.HttpContext, user, "Schema_Vendor");
                return RedirectToAction("index", "Home");
            }

        }

        [Route("logout")]
        public IActionResult Logout()
        {
            var securityManager = new SecurityManager();
            securityManager.SingOut(this.HttpContext, "Schema_Vendor");
            return RedirectToAction("login", "cliente");
        }

        [HttpGet]
        [Route("historia")]
        public IActionResult Historia()
        {

            var user = iusuario.hisoriaClaims(User);
            if (user == null)
            {
                return View("login", "cliente");
            }
            else
            {
                //var cliente = _db.Usuarios.Include(p => p.Facturas).SingleOrDefault(a => a.Username.Equals(user.Value));
                var cliente = iusuario.clienteHistoria(user);

                ViewBag.facturas = Iventa.listaHistoria(cliente);
                //ViewBag.productos = _db.Facturas.Include(p => p.Producto).ThenInclude(y => y.Fotos).ToList();

                return View("historia");    
            }

        }

        [Authorize(Roles = "Cliente", AuthenticationSchemes = "Schema_Vendor")]
        [HttpGet]
        [Route("detalles/{id}")]
        public IActionResult Detalles(int id)
        {
            //ViewBag.facturadetalle = _db.FacturaDetalles.Include(p => p.Producto).Where(i => i.FacturaId == id).ToList();

            //ViewBag.facturadetalle = _db.Facturas.Include(p => p.Producto).ToList();
            
            var facturas = Iventa.Facturadecliente(id);
            ViewBag.facturas = facturas;

            //var chatUsuario = _db.Chats.Include(o => o.Usuario).Include(o => o.Factura).Where(x => x.FacturaId == id).ToList();
            //ViewBag.chatUsuario = chatUsuario.;

            ViewBag.Producto = Iventa.ProductoFactura(facturas);
           
            ViewBag.usuario = facturas;


            return View("Detalles");
            
        }

        [Route("marcarcomorecibido/{id}")]
        public ActionResult MarcarComoRecibido(int id)
        {
            if (id <= 0)
            {
                return View();
            }
            //var user = Convert.ToInt32(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            //// TO-DO validar si ya existe el libro en la biblioteca, en ese caso no guardar y notificar

            //var venta = _db.Facturas
            //    .Where(o => o.ProductoId == productoId && o.UsuarioId == user)
            //    .FirstOrDefault();

            //venta.Estado = 3;
            //_db.SaveChanges();
            Iventa.marcarComoRecibido(id);

            //TempData["SuccessMessage"] = "Se marco como leyendo el libro";

            return RedirectToAction("Historia");
        }

        [HttpPost]
        [Route("process")]
        public IActionResult Process(int id)
        {
            //var venta = _db.Facturas.Include(o => o.Usuario).Include(o => o.Producto).FirstOrDefault(x => x.Id == id);
            var venta = Iventa.procesFactura(id);

            Iventa.guardarProcesFactura(venta);
            //venta.Estado = 3;
            //_db.SaveChanges();
            return RedirectToAction("Historia");

        }

        [HttpPost]
        public ActionResult AddComentario(Chat chat, Factura FacturaId)
        {

            var user = Convert.ToInt32(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == 0)
            {
                return RedirectToAction("Login", "Cliente");
            }

            else
            {
                chat.UsuarioId = user;
                chat.Fecha = DateTime.Now;
                _db.Chats.Add(chat);;

                _db.SaveChanges();

                return RedirectToAction("Detalles", new { id = chat.FacturaId });
            }


        }
    }
}
