using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.PostDtos
{
    public class ProcedimientoDto
    {
        public string ID_Procedimiento { get; set; }
        public DateTime Fecha_Proc { get; set; }
        public string Detalles_Proc { get; set; }
        public TimeSpan Hora_Inicio_Proc { get; set; }
        public TimeSpan Hora_Fin_Proc { get; set; }
        public string ID_Tratamiento { get; set; }
        public string ID_Paciente { get; set; }
    }

}
