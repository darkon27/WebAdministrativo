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
    
    public partial class VIEW_TareaAsignadas
    {
        public string SOLICITANTE { get; set; }
        public string RESPONSABLE { get; set; }
        public string ALTERNO { get; set; }
        public string INVOLUCRADO { get; set; }
        public string TIPOREQUERIMIENTO { get; set; }
        public string DESESTADO { get; set; }
        public string COLORESTADO { get; set; }
        public string AREA { get; set; }
        public string FECREGISTRO { get; set; }
        public string FECINIPLAZO { get; set; }
        public string FECFINPLAZO { get; set; }
        public Nullable<int> IDPERSONA { get; set; }
        public int ID_TAREA { get; set; }
        public string NROREQUERIMIENTO { get; set; }
        public Nullable<int> TIPO { get; set; }
        public string TAREA { get; set; }
        public Nullable<int> ID_SOLICITANTE { get; set; }
        public Nullable<int> ID_RESPONSABLE { get; set; }
        public Nullable<int> ID_ALTERNO { get; set; }
        public Nullable<int> ID_INVOLUCRADO { get; set; }
        public Nullable<int> IDAREA { get; set; }
        public Nullable<System.DateTime> FECHAREGISTRO { get; set; }
        public Nullable<System.DateTime> FECHAINIPLAZO { get; set; }
        public Nullable<System.DateTime> FECHAFINPLAZO { get; set; }
        public Nullable<decimal> PRECIO { get; set; }
        public string OBSERVACION { get; set; }
        public Nullable<int> ESTADO { get; set; }
        public string USUARIOCREACION { get; set; }
        public string USUARIOMODIFICACION { get; set; }
        public Nullable<System.DateTime> FECHACREACION { get; set; }
        public Nullable<System.DateTime> FECHAMODIFICACION { get; set; }
    }
}
