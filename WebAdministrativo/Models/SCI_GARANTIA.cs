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
    
    public partial class SCI_GARANTIA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SCI_GARANTIA()
        {
            this.SCI_GARANTIADETALLE = new HashSet<SCI_GARANTIADETALLE>();
        }
    
        public int IDGARANTIA { get; set; }
        public int IDEMPRESA { get; set; }
        public Nullable<System.DateTime> FECHAREGISTRO { get; set; }
        public string CODIGO { get; set; }
        public string DESCRIPCION { get; set; }
        public int IDCONTACTO { get; set; }
        public string EMAIL { get; set; }
        public Nullable<System.DateTime> FECHAINICIO { get; set; }
        public Nullable<System.DateTime> FECHAFIN { get; set; }
        public string REFERENCIA { get; set; }
        public string OBSERVACION { get; set; }
        public Nullable<int> ESTADO { get; set; }
        public string USUARIOCREACION { get; set; }
        public string USUARIODMODIFICACION { get; set; }
        public Nullable<System.DateTime> FECHACREACION { get; set; }
        public Nullable<System.DateTime> FECHAMODIFICACION { get; set; }
        public string IPMODIFICACION { get; set; }
        public string IPCREACION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SCI_GARANTIADETALLE> SCI_GARANTIADETALLE { get; set; }
        public virtual SCI_PERSONA SCI_PERSONA { get; set; }
        public virtual SCI_PERSONA SCI_PERSONA1 { get; set; }
    }
}