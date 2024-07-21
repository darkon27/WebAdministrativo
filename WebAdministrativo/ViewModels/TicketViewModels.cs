using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAdministrativo.Models;

namespace WebAdministrativo.ViewModels
{
    public class TicketViewModels
    {
        public SCI_TICKET Ticket { get; set; }
        public VIEW_Ticket VIEW { get; set; }
        public List<VIEW_Ticket> ListTicket { get; set; }
        public List<VIEW_Accesos> ListSOLICITANTE { get; set; }
        public List<VIEW_Accesos> ListRESPONSABLE { get; set; }
        public List<SCI_MAESTRODETALLE> ListTipo { get; set; }
        public List<SCI_MAESTRODETALLE> ListEstado { get; set; }
        public List<SCI_MAESTRODETALLE> ListTipoTicket { get; set; }
        
    }
}