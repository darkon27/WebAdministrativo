using System.Collections.Generic;
using WebAdministrativo.Models;

namespace WebAdministrativo.ViewModels
{
    public class PersonaViewModels
    {
        public SCI_PERSONA Persona { get; set; }
        public VIEW_Persona VIEW { get; set; }
        public List<SCI_MAESTRODETALLE> ListTipoDocumento { get; set; }
        public List<SCI_MAESTRODETALLE> ListTipoPersona { get; set; }
        public List<SCI_MAESTRODETALLE> ListSexo { get; set; }
        public List<SCI_MAESTRODETALLE> ListEstCivil { get; set; }
        public List<SCI_MAESTRODETALLE> ListEstado { get; set; }

    }
}