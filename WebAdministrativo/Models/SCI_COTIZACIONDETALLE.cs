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
    
    public partial class SCI_COTIZACIONDETALLE
    {
        public int ID { get; set; }
        public int IDPRODUCTO { get; set; }
        public int IDCOTIZACION { get; set; }
        public Nullable<int> CANTIDAD { get; set; }
        public Nullable<decimal> IGV { get; set; }
        public Nullable<decimal> MONTOAFECTO { get; set; }
        public Nullable<decimal> MONTOIMPUESTO { get; set; }
        public Nullable<decimal> MONTOTOTAL { get; set; }
        public Nullable<int> ESTADO { get; set; }
    
        public virtual SCI_COTIZACION SCI_COTIZACION { get; set; }
        public virtual SCI_PRODUCTO SCI_PRODUCTO { get; set; }
    }
}
