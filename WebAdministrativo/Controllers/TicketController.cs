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
using WebAdministrativo.Service;
using System.Data.Entity.Validation;

namespace WebAdministrativo.Controllers
{
    public class TicketController : Controller
    {
        private BDIntegrityEntities context = new BDIntegrityEntities();
        // GET: Ticket
        public ActionResult Index()
        {
            TicketViewModels ViewMode = new TicketViewModels();
            int IdUsuarioAdmin = 0;
            if (Session["VIEW_Accesos"] != null)
            {
                List<VIEW_Accesos> UsuAccesos = (List<VIEW_Accesos>)Session["VIEW_Accesos"];
                foreach (var intem in UsuAccesos)
                {
                    ViewBag.UserNameAdmin = intem.NOMBRECOMPLETO;
                    ViewBag.TipoUsuarioAdmin = intem.DESTIPOUSUARIO;
                    ViewBag.IdUsuarioAdmin = intem.IDPERSONA;
                    IdUsuarioAdmin = intem.IDPERSONA;
                }
                GlobalAdmin.FechaRegistro = DateTime.Now.ToShortDateString();
                ViewBag.FechaRegistro = GlobalAdmin.FechaRegistro;
                List<SCI_MAESTRODETALLE> listMAESTRODETALLE = (List<SCI_MAESTRODETALLE>)Session["VIEW_MAESTRODETALLE"];
                ViewBag.ListEstado = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 22).ToList();
                ViewBag.ListTipoTicket = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 20).ToList();
                
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            //menu;
            var listitem = GlobalAdmin.lstSalida.Where(x => x.IsActive == "nav-parent nav-expanded nav-active");
            int id = 0;
            foreach (var item in listitem)
            {
                id = item.IDGRUPO;
            }
            UtilScripts.ActualizarCampo(id, "nav-parent");
            UtilScripts.ActualizarCampo(6, "nav-parent nav-expanded nav-active");
            ViewBag.lstSalida = GlobalAdmin.lstSalida;

