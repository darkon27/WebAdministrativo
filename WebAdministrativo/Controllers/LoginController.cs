using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Transactions;
using WebAdministrativo.Models;
using WebAdministrativo.ViewModels;
using WebAdministrativo.Clases;
using WebAdministrativo.Service;
using System.Text;
using System.Data.Entity.Validation;

namespace WebAdministrativo.Controllers
{
    public class LoginController : Controller
    {
        private BDIntegrityEntities context = new BDIntegrityEntities();
        // GET: Login
        public ActionResult Index()
        {
            GlobalAdmin.IdUsuarioAdmin = 0;
            GlobalAdmin.TipoUserAdmin = "";
            GlobalAdmin.UserAdmin = "";
            GlobalAdmin.UserNameAdmin = "";
            return View();
        }

        public ActionResult ValidateUserAdmin(VIEW_Accesos Usu)
        {
            var viewmodel = new List<VIEW_Accesos>();
            try
            {
                viewmodel = UsuarioService.Accesos(Usu);
                //   viewmodel = context.VIEW_Accesos.Where(x => x.EMAIL == Usu.EMAIL && x.PASSWORD == Usu.PASSWORD).ToList();
                // Validar los datos de inicio de sesión aquí
                if (viewmodel.Count > 0)
                {
                    foreach (var item in viewmodel)
                    {
                        if (item.ESTADO == 2)
                        {
                            TempData["Message"] = "El Usuario esta Inactivo";
                            TempData["MessageType"] = "warning";
                            return View("Index");
                        }
                        if (!string.IsNullOrEmpty(item.FECHAEXPIRACION.ToString()))
                        {
                            DateTime toFechaAhora = DateTime.Now;
                            if (Convert.ToDateTime(item.FECHAEXPIRACION.ToString()) < Convert.ToDateTime(toFechaAhora))
                            {
                                TempData["Message"] = "La fecha del Usuario a caducado";
                                TempData["MessageType"] = "warning";
                                return View("Index");
                            }
                        }

                        GlobalAdmin.IdUsuarioAdmin = item.IDUSUARIO;
                        GlobalAdmin.TipoUserAdmin = item.DESTIPOUSUARIO;
                        GlobalAdmin.TipoUsuario = item.TIPOUSUARIO;
                        GlobalAdmin.UserAdmin = item.USUARIO;
                        GlobalAdmin.UserNameAdmin = item.NOMBRECOMPLETO;
                        GlobalAdmin.lstSalida = MenuService.SetListSalida(item.DESTIPOUSUARIO);
                        Session["VIEW_Accesos"] = viewmodel;
                        Session["VIEW_MAESTRODETALLE"] = MaestroService.ListarMaestroActivos();
                        //string asunto = "Ingreso al Sistema Web";
                        //string mensaje = "<div><h2>Su Nuevo Acceso al Sistema Web </h2>";
                        //mensaje += "<p><strong>Su Usuario es : </strong>" + Usu.EMAIL + "</p>";
                        //mensaje += "<p><strong>Su Password es :</strong>" + Usu.PASSWORD + "</p>";
                        //ClassBase.EnviarCorreo(Usu.EMAIL, null, null, asunto, mensaje, null);
                    }
                    // Autenticación exitosa, redirigir a otra vista
                    return RedirectToAction("Index", "Home"); // Redirige al controlador Home y acción Index
                }
                else
                {
                    TempData["Message"] = "El Usuario NO Existe";
                    TempData["MessageType"] = "warning";
                    return View("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                string msj = "Ocurrió un error al registrar los datos. Inténtelo de nuevo.";
                string msjson = "";
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        // Mostrar el error en la consola o registrarlo en un log
                        msjson += $"  Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                    }
                }
                TempData["Message"] = msj + msjson;
                TempData["MessageType"] = "btn-danger";
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult Registrate()
        {
            GlobalAdmin.IdUsuarioAdmin = 0;
            GlobalAdmin.TipoUserAdmin = "";
            GlobalAdmin.UserAdmin = "";
            GlobalAdmin.UserNameAdmin = "";

            VIEW_MaestroDetalle Detalle = new VIEW_MaestroDetalle();
            Detalle.IDMAESTRO = 3;
            Detalle.ESTADO = 1;
            ViewBag.ListTipoDocumento = MaestroService.Buscar(Detalle);
            return View();
        }

        [HttpPost]
        public ActionResult Grabar(VIEW_Accesos Accesos)
        {
            if (!ClassBase.Email_bien_escrito(Accesos.EMAIL))
            {
                TempData["Message"] = "Ingrese bien el correo electrónico.";
                return RedirectToAction("Registrate");
            }

            if (context.SCI_PERSONA.Any(C => C.DOCUMENTO == Accesos.DOCUMENTO && C.TIPODOCUMENTO == Accesos.TIPODOCUMENTO))
            {
                TempData["Message"] = "Ya existe el documento ingresado.";
                return RedirectToAction("Registrate");
            }

            if (context.SCI_PERSONA.Any(C => C.EMAIL == Accesos.EMAIL))
            {
                TempData["Message"] = "Ya existe el correo ingresado.";
                return RedirectToAction("Registrate");
            }

            if (context.SCI_USUARIO.Any(C => C.USUARIO == Accesos.USUARIO))
            {
                TempData["Message"] = "Ya Existe el usuario.";
                return RedirectToAction("Registrate");
            }

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    int Idsecuencia = context.SCI_PERSONA.Max(C => C.IDPERSONA) + 1;
                    Accesos.IDPERSONA = Idsecuencia;
                    var objPERSONA = new SCI_PERSONA
                    {
                        TIPODOCUMENTO = Accesos.TIPODOCUMENTO,
                        DOCUMENTO = Accesos.DOCUMENTO,
                        NOMBRE = Accesos.NOMBRE.Trim(),
                        APEPATERNO = Accesos.APEPATERNO.Trim(),
                        APEMATERNO = Accesos.APEMATERNO.Trim(),
                        FECHANACIMIENTO = Accesos.FECHAEXPIRACION,
                        NOMBRECOMPLETO = $"{Accesos.NOMBRE.Trim()} {Accesos.APEPATERNO.Trim()} {Accesos.APEMATERNO.Trim()}",
                        EMAIL = Accesos.EMAIL,
                        ESTADO = 1,
                        FECHAMODIFICACION = DateTime.Now, // Actualizar la fecha de modificación
                        USUARIOMODIFICACION = Accesos.USUARIO,
                        IPMODIFICACION = UtilScripts.ObtenerIP()
                    };

                    var objUSUARIO = new SCI_USUARIO
                    {
                        USUARIO = Accesos.USUARIO,
                        PASSWORD = Accesos.PASSWORD,
                        FECHAEXPIRACION = DateTime.Now.AddYears(1),
                        TIPOUSUARIO = 2,
                        IDUSUARIO = Accesos.IDPERSONA,
                        CADUCIDAD = 1,
                        ESTADO = 1,
                        FECHACREACION = DateTime.Now, // Actualizar la fecha de modificación
                        USUARIOCREACION = Accesos.USUARIO,
                        IPCREACION = UtilScripts.ObtenerIP()
                    };

                    context.SCI_PERSONA.Add(objPERSONA);
                    context.SCI_USUARIO.Add(objUSUARIO);
                    context.SaveChanges();
                    transaction.Commit();
                    var asunto = "Ingreso al Sistema Web";
                    var mensaje = new StringBuilder();
                    mensaje.AppendLine("<div><h2>Su Nuevo Acceso al Sistema Web </h2>");
                    mensaje.AppendLine($"<p><strong>Su Usuario es : </strong>{Accesos.EMAIL}</p>");
                    mensaje.AppendLine($"<p><strong>Su Password es :</strong>{Accesos.PASSWORD}</p></div>");
                    ClassBase.EnviarCorreo(Accesos.EMAIL, null, null, asunto, mensaje.ToString(), null);
                    TempData["Message"] = "Registro exitoso. " + mensaje;
                }
                catch (DbEntityValidationException ex)
                {
                    string msj = "Ocurrió un error al registrar los datos. Inténtelo de nuevo.";
                    string msjson = "";
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            // Mostrar el error en la consola o registrarlo en un log
                            msjson += $"  Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                        }
                    }
                    transaction.Rollback();
                    TempData["Message"] = msj + msjson;
                }
            }
            return RedirectToAction("Registrate");
        }

