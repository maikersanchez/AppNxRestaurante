using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppNxRestaurante.ReporteDto
{
    public class ClienteReporteDto
    {
        public string IdCliente { set; get; }
        public string VNombre { set; get; }
        public string VApellido1 { set; get; }
        public string VApellido2 { set; get; }
        public Decimal Total { set; get; }
    }
}
