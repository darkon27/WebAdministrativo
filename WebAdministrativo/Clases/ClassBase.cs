using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Web;

namespace WebAdministrativo.Clases
{
    public class ClassBase
    {
        public static Boolean Email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static void EnviarCorreo(string str_pTo, string str_pCC = null, string str_pBCC = null, string asunto = null, string mensaje = null, string ruta_archivo_adjunto = null)
        {
            try
            {
                using (MailMessage obj_Mail = new MailMessage())
                {
                    obj_Mail.From = new MailAddress("mateoj@royalsystems.net", "Administrador del Sistema");

                    if (!string.IsNullOrEmpty(str_pTo))
                    {
                        obj_Mail.To.Add(new MailAddress(str_pTo));
                    }
                    if (!string.IsNullOrEmpty(str_pCC))
                    {
                        obj_Mail.CC.Add(new MailAddress(str_pCC));
                    }
                    if (!string.IsNullOrEmpty(str_pBCC))
                    {
                        obj_Mail.Bcc.Add(new MailAddress(str_pBCC));
                    }
                    obj_Mail.Subject = asunto;
                    obj_Mail.IsBodyHtml = true;
                    obj_Mail.Body = mensaje;

                    if (!string.IsNullOrEmpty(ruta_archivo_adjunto))
                    {
                        Attachment attachment = new Attachment(ruta_archivo_adjunto);
                        obj_Mail.Attachments.Add(attachment);
                    }

                    using (SmtpClient obj_Smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        obj_Smtp.EnableSsl = true; // Asegurarse de que SSL esté habilitado
                        obj_Smtp.Credentials = new System.Net.NetworkCredential("mateoj@royalsystems.net", "06mateoj2024"); // Mover a configuración segura
                        obj_Smtp.Send(obj_Mail);
                    }
                }
            }
            catch (Exception ex)
            {
                // Aquí puedes registrar el error en un log o manejar la excepción según sea necesario
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
            }
        }


    }
}