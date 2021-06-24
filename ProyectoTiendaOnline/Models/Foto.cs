using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaOnline.Models
{
    public class Foto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public int ProductoId { get; set; }
        public bool Destacado { get; set; }

        public virtual Producto Producto { get; set; }
    }
}
