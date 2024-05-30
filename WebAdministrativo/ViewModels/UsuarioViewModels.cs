using System.Collections.Generic;
using WebAdministrativo.Models;

namespace WebAdministrativo.ViewModels
{
    public class UsuarioViewModels
    {
        public SCI_USUARIO Usuario { get; set; }
        public VIEW_Accesos VIEW { get; set; }
        public List<SCI_MAESTRODETALLE> ListTipoUsu { get; set; }
        public List<SCI_MAESTRODETALLE> ListEstado { get; set; }
    }

}