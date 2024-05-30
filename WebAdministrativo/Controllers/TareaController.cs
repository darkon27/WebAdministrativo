using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using WebAdministrativo.Models;
using WebAdministrativo.ViewModels;
using System.Data.Entity.Infrastructure;
using WebAdministrativo.Clases;
using System.Data.Entity.Validation;

namespace WebAdministrativo.Controllers
{
    public class TareaController : Controller
    {
        private BDIntegrityEntities context = new BDIntegrityEntities();

        // GET: Tarea
        public ActionResult Index()
        {
            TareaViewModels ViewMode = new TareaViewModels();
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
                ViewBag.IdUsuarioAdmin = GlobalAdmin.IdUsuarioAdmin;
                List<SCI_MAESTRODETALLE> listMAESTRODETALLE = (List<SCI_MAESTRODETALLE>)Session["VIEW_MAESTRODETALLE"];

                ViewBag.ListTipoTarea = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 21).ToList();
                ViewBag.ListArea = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 24).ToList();
                ViewBag.ListEstado = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 22).ToList();

                ViewBag.ListSOLICITANTE = context.VIEW_Accesos.Where(x => x.ESTADO == 1).ToList();
                ViewBag.ListALTERNO = ViewBag.ListSOLICITANTE;
                ViewBag.ListINVOLUCRADO = ViewBag.ListSOLICITANTE;


                //menu;
                var listitem = GlobalAdmin.lstSalida.Where(x => x.IsActive == "nav-parent nav-expanded nav-active");
                int id = 0;
                foreach (var item in listitem)
                {
                    id = item.IDGRUPO;
                }
                if(id != 5)
                {
                    UtilScripts.ActualizarCampo(id, "nav-parent");
                    UtilScripts.ActualizarCampo(5, "nav-parent nav-expanded nav-active");
                }
                ViewBag.lstSalida = GlobalAdmin.lstSalida;


                if (GlobalAdmin.TipoUsuario == 4)
                {
                    ViewBag.ListRESPONSABLE = context.VIEW_Accesos.Where(x => x.ESTADO == 1 && x.TIPOUSUARIO != 2 && x.IDPERSONA == GlobalAdmin.IdUsuarioAdmin).ToList();
                }
                else
                {
                    ViewBag.ListRESPONSABLE = context.VIEW_Accesos.Where(x => x.ESTADO == 1 && x.TIPOUSUARIO != 2).ToList();
                }
                ViewMode.ListaTareas = context.VIEW_TareaAsignadas.Where(x => x.IDPERSONA == GlobalAdmin.IdUsuarioAdmin).ToList();
                ViewBag.ListaAcciones = context.VIEW_Accion.Where(x => x.ID_RESPONSABLE == GlobalAdmin.IdUsuarioAdmin).ToList();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            return View(ViewMode.ListaTareas);
        }

        public ActionResult Widgets()
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
                //menu;
                var listitem = GlobalAdmin.lstSalida.Where(x => x.IsActive == "nav-parent nav-expanded nav-active");
                int id = 0;
                foreach (var item in listitem)
                {
                    id = item.IDGRUPO;
                }
                if (id != 5)
                {
                    UtilScripts.ActualizarCampo(id, "nav-parent");
                    UtilScripts.ActualizarCampo(5, "nav-parent nav-expanded nav-active");
                }
                ViewBag.lstSalida = GlobalAdmin.lstSalida;
            }
            return View();
        }


        [HttpGet]
        public ActionResult Modificar(int id)
        {
            TareaViewModels ViewMode = new TareaViewModels();
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

                ViewMode.ListTipoTarea = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 21).ToList();
                ViewMode.ListArea = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 24).ToList();
                ViewMode.ListEstado = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 22).ToList();
                ViewBag.lstSalida = GlobalAdmin.lstSalida;
                ViewMode.ListRESPONSABLE = context.VIEW_Accesos.Where(x => x.ESTADO == 1 && x.TIPOUSUARIO != 2).ToList();
                ViewMode.ListSOLICITANTE = ViewMode.ListRESPONSABLE;
                ViewMode.ListALTERNO = ViewMode.ListRESPONSABLE;
                ViewMode.ListINVOLUCRADO = ViewMode.ListRESPONSABLE;
                ViewMode.ListaAcciones = context.VIEW_Accion.Where(x => x.ID_TAREA == id).ToList();
                var ListaTareas = context.VIEW_TareaAsignadas.Where(x => x.ID_TAREA == id).ToList();
                foreach (var intem in ListaTareas)
                {
                    ViewMode.VIEW = intem;
                }
         
            }
            return View(ViewMode);
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            TareaViewModels ViewMode = new TareaViewModels();
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
                ViewBag.lstSalida = GlobalAdmin.lstSalida;
                ViewBag.ListTipoTarea = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 21).ToList();
                ViewBag.ListArea = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 24).ToList();
                ViewBag.ListEstado = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 22).ToList();
         
                ViewBag.ListRESPONSABLE = context.VIEW_Accesos.Where(x => x.ESTADO == 1 && x.TIPOUSUARIO != 2).ToList();
                ViewBag.ListSOLICITANTE = ViewBag.ListRESPONSABLE;
                ViewBag.ListALTERNO = ViewBag.ListRESPONSABLE;
                ViewBag.ListINVOLUCRADO = ViewBag.ListRESPONSABLE;
            }
            return View(ViewMode);
        }

        [HttpPost]
        public ActionResult Guardar(TareaViewModels ViewModels)
        {
            if (!ModelState.IsValid)
            {
                // Manejar el caso de datos no válidos, posiblemente redirigir a una vista de error o mostrar un mensaje al usuario
            }

            int Idsecuencia = 0;
            ViewModels.Tareas.ESTADO = 1;
            ViewModels.Tareas.FECHACREACION = DateTime.Now; // Actualizar la fecha de modificación
            ViewModels.Tareas.USUARIOCREACION = GlobalAdmin.UserAdmin; // Actualizar la fecha de modificación
            ViewModels.Tareas.ID_SOLICITANTE = GlobalAdmin.IdUsuarioAdmin;
            ViewModels.Tareas.FECHAREGISTRO = DateTime.Now;

            using (var transactionScope = context.Database.BeginTransaction())
            {
                // Guardar los cambios en la base de datos
                try
                {
                    var listado = context.SCI_TAREAS;
                    if (listado.Count() > 0)
                    {
                        Idsecuencia = listado.Max(C => C.ID_TAREA);
                    }
                    else
                    {
                        Idsecuencia = 0;
                    }
                    ViewModels.Tareas.ID_TAREA = Idsecuencia + 1;

                    int? ID_AL = null;
                    if (ViewModels.Tareas.ID_ALTERNO == 0)
                    {
                        ViewModels.Tareas.ID_ALTERNO = ID_AL;
                    }

                    if (ViewModels.Tareas.ID_INVOLUCRADO == 0)
                                    {
                        ViewModels.Tareas.ID_INVOLUCRADO=ID_AL;
                    }                    

                    ViewModels.Tareas.NROREQUERIMIENTO = DateTime.Now.ToString("yyyyMM") + "-" + ViewModels.Tareas.ID_TAREA.ToString("D6");
                    context.SCI_TAREAS.Add(ViewModels.Tareas);
                    context.SaveChanges();
                    //List<SCI_ALERTAS> ListAlertas = new List<SCI_ALERTAS>();
                    if (!string.IsNullOrEmpty(ViewModels.Tareas.ID_SOLICITANTE.ToString()))
                    {
                        SCI_ALERTAS objAlert = new SCI_ALERTAS();
                        objAlert.ID = 1;
                        objAlert.IDRELACIONADO = ViewModels.Tareas.ID_TAREA;
                        objAlert.IDPERSONA = ViewModels.Tareas.ID_SOLICITANTE;
                        objAlert.TIPOALERTA = 1;
                        objAlert.PROCESO = 1;
                        objAlert.CODIGO = ViewModels.Tareas.NROREQUERIMIENTO;
                        objAlert.ESTADO = 1;
                        objAlert.USUARIOCREACION = GlobalAdmin.UserAdmin; // Actualizar la fecha de modificación
                        objAlert.FECHACREACION = DateTime.Now;
                        //ViewModels.ListaAlertas.Add(objAlert);
                        context.SCI_ALERTAS.Add(objAlert);
                    }

                    if (!string.IsNullOrEmpty(ViewModels.Tareas.ID_RESPONSABLE.ToString()))
                    {
                        SCI_ALERTAS objAlert = new SCI_ALERTAS();
                        objAlert.ID = 2;
                        objAlert.IDRELACIONADO = ViewModels.Tareas.ID_TAREA;
                        objAlert.IDPERSONA = ViewModels.Tareas.ID_RESPONSABLE;
                        objAlert.TIPOALERTA = 1;
                        objAlert.PROCESO = 1;
                        objAlert.CODIGO = ViewModels.Tareas.NROREQUERIMIENTO;
                        objAlert.ESTADO = 1;
                        objAlert.USUARIOCREACION = GlobalAdmin.UserAdmin; // Actualizar la fecha de modificación
                        objAlert.FECHACREACION = DateTime.Now;
                        //ViewModels.ListaAlertas.Add(objAlert);
                        context.SCI_ALERTAS.Add(objAlert);
                    }

                    if (!string.IsNullOrEmpty(ViewModels.Tareas.ID_ALTERNO.ToString()))
                    {
                        SCI_ALERTAS objAlert = new SCI_ALERTAS();
                        objAlert.ID = 3;
                        objAlert.IDRELACIONADO = ViewModels.Tareas.ID_TAREA;
                        objAlert.IDPERSONA = ViewModels.Tareas.ID_ALTERNO;
                        objAlert.TIPOALERTA = 1;
                        objAlert.PROCESO = 1;
                        objAlert.CODIGO = ViewModels.Tareas.NROREQUERIMIENTO;
                        objAlert.ESTADO = 1;
                        objAlert.USUARIOCREACION = GlobalAdmin.UserAdmin; // Actualizar la fecha de modificación
                        objAlert.FECHACREACION = DateTime.Now;
                        //ViewModels.ListaAlertas.Add(objAlert);
                        context.SCI_ALERTAS.Add(objAlert);
                    }

                    if (!string.IsNullOrEmpty(ViewModels.Tareas.ID_INVOLUCRADO.ToString()))
                    {
                        SCI_ALERTAS objAlert = new SCI_ALERTAS();
                        objAlert.ID = 4;
                        objAlert.IDRELACIONADO = ViewModels.Tareas.ID_TAREA;
                        objAlert.IDPERSONA = ViewModels.Tareas.ID_INVOLUCRADO;
                        objAlert.TIPOALERTA = 1;
                        objAlert.PROCESO = 1;
                        objAlert.CODIGO = ViewModels.Tareas.NROREQUERIMIENTO;
                        objAlert.ESTADO = 1;
                        objAlert.USUARIOCREACION = GlobalAdmin.UserAdmin; // Actualizar la fecha de modificación
                        objAlert.FECHACREACION = DateTime.Now;
                        //ViewModels.ListaAlertas.Add(objAlert);
                        context.SCI_ALERTAS.Add(objAlert);
                    }

                    // Aquí puedes manejar los valores seleccionados
                    if (ViewModels.Straccion != null)
                    {
                        foreach (var accion in ViewModels.Straccion)
                        {
                            // Lógica para manejar cada acción
                            System.Diagnostics.Debug.WriteLine(accion);
                        }
                    }

                    context.SaveChanges();
                    transactionScope.Commit();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            // Mostrar el error en la consola o registrarlo en un log
                            Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                        }
                    }
                    transactionScope.Rollback();
                    //throw new Exception("Error al modificar el registro.", ex);
                    //idreturn = -1;

                }
            }
            return RedirectToAction("Index");
        }
      

        [HttpGet]
        public ActionResult Ver(int id)
        {
            TareaViewModels ViewMode = new TareaViewModels();
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

                var ListaTareas = context.VIEW_TareaAsignadas.Where(x => x.ID_TAREA == id && x.IDPERSONA == GlobalAdmin.IdUsuarioAdmin).ToList();
                foreach (var intem in ListaTareas)
                {
                    ViewMode.VIEW = intem;
                }

            }
            return View(ViewMode);
        }
    }
}