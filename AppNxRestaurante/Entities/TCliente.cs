using System;
using System.Collections.Generic;

namespace AppNxRestaurante.Entities
{
    public partial class TCliente
    {
        public TCliente()
        {
            TFactura = new HashSet<TFactura>();
        }

        public int IdCliente { get; set; }
        public string VNombre { get; set; }
        public string VApellido1 { get; set; }
        public string VApellido2 { get; set; }
        public byte BActivo { get; set; }
        public DateTime FCreacion { get; set; }
        public DateTime? FModificacion { get; set; }

        public ICollection<TFactura> TFactura { get; set; }
    }
}
