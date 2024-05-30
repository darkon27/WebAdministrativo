using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdministrativo.ViewModels
{
    public class Menu
    {
        public int IDMENU { get; set; }
        public int IDGRUPO { get; set; }
        public Nullable<int> IDPERFIL { get; set; }
        public string NOMBRE { get; set; }
        public string CONCEPTO { get; set; }
        public Nullable<int> ORDEN { get; set; }
        public string OBJETO { get; set; }
        public string URL { get; set; }
        public string DESCRIPCION { get; set; }
        public string ICON { get; set; }
    }
}