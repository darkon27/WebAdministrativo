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
    
    public partial class SCI_ACCIONES
    {
        public int ID_ACCION { get; set; }
        public int ID_TAREA { get; set; }
        public Nullable<int> ID_RESPONSABLE { get; set; }
        public string DESCRIPCION { get; set; }
        public Nullable<System.DateTime> FECHA { get; set; }
        public string OBSERVACION { get; set; }
        public Nullable<int> ESTADO { get; set; }
        public string USUARIOCREACION { get; set; }
        public string USUARIOMODIFICACION { get; set; }
        public Nullable<System.DateTime> FECHACREACION { get; set; }
        public Nullable<System.DateTime> FECHAMODIFICACION { get; set; }
    
        public virtual SCI_TAREAS SCI_TAREAS { get; set; }
    }
}