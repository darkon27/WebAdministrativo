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
    
    public partial class SCI_EMPLEADO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SCI_EMPLEADO()
        {
            this.SCI_COTIZACION = new HashSet<SCI_COTIZACION>();
        }
    
        public int IDEMPLEADO { get; set; }
        public Nullable<int> IDNEGOCIO { get; set; }
        public Nullable<System.DateTime> FECHAINICIO { get; set; }
        public Nullable<System.DateTime> FECHACESE { get; set; }
        public Nullable<System.DateTime> FECHALIQUIDACION { get; set; }
        public Nullable<System.DateTime> FECHAREINGRESO { get; set; }
        public Nullable<int> TIPO_TRABAJADOR { get; set; }
        public byte[] FOTO { get; set; }
        public string TELEFONO { get; set; }
        public string ANEXO { get; set; }
        public string CELULAR { get; set; }
        public string EMAIL { get; set; }
        public Nullable<int> TIPOMONEDA { get; set; }
        public Nullable<decimal> SUELDO_ACTUAL { get; set; }
        public Nullable<decimal> SUELDO_ANTERIOR { get; set; }
        public Nullable<int> ESTADO { get; set; }
        public string USUARIOCREACION { get; set; }
        public string USUARIOMODIFICACION { get; set; }
        public Nullable<System.DateTime> FECHACREACION { get; set; }
        public Nullable<System.DateTime> FECHAMODIFICACION { get; set; }
        public string IPREACION { get; set; }
        public string IPMODIFICACION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SCI_COTIZACION> SCI_COTIZACION { get; set; }
        public virtual SCI_NEGOCIO SCI_NEGOCIO { get; set; }
        public virtual SCI_PERSONA SCI_PERSONA { get; set; }
    }
}
