using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaOnline.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public int UsuarioId { get; set; }
        public string Texto { get; set; }
        public DateTime Fecha { get; set; }
        public Factura Factura { get; set; }
        public Usuario Usuario { get; set; }
    }
}
