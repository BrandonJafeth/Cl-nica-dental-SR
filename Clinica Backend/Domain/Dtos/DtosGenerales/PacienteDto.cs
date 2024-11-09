using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.PostDtos
{
    public class PacienteDto
    {
        public string ID_Paciente { get; set; }
        public string Nombre_Pac { get; set; }
        public string Apellido1_Pac { get; set; }
        public string Apellido2_Pac { get; set; }
        public DateTime Fecha_Nacimiento_Pac { get; set; }
        public string Telefono_Pac { get; set; }
        public string Correo_Pac { get; set; }
        public string Direccion_Pac { get; set; }
    }

}
