using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.PostDtos
{
    public class DentistaDto
    {
        public string ID_Dentista { get; set; }
        public string Nombre_Den { get; set; }
        public string Apellido1_Den { get; set; }
        public string Apellido2_Den { get; set; }
        public string Direccion_Den { get; set; }
        public DateOnly FechaNacimiento_Den { get; set; }
        public string Telefono_Den { get; set; }
        public string Correo_Den { get; set; }
        public string ID_Funcionario { get; set; }
    }
}
