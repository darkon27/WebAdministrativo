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
    public class MaestroController : Controller
    {

        // GET: Maestro
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
                ViewBag.ListEstado = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 1).ToList();

                SCI_MAESTRO obMaestro = new SCI_MAESTRO();
                obMaestro.ESTADO = 1;
                ViewBag.ListTipo = MaestroService.BuscarMaestro(obMaestro);

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
            MaestroDetalleViewModels ViewMode = new MaestroDetalleViewModels();
            var Lista = MaestroService.Buscar(ViewMode.VIEW);

            return View(Lista);
        }

        public ActionResult Nuevo()
        {
            MaestroDetalleViewModels ViewMode = new MaestroDetalleViewModels();
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
                ViewBag.ListEstado = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 1).ToList();
                ViewBag.lstSalida = GlobalAdmin.lstSalida;
                SCI_MAESTRO obMaestro = new SCI_MAESTRO();
                obMaestro.ESTADO = 1;
                ViewBag.ListTipo = MaestroService.BuscarMaestro(obMaestro);

            }
            return View(ViewMode);
        }

        [HttpGet]
        public ActionResult Ver(int id)
        {
            MaestroDetalleViewModels ViewMode = new MaestroDetalleViewModels();
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
                    VIEW_MaestroDetalle obdet = new VIEW_MaestroDetalle();
                    obdet.ID = id;
                    var Lista = MaestroService.Buscar(obdet);
                    ViewBag.lstSalida = GlobalAdmin.lstSalida;
                    foreach (var intem in Lista)
                    {
                        ViewMode.VIEW = intem;
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                // Manejar el caso de concurrencia optimista, por ejemplo, recargando los datos y volviendo a intentar
                ViewBag.Message = "Registro no se pudo crear ";
            }
            return View(ViewMode);
        }

        [HttpGet]
        public ActionResult Modificar(int id)
        {
            MaestroDetalleViewModels ViewMode = new MaestroDetalleViewModels();
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
                ViewBag.lstSalida = GlobalAdmin.lstSalida;
                List<SCI_MAESTRODETALLE> listMAESTRODETALLE = (List<SCI_MAESTRODETALLE>)Session["VIEW_MAESTRODETALLE"];
                ViewMode.ListEstado = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 1).ToList();

                SCI_MAESTRO obMaestro = new SCI_MAESTRO();
                obMaestro.ESTADO = 1;
                ViewMode.ListTipo = MaestroService.BuscarMaestro(obMaestro);

                VIEW_MaestroDetalle obdet = new VIEW_MaestroDetalle();
                obdet.ID = id;
                var Lista = MaestroService.Buscar(obdet);
                foreach (var intem in Lista)
                {
                    ViewMode.VIEW = intem;
                }
            }
            return View(ViewMode);
        }

        [HttpPost]
        public ActionResult Guardar(MaestroDetalleViewModels ViewMode)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                // Manejar el caso en que el modelo no sea válido, por ejemplo, retornando la vista con los errores
                return View(ViewMode);
            }

            // Guardar los cambios en la base de datos
            try
            {
                SCI_MAESTRODETALLE obj = new SCI_MAESTRODETALLE();
                obj.ID = 0;
                obj.IDMAESTRO = ViewMode.VIEW.IDMAESTRO;
                obj.CODIGO = ViewMode.VIEW.CODIGO;
                var List1 = MaestroService.Validacion(obj);

                if (List1.Count > 0)
                {
                    TempData["Message"] = "El Codigo ya existe ";
                    TempData["MessageType"] = "warning";
                    return View(ViewMode);
                }

                obj = new SCI_MAESTRODETALLE();
                obj.ID = 0;
                obj.IDMAESTRO = ViewMode.VIEW.IDMAESTRO;
                obj.NOMBRE = ViewMode.VIEW.NOMBRE;
                var List2 = MaestroService.Validacion(obj);

                if (List2.Count > 0)
                {
                    TempData["Message"] = "El Nombre ya existe ";
                    TempData["MessageType"] = "warning";
                    return View(ViewMode);
                }

                ViewMode.Detalle = new SCI_MAESTRODETALLE();
                ViewMode.Detalle.CODIGO = ViewMode.VIEW.CODIGO;
                ViewMode.Detalle.IDMAESTRO = ViewMode.VIEW.IDMAESTRO;
                ViewMode.Detalle.NOMBRE = ViewMode.VIEW.NOMBRE;
                ViewMode.Detalle.DESCRIPCION = ViewMode.VIEW.DESCRIPCION;
                ViewMode.Detalle.ESTADO = ViewMode.VIEW.ESTADO;
                ViewMode.Detalle.VERSION = 1;
                var resul = MaestroService.Insertar(ViewMode);
                TempData["Message"] = resul.Message;
                TempData["MessageType"] = "success";
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
                TempData["MessageType"] = "danger";
            }

            // Redirigir al usuario a la página de índice
            return RedirectToAction("Nuevo");
        }

        [HttpPost]
        public ActionResult Actualizar(MaestroDetalleViewModels ViewMode)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                // Manejar el caso en que el modelo no sea válido, por ejemplo, retornando la vista con los errores
                return View(ViewMode);
            }

            // Guardar los cambios en la base de datos
            try
            {
                SCI_MAESTRODETALLE obj = new SCI_MAESTRODETALLE();
                obj.ID = ViewMode.VIEW.ID;
                obj.IDMAESTRO = ViewMode.VIEW.IDMAESTRO;
                obj.CODIGO = ViewMode.VIEW.CODIGO;
                var List1 = MaestroService.Validacion(obj);

                if (List1.Count > 1)
                {
                    ViewBag.Message = "El Codigo ya existe ";
                    return View(ViewMode);
                }

                obj = new SCI_MAESTRODETALLE();
                obj.ID = ViewMode.VIEW.ID;
                obj.IDMAESTRO = ViewMode.VIEW.IDMAESTRO;
                obj.NOMBRE = ViewMode.VIEW.NOMBRE;
                var List2 = MaestroService.Validacion(obj);

                if (List2.Count > 1)
                {
                    ViewBag.Message = "El Nombre ya existe ";
                    return View(ViewMode);
                }
                ViewMode.Detalle = new SCI_MAESTRODETALLE();
                var resul = MaestroService.Modificar(2,ViewMode);

            }
            catch (DbUpdateConcurrencyException)
            {
                // Manejar el caso de concurrencia optimista, por ejemplo, recargando los datos y volviendo a intentar
                ViewBag.Message = "Registro no se pudo actualizar ";
            }

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
            // Guardar los cambios en la base de datos
            try
            {
                MaestroDetalleViewModels ViewMode = new MaestroDetalleViewModels();
                VIEW_MaestroDetalle Viewrr = new VIEW_MaestroDetalle();
                // Inicializar la propiedad VIEW dentro de MaestroDetalleViewModels
                ViewMode.VIEW = Viewrr; // Supongamos que ViewModel es el tipo de la propiedad VIEW

                ViewMode.VIEW.ID = id;
                ViewMode.VIEW.ESTADO = 2;
                var resul = MaestroService.Modificar(3,ViewMode);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Manejar el caso de concurrencia optimista, por ejemplo, recargando los datos y volviendo a intentar
                ViewBag.Message = "Registro no se pudo actualizar ";
            }

            // Redirigir al usuario a la página de índice
            return RedirectToAction("Index");
        }

        public JsonResult Buscar(VIEW_MaestroDetalle Detalle)
        {
            //   ObjDetalle.ESTADO = 1;
            var Lista = MaestroService.Buscar(Detalle);
            object json = new { data = Lista };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

    }
}