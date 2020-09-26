using System;
using System.Collections.Generic;

namespace AppNxRestaurante.Entities
{
    public partial class TCocinero
    {
        public TCocinero()
        {
            TDetalleFactura = new HashSet<TDetalleFactura>();
        }

        public int IdCocinero { get; set; }
        public string VNombre { get; set; }
        public string VApellido1 { get; set; }
        public string VApellido2 { get; set; }
        public byte BActivo { get; set; }
        public DateTime FCreacion { get; set; }
        public DateTime? FModificacion { get; set; }

        public ICollection<TDetalleFactura> TDetalleFactura { get; set; }
    }
}
