using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAdministrativo.Models;

namespace WebAdministrativo.ViewModels
{
    public class TareaViewModels
    {
        public SCI_TAREAS Tareas { get; set; }

        public VIEW_TareaAsignadas VIEW { get; set; }

        public List<VIEW_TareaAsignadas> ListaTareas { get; set; }

        public List<VIEW_Accion> ListaAcciones { get; set; }

        public  List<SCI_ALERTAS> ListaAlertas { get; set; }

        public List<string> Straccion { get; set; }        

        public SCI_ACCIONES Acciones { get; set; }

        public List<VIEW_Accesos> ListSOLICITANTE { get; set; }

        public List<VIEW_Accesos> ListRESPONSABLE { get; set; }

        public List<VIEW_Accesos> ListALTERNO { get; set; }

        public List<VIEW_Accesos> ListINVOLUCRADO { get; set; }
        

        public List<SCI_MAESTRODETALLE> ListArea { get; set; }

        public List<SCI_MAESTRODETALLE> ListTipoTarea { get; set; }

        public List<SCI_MAESTRODETALLE> ListEstado { get; set; }
    }
}