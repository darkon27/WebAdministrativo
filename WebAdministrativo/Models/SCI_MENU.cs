//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAdministrativo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SCI_MENU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SCI_MENU()
        {
            this.SCI_MENUAUTORIZACION = new HashSet<SCI_MENUAUTORIZACION>();
        }
    
        public int IDMENU { get; set; }
        public int IDGRUPO { get; set; }
        public string CONCEPTO { get; set; }
        public Nullable<int> ORDEN { get; set; }
        public string OBJETO { get; set; }
        public string URL { get; set; }
        public string RUTAIMAGEN { get; set; }
        public Nullable<int> IDMENUPADRE { get; set; }
        public Nullable<int> ESTADO { get; set; }
        public string USUARIOCREACION { get; set; }
        public string USUARIOMODIFICACION { get; set; }
        public Nullable<System.DateTime> FECHACREACION { get; set; }
        public Nullable<System.DateTime> FECHAMODIFICACION { get; set; }
        public string IPCREACION { get; set; }
        public string IPMODIFICACION { get; set; }
    
        public virtual SCI_MENUGRUPO SCI_MENUGRUPO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SCI_MENUAUTORIZACION> SCI_MENUAUTORIZACION { get; set; }
    }
}