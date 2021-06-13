using Microsoft.EntityFrameworkCore;
using ProyectoTiendaOnline.Contenedor;
using ProyectoTiendaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaOnline.Services
{
    public class VentaService : IVentaContenedor
    {
        private DataBaseContext _db;
        public VentaService(DataBaseContext _db)
        {
            this._db = _db;

        }
        public Factura getFacturaDetalle(int id)
        {
            var detalle = _db.Facturas.Include(p => p.Producto).FirstOrDefault();

            return detalle;
        }

        public void guardarProducto(Producto producto, Usuario cliente, int cantidad)
        {
            var factura = new Factura()
            {
                Estado = 1,
                Creado = DateTime.Now,
                ProductoId = producto.Id,
                UsuarioId = cliente.Id,
                Precio = producto.Precio,
                Cantidad = cantidad,

            };
            _db.Facturas.Add(factura);
            producto.Cantidad -= factura.Cantidad;
            _db.SaveChanges();
        }

        public void guardarVentaProcess(Factura venta)
        {
            venta.Estado = 2;
            _db.SaveChanges();
        }

        public Factura Facturadecliente(int id)
        {
            return _db.Facturas.Include(o => o.Usuario).Include(o => o.Producto).Include(y => y.Chats).ThenInclude(y => y.Usuario).FirstOrDefault(x => x.Id == id);
        }

        public List<Factura> listaHistoria(Usuario cliente)
        {
            return cliente.Facturas.ToList();
        }

        public void marcarComoRecibido(int id)
        {
            var venta = _db.Facturas.Include(o => o.Usuario).Include(o => o.Producto).FirstOrDefault(x => x.Id == id);
            venta.Estado = 3;
            _db.SaveChanges();
        }

        public Factura ventaDeTalle(int id)
        {
            return _db.Facturas.Include(o => o.Producto).Include(o => o.Usuario).Include(y => y.Chats).ThenInclude(y => y.Usuario).FirstOrDefault(x => x.Id == id);
        }

        public Factura ventaProcess(int id)
        {
            return _db.Facturas.Include(o => o.Usuario).Include(o => o.Producto).FirstOrDefault(x => x.Id == id);
        }

        public Producto ProductoFactura(Factura facturas)
        {
            return facturas.Producto;
        }

        ICollection<Chat> IVentaContenedor.ventaChat(Factura ventas)
        {
            return ventas.Chats;
        }

        public Producto ventaDEtalleProd(Factura ventas)
        {
            return ventas.Producto;
        }

        public Factura procesFactura(int id)
        {
            return _db.Facturas.Include(o => o.Usuario).Include(o => o.Producto).FirstOrDefault(x => x.Id == id);
        }

        public void guardarProcesFactura(Factura venta)
        {
            venta.Estado = 3;
            _db.SaveChanges();
        }
    }
}
