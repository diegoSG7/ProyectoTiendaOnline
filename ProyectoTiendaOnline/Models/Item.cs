using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaOnline.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Foto { get; set; }
        public int Cantidad { get; set; }

    }
}
