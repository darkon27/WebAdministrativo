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
    
    public partial class SCI_MENUGRUPO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SCI_MENUGRUPO()
        {
            this.SCI_MENU = new HashSet<SCI_MENU>();
        }
    
        public int IDGRUPO { get; set; }
        public string DESCRIPCION { get; set; }
        public Nullable<int> ESTADO { get; set; }
        public string ICON { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SCI_MENU> SCI_MENU { get; set; }
    }
}
