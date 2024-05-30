using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Transactions;
using WebAdministrativo.Models;
using WebAdministrativo.ViewModels;
using System.Data.Entity.Infrastructure;
using WebAdministrativo.Service;
using WebAdministrativo.Clases;
using System.Data.Entity.Validation;

namespace WebAdministrativo.Controllers
{
    public class GarantiaController : Controller
    {
        private BDIntegrityEntities context = new BDIntegrityEntities();
        // GET: Garantia
        public ActionResult Index()
        {
            GarantiaViewModels ViewMode = new GarantiaViewModels();
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
                ViewBag.ListEstado = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 22).ToList();
            }

            //menu;
            var listitem = GlobalAdmin.lstSalida.Where(x => x.IsActive == "nav-parent nav-expanded nav-active");
            int id = 0;
            foreach (var item in listitem)
            {
                id = item.IDGRUPO;
            }
            UtilScripts.ActualizarCampo(id, "nav-parent");
            UtilScripts.ActualizarCampo(4, "nav-parent nav-expanded nav-active");
            ViewBag.lstSalida = GlobalAdmin.lstSalida;


            if (Session["ResultadosBusqueda"] != null)
            {
                var resultadosBusqueda = (List<VIEW_Garantia>)Session["ResultadosBusqueda"];
                ViewBag.ListGarantia = resultadosBusqueda;
                Session.Remove("ResultadosBusqueda");
            }
            else
            {
                var ListGarantia = context.VIEW_Garantia.Where(x => x.IDCONTACTO == GlobalAdmin.IdUsuarioAdmin).ToList();
                ViewBag.ListGarantia = ListGarantia;
            }
            return View(ViewBag.ListGarantia);
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
                UtilScripts.ActualizarCampo(id, "nav-parent");
                UtilScripts.ActualizarCampo(4, "nav-parent nav-expanded nav-active");

