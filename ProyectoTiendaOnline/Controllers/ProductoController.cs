using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoTiendaOnline.Contenedor;
using ProyectoTiendaOnline.Helpers;
using ProyectoTiendaOnline.Models;
using X.PagedList;

namespace ProyectoTiendaOnline.Controllers
{
    [Route("producto")]
    public class ProductoController : Controller
    {
        private IProductoContenedor IProducto;
        private IUsuarioContenedor iusuario;
        private IVentaContenedor IVenta;
        private ICategoriaContenedor Icategoria;
        private DataBaseContext _db;


        public ProductoController(DataBaseContext db, IProductoContenedor IProducto, IUsuarioContenedor iusuario, IVentaContenedor IVenta, ICategoriaContenedor Icategoria)
        {
            _db = db;
            this.IProducto = IProducto;
            this.iusuario = iusuario;
            this.IVenta = IVenta;
            this.Icategoria = Icategoria;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [Route("detalles/{id}")]
        public IActionResult Detalles(int id)
        {

            var user = Convert.ToInt32(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var producto = _db.Productos.Include(o => o.Fotos).Include(a => a.Facturas).Include(o => o.Parent).Include(o => o.Usuario).Include(o => o.Comentarios).ThenInclude(y => y.Usuario).FirstOrDefault(x => x.Id == id);
            
            var factura = _db.Facturas.Include(a => a.Usuario).Include(a => a.Producto).FirstOrDefault(a => a.UsuarioId.Equals(user) && a.ProductoId == id);
            var fotodastacada = producto.Fotos.SingleOrDefault(p => p.Destacado);

            var comentarioUsuario = producto.Comentarios.FirstOrDefault(a => a.UsuarioId.Equals(user));
            ViewBag.ComentariUsuario = comentarioUsuario;

            ViewBag.Producto = producto;
            ViewBag.FotoDestacada = fotodastacada == null ? "noimage.PNG" : fotodastacada.Nombre;
            ViewBag.ProductoImagenes = producto.Fotos.ToList();
            ViewBag.factura = factura;
            ViewBag.Comentarios = producto.Comentarios;
            ViewBag.TotalComentarios = producto.Comentarios.Count();
            

            return View("Detalles");
        }

        [Route("categoria/{id}")]
        public IActionResult Categoria(int id, int? page)
        {
            var numPagina = page ?? 1;
            var categoria = Icategoria.categoriaProduct(id);
            ViewBag.Categoria = categoria;
            
            ViewBag.Productos = categoria.Productos.ToList().ToPagedList(numPagina, 9);
            return View("Categoria");
        }

        [Route("buscar")]
        public IActionResult Buscar(string palabra, int? page)
        {
            var numPagina = page ?? 1;
            var producto = IProducto.listaProductoBuscar(palabra);
            ViewBag.palabra = palabra;

            // ViewBag.Productos = IProducto.topalist(producto, numPagina);
            ViewBag.Productos = producto.ToPagedList(numPagina, 9);
            return View("buscar");
        }

        [HttpPost]
        public ActionResult AddComentario(Comentario comentario, int ProductoId)
        {

            var user = Convert.ToInt32(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == 0)
            {
                return RedirectToAction("Login", "Cliente");
            }

            else
            {
                comentario.UsuarioId = user;
                comentario.Fecha = DateTime.Now;
                _db.Comentarios.Add(comentario);

                var producto = _db.Productos.Where(o => o.Id == comentario.ProductoId).FirstOrDefault();


                if (producto.Puntaje == 0)
                {
                    producto.Puntaje = comentario.Puntaje;
                }
                else
                {
                    producto.Puntaje = (producto.Puntaje + comentario.Puntaje) / 2;
                   
                }
                    


                _db.SaveChanges();

                return RedirectToAction("Detalles", new { id = comentario.ProductoId });
            }

            
        }

        [HttpPost]
        [Route("checkout")]
        public IActionResult CheckOut(int id, int cantidad)
        {
            var user = iusuario.hisoriaClaims(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Cliente");
            }
            else
            {
               
                var cliente = _db.Usuarios.SingleOrDefault(a => a.Username.Equals(user.Value));
                //var producto = _db.Productos.SingleOrDefault(a => a.Id.Equals(id));
                var producto = IProducto.priodcutoFind(id);

                if (producto.Cantidad >= cantidad) {
                    IVenta.guardarProducto(producto, cliente, cantidad);
                }
                

            }
            return RedirectToAction("Index", "Home");
        }
    }
}
