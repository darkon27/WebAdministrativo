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
    
    public partial class SCI_COTIZACION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SCI_COTIZACION()
        {
            this.SCI_COMPROBANTE = new HashSet<SCI_COMPROBANTE>();
            this.SCI_COTIZACIONDETALLE = new HashSet<SCI_COTIZACIONDETALLE>();
        }
    
        public int IDCOTIZACION { get; set; }
        public string NROCOTIZACION { get; set; }
        public int IDCLIENTE { get; set; }
        public Nullable<System.DateTime> FECHAREGISTRO { get; set; }
        public Nullable<System.DateTime> FECHAINICIO { get; set; }
        public Nullable<System.DateTime> FECHAFIN { get; set; }
        public Nullable<System.DateTime> FECHACADUCIDAD { get; set; }
        public Nullable<int> IDEMPLEADO { get; set; }
        public Nullable<int> TIPOCAMBO { get; set; }
        public string MONEDA { get; set; }
        public Nullable<decimal> MONTOAFECTO { get; set; }
        public Nullable<decimal> MONTOIMPUESTO { get; set; }
        public Nullable<decimal> MONTOTOTAL { get; set; }
        public Nullable<int> ESTADO { get; set; }
        public string USUARIOCREACION { get; set; }
        public string USUARIOMODIFICACION { get; set; }
        public Nullable<System.DateTime> FECHACREACION { get; set; }
        public Nullable<System.DateTime> FECHAMODIFICACION { get; set; }
        public string IPCREACION { get; set; }
        public string IPMODIFICACION { get; set; }
    
        public virtual SCI_CLIENTEMAST SCI_CLIENTEMAST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SCI_COMPROBANTE> SCI_COMPROBANTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SCI_COTIZACIONDETALLE> SCI_COTIZACIONDETALLE { get; set; }
        public virtual SCI_EMPLEADO SCI_EMPLEADO { get; set; }
    }
}
