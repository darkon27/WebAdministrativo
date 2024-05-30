using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAdministrativo.ViewModels;

namespace WebAdministrativo.Clases
{
    /// <summary>Clase para métodos generales.</summary>
    /// <remarks>    /// ****************************************************************************************************************
    ///                                         ORDENAR METODOS Y FUNCIONES POR ORDEN ALFABETICO
    /// **************************************************************************************************************** </remarks>
    public class UtilScripts
    {


        #region Metodos Publicos

        public static void ActualizarCampo(int id, string nuevoValor)
        {
            if (GlobalAdmin.lstSalida != null)
            {
                // Buscar el objeto en la lista
                var item = GlobalAdmin.lstSalida.FirstOrDefault(x => x.IDGRUPO == id);
                if (item != null)
                {
                    // Actualizar el campo
                    item.IsActive = nuevoValor;
                    // Volver a guardar la lista en la sesión (esto no es necesario si la lista en sí se modifica, ya que las listas son tipos por referencia)
                    GlobalAdmin.lstSalida = GlobalAdmin.lstSalida;
                }
            }
        }



        /// <summary>Método para mostrar un mensaje informativo.</summary>
        /// <param name="pagina">this.Page de la clase.</param>
        /// <param name="mensaje">mensaje a mostrar.</param>
        public static void Alert(Page pPagina, string pMensaje)
        {
            ScriptManager.RegisterStartupScript(pPagina, pPagina.GetType(), "scriptAlert", string.Format("alert('{0}');", pMensaje), true);
        }

        /// <summary>Método para mostrar un mensaje informativo.</summary>
        /// <param name="pagina">this.Page de la clase.</param>
        /// <param name="mensaje">mensaje a mostrar.</param>
        public static void Alert(Page pPagina, string pMensaje, Button btn_Aceptar)
        {
            ScriptManager.RegisterStartupScript(pPagina, pPagina.GetType(), "scriptAlert", string.Format("alert('{0}');document.getElementById('{1}').click();", pMensaje, btn_Aceptar.ClientID), true);
        }

        public static void PopupPage(Page pPagina, string PaginaOut, string pMensaje)
        {
            ScriptManager.RegisterStartupScript(pPagina, pPagina.GetType(), "scriptAlert", string.Format("showModalDialog(\"" + "{0}" + "?bv=" + "'{1}'" + "\",\"Imprimir\",\"menubar=1,resizable=1,width=350,height=250\");", PaginaOut, pMensaje), true);
        }
        /// <summary>Método para mostrar un mensaje informativo.</summary>
        /// <param name="pagina">this.Page de la clase.</param>
        /// <param name="mensaje">mensaje a mostrar.</param>
        public static void Alert(Page pPagina, string pMensaje, ImageButton btn_Aceptar)
        {
            ScriptManager.RegisterStartupScript(pPagina, pPagina.GetType(), "scriptAlert", string.Format("alert('{0}');document.getElementById('{1}').click();", pMensaje, btn_Aceptar.ClientID), true);
        }


        public static string FillData(DataTable pData, string pValueField, string pTextField, string Valor)
        {
            string valores = "";
            try
            {
                foreach (DataRow Empall in pData.AsEnumerable())
                {
                    if (Empall[pValueField].ToString() == Valor)
                    {
                        valores = Empall[pTextField].ToString();
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
            return valores;
        }


        /// <summary>Función que obtiene el IP desde la cual el cliente está conectado.</summary>
        /// <returns></returns>
        public static string ObtenerIP()
        {
            string IP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(IP) && !IP.ToLower().Equals("unknown"))
            {
                string[] IPs = IP.Split(',');
                IP = IPs[0].Trim();
            }
            else
            {
                IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            return IP;
        }

        /// <summary>Función que obtiene el IP desde la cual el cliente está conectado.</summary>
        /// <returns></returns>
        public static string ObtenerPC()
        {
            string PC = HttpContext.Current.Request.ServerVariables["LOGON_USER"];

            if (!string.IsNullOrEmpty(PC) && !PC.ToLower().Equals("unknown"))
            {
                //PC = HttpContext.Current.User.Identity.Name;  UNMAPPED_REMOTE_USER
                string[] IPs = PC.Split(',');
                PC = IPs[0].Trim();
            }
            else
            {
                PC = HttpContext.Current.Request.ServerVariables["UNMAPPED_REMOTE_USER"];
            }

            return PC;
        }

        #endregion

        #region Funciones Publicas
               
        /// <summary>Método para leer un archivo</summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] LeerArchivo(string pRutaArchivo)
        {
            byte[] buffer;
            System.IO.FileStream fileStream = new System.IO.FileStream(pRutaArchivo, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            try
            {
                int tamano = (int)fileStream.Length;
                buffer = new byte[tamano];            // Crear buffer.
                int conteo;                            // Número actual de bytes leídos.
                int suma = 0;                          // Numero total de bytes leídos.

                // Leer hasta que el método de lectura regrese 0 (Se alcanzó el fin del stream, normalmente se consigue en la primera leída).
                while ((conteo = fileStream.Read(buffer, suma, tamano - suma)) > 0)
                    suma += conteo;
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }

        /// <summary>Método para obtener un tamaño normalizado de acuerdo a bytes.</summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToByteString(long bytes)
        {
            if (bytes > terabyte) return (bytes / terabyte).ToString("0.00 TB");
            else if (bytes > gigabyte) return (bytes / gigabyte).ToString("0.00 GB");
            else if (bytes > megabyte) return (bytes / megabyte).ToString("0.00 MB");
            else if (bytes > kilobyte) return (bytes / kilobyte).ToString("0.00 KB");
            else return bytes + " Byte(s)";
        }

        /// <summary>Método para traer una tabla de Configuracion XML de una aplicación.</summary>
        /// <param name="pAplicacion"></param>
        /// <param name="pNombreTabla"></param>
        /// <returns></returns>
        public static DataTable TraerConfiguracionXML(string pAplicacion, string pNombreTabla)
        {
            DataSet setXml = new DataSet();
            setXml.ReadXml(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format("{0}\\XML\\ConfiguracionXML.xml", pAplicacion)));
            return setXml.Tables[pNombreTabla];
        }

        /// <summary>Método para traer una tabla a partir de SysTables de una aplicación.</summary>
        /// <param name="aplicacion"></param>
        /// <param name="nombreTabla"></param>
        /// <returns></returns>
        public static DataTable TraerSysTable(string pAplicacion, string pNombreTabla)
        {
            DataSet setXml = new DataSet();
            setXml.ReadXml(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format("{0}\\XML\\SysTables.xml", pAplicacion)));
            return setXml.Tables[pNombreTabla];
        }

        /// <summary>Método para traer una tabla a partir de SysTables de una aplicación.</summary>
        /// <param name="aplicacion"></param>
        /// <param name="nombreTabla"></param>
        /// <returns></returns>
        public static DataRow TraerTableTextByValue(DataTable pTabla, string pRowKey, string pValor)
        {
            foreach (DataRow fila in pTabla.Rows)
            {
                if (fila[pRowKey].ToString() == pValor)
                {
                    return fila;
                }
            }
            return null;
        }

        #endregion

        #region Variables Privadas

        public static readonly float kilobyte = 1024;
        public static readonly float megabyte = 1024 * kilobyte;
        public static readonly float gigabyte = 1024 * megabyte;
        public static readonly float terabyte = 1024 * gigabyte;

        #endregion
    }
}
