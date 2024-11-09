using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.PostDtos
{
    public class TratamientoDto
    {
        public string ID_Tratamiento { get; set; }
        public string Nombre_Tra { get; set; }
        public string Descripcion_Tra { get; set; }
        public string ID_TipoTratamiento { get; set; }
        public string ID_EstadoTratamiento { get; set; }
    }

}
