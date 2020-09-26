using System;
using System.Collections.Generic;

namespace AppNxRestaurante.Entities
{
    public partial class TDetalleFactura
    {
        public long IdDetalleFactura { get; set; }
        public long IdFactura { get; set; }
        public int IdCocinero { get; set; }
        public string VPlato { get; set; }
        public decimal DImporte { get; set; }
        public decimal? DValor { get; set; }
        public byte BActivo { get; set; }
        public DateTime FCreacion { get; set; }
        public DateTime? FModificacion { get; set; }

        public TCocinero IdCocineroNavigation { get; set; }
        public TFactura IdFacturaNavigation { get; set; }
    }
}