            var ListTicket = context.VIEW_Ticket.Where(x => x.IDSOLICITANTE == IdUsuarioAdmin).ToList();
            return View(ListTicket);
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
                if (id != 7)
                {
                    UtilScripts.ActualizarCampo(id, "nav-parent");
                    UtilScripts.ActualizarCampo(7, "nav-parent nav-expanded nav-active");
                }
                ViewBag.lstSalida = GlobalAdmin.lstSalida;
            }
            return View();
        }

        [HttpGet]
        public ActionResult Ver(int id)
        {
            TicketViewModels ViewMode = new TicketViewModels();
            if (Session["VIEW_Accesos"] != null)
            {
                List<VIEW_Accesos> UsuAccesos = (List<VIEW_Accesos>)Session["VIEW_Accesos"];
                foreach (var intem in UsuAccesos)
                {
                    ViewBag.UserNameAdmin = intem.NOMBRECOMPLETO;
                    ViewBag.TipoUsuarioAdmin = intem.DESTIPOUSUARIO;
                    ViewBag.idu = intem.DESTIPOUSUARIO;
                }
                ViewBag.lstSalida = GlobalAdmin.lstSalida;
                var ListTicket = context.VIEW_Ticket.Where(x => x.IDTICKET == id).ToList();
                foreach (var intem in ListTicket)
                {
                    ViewMode.VIEW = intem;
                }
                GlobalAdmin.FechaRegistro = DateTime.Now.ToShortDateString();
                ViewBag.FechaRegistro = GlobalAdmin.FechaRegistro;
                List<SCI_MAESTRODETALLE> listMAESTRODETALLE = (List<SCI_MAESTRODETALLE>)Session["VIEW_MAESTRODETALLE"];
                ViewMode.ListTipoTicket = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 20).ToList();
                ViewMode.ListTipo = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 21).ToList();
                //ViewMode.ListEstado = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 22).ToList();

                ViewMode.ListRESPONSABLE = context.VIEW_Accesos.Where(x => x.ESTADO == 1 && x.TIPOUSUARIO != 2).ToList();
                ViewMode.ListSOLICITANTE = ViewBag.ListRESPONSABLE;

            }
            return View(ViewMode);
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            TicketViewModels ViewMode = new TicketViewModels();
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
                ViewBag.ListTipoTicket = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 20).ToList();
                ViewBag.ListRESPONSABLE = context.VIEW_Accesos.Where(x => x.ESTADO == 1 && x.TIPOUSUARIO != 2).ToList();
                ViewBag.lstSalida = GlobalAdmin.lstSalida;
            }
            return View(ViewMode);
        }

        [HttpPost]
        public ActionResult Guardar(TicketViewModels ViewModels)
        {
            int Idsecuencia = 0;
            ViewModels.Ticket.ESTADO = 1;
            ViewModels.Ticket.FECHACREACION = DateTime.Now; // Actualizar la fecha de modificación
            ViewModels.Ticket.USUARIOCREACION = GlobalAdmin.UserAdmin; // Actualizar la fecha de modificación
            ViewModels.Ticket.IDSOLICITANTE = GlobalAdmin.IdUsuarioAdmin;
            ViewModels.Ticket.FECHAREGISTRO = DateTime.Now;
            // Guardar los cambios en la base de datos
            try
            {
                var listado = context.SCI_TICKET;
                if (listado.Count() > 0)
                {
                    Idsecuencia = listado.Max(C => C.IDTICKET);
                }
                else
                {
                    Idsecuencia = 0;
                }
                ViewModels.Ticket.IDTICKET = Idsecuencia + 1;
                context.SCI_TICKET.Add(ViewModels.Ticket);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Manejar el caso de concurrencia optimista, por ejemplo, recargando los datos y volviendo a intentar
                context.Entry(ViewModels.Ticket).Reload();
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Asignar()
        {
            TicketViewModels ViewMode = new TicketViewModels();
            if (Session["VIEW_Accesos"] != null)
            {
                List<VIEW_Accesos> UsuAccesos = (List<VIEW_Accesos>)Session["VIEW_Accesos"];
                foreach (var intem in UsuAccesos)
                {
                    ViewBag.UserNameAdmin = intem.NOMBRECOMPLETO;
                    ViewBag.TipoUsuarioAdmin = intem.DESTIPOUSUARIO;
                    ViewBag.IdUsuarioAdmin = intem.IDPERSONA;
                    ViewBag.idu = intem.DESTIPOUSUARIO;
                }
                GlobalAdmin.FechaRegistro = DateTime.Now.ToShortDateString();
                ViewBag.FechaRegistro = GlobalAdmin.FechaRegistro;
                List<SCI_MAESTRODETALLE> listMAESTRODETALLE = (List<SCI_MAESTRODETALLE>)Session["VIEW_MAESTRODETALLE"];
         
                ViewBag.ListTipoTarea = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 21).ToList();
                ViewBag.ListArea = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 24).ToList();
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
            UtilScripts.ActualizarCampo(6, "nav-parent nav-expanded nav-active");
            ViewBag.lstSalida = GlobalAdmin.lstSalida;


            if (GlobalAdmin.TipoUsuario == 4)
            {
                ViewMode.ListTicket = context.VIEW_Ticket.Where(x => x.IDRESPONSABLE == GlobalAdmin.IdUsuarioAdmin).ToList();   
                ViewBag.ListRESPONSABLE = context.VIEW_Accesos.Where(x => x.ESTADO == 1 && x.TIPOUSUARIO != 2 && x.IDPERSONA == GlobalAdmin.IdUsuarioAdmin).ToList();
            }
            else
            {
                ViewBag.ListRESPONSABLE = context.VIEW_Accesos.Where(x => x.ESTADO == 1 && x.TIPOUSUARIO != 2).ToList();
                ViewMode.ListTicket = context.VIEW_Ticket.ToList();
            }
           
            return View(ViewMode.ListTicket);
        }

        [HttpGet]
        public ActionResult Modificar(int id)
        {
            TicketViewModels ViewMode = new TicketViewModels();
            if (Session["VIEW_Accesos"] != null)
            {
                List<VIEW_Accesos> UsuAccesos = (List<VIEW_Accesos>)Session["VIEW_Accesos"];
                foreach (var intem in UsuAccesos)
                {
                    ViewBag.UserNameAdmin = intem.NOMBRECOMPLETO;
                    ViewBag.TipoUsuarioAdmin = intem.DESTIPOUSUARIO;
                    ViewBag.idu = intem.DESTIPOUSUARIO;
                    ViewBag.IdUsuarioAdmin = intem.IDPERSONA;
                }
                ViewBag.lstSalida = GlobalAdmin.lstSalida;
                var ListTicket = context.VIEW_Ticket.Where(x => x.IDTICKET == id).ToList();
                foreach (var intem in ListTicket)
                {
                    ViewMode.VIEW = intem;
                    ViewMode.VIEW.FECHAINI = DateTime.Now;
                }
                GlobalAdmin.FechaRegistro = DateTime.Now.ToShortDateString();
                ViewBag.FechaRegistro = GlobalAdmin.FechaRegistro;
                List<SCI_MAESTRODETALLE> listMAESTRODETALLE = (List<SCI_MAESTRODETALLE>)Session["VIEW_MAESTRODETALLE"];
                ViewMode.ListTipoTicket = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 20).ToList();
                ViewMode.ListTipo = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 21).ToList();
                ViewMode.ListEstado = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 22).ToList();

                ViewMode.ListRESPONSABLE = context.VIEW_Accesos.Where(x => x.ESTADO == 1 && x.TIPOUSUARIO != 2).ToList();

            }
            return View(ViewMode);
        }

        [HttpPost]
        public ActionResult Actualizar(TicketViewModels ticket)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                // Manejar el caso en que el modelo no sea válido, por ejemplo, retornando la vista con los errores
                return View(ticket);
            }
            try
            {
                var Existente = new SCI_TICKET();
                Existente.IDTICKET = ticket.VIEW.IDTICKET;
                Existente.IDRESPONSABLE = ticket.VIEW.IDRESPONSABLE;
                Existente.OBSERVACION = ticket.VIEW.OBSERVACION;
                Existente.SOLUCION = ticket.VIEW.SOLUCION;
                Existente.TIPOTICKET = ticket.VIEW.TIPOTICKET;
                Existente.FECHAINI = DateTime.Now;
                Existente.ESTADO = 2; // EN PROCESO
                Existente.FECHAMODIFICACION = DateTime.Now; // Actualizar la fecha de modificación
                Existente.USUARIODMODIFICACION = GlobalAdmin.UserAdmin;
                Existente.IPMODIFICACION = UtilScripts.ObtenerIP();
                var respt = TicketService.Modificar(2, Existente);
                TempData["Message"] = "Registro Exitoso";
                TempData["MessageType"] = "primary";
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
                TempData["Message"] = "Hubo un problema al guardar la Ticket.";
                TempData["MessageType"] = "danger";
            }
            //// Obtener el usuario existente desde la base de datos
            //var usuarioExistente = context.SCI_TICKET.Find(ticket.VIEW.IDTICKET);
            //if (usuarioExistente == null)
            //{
            //    // Manejar el caso en que el usuario no existe, por ejemplo, mostrando un mensaje de error
            //    TempData["ErrorMessage"] = "El usuario no existe.";
            //    return RedirectToAction("Index");
            //}

            //usuarioExistente.IDRESPONSABLE = ticket.VIEW.IDRESPONSABLE;
            //usuarioExistente.OBSERVACION = ticket.VIEW.OBSERVACION;
            //usuarioExistente.FECHAINI = DateTime.Now;
            //usuarioExistente.ESTADO = 2; // EN PROCESO
            //usuarioExistente.FECHAMODIFICACION = DateTime.Now; // Actualizar la fecha de modificación
            //// Guardar los cambios en la base de datos
            //try
            //{
            //    context.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    // Manejar el caso de concurrencia optimista, por ejemplo, recargando los datos y volviendo a intentar
            //    context.Entry(usuarioExistente).Reload();
            //    context.SaveChanges();
            //}

            // Redirigir al usuario a la página de índice
            return RedirectToAction("Asignar");
        }

        [HttpGet]
        public ActionResult modalAprobar(int id)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                // Manejar el caso en que el modelo no sea válido, por ejemplo, retornando la vista con los errores
                return View(id);
            }

            var Existente = new SCI_TICKET();
            // Actualizar los datos del usuario existente con los nuevos datos del modelo  
            Existente.IDTICKET = id;
            Existente.USUARIODMODIFICACION = GlobalAdmin.UserAdmin;
            Existente.ESTADO = 4;
            Existente.FECHAFIN = DateTime.Now;
            Existente.FECHAMODIFICACION = DateTime.Now; // Actualizar la fecha de modificación
            Existente.IPMODIFICACION = UtilScripts.ObtenerIP();
            // Guardar los cambios en la base de datos
            try
            {
                Existente.IDTICKET = TicketService.Modificar(3, Existente);
                TempData["Message"] = "Registro Exitoso";
                TempData["MessageType"] = "primary";
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
                TempData["ErrorMessage"] = "Hubo un problema al guardar la Ticket.";
                TempData["MessageType"] = "danger";
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
            var Existente = new SCI_TICKET();
            // Actualizar los datos del usuario existente con los nuevos datos del modelo  
            Existente.IDTICKET = id;
            Existente.USUARIODMODIFICACION = GlobalAdmin.UserAdmin;
            Existente.ESTADO = 2;
            Existente.FECHAFIN = null;
            Existente.FECHAMODIFICACION = DateTime.Now; // Actualizar la fecha de modificación
            Existente.IPMODIFICACION = UtilScripts.ObtenerIP();
            // Guardar los cambios en la base de datos
            try
            {
                Existente.IDTICKET = TicketService.Modificar(3, Existente);
                TempData["Message"] = "Registro Exitoso";
                TempData["MessageType"] = "primary";
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
                TempData["ErrorMessage"] = "Hubo un problema al guardar la Ticket.";
                TempData["MessageType"] = "danger";
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

            var Existente = new SCI_TICKET();
            // Actualizar los datos del usuario existente con los nuevos datos del modelo  
            Existente.IDTICKET = id;
            Existente.USUARIODMODIFICACION = GlobalAdmin.UserAdmin;
            Existente.ESTADO = 5;
            Existente.FECHAMODIFICACION = DateTime.Now; // Actualizar la fecha de modificación
            Existente.IPMODIFICACION = UtilScripts.ObtenerIP();
            // Guardar los cambios en la base de datos
            try
            {
                Existente.IDTICKET = TicketService.Modificar(3, Existente);
                TempData["Message"] = "Registro Exitoso";
                TempData["MessageType"] = "primary";
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
                TempData["ErrorMessage"] = "Hubo un problema al guardar la Ticket.";
                TempData["MessageType"] = "danger";
            }

            // Redirigir al usuario a la página de índice
            return RedirectToAction("Asignar");
        }

        public JsonResult Buscar(SCI_TICKET filtro)
        {        
            var tickets = TicketService.Buscar(filtro);
            object json = new { data = tickets };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult BuscarAsignar(VIEW_Ticket filtro)
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

            if (GlobalAdmin.TipoUsuario == 4)
            {            
                ViewBag.ListRESPONSABLE = context.VIEW_Accesos.Where(x => x.ESTADO == 1 && x.TIPOUSUARIO != 2 && x.IDPERSONA == GlobalAdmin.IdUsuarioAdmin).ToList();
            }
            else
            {
                ViewBag.ListRESPONSABLE = context.VIEW_Accesos.Where(x => x.ESTADO == 1 && x.TIPOUSUARIO != 2).ToList();
            }

            // var tickets = context.VIEW_Ticket.Where(t => t.IDSOLICITANTE == filtro.IDSOLICITANTE && t.ESTADO == filtro.ESTADO).ToList(); // Ejemplo de búsqueda por nombre

            IQueryable<VIEW_Ticket> query = context.VIEW_Ticket;

            // Aplicar filtros
            if (!string.IsNullOrEmpty(filtro.IDSOLICITANTE.ToString()))
            {
                query = query.Where(t => t.IDSOLICITANTE == filtro.IDSOLICITANTE);
            }
            if (!string.IsNullOrEmpty(filtro.IDRESPONSABLE.ToString()))
            {
                query = query.Where(t => t.IDRESPONSABLE == filtro.IDRESPONSABLE);
            }
            if (filtro.FECHAINI != null)
            {
                query = query.Where(t => t.FECHAREGISTRO >= filtro.FECHAINI);
            }
            if (filtro.FECHAINI != null)
            {
                query = query.Where(t => t.FECHAREGISTRO >= filtro.FECHAINI);
            }

            if (filtro.FECHAFIN != null)
            {
                query = query.Where(t => t.FECHAREGISTRO <= filtro.FECHAFIN);
            }

            if (!string.IsNullOrEmpty(filtro.ESTADO.ToString()))
            {
                query = query.Where(t => t.ESTADO == filtro.ESTADO);
            }

            var tickets = query.ToList();



            //return View(tickets);
            return View("Asignar", tickets); // Devolver los resultados a la vista Index
        }

    }
}