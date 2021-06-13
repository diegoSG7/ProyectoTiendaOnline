using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoTiendaOnline.Areas.Vendedor.Models.ViewModels;
using ProyectoTiendaOnline.Contenedor;
using ProyectoTiendaOnline.Models;
using ProyectoTiendaOnline.Security;

namespace ProyectoTiendaOnline.Areas.Vendedor.Controllers
{
    [Authorize(Roles = "Cliente", AuthenticationSchemes = "Schema_Vendor")]
    [Area("vendedor")]
    [Route("vendedor/producto")]
    public class ProductoController : Controller
    {
        private DataBaseContext _db;
        private IProductoContenedor Iprod;


        public ProductoController(DataBaseContext db, IProductoContenedor Iprod)
        {
            _db = db;
            this.Iprod = Iprod;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            //ViewBag.productos = _db.Productos.Include(p => p.Fotos).Include(p => p.Usuario).Include(p => p.Parent).ToList();
            ViewBag.productos = Iprod.listaProductoCreado();

            return View();
        }

        [HttpGet]
        [Route("agregar")]
        public IActionResult Agregar()
        {
            var productoViewModel = new ProductoViewModel();
            productoViewModel.Producto = new Producto();
            productoViewModel.Categorias = new List<SelectListItem>();
            var categorias = _db.Categorias.ToList();
            foreach (var categoria in categorias)
            {
                var grupo = new SelectListGroup { Name = categoria.Nombre };
                if (categoria.InverseParents != null && categoria.InverseParents.Count > 0)
                {
                    foreach (var subcategoria in categoria.InverseParents)
                    {
                        var selectListItem = new SelectListItem
                        {
                            Text = subcategoria.Nombre,
                            Value = subcategoria.Id.ToString(),
                            Group = grupo
                        };
                        productoViewModel.Categorias.Add(selectListItem);
                    }
                }
            }

            return View("Agregar", productoViewModel);
        }

        [HttpPost]
        [Route("agregar")]
        public IActionResult Agregar(ProductoViewModel productoViewModel)
        {
            _db.Productos.Add(productoViewModel.Producto);
            var user = Convert.ToInt32(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            productoViewModel.Producto.UsuarioId = user;
            _db.SaveChanges();

            var defaultFoto = new Foto
            {
                Nombre = "noimage.PNG",
                ProductoId = productoViewModel.Producto.Id,
                Destacado = true
            };
            _db.Fotos.Add(defaultFoto);
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            _db.SaveChanges();
            return RedirectToAction("index", "producto", new { area = "vendedor" });
        }

        [HttpGet]
        [Route("eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            //var producto = _db.Productos.Find(id);

            //_db.Productos.Remove(producto);
            //_db.SaveChanges();
            var producto = Iprod.priodcutoFind(id);

            Iprod.eliminarProductoCreado(producto);
            return RedirectToAction("index", "producto", new { area = "vendedor" });
        }

        [HttpGet]
        [Route("editar/{id}")]
        public IActionResult Editar(int id)
        {
            var productoViewModel = new ProductoViewModel();
            productoViewModel.Producto = _db.Productos.Find(id);
            productoViewModel.Categorias = new List<SelectListItem>();
            var categorias = _db.Categorias.ToList();
            foreach (var categoria in categorias)
            {
                var grupo = new SelectListGroup { Name = categoria.Nombre };
                if (categoria.InverseParents != null && categoria.InverseParents.Count > 0)
                {
                    foreach (var subcategoria in categoria.InverseParents)
                    {
                        var selectListItem = new SelectListItem
                        {
                            Text = subcategoria.Nombre,
                            Value = subcategoria.Id.ToString(),
                            Group = grupo
                        };
                        productoViewModel.Categorias.Add(selectListItem);
                    }
                }
            }

            return View("Editar", productoViewModel);
        }

        [HttpPost]
        [Route("editar/{id}")]
        public IActionResult Editar(int id, ProductoViewModel productoViewModel)
        {
            Iprod.guardarEditar(productoViewModel);
            //_db.Entry(productoViewModel.Producto).State = EntityState.Modified;
            //_db.SaveChanges();
            return RedirectToAction("index", "producto", new { area = "vendedor" });
        }
    }
}
