using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAdministrativo.Models;
using WebAdministrativo.ViewModels;
using System.Data.Entity.Infrastructure;
using WebAdministrativo.Service;
using WebAdministrativo.Clases;
using System.Data.Entity.Validation;

namespace WebAdministrativo.Controllers
{
    public class PersonaController : Controller
    {
        // GET: Persona
        public ActionResult Index()
        {
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
                ViewBag.ListTipoPersona     = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 2).ToList();
                ViewBag.ListTipoDocumento   = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 3).ToList();
                ViewBag.ListEstado          = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 1).ToList();

                //menu;
                var listitem = GlobalAdmin.lstSalida.Where(x => x.IsActive == "nav-parent nav-expanded nav-active");
                int id = 0;
                foreach (var item in listitem)
                {
                    id = item.IDGRUPO;
                }
                if (id != 7)
                {
                    UtilScripts.ActualizarCampo(id, "nav-parent");
                    UtilScripts.ActualizarCampo(7, "nav-parent nav-expanded nav-active");
                }
                ViewBag.lstSalida = GlobalAdmin.lstSalida;

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
   
            VIEW_Persona Entity = new VIEW_Persona();
            //ViewMode.VIEW.IDPERSONA = 0;
            var Lista = PersonaService.Buscar(Entity);    
            return View(Lista);
        }

        public JsonResult Buscar(VIEW_Persona Detalle)
        {
            //   ObjDetalle.ESTADO = 1;
            var Lista = PersonaService.Buscar(Detalle);
            object json = new { data = Lista };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarEmpresaPorRuc(string ruc)
        {
            VIEW_Persona ViewMode = new VIEW_Persona();
            ViewMode.TIPODOCUMENTO = "R";
            ViewMode.TIPOPERSONA = "J";
            ViewMode.DOCUMENTO = ruc;
            var empresa = PersonaService.Buscar(ViewMode); // Reemplaza esto con tu lógica de obtención de empresa
            foreach ( var item in empresa) {
                ViewMode = item;
            }
            if (ViewMode != null)
            {
                return Json(new { success = true, idEmpresa = ViewMode.IDPERSONA, nombreEmpresa = ViewMode.NOMBRECOMPLETO }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "Empresa no encontrada" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Nuevo()
        {
            PersonaViewModels ViewMode = new PersonaViewModels();
            if (Session["VIEW_Accesos"] != null)
            {
                List<VIEW_Accesos> UsuAccesos = (List<VIEW_Accesos>)Session["VIEW_Accesos"];
                foreach (var intem in UsuAccesos)
                {
                    ViewBag.UserNameAdmin = intem.NOMBRECOMPLETO;
                    ViewBag.TipoUsuarioAdmin = intem.DESTIPOUSUARIO;
                    ViewBag.idu = intem.DESTIPOUSUARIO;
                }

                GlobalAdmin.FechaRegistro = DateTime.Now.ToShortDateString();
                ViewBag.FechaRegistro = GlobalAdmin.FechaRegistro;
                List<SCI_MAESTRODETALLE> listMAESTRODETALLE = (List<SCI_MAESTRODETALLE>)Session["VIEW_MAESTRODETALLE"];
                ViewMode.ListTipoPersona    = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 2).ToList();
                ViewMode.ListTipoDocumento  = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 3).ToList();
                ViewMode.ListEstado         = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 1).ToList();
                ViewMode.ListEstCivil       = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 4).ToList();
                ViewMode.ListSexo           = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 7).ToList();
                ViewBag.lstSalida = GlobalAdmin.lstSalida;
            }
            return View(ViewMode);
        }

        [HttpGet]
        public ActionResult Ver(int id)
        {
            PersonaViewModels ViewMode = new PersonaViewModels();
            try
            {
                if (Session["VIEW_Accesos"] != null)
                {
                    List<VIEW_Accesos> UsuAccesos = (List<VIEW_Accesos>)Session["VIEW_Accesos"];
                    foreach (var intem in UsuAccesos)
                    {
                        ViewBag.UserNameAdmin = intem.NOMBRECOMPLETO;
                        ViewBag.TipoUsuarioAdmin = intem.DESTIPOUSUARIO;
                        ViewBag.idu = intem.DESTIPOUSUARIO;
                    }
                    GlobalAdmin.FechaRegistro = DateTime.Now.ToShortDateString();
                    ViewBag.FechaRegistro = GlobalAdmin.FechaRegistro;
                    VIEW_Persona obdet = new VIEW_Persona();
                    obdet.IDPERSONA = id;
                    var Lista = PersonaService.Buscar(obdet);
                    foreach (var intem in Lista)
                    {
                        ViewMode.VIEW = intem;
                    }
                    ViewBag.lstSalida = GlobalAdmin.lstSalida;
                }
            }
            catch (Exception ex)
            {
                // Manejar el caso de concurrencia optimista, por ejemplo, recargando los datos y volviendo a intentar
                ViewBag.Message = ex.Message;
            }
            return View(ViewMode);
        }

        [HttpGet]
        public ActionResult Modificar(int id)
        {
            PersonaViewModels ViewMode = new PersonaViewModels();
            if (Session["VIEW_Accesos"] != null)
            {
                List<VIEW_Accesos> UsuAccesos = (List<VIEW_Accesos>)Session["VIEW_Accesos"];
                foreach (var intem in UsuAccesos)
                {
                    ViewBag.UserNameAdmin = intem.NOMBRECOMPLETO;
                    ViewBag.TipoUsuarioAdmin = intem.DESTIPOUSUARIO;
                    ViewBag.idu = intem.DESTIPOUSUARIO;
                }
                GlobalAdmin.FechaRegistro = DateTime.Now.ToShortDateString();
                ViewBag.FechaRegistro = GlobalAdmin.FechaRegistro;
                List<SCI_MAESTRODETALLE> listMAESTRODETALLE = (List<SCI_MAESTRODETALLE>)Session["VIEW_MAESTRODETALLE"];
                ViewMode.ListTipoPersona = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 2).ToList();
                ViewMode.ListTipoDocumento = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 3).ToList();
                ViewMode.ListEstado = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 1).ToList();
                ViewMode.ListEstCivil = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 4).ToList();
                ViewMode.ListSexo = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 7).ToList();
                ViewBag.lstSalida = GlobalAdmin.lstSalida;
                VIEW_Persona obdet = new VIEW_Persona();
                obdet.IDPERSONA = id;
                var Lista = PersonaService.Buscar(obdet);
                foreach (var intem in Lista)
                {
                    ViewMode.VIEW = intem;
                }
            }
            return View(ViewMode);
        }


        [HttpPost]
        public ActionResult Guardar(PersonaViewModels ViewModels)
        {
            VIEW_Persona Detalle = new VIEW_Persona();
            Detalle.TIPODOCUMENTO = ViewModels.VIEW.TIPODOCUMENTO;
            Detalle.DOCUMENTO = ViewModels.VIEW.DOCUMENTO;
            var Listaxxx = PersonaService.Buscar(Detalle);

            if (Listaxxx.Count > 0)
            {
                return RedirectToAction("Index");
            }

            SCI_PERSONA Persona = new SCI_PERSONA();
            Persona.APEMATERNO      = ViewModels.VIEW.APEMATERNO;
            Persona.APEPATERNO      = ViewModels.VIEW.APEPATERNO;

            Persona.TIPOPERSONA     = ViewModels.VIEW.TIPOPERSONA;
            Persona.TIPODOCUMENTO   = ViewModels.VIEW.TIPODOCUMENTO;
            Persona.DOCUMENTO       = ViewModels.VIEW.DOCUMENTO;
            Persona.DIRECCION       = ViewModels.VIEW.DIRECCION;
            Persona.TELEFONO        = ViewModels.VIEW.TELEFONO;
            Persona.CELULAR         = ViewModels.VIEW.CELULAR;
            Persona.EMAIL           = ViewModels.VIEW.EMAIL;

            if (Persona.TIPODOCUMENTO != "R")
            {
                Persona.ESTADOCIVIL     = ViewModels.VIEW.ESTADOCIVIL;
                Persona.SEXO            = ViewModels.VIEW.SEXO;
                Persona.FECHANACIMIENTO = ViewModels.VIEW.FECHANACIMIENTO;
                Persona.IDUBIGEO        = ViewModels.VIEW.IDUBIGEO;
                Persona.NOMBRECOMPLETO  = ViewModels.VIEW.APEMATERNO  + " "+ ViewModels.VIEW.APEPATERNO + " " + ViewModels.VIEW.NOMBRE;
            }
            else
            {    
                Persona.NOMBRECOMPLETO = ViewModels.VIEW.NOMBRE;
            }

            Persona.ESTADO = 1;
            Persona.FECHACREACION = DateTime.Now; // Actualizar la fecha de modificación
            Persona.USUARIOCREACION = GlobalAdmin.UserAdmin; // Actualizar la fecha de modificación
            Persona.IPCREACION = UtilScripts.ObtenerIP();


            try
            {
                var ValRerurn = PersonaService.Nuevo(1, Persona);
                if (ValRerurn < 1)
                {
                    return View(ViewModels);
                }
                TempData["Message"] = "Registro Exitoso";
                TempData["MessageType"] = "primary";
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException ex)
            {
                string msj = "Ocurrió un error al Guardar Garantias los datos. Inténtelo de nuevo.";
                string msjson = "";
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        // Mostrar el error en la consola o registrarlo en un log
                        msjson += $"  Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                    }
                }
                UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Error|Guardar =" + msj + msjson);
                TempData["Message"] = "Hubo un problema al guardar la garantía.";
                TempData["MessageType"] = "danger";
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Actualizar(PersonaViewModels ViewModels)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Por favor, corrija los errores en el formulario.";
                return View(ViewModels);
            }

            // Actualizar los datos del usuario existente con los nuevos datos del modelo
            var Persona = new SCI_PERSONA();
            Persona.APEMATERNO = ViewModels.VIEW.APEMATERNO;
            Persona.APEPATERNO = ViewModels.VIEW.APEPATERNO;
            Persona.IDPERSONA = ViewModels.VIEW.IDPERSONA;
            Persona.TIPOPERSONA = ViewModels.VIEW.TIPOPERSONA;
            Persona.TIPODOCUMENTO = ViewModels.VIEW.TIPODOCUMENTO;
            Persona.DOCUMENTO = ViewModels.VIEW.DOCUMENTO;
            Persona.DIRECCION = ViewModels.VIEW.DIRECCION;
            Persona.TELEFONO = ViewModels.VIEW.TELEFONO;
            Persona.CELULAR = ViewModels.VIEW.CELULAR;
            Persona.EMAIL = ViewModels.VIEW.EMAIL;
            Persona.ESTADO = ViewModels.VIEW.ESTADO;

            if (Persona.TIPODOCUMENTO != "R")
            {
                Persona.NOMBRE = ViewModels.VIEW.NOMBRE;
                Persona.ESTADOCIVIL = ViewModels.VIEW.ESTADOCIVIL;
                Persona.SEXO = ViewModels.VIEW.SEXO;
                Persona.FECHANACIMIENTO = ViewModels.VIEW.FECHANACIMIENTO;
                Persona.IDUBIGEO = ViewModels.VIEW.IDUBIGEO;
                Persona.NOMBRECOMPLETO = $"{ViewModels.VIEW.APEMATERNO} {ViewModels.VIEW.APEPATERNO} {ViewModels.VIEW.NOMBRE}";
            }
            else
            {
                Persona.NOMBRECOMPLETO = ViewModels.VIEW.NOMBRE;
            }

            Persona.FECHAMODIFICACION = DateTime.Now; // Actualizar la fecha de modificación
            Persona.USUARIOMODIFICACION = GlobalAdmin.UserAdmin;
            Persona.IPMODIFICACION = UtilScripts.ObtenerIP();

            // Guardar los cambios en la base de datos
            try
            {
                var vareturn = PersonaService.Modificar(2, Persona);
                if (vareturn < 1)
                {
                    TempData["ErrorMessage"] = "No se pudo actualizar la persona. Intente nuevamente.";
                    return View(ViewModels);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                TempData["ErrorMessage"] = "Error de concurrencia al intentar actualizar la persona. Intente nuevamente.";
                return View(ViewModels);
            }

            TempData["SuccessMessage"] = "La persona se ha actualizado correctamente.";
            // Redirigir al usuario a la página de índice
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult modalAnular(int id)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                // Manejar el caso en que el modelo no sea válido, por ejemplo, retornando la vista con los errores
                return View(id);
            }
            var Existente = new SCI_PERSONA();

            // Actualizar los datos del usuario existente con los nuevos datos del modelo  
            Existente.IDPERSONA = id;
            Existente.USUARIOMODIFICACION = GlobalAdmin.UserAdmin;
            Existente.ESTADO = 2;
            Existente.FECHAMODIFICACION = DateTime.Now; // Actualizar la fecha de modificación
            Existente.IPMODIFICACION = UtilScripts.ObtenerIP();
            // Guardar los cambios en la base de datos
            try
            {
                PersonaService.Modificar(3, Existente);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Manejar el caso de concurrencia optimista, por ejemplo, recargando los datos y volviendo a intentar
                //context.Entry(usuarioExistente).Reload();
                //context.SaveChanges();
            }

            // Redirigir al usuario a la página de índice
            return RedirectToAction("Index");
        }

        //// GET: Persona/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Persona/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Persona/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Persona/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Persona/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Persona/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Persona/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
