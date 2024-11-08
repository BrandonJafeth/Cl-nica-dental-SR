using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.PostDtos
{
    public class PagoPostDto
    {
        public Guid ID_Pago { get; set; }
        public decimal Monto_Pago { get; set; }
        public DateTime Fecha_Pago { get; set; }
        public string ID_Factura { get; set; }
        public string ID_Tipo_Pago { get; set; }
    }

}
