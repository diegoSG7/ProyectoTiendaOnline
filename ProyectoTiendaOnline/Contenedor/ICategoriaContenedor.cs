using ProyectoTiendaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaOnline.Contenedor
{
   public interface ICategoriaContenedor
    {
        Categoria getCategoria(int id);
        List<Categoria> listaCatergoria();
        void categoiaNuevo(Categoria categoria);
        void eliminarCategoria(int id);
        void editarCategoria(int id, Categoria categoria);
        void agregarSubCategoria(Categoria subcategoria);
        Categoria getIdCategoria(int id);
        Categoria categoriawSubCateg(int id);
        Categoria categoriaProduct(int id);
        List<Categoria> topalist(Categoria categoria, int numPagina);
    }
}
