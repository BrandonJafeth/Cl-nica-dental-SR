using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.PostDtos
{
    public class HistorialMedicoPostDto
    {
        public string ID_HistorialMedico { get; set; }
        public DateTime Fecha_Historial { get; set; }
        public string Diagnostico { get; set; }
        public string ID_Paciente { get; set; }
    }

}
