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
    
    public partial class SCI_GARANTIADETALLE
    {
        public int IDGARANTIA { get; set; }
        public int ID { get; set; }
        public string ITEM { get; set; }
        public string DESCRIPCION { get; set; }
        public string MARCA { get; set; }
        public string MODELO { get; set; }
        public Nullable<int> CANTIDAD { get; set; }
        public Nullable<int> ESTADO { get; set; }
        public string USUARIOCREACION { get; set; }
        public string USUARIODMODIFICACION { get; set; }
        public Nullable<System.DateTime> FECHACREACION { get; set; }
        public Nullable<System.DateTime> FECHAMODIFICACION { get; set; }
        public string IPMODIFICACION { get; set; }
        public string IPCREACION { get; set; }
    
        public virtual SCI_GARANTIA SCI_GARANTIA { get; set; }
    }
}
