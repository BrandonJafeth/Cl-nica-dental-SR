using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.PostDtos
{
    public class CitaDto
    {
        public string ID_Cita { get; set; }
        public DateOnly Fecha_Cita { get; set; }
        public string Motivo { get; set; }
        public TimeOnly Hora_Inicio { get; set; }
        public TimeOnly Hora_Fin { get; set; }
        public string ID_Paciente { get; set; }
        public string ID_Dentista { get; set; }
        public string ID_Funcionario { get; set; }
        public string ID_EstadoCita { get; set; }
    }

}