        public ActionResult EnvioEmail()
        {
            GlobalAdmin.IdUsuarioAdmin = 0;
            GlobalAdmin.TipoUserAdmin = "";
            GlobalAdmin.UserAdmin = "";
            GlobalAdmin.UserNameAdmin = "";
            return View();
        }

        public ActionResult Profile()
        {
            VIEW_Accesos Entity = new VIEW_Accesos();
            if (Session["VIEW_Accesos"] != null)
            {
                List<VIEW_Accesos> UsuAccesos = (List<VIEW_Accesos>)Session["VIEW_Accesos"];
                foreach (var intem in UsuAccesos)
                {
                    ViewBag.UserNameAdmin = intem.NOMBRECOMPLETO;
                    ViewBag.TipoUsuarioAdmin = intem.DESTIPOUSUARIO;
                    ViewBag.IdUsuarioAdmin = intem.IDPERSONA;
                }
                GlobalAdmin.FechaRegistro = DateTime.Now.ToShortDateString();
                ViewBag.FechaRegistro = GlobalAdmin.FechaRegistro;
                List<SCI_MAESTRODETALLE> listMAESTRODETALLE = (List<SCI_MAESTRODETALLE>)Session["VIEW_MAESTRODETALLE"];
                ViewBag.ListEstado = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 1).ToList();

                //menu;
                var listitem = GlobalAdmin.lstSalida.Where(x => x.IsActive == "nav-parent nav-expanded nav-active");
                int id = 0;
                foreach (var item in listitem)
                {
                    id = item.IDGRUPO;
                }
                UtilScripts.ActualizarCampo(id, "nav-parent");
                UtilScripts.ActualizarCampo(7, "nav-parent nav-expanded nav-active");
                ViewBag.lstSalida = GlobalAdmin.lstSalida;

                Entity.IDPERSONA = ViewBag.IdUsuarioAdmin;
                var ListUsuario = UsuarioService.Buscar(Entity);
                foreach (var intem in ListUsuario)
                {
                    Entity = intem;
                }
            }

            return View(Entity);
        }



    }
}