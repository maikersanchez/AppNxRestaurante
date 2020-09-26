using System;
using System.Collections.Generic;

namespace AppNxRestaurante.Entities
{
    public partial class TMesa
    {
        public TMesa()
        {
            TFactura = new HashSet<TFactura>();
        }

        public string IdMesa { get; set; }
        public int NMaxComensales { get; set; }
        public string VUbicacion { get; set; }

        public ICollection<TFactura> TFactura { get; set; }
    }
}
