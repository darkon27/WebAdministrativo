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

            if(GlobalAdmin.TipoUsuario == 4)
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

            // Obtener el usuario existente desde la base de datos
            var usuarioExistente = context.SCI_TICKET.Find(ticket.VIEW.IDTICKET);
            if (usuarioExistente == null)
            {
                // Manejar el caso en que el usuario no existe, por ejemplo, mostrando un mensaje de error
                TempData["ErrorMessage"] = "El usuario no existe.";
                return RedirectToAction("Index");
            }

            usuarioExistente.IDRESPONSABLE = ticket.VIEW.IDRESPONSABLE;
            usuarioExistente.OBSERVACION = ticket.VIEW.OBSERVACION;
            usuarioExistente.FECHAINI = DateTime.Now;
            usuarioExistente.ESTADO = 2; // EN PROCESO
            usuarioExistente.FECHAMODIFICACION = DateTime.Now; // Actualizar la fecha de modificación
            // Guardar los cambios en la base de datos
            try
            {
                context.SaveChanges();
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
        public ActionResult UpdAprobarEstado(string IDTICKET)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                // Manejar el caso en que el modelo no sea válido, por ejemplo, retornando la vista con los errores
                return View(IDTICKET);
            }

            // Obtener el usuario existente desde la base de datos
            var usuarioExistente = context.SCI_TICKET.Find(IDTICKET);
            if (usuarioExistente == null)
            {
                // Manejar el caso en que el usuario no existe, por ejemplo, mostrando un mensaje de error
                TempData["ErrorMessage"] = "El usuario no existe.";
                return RedirectToAction("Index");
            }

            usuarioExistente.FECHAINI = DateTime.Now;
            usuarioExistente.ESTADO = 4; //  FINALIZADO
            usuarioExistente.FECHAMODIFICACION = DateTime.Now; // Actualizar la fecha de modificación
            usuarioExistente.USUARIODMODIFICACION = GlobalAdmin.UserAdmin; // Actualizar la fecha de modificación
            // Guardar los cambios en la base de datos
            try
            {
                context.SaveChanges();
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
        public ActionResult modalRevertir(string IDTICKET)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                // Manejar el caso en que el modelo no sea válido, por ejemplo, retornando la vista con los errores
                return View(IDTICKET);
            }

            // Obtener el usuario existente desde la base de datos
            var usuarioExistente = context.SCI_TICKET.Find(IDTICKET);
            if (usuarioExistente == null)
            {
                // Manejar el caso en que el usuario no existe, por ejemplo, mostrando un mensaje de error
                TempData["ErrorMessage"] = "El usuario no existe.";
                return RedirectToAction("Index");
            }

            usuarioExistente.FECHAINI = DateTime.Now;
            usuarioExistente.FECHAFIN = usuarioExistente.FECHAMODIFICACION;
            usuarioExistente.ESTADO = 2; // EN PROCESO
            usuarioExistente.FECHAMODIFICACION = DateTime.Now; // Actualizar la fecha de modificación
            usuarioExistente.USUARIODMODIFICACION = GlobalAdmin.UserAdmin; // Actualizar la fecha de modificación
            // Guardar los cambios en la base de datos
            try
            {
                context.SaveChanges();
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
        public ActionResult modalAnular(string IDTICKET)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                // Manejar el caso en que el modelo no sea válido, por ejemplo, retornando la vista con los errores
                return View(IDTICKET);
            }

            // Obtener el usuario existente desde la base de datos
            var usuarioExistente = context.SCI_TICKET.Find(IDTICKET);
            if (usuarioExistente == null)
            {
                // Manejar el caso en que el usuario no existe, por ejemplo, mostrando un mensaje de error
                TempData["ErrorMessage"] = "El usuario no existe.";
                return RedirectToAction("Index");
            }

            usuarioExistente.FECHAINI = DateTime.Now;
            usuarioExistente.ESTADO = 5; // ANULADO
            usuarioExistente.FECHAMODIFICACION = DateTime.Now; // Actualizar la fecha de modificación
            usuarioExistente.USUARIODMODIFICACION = GlobalAdmin.UserAdmin; // Actualizar la fecha de modificación
            // Guardar los cambios en la base de datos
            try
            {
                context.SaveChanges();
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
        public ActionResult Buscar(SCI_TICKET filtro)
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
            return View("Index", tickets); // Devolver los resultados a la vista Index
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