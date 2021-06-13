using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoTiendaOnline.Contenedor;
using ProyectoTiendaOnline.Models;

namespace ProyectoTiendaOnline.Areas.Vendedor.Controllers
{
    [Authorize(Roles = "Cliente", AuthenticationSchemes = "Schema_Vendor")]
    [Area("vendedor")]
    [Route("vendedor/venta")]
    public class VentaController : Controller
    {
        private DataBaseContext _db;
        private IVentaContenedor Iventa;
        public VentaController(DataBaseContext db, IVentaContenedor Iventa)
        {
            _db = db;
            this.Iventa = Iventa;
        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var user = Convert.ToInt32(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            ViewBag.ventas = _db.Facturas.Include(p => p.Producto).Include(o => o.Usuario).Where(o => o.Producto.UsuarioId == user).OrderByDescending(i => i.Id).ToList();
            return View();
        }

        [Route("detalles/{id}")]
        public IActionResult Detalles(int id)
        {

            //ViewBag.ventas = _db.Facturas.Include(o => o.Usuario).Include(o => o.Producto).FirstOrDefault(x => x.Id == id);
            var ventas = Iventa.ventaDeTalle(id);
            ViewBag.ventas = ventas;
            //ViewBag.Producto = ventas.Producto;
            ViewBag.Producto = Iventa.ventaDEtalleProd(ventas);
            ViewBag.usuario = ventas;
            //ViewBag.chats = ventas.Chats;
            ViewBag.chats = Iventa.ventaChat(ventas);
            return View("Detalles");

        }
        
        [HttpPost]
        [Route("process")]
        public IActionResult Process(int id)
        {
            if (id <= 0)
            {
                return View();
            }
            var venta = Iventa.ventaProcess(id);

            Iventa.guardarVentaProcess(venta);
            return RedirectToAction("Index", "Venta", new { area = "vendedor"});

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
                _db.Chats.Add(chat); ;

                _db.SaveChanges();

                return RedirectToAction("Detalles", new { id = chat.FacturaId });
            }


        }
    }
}
