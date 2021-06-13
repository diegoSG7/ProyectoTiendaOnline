using ProyectoTiendaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaOnline.Contenedor
{
   public interface IVentaContenedor
    {
        Factura getFacturaDetalle(int id);
        Factura Facturadecliente(int id);
        List<Factura> listaHistoria(Usuario cliente);
        void marcarComoRecibido(int id);
        void guardarProducto(Producto producto, Usuario cliente, int cantidad);

        Factura ventaDeTalle(int id);
        Factura ventaProcess(int id);
        void guardarVentaProcess(Factura venta);

        Producto ProductoFactura(Factura facturas);

        Producto ventaDEtalleProd(Factura ventas);
        ICollection<Chat> ventaChat(Factura ventas);

        Factura procesFactura(int id);
        void guardarProcesFactura(Factura venta);
    }
}
