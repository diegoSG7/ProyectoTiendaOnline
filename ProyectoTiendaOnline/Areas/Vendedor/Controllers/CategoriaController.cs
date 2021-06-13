using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("vendedor/categoria")]
    public class CategoriaController : Controller
    {
        private DataBaseContext _db;
        private ICategoriaContenedor Icategoria;
        public CategoriaController(ICategoriaContenedor Icategoria)
        {
            //_db = db;
            this.Icategoria = Icategoria;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            //ViewBag.categorias = _db.Categorias.Where(c => c.Parent == null).Include(p => p.InverseParents).ToList();
            ViewBag.categorias = Icategoria.listaCatergoria();

            return View();
        }

        [HttpGet]
        [Route("agregar")]
        public IActionResult Agregar()
        {
            // var categoria = new Categoria();
            return View("Agregar", new Categoria());
        }

        [HttpPost]
        [Route("agregar")]
        public IActionResult Agregar(Categoria categoria)
        {
            //categoria.Parent = null;
            //_db.Categorias.Add(categoria);
            //_db.SaveChanges();
            Icategoria.categoiaNuevo(categoria);
            return RedirectToAction("Index", "categoria", new { area = "vendedor" });
        }

        [HttpGet]
        [Route("eliminar/{id}")]
        public IActionResult Elimiar(int id)
        {
            if (id <= 0)
            {
                return View();
            }
            //var categoria = _db.Categorias.Find(id);
            //_db.Categorias.Remove(categoria);
            //_db.SaveChanges();
            Icategoria.eliminarCategoria(id);
            return RedirectToAction("Index", "categoria", new { area = "vendedor" });
        }

        [HttpGet]
        [Route("editar/{id}")]
        public IActionResult Editar(int id)
        {
            // var categoria = _db.Categorias.Find(id);
            var categoria = Icategoria.getIdCategoria(id);
            return View("Editar", categoria);
        }

        [HttpPost]
        [Route("editar/{id}")]
        public IActionResult Editar(int id, Categoria categoria)
        {
            //var categoriaActual = _db.Categorias.Find(id);
            //categoriaActual.Nombre = categoria.Nombre;
            //_db.SaveChanges();
            Icategoria.editarCategoria(id, categoria);
            return RedirectToAction("Index", "categoria", new { area = "vendedor" });
        }

        [HttpGet]
        [Route("agregarsubcategoria/{id}")]
        public IActionResult AgregarSubCategoria(int id)
        {
            //var subcategoria = new Categoria()
            //{
            //    ParentId = id
            //};
            var subcategoria = Icategoria.categoriawSubCateg(id);
            return View("AgregarSubCategoria", subcategoria);
        }

        [HttpPost]
        [Route("agregarsubcategoria/{categoriaId}")]
        public IActionResult AgregarSubCategoria(Categoria subcategoria)
        {
            //_db.Categorias.Add(subcategoria);
            //_db.SaveChanges();
            Icategoria.agregarSubCategoria(subcategoria);
            return RedirectToAction("Index", "categoria", new { area = "vendedor" });
        }
    }
}
