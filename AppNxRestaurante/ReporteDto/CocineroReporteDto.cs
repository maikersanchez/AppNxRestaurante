using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppNxRestaurante.Dto
{
    public class CocineroReporteDto
    {
        public string IdCocinero { set; get; }
        public string VNombre { set; get; }
        public string VApellido1 { set; get; }
        public string VApellido2 { set; get; }
        public string Month { set; get; }
        public Decimal totalVentas { set; get; }
    }
}
