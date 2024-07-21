using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdministrativo.ViewModels
{
    public class TicketEstadistico
    {
        public string DESTIPOTICKET { get; set; }
        public int TOTAL_REGISTROS { get; set; }
        public int FINALIZADO { get; set; }
        public int PENDIENTE { get; set; }
        public int ANULADO { get; set; }
        public int EN_PROCESO { get; set; }
        public int PORCENTAJE { get; set; }
    }
}