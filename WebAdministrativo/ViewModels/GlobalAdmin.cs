using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdministrativo.ViewModels
{
    public class GlobalAdmin
    {
        public int IdMenuRequest { get; set; }
        public int IdGrupoRequest { get; set; }
        public string HrefRequest { get; set; }

        /// VALORES ESTATICOS

        public static string UserAdmin { get; set; }
        public static string UserNameAdmin { get; set; }
        public static string TipoUserAdmin { get; set; }
        public static int IdUsuarioAdmin { get; set; }
        public static int? TipoUsuario { get; set; }

        public static string Prueba { get; set; }     
      
        public static int IdMenuCap { get; set; }
        public static string HrefCap { get; set; }
        public static string FechaRegistro { get; set; }

        public static string NombreMenuGrupo { get; set; }
        public static string NombreGrupo { get; set; }
        public static string AccionOpenSetList { get; set; }
        public static bool DetectActionController { get; set; }

        ///////////////
        /// TIPO DE MODEDA 
        /// 
        public static string TipoModena { get; set; }
        public static string NombreEmpresaCabecera { get; set; }
        public static string LogoEmpresaCabecera { get; set; }
        public static string LogoEmpresaMain { get; set; }

        public static List<MenuGrupo> lstSalida { get; set; }

    }
}