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
    
    public partial class SCI_PRODUCTODETALLE
    {
        public int ID { get; set; }
        public int IDPRODUCTO { get; set; }
        public byte[] IMAGEN { get; set; }
        public string DESCRIPCION { get; set; }
        public Nullable<int> IDCARACTERISTICA { get; set; }
        public string URL { get; set; }
        public Nullable<int> IDGRUPO { get; set; }
        public Nullable<int> ESTADO { get; set; }
    
        public virtual SCI_CARACTERISTICAS SCI_CARACTERISTICAS { get; set; }
        public virtual SCI_PRODUCTO SCI_PRODUCTO { get; set; }
    }
}