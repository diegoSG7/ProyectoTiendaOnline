using Microsoft.EntityFrameworkCore;
using ProyectoTiendaOnline.Areas.Vendedor.Models.ViewModels;
using ProyectoTiendaOnline.Contenedor;
using ProyectoTiendaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using X.PagedList;

namespace ProyectoTiendaOnline.Services
{
    public class ProductoService : IProductoContenedor
    {
        private DataBaseContext _db;
        public ProductoService(DataBaseContext _db)
        {
            this._db = _db;
        }

        public void eliminarProductoCreado(Producto producto)
        {
            _db.Productos.Remove(producto);
            _db.SaveChanges();
        }

        public bool getFotoDestacada(Producto producto)
        {
            producto.Fotos.SingleOrDefault(p => p.Destacado);

            return true;
        }

        public Producto getProducto(int id)
        {
            var producto = _db.Productos.Include(o => o.Fotos).Include(o => o.Parent).Include(o => o.Usuario).Include(o => o.Comentarios).FirstOrDefault(x => x.Id == id);

            var fotodastacada = producto.Fotos.SingleOrDefault(p => p.Destacado);

            return producto;
        }

        public List<Producto> listaProductoBuscar(string palabra)
        {
            
            return _db.Productos.Include(o => o.Fotos).Include(o => o.Parent).Where(p => p.Nombre.Contains(palabra)).ToList();
        }

        public List<Producto> listaProductoCreado()
        {
            return _db.Productos.Include(p => p.Fotos).Include(p => p.Usuario).Include(p => p.Parent).ToList();
        }

        public List<Producto> listaultimosProductos()
        {
            return _db.Productos.Include(o => o.Usuario).Include(o => o.Fotos).Include(o => o.Parent).OrderByDescending(p => p.Id).Take(12).ToList();

        }

        public Producto priodcutoFind(int id)
        {
            return _db.Productos.Find(id);
        }

        public List<Producto> topalist(List<Producto> producto, int numPagina)
        {
            return (List<Producto>)producto.ToPagedList(numPagina, 9);
        }

        public void guardarEditar(ProductoViewModel productoViewModel)
        {
            _db.Entry(productoViewModel.Producto).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
