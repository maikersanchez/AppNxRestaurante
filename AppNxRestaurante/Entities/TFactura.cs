using System;
using System.Collections.Generic;

namespace AppNxRestaurante.Entities
{
    public partial class TFactura
    {
        public TFactura()
        {
            TDetalleFactura = new HashSet<TDetalleFactura>();
        }

        public long IdFactura { get; set; }
        public int IdCliente { get; set; }
        public string IdMesa { get; set; }
        public int IdCamarero { get; set; }
        public DateTime FFactura { get; set; }
        public byte BActivo { get; set; }
        public DateTime FCreacion { get; set; }
        public DateTime? FModificacion { get; set; }

        public TCamarero IdCamareroNavigation { get; set; }
        public TCliente IdClienteNavigation { get; set; }
        public TMesa IdMesaNavigation { get; set; }
        public ICollection<TDetalleFactura> TDetalleFactura { get; set; }
    }
}
