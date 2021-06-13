using Microsoft.EntityFrameworkCore;
using ProyectoTiendaOnline.Contenedor;
using ProyectoTiendaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ProyectoTiendaOnline.Services
{
    public class CategoriaService : ICategoriaContenedor
    {
        private DataBaseContext _db;
        public CategoriaService(DataBaseContext _db)
        {
            this._db = _db;
        }

        public void agregarSubCategoria(Categoria subcategoria)
        {
            _db.Categorias.Add(subcategoria);
            _db.SaveChanges();
        }

        public void categoiaNuevo(Categoria categoria)
        {
            categoria.Parent = null;
            _db.Categorias.Add(categoria);
            _db.SaveChanges();
        }

        public Categoria categoriaProduct(int id)
        {
            return _db.Categorias.Include(o => o.Productos).ThenInclude(y => y.Fotos).FirstOrDefault(x => x.Id == id);
        }

        public Categoria categoriawSubCateg(int id)
        {
            var subcategoria = new Categoria()
            {
                ParentId = id
            };
            return subcategoria;
        }

        public void editarCategoria(int id, Categoria categoria)
        {
            var categoriaActual = _db.Categorias.Find(id);
            categoriaActual.Nombre = categoria.Nombre;
            _db.SaveChanges();
        }

        public void eliminarCategoria(int id)
        {
            var categoria = _db.Categorias.Find(id);
            _db.Categorias.Remove(categoria);
            _db.SaveChanges();

        }

        public Categoria getCategoria(int id)
        {
            return _db.Categorias.Include(o => o.Productos).ThenInclude(y => y.Fotos).FirstOrDefault(x => x.Id == id);

        }

        public Categoria getIdCategoria(int id)
        {
            var categoria = _db.Categorias.Find(id);
            return categoria;
        }

        public List<Categoria> listaCatergoria()
        {
            return _db.Categorias.Where(c => c.Parent == null).Include(p => p.InverseParents).ToList();
        }

        public List<Categoria> topalist(Categoria categoria, int numPagina)
        {
            return (List<Categoria>)categoria.Productos.ToList().ToPagedList(numPagina, 9);
        }

    }
}