                ViewBag.lstSalida = GlobalAdmin.lstSalida;

            }
            return View();
        }
        

        public ActionResult Nuevo()
        {
            GarantiaViewModels ViewMode = new GarantiaViewModels();
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
                ViewBag.ListTipo = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 21).ToList();
                ViewBag.ListContacto = context.VIEW_Accesos.Where(x => x.ESTADO == 1 && x.TIPOUSUARIO != 2).ToList();
                ViewBag.ListEmpresa = context.SCI_PERSONA.Where(x => x.ESTADO == 1 && x.TIPODOCUMENTO == "R").ToList();
                ViewMode.ListaDetalle = context.VIEW_GarantiaDetalle.Where(x => x.IDGARANTIA == 0).ToList();
                ViewBag.lstSalida = GlobalAdmin.lstSalida;
            }
            return View(ViewMode);
        }

        [HttpPost]
        public ActionResult Guardar(GarantiaViewModels ViewModels)
        {            
            ViewModels.Garantia.ESTADO = 1;
            ViewModels.Garantia.FECHACREACION = DateTime.Now; // Actualizar la fecha de modificación
            ViewModels.Garantia.USUARIOCREACION = GlobalAdmin.UserAdmin; // Actualizar la fecha de modificación
            ViewModels.Garantia.IDCONTACTO = GlobalAdmin.IdUsuarioAdmin;
            ViewModels.Garantia.FECHAREGISTRO = DateTime.Now;
            // Guardar los cambios en la base de datos
            try
            {
                ViewModels.Garantia.IDGARANTIA = GarantiaService.InsertarGarantias(ViewModels);
                TempData["ErrorMessage"] = "Registro Exitoso";
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
            }
            //catch (DbUpdateConcurrencyException)
            //{
            //    // Manejar el caso de concurrencia optimista, por ejemplo, recargando los datos y volviendo a intentar
            //    //context.Entry(ViewModels.Garantia).Reload();
            //    //context.SaveChanges();
            //    ViewBag.Message = "Registro Forzado ";
            //}

            //ViewBag.Message = "Registro Forzado ";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Ver(int id)
        {
            GarantiaViewModels ViewMode = new GarantiaViewModels();
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
                var Lista = context.VIEW_Garantia.Where(x => x.IDGARANTIA == id).ToList();
                var Listadetalle = context.VIEW_GarantiaDetalle.Where(x => x.IDGARANTIA == id).ToList();
                foreach (var intem in Lista)
                {
                    ViewMode.VIEW = intem;
                }
                ViewMode.ListaDetalle = Listadetalle;
                ViewBag.lstSalida = GlobalAdmin.lstSalida;
            }
            return View(ViewMode);
        }

        public ActionResult Asignar()
        {
            GarantiaViewModels ViewMode = new GarantiaViewModels();
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
                ViewBag.ListEstado = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 22).ToList();


                //menu;
                var listitem = GlobalAdmin.lstSalida.Where(x => x.IsActive == "nav-parent nav-expanded nav-active");
                int id = 0;
                foreach (var item in listitem)
                {
                    id = item.IDGRUPO;
                }
                UtilScripts.ActualizarCampo(id, "nav-parent");
                UtilScripts.ActualizarCampo(4, "nav-parent nav-expanded nav-active");

                ViewBag.lstSalida = GlobalAdmin.lstSalida;
            }
            DateTime fecFiltro = DateTime.Now.AddDays(-30);
            var ListGarantia = context.VIEW_Garantia.Where(x => x.FECHAREGISTRO > fecFiltro).ToList();
            return View(ListGarantia);
        }

        [HttpGet]
        public ActionResult Modificar(int id)
        {
            GarantiaViewModels ViewMode = new GarantiaViewModels();
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
                var Lista = context.VIEW_Garantia.Where(x => x.IDGARANTIA == id).ToList();
                var Listadetalle = context.VIEW_GarantiaDetalle.Where(x => x.IDGARANTIA == id).ToList();

                List<SCI_MAESTRODETALLE> listMAESTRODETALLE = (List<SCI_MAESTRODETALLE>)Session["VIEW_MAESTRODETALLE"];
                ViewBag.ListTipo = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 21).ToList();
                ViewBag.ListContacto = context.VIEW_Accesos.Where(x => x.ESTADO == 1 && x.TIPOUSUARIO != 2).ToList();
                ViewBag.ListEmpresa = context.SCI_PERSONA.Where(x => x.ESTADO == 1 && x.TIPODOCUMENTO == "R").ToList();

                foreach (var intem in Lista)
                {
                    ViewMode.VIEW = intem;
                    if (!string.IsNullOrEmpty(ViewMode.VIEW.FECHAINICIO.ToString()))
                    {

                    }
                    else
                    {
                        ViewMode.VIEW.FECHAINICIO = DateTime.Now;
                    }
                }
                ViewMode.ListaDetalle = Listadetalle;
                ViewBag.lstSalida = GlobalAdmin.lstSalida;
            }
            return View(ViewMode);
        }

        [HttpPost]
        public ActionResult Actualizar(GarantiaViewModels ViewModels)
        {
            //VIEW_Garantia Garantia = new VIEW_Garantia();
            ViewModels.VIEW.ESTADO = 1;
            ViewModels.VIEW.FECHAMODIFICACION = DateTime.Now; // Actualizar la fecha de modificación
            ViewModels.VIEW.USUARIODMODIFICACION = GlobalAdmin.UserAdmin; // Actualizar la fecha de modificación
            ViewModels.VIEW.IDCONTACTO = GlobalAdmin.IdUsuarioAdmin;
            ViewModels.VIEW.FECHAREGISTRO = DateTime.Now;
            ViewModels.VIEW.IPMODIFICACION = UtilScripts.ObtenerIP();
            //ViewModels.VIEW = Garantia;
            try
            {
                var respt = GarantiaService.Modificar(2, ViewModels);
                TempData["ErrorMessage"] = respt.Message;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Manejar el caso de concurrencia optimista, por ejemplo, recargando los datos y volviendo a intentar
                context.Entry(ViewModels).Reload();
                context.SaveChanges();
            }
            //using (var transactionScope = new TransactionScope())
            //{
            //    // Guardar los cambios en la base de datos
            //    try
            //    {
            //        var listado = context.SCI_GARANTIA;
            //        if (listado.Count() > 0)
            //        {
            //            Idsecuencia = listado.Max(C => C.IDGARANTIA);
            //        }
            //        else
            //        {
            //            Idsecuencia = 0;
            //        }
            //        ViewModels.Garantia.IDGARANTIA = Idsecuencia + 1;
            //        context.SCI_GARANTIA.Add(ViewModels.Garantia);
            //        context.SaveChanges();
            //        int linea = 0;
            //        foreach (var odj in ViewModels.ListaDetalle)
            //        {
            //            SCI_GARANTIADETALLE EntyObj = new SCI_GARANTIADETALLE();
            //            EntyObj.IDGARANTIA = ViewModels.Garantia.IDGARANTIA;
            //            EntyObj.ITEM = odj.ITEM;
            //            EntyObj.CANTIDAD = odj.CANTIDAD;
            //            EntyObj.MARCA = odj.MARCA;
            //            EntyObj.MODELO = odj.MODELO;
            //            EntyObj.DESCRIPCION = odj.DESCRIPCION;
            //            EntyObj.ESTADO = 1;
            //            EntyObj.FECHACREACION = DateTime.Now; // Actualizar la fecha de modificación
            //            EntyObj.USUARIOCREACION = GlobalAdmin.UserAdmin; // Actualizar la fecha de modificación
            //            linea++;
            //            EntyObj.ID = linea;
            //            context.SCI_GARANTIADETALLE.Add(EntyObj);
            //            context.SaveChanges();
            //        }
            //        ViewBag.Message = "Registro Insertado";
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        // Manejar el caso de concurrencia optimista, por ejemplo, recargando los datos y volviendo a intentar
            //        context.Entry(ViewModels.Garantia).Reload();
            //        context.SaveChanges();
            //        ViewBag.Message = "Registro Forzado ";
            //    }
            //    transactionScope.Complete();
            //}
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Buscar(GarantiaViewModels _garantiaViewModels)
        {
            List<VIEW_Garantia> ListGarantia = new List<VIEW_Garantia>();
            if (_garantiaViewModels.VIEW != null)
            {
                VIEW_Garantia vIEW_Garantia = _garantiaViewModels.VIEW;
                Session["FiltroBusqueda"] = new { NOMBRECOMPLETO = vIEW_Garantia.CONTACTO, FECHAREGISTRO  = vIEW_Garantia.FECHAFIN, ESTADO = vIEW_Garantia.ESTADO, DESCRIPCION = vIEW_Garantia.DESCRIPCION};
                ListGarantia = GarantiaService.BuscarGarantias(vIEW_Garantia);
            }
            ViewBag.ListGarantia = ListGarantia;
            Session["ResultadosBusqueda"] = ListGarantia;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdAprobarEstado(int id)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                // Manejar el caso en que el modelo no sea válido, por ejemplo, retornando la vista con los errores
                return View(id);
            }

            GarantiaViewModels ViewModel = new GarantiaViewModels();
            VIEW_Garantia usuarioExistente = new VIEW_Garantia();
            usuarioExistente.IDGARANTIA =Convert.ToInt32(id);
            usuarioExistente.FECHAINICIO = DateTime.Now;
            usuarioExistente.ESTADO = 4; //  FINALIZADO
            usuarioExistente.FECHAMODIFICACION = DateTime.Now; // Actualizar la fecha de modificación
            usuarioExistente.USUARIODMODIFICACION = GlobalAdmin.UserAdmin; // Actualizar la fecha de modificación
            usuarioExistente.IPMODIFICACION = UtilScripts.ObtenerIP();
            ViewModel.VIEW = usuarioExistente;
            // Guardar los cambios en la base de datos
            try
            {
                var respt = GarantiaService.Modificar(3, ViewModel);
                TempData["ErrorMessage"] = respt.Message;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Manejar el caso de concurrencia optimista, por ejemplo, recargando los datos y volviendo a intentar
                context.Entry(usuarioExistente).Reload();
                context.SaveChanges();
            }

            // Redirigir al usuario a la página de índice
            return RedirectToAction("Asignar");
        }

        [HttpGet]
        public ActionResult modalRevertir(int id)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                // Manejar el caso en que el modelo no sea válido, por ejemplo, retornando la vista con los errores
                return View(id);
            }

            GarantiaViewModels ViewModel = new GarantiaViewModels();
            VIEW_Garantia usuarioExistente = new VIEW_Garantia();
            usuarioExistente.IDGARANTIA = Convert.ToInt32(id);
            usuarioExistente.FECHAINICIO = DateTime.Now;
            usuarioExistente.FECHAFIN = usuarioExistente.FECHAMODIFICACION;
            usuarioExistente.ESTADO = 2; // EN PROCESO
            usuarioExistente.FECHAMODIFICACION = DateTime.Now; // Actualizar la fecha de modificación
            usuarioExistente.USUARIODMODIFICACION = GlobalAdmin.UserAdmin; // Actualizar la fecha de modificación
            usuarioExistente.IPMODIFICACION = UtilScripts.ObtenerIP();
            ViewModel.VIEW = usuarioExistente;
            // Guardar los cambios en la base de datos
            try
            {
                var respt = GarantiaService.Modificar(3, ViewModel);
                TempData["ErrorMessage"] = respt.Message;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Manejar el caso de concurrencia optimista, por ejemplo, recargando los datos y volviendo a intentar
                context.Entry(usuarioExistente).Reload();
                context.SaveChanges();
            }

            // Redirigir al usuario a la página de índice
            return RedirectToAction("Asignar");
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
            GarantiaViewModels ViewModel = new GarantiaViewModels();
            VIEW_Garantia usuarioExistente = new VIEW_Garantia();
            usuarioExistente.IDGARANTIA = Convert.ToInt32(id);
            usuarioExistente.ESTADO = 5; // ANULADO
            usuarioExistente.FECHAMODIFICACION = DateTime.Now; // Actualizar la fecha de modificación
            usuarioExistente.USUARIODMODIFICACION = GlobalAdmin.UserAdmin; // Actualizar la fecha de modificación
            usuarioExistente.IPMODIFICACION = UtilScripts.ObtenerIP();
            ViewModel.VIEW = usuarioExistente;
            // Guardar los cambios en la base de datos
            try
            {
                var respt = GarantiaService.Modificar(3, ViewModel);
                TempData["ErrorMessage"] = respt.Message;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Manejar el caso de concurrencia optimista, por ejemplo, recargando los datos y volviendo a intentar
                context.Entry(usuarioExistente).Reload();
                context.SaveChanges();
            }

            // Redirigir al usuario a la página de índice
            return RedirectToAction("Asignar");
        }


    }
}