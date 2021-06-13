using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaOnline.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Detalles { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public double Puntaje { get; set; }
        public int CategoriaId { get; set; }
        public int UsuarioId { get; set; }

        public virtual Categoria Parent { get; set; }
        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<Foto> Fotos { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }

    }
}
