using ProyectoTiendaOnline.Areas.Vendedor.Models.ViewModels;
using ProyectoTiendaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaOnline.Contenedor
{
    public interface IProductoContenedor
    {
        Producto getProducto(int id);
        bool getFotoDestacada(Producto producto);

        List<Producto> listaultimosProductos();
        Producto priodcutoFind(int id);
        List<Producto> listaProductoCreado();
        void eliminarProductoCreado(Producto producto);
        List<Producto> listaProductoBuscar(string palabra);
        List<Producto> topalist(List<Producto> producto, int numPagina);
        void guardarEditar(ProductoViewModel productoViewModel);
    }
}
