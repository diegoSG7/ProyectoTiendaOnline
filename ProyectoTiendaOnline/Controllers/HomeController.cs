using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProyectoTiendaOnline.Contenedor;
using ProyectoTiendaOnline.Models;

namespace ProyectoTiendaOnline.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        private DataBaseContext _db;
        private IProductoContenedor Iprod;
        public HomeController(DataBaseContext db, IProductoContenedor Iprod)
        {
            _db = db;
            this.Iprod = Iprod;
        }

        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            //ViewBag.ultimosproductos = _db.Productos.OrderByDescending(p => p.Id).Where(p =>
            //    p => Status).Take(6).ToList();
            //ViewBag.ultimosproductos = _db.Productos.Include(o => o.Usuario).Include(o => o.Fotos).Include(o => o.Parent).OrderByDescending(p => p.Id).Take(6).ToList();
            ViewBag.ultimosproductos = Iprod.listaultimosProductos();

            return View();
        }


    }
}
