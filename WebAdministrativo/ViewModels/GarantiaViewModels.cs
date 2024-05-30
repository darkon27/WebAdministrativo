using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAdministrativo.Models;

namespace WebAdministrativo.ViewModels
{
    public class GarantiaViewModels
    {
        public SCI_GARANTIA Garantia { get; set; }

        public VIEW_Garantia VIEW { get; set; }

        public List<VIEW_Garantia> ListaGarantia { get; set; }

        public List<VIEW_GarantiaDetalle> ListaDetalle { get; set; }

        public List<SCI_PRODUCTO> ListaProducto { get; set; }

        public List<SCI_PERSONA> ListEmpresa { get; set; }

        public List<VIEW_Accesos> ListContacto { get; set; }

        public List<SCI_MAESTRODETALLE> ListEstado { get; set; }
    }

}