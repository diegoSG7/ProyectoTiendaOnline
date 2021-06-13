using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoTiendaOnline.Contenedor;
using ProyectoTiendaOnline.Models;

namespace ProyectoTiendaOnline.Areas.Vendedor.Controllers
{
    [Authorize(Roles = "Cliente", AuthenticationSchemes = "Schema_Vendor")]
    [Area("vendedor")]
    [Route("vendedor/foto")]
    public class FotoController : Controller
    {
        private DataBaseContext _db;
        private IHostingEnvironment _he;
        private IFotoContenedor Ifoto;
        private IProductoContenedor Iprod;
        public FotoController(DataBaseContext db, IHostingEnvironment he, IFotoContenedor Ifoto, IProductoContenedor Iprod)
        {
            _db = db;
            _he = he;
            this.Ifoto = Ifoto;
            this.Iprod = Iprod;
        }

        [Route("index/{id}")]
        public IActionResult Index(int id)
        {
            //ViewBag.Producto = _db.Productos.Find(id);
            ViewBag.Producto = Iprod.priodcutoFind(id);
            //ViewBag.Fotos = _db.Fotos.Where(c => c.ProductoId == id).ToList();
            ViewBag.Fotos = Ifoto.listafoto(id);
            return View();
        }

        [HttpGet]
        [Route("agregar/{id}")]
        public IActionResult Agregar(int id)
        {
            //  ViewBag.Producto = _db.Productos.Find(id);
            ViewBag.Producto = Iprod.priodcutoFind(id);
            //var foto = new Foto()
            //{
            //    ProductoId = id
            //};
            var foto = Ifoto.fotoNew(id);
            return View("agregar", foto);
        }

        [HttpPost]
        [Route("agregar/{productId}")]
        public IActionResult Agregar(int productoId, Foto foto, IFormFile fileUpload)
        {
            var fileName = DateTime.Now.ToString("MMddyyyyhhmmss") + fileUpload.FileName;

            var path = Path.Combine(this._he.WebRootPath, "imagenes", fileName);
            var stream = new FileStream(path, FileMode.Create);
            fileUpload.CopyToAsync(stream);
            foto.Nombre = fileName;
            //_db.Fotos.Add(foto);
            //_db.SaveChanges();

            Ifoto.agregarfoto(foto);
            return RedirectToAction("index", "foto", new { area = "vendedor", id = productoId });
        }

        [HttpGet]
        [Route("eliminar/{id}/productoid")]
        public IActionResult Eliminar(int id, int productoId)
        {
            if (id <= 0)
            {
                return View();
            }
            //var foto = _db.Fotos.Find(id);
            var foto = Ifoto.getIdFind(id);



            Ifoto.eliminarFoto(foto);

            //_db.Fotos.Remove(foto);
            //_db.SaveChanges();
            return RedirectToAction("index", "foto", new { area = "vendedor", id = productoId });
        }

        [HttpGet]
        [Route("editar/{id}/{productoId}")]
        public IActionResult Editar(int id, int productoId)
        {
            ViewBag.Producto = _db.Productos.Find(productoId);
            var foto = _db.Fotos.Find(id);

            return View("editar", foto);
        }

        [HttpPost]
        [Route("editar/{fotoId}/{productoId}")]
        public IActionResult Editar(int fotoId, int productoId, Foto foto, IFormFile fileUpload)
        {
            var currentFoto = _db.Fotos.Find(foto.Id);
            if (fileUpload != null && !string.IsNullOrEmpty(fileUpload.FileName))
            {
                var fileName = DateTime.Now.ToString("MMddyyyyhhmmss") + fileUpload.FileName;
                var path = Path.Combine(this._he.WebRootPath, "imagenes", fileName);
                var stream = new FileStream(path, FileMode.Create);
                fileUpload.CopyToAsync(stream);
                currentFoto.Nombre = fileName;
            }
            currentFoto.Destacado = foto.Destacado;
            _db.SaveChanges();
            return RedirectToAction("index", "foto", new { area = "vendedor", id = productoId });
        }
    }
}
