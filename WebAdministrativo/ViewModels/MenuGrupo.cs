using System;
using System.Collections.Generic;

namespace WebAdministrativo.ViewModels
{
    public class MenuGrupo
    {        
        public Nullable<int> ESTADO { get; set; }
        public int IDGRUPO { get; set; }
        public string DESCRIPCION { get; set; }
        public string ICON { get; set; }
        public string IsActive { get; set; }

        public List<Menu> listaMenu_Items { get; set; }
    }

    public class GroupedResultType
    {
        public int IDGRUPO { get; set; }
        public string NOMBRE { get; set; }
    }
}