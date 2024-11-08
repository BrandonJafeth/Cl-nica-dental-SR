using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.PostDtos
{
    public class UsuariosPostDto
    {
        public string ID_Usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string Token { get; set; }
        public string ID_Funcionario { get; set; }
    }

}
