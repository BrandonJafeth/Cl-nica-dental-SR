using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.PostDtos
{
    public class TipoTratamientoPostDto
    {
        public string ID_TipoTratamiento { get; set; } = null!;
        public string? Nombre_Tipo_Tratamiento { get; set; }
        public string? Descripcion_Tipo_Tratamiento { get; set; }
    }
}
