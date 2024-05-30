using System.Collections.Generic;
using WebAdministrativo.Models;

namespace WebAdministrativo.ViewModels
{
    public class MaestroDetalleViewModels
    {
        public SCI_MAESTRODETALLE Detalle { get; set; }
        public VIEW_MaestroDetalle VIEW { get; set; }

        public List<VIEW_MaestroDetalle> ListaDetalle { get; set; }
        public List<SCI_MAESTRO> ListTipo { get; set; }
        public List<SCI_MAESTRODETALLE> ListEstado { get; set; }
    }
}