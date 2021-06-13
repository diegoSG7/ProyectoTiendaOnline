using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaOnline.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime Creado { get; set; }
        public int ProductoId { get; set; }
        public int UsuarioId { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public int Estado { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Producto Producto { get; set; }

        public virtual ICollection<Chat> Chats { get; set; }

    }
}
