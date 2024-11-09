using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.PostDtos
{
    public class HistorialTratamientoDto
    {
        public string ID_Historial_Tratamiento { get; set; }
        public string ID_HistorialMedico { get; set; }
        public string ID_Tratamiento { get; set; }
        public DateTime Fecha_Tratamiento { get; set; }
    }

}
