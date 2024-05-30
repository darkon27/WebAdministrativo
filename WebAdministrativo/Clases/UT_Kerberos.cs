using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebAdministrativo.Clases
{
    public class UT_Kerberos
    {
        private const string Co_LlavePrivada = "P2#5o*gH";

        public static void WriteLog(string strLog)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string logFilePath = @"C:\Logs\";
            logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("dd-MM-yyyy") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            log.WriteLine(strLog);
            log.Close();
        }

        /// <summary>
        /// Encripta una cadena con una llave privada pre-establecida.
        /// </summary>
        /// <param name="str_pEncriptar">La cadena a Encriptar.</param>
        /// <returns>El criptograma resultante.</returns>
        public static string Encriptar(string str_pEncriptar)
        {
            return Encriptar(str_pEncriptar, Co_LlavePrivada);
        }

        /// <summary>
        /// Encripta una cadena con la llave privada indicada.
        /// </summary>
        /// <param name="str_pEncriptar">La cadena a Encriptar.</param>
        /// <param name="str_pKeyPrivada8carac">La llave privada usada para encriptar la cadena.</param>
        /// <returns>El criptograma resultante.</returns>
        public static string Encriptar(string str_pEncriptar, string str_pKeyPrivada8carac)
        {
            string str_ReturnValue;
            DESCryptoServiceProvider obj_DESCryptoServiceProvider;
            byte[] byt_InputArray;
            MemoryStream ms_Memory;
            CryptoStream cs_Crypto;
            byte[] byt_mIV = { 0x45, 0x32, 0xA5, 0x18, 0x67, 0x58, 0xAC, 0xBA };
            byte[] byt_mkey = { };

            try
            {
                byt_mkey = System.Text.Encoding.UTF8.GetBytes(str_pKeyPrivada8carac.Substring(0, 8));
                obj_DESCryptoServiceProvider = new DESCryptoServiceProvider();
                byt_InputArray = Encoding.UTF8.GetBytes(str_pEncriptar);
                ms_Memory = new MemoryStream();
                cs_Crypto = new CryptoStream(ms_Memory, obj_DESCryptoServiceProvider.CreateEncryptor(byt_mkey, byt_mIV), CryptoStreamMode.Write);
                cs_Crypto.Write(byt_InputArray, 0, byt_InputArray.Length);
                cs_Crypto.FlushFinalBlock();
                str_ReturnValue = Convert.ToBase64String(ms_Memory.ToArray());
            }
            catch (Exception)
            {
                str_ReturnValue = "";
            }
            finally
            {
                obj_DESCryptoServiceProvider = null;
                ms_Memory = null;
                cs_Crypto = null;
            }
            return str_ReturnValue;
        }

        /// <summary>
        /// Desencripta una cadena con una llave privada pre-establecida.
        /// </summary>
        /// <param name="str_pDesEncriptar">El criptograma a desencriptar.</param>
        /// <returns>El texto claro resultante de la desencriptación.</returns>
        public static string Desencriptar(string str_pDesEncriptar)
        {
            return Desencriptar(str_pDesEncriptar, Co_LlavePrivada);
        }

        /// <summary>
        /// Desencripta una cadena con la llave privada indicada.
        /// </summary>
        /// <param name="str_pDesEncriptar">El criptograma a desencriptar.</param>
        /// <param name="str_pKeyPrivada8carac">La llave privada usada para desencriptar la cadena.</param>
        /// <returns>Retorna la cadena desencriptada.</returns>
        public static string Desencriptar(string str_pDesEncriptar, string str_pKeyPrivada8carac)
        {
            string str_ReturnValue;
            byte[] byt_InputArray = new byte[str_pDesEncriptar.Length + 1];
            DESCryptoServiceProvider obj_DESCryptoServiceProvider;
            MemoryStream ms_Memory;
            CryptoStream cs_Crypto;
            System.Text.Encoding obj_Encoding;
            byte[] byt_mIV = { 0x45, 0x32, 0xA5, 0x18, 0x67, 0x58, 0xAC, 0xBA };
            byte[] byt_mkey = { };

            try
            {
                byt_mkey = System.Text.Encoding.UTF8.GetBytes(str_pKeyPrivada8carac.Substring(0, 8));
                obj_DESCryptoServiceProvider = new DESCryptoServiceProvider();
                byt_InputArray = Convert.FromBase64String(str_pDesEncriptar);
                ms_Memory = new MemoryStream();
                cs_Crypto = new CryptoStream(ms_Memory, obj_DESCryptoServiceProvider.CreateDecryptor(byt_mkey, byt_mIV), CryptoStreamMode.Write);
                cs_Crypto.Write(byt_InputArray, 0, byt_InputArray.Length);
                cs_Crypto.FlushFinalBlock();
                obj_Encoding = System.Text.Encoding.UTF8;
                str_ReturnValue = obj_Encoding.GetString(ms_Memory.ToArray());
            }
            catch (Exception)
            {
                str_ReturnValue = "";
            }
            finally
            {
                obj_DESCryptoServiceProvider = null;
                ms_Memory = null;
                cs_Crypto = null;
            }
            return str_ReturnValue;
        }

        /// <summary>
        /// Encripta una cadena usando el primer algoritmo de encriptación del sistema Spring.
        /// </summary>
        /// <param name="str_pPassword">El texto claro a encriptar.</param>
        /// <returns>El criptograma resultante.</returns>
        public static string EncriptarPassword(string str_pPassword)
        {
            int int_Longitud;
            int int_Contador;
            int int_Caracter;
            int int_Semilla;
            string str_Cifrado = "";
            string str_Prueba;

            str_pPassword = str_pPassword.Trim();

            int_Longitud = str_pPassword.Length;
            for (int_Contador = 0; int_Contador <= int_Longitud - 1; int_Contador++)
            {
                int_Caracter = Encoding.ASCII.GetBytes(str_pPassword[int_Contador].ToString())[0];
                int_Semilla = int_Contador + 1;
                int_Caracter += int_Semilla;
                str_Prueba = System.Convert.ToString(Convert.ToChar(int_Caracter));
                str_Cifrado += Convert.ToChar(int_Caracter);
            }
            return str_Cifrado;
        }
        /// <summary>
        /// Desencripta una cadena usando el primer algoritmo de encriptación del sistema Spring.
        /// </summary>
        /// <param name="str_pPasswordEncriptado">El criptograma a desencriptar.</param>
        /// <returns>El texto claro resultante.</returns>
        public static string DesencriptarPassword(string str_pPasswordEncriptado)
        {
            int int_Longitud;
            int int_Contador;
            int int_Caracter;
            int int_Semilla;
            string str_Cifrado = string.Empty;
            string str_Prueba;

            str_pPasswordEncriptado = str_pPasswordEncriptado.Trim();

            int_Longitud = str_pPasswordEncriptado.Length;
            for (int_Contador = 0; int_Contador <= int_Longitud - 1; int_Contador++)
            {
                int_Caracter = Encoding.ASCII.GetBytes(str_pPasswordEncriptado[int_Contador].ToString())[0];
                int_Semilla = int_Contador + 1;
                int_Caracter -= int_Semilla;
                str_Prueba = System.Convert.ToString(Convert.ToChar(int_Caracter));
                str_Cifrado += Convert.ToChar(int_Caracter);
            }
            return str_Cifrado;
        }

        /// <summary>
        /// Encripta una cadena usando el segundo algoritmo de encriptación del sistema Spring.
        /// </summary>
        /// <param name="str_pCadena">El texto claro a encriptar.</param>
        /// <returns>El criptograma resultante.</returns>
        public static string EncriptarIni(string str_pCadena)
        {
            if (str_pCadena.Trim().Length == 0)
            {
                return "";
            }

            string str_Tabla;
            string str_OutPut;
            //			string str_char;
            string str_Primero;
            string str_Segundo;
            string str_Tercero;
            //------------ antes era long
            int int_K;
            int int_M;
            int int_Pos;
            int int_Length;
            Random obj_Random = new Random();
            int_Length = str_pCadena.Trim().Length;
            str_OutPut = "";
            str_Tabla = "5736082914";

            for (int_K = 1; int_K <= 10; int_K++)
            {
                str_OutPut += string.Format("{0:000}", obj_Random.Next(0, int_K * 25));
            }

            str_OutPut += Convert.ToString(int_Length * int_Length + 100) + string.Format("{0:000}", obj_Random.Next(0, 256));

            for (int_K = 1; int_K <= int_Length; int_K++)
            {
                int_Pos = str_Tabla.IndexOf(string.Format("{0:00}", int_K).Substring(2, 1)); //pos(w_tabla,mid(string(k,"00"),2,1))
                int_M = (int_Pos) * 3 + 1;
                str_Primero = str_OutPut.Substring(0, int_Pos * 3);
                str_Segundo = string.Format("{0:000}", Encoding.ASCII.GetBytes(str_pCadena.Substring(int_K, 1))[0] + int_K * int_K);
                str_Tercero = str_OutPut.Substring(((int_Pos + 1) * 3) + 1);
                str_OutPut = str_Primero + str_Segundo + str_Tercero;
            }
            return str_OutPut;
        }

        /// <summary>
        /// Desencripta una cadena usando el segundo algoritmo de encriptación del sistema Spring.
        /// </summary>
        /// <param name="str_pCadena">El criptograma a desencriptar.</param>
        /// <returns>El texto claro resultante.</returns>
        public static string DesencriptarIni(string str_pCadena)
        {
            string str_Tabla;
            string str_OutPut;
            //			string str_char;
            //-------------------- antes era long
            int int_K;
            int int_M;
            int int_Pos;
            int int_Length;
            Random obj_Random = new Random();
            int_Length = str_pCadena.Trim().Length;
            str_OutPut = "";
            str_Tabla = "5736082914";

            if (int_Length != 36)
            {
                return "";
            }

            int_Length = Convert.ToInt32(Math.Sqrt(System.Convert.ToDouble((System.Convert.ToDecimal(str_pCadena.Substring(31, 3)) - 100))));
            for (int_K = 1; int_K <= int_Length; int_K++)
            {
                int_Pos = str_Tabla.IndexOf(string.Format("{0:00}", int_K).Substring(2, 1));
                int_M = Convert.ToInt32(str_pCadena.Substring((int_Pos) * 3 + 1, 3));
                int_M = int_M - int_K * int_K;
                str_OutPut += Convert.ToChar(int_M);
            }
            return str_OutPut;
        }
    }
}
