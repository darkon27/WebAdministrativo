using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAdministrativo.Models;
using WebAdministrativo.ViewModels;
using System.Data.Entity.Infrastructure;
using WebAdministrativo.Service;
using WebAdministrativo.Clases;

namespace WebAdministrativo.Controllers
{
    public class UsuarioController : Controller
    {
        //private BDIntegrityEntities context = new BDIntegrityEntities();
        // GET: Usuario
        public ActionResult Index()
        {
            VIEW_Accesos ViewMode = new VIEW_Accesos();
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

                ViewBag.ListTipoUsu = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 5).ToList();
                ViewBag.ListEstado = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 1).ToList();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            VIEW_Accesos Entity = new VIEW_Accesos();
            var ListUsuario = UsuarioService.Buscar(Entity);
            ViewBag.lstSalida = GlobalAdmin.lstSalida;

            return View(ListUsuario);
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            UsuarioViewModels ViewMode = new UsuarioViewModels();
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
                GlobalAdmin.FechaRegistro = DateTime.Now.ToShortDateString();
                ViewBag.FechaRegistro = GlobalAdmin.FechaRegistro;
                List<SCI_MAESTRODETALLE> listMAESTRODETALLE = (List<SCI_MAESTRODETALLE>)Session["VIEW_MAESTRODETALLE"];

                ViewBag.ListTipoUsu = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 5).ToList();
                ViewBag.ListEstado = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 1).ToList();
                ViewBag.lstSalida = GlobalAdmin.lstSalida;
            }
            return View(ViewMode);
        }

        [HttpGet]
        public ActionResult Modificar(int id)
        {
            UsuarioViewModels ViewMode = new UsuarioViewModels();
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
      
                VIEW_Accesos Entity = new VIEW_Accesos();
                Entity.IDPERSONA = id;
                var ListUsuario = UsuarioService.Buscar(Entity);
                foreach (var intem in ListUsuario)
                {
                    ViewMode.VIEW = intem;
                }
                GlobalAdmin.FechaRegistro = DateTime.Now.ToShortDateString();
                ViewBag.FechaRegistro = GlobalAdmin.FechaRegistro;
                List<SCI_MAESTRODETALLE> listMAESTRODETALLE = (List<SCI_MAESTRODETALLE>)Session["VIEW_MAESTRODETALLE"];
                ViewMode.ListTipoUsu = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 5).ToList();
                ViewMode.ListEstado = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 1).ToList();
                ViewBag.lstSalida = GlobalAdmin.lstSalida;
            }
            return View(ViewMode);
        }

        [HttpGet]
        public ActionResult Ver(int id)
        {
            UsuarioViewModels ViewMode = new UsuarioViewModels();
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

                VIEW_Accesos Entity = new VIEW_Accesos();
                Entity.IDPERSONA = id;
                var ListUsuario = UsuarioService.Buscar(Entity);
                foreach (var intem in ListUsuario)
                {
                    ViewMode.VIEW = intem;
                }
                GlobalAdmin.FechaRegistro = DateTime.Now.ToShortDateString();
                ViewBag.FechaRegistro = GlobalAdmin.FechaRegistro;
                List<SCI_MAESTRODETALLE> listMAESTRODETALLE = (List<SCI_MAESTRODETALLE>)Session["VIEW_MAESTRODETALLE"];
                ViewMode.ListTipoUsu = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 5).ToList();
                ViewMode.ListEstado = listMAESTRODETALLE.Where(x => x.IDMAESTRO == 1).ToList();
                ViewBag.lstSalida = GlobalAdmin.lstSalida;
            }
            return View(ViewMode);
        }

        [HttpPost]
        public ActionResult Actualizar(UsuarioViewModels Usuario)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                // Manejar el caso en que el modelo no sea válido, por ejemplo, retornando la vista con los errores
                return View(Usuario);
            }

            // Actualizar los datos del usuario existente con los nuevos datos del modelo
            var usuarioExistente = new SCI_USUARIO();
            usuarioExistente.IDUSUARIO = Usuario.VIEW.IDUSUARIO;
            usuarioExistente.USUARIO = Usuario.VIEW.USUARIO;
            usuarioExistente.FECHAEXPIRACION = Usuario.VIEW.FECHAEXPIRACION;
            usuarioExistente.TIPOUSUARIO = Usuario.VIEW.TIPOUSUARIO;
            usuarioExistente.PASSWORD = Usuario.VIEW.PASSWORD;
            usuarioExistente.ESTADO = Usuario.VIEW.ESTADO;
            usuarioExistente.FECHAMODIFICACION = DateTime.Now; // Actualizar la fecha de modificación
            usuarioExistente.USUARIOMODIFICACION = GlobalAdmin.UserAdmin;
            usuarioExistente.FLAGEXPIRACION = Usuario.VIEW.FLAGEXPIRACION;
            usuarioExistente.IPMODIFICACION = UtilScripts.ObtenerIP();

            // Guardar los cambios en la base de datos
            try
            {
                UsuarioService.Modificar(2,usuarioExistente);
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

        [HttpGet]
        public ActionResult modalAnular(int id)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                // Manejar el caso en que el modelo no sea válido, por ejemplo, retornando la vista con los errores
                return View(id);
            }
            var usuarioExistente = new SCI_USUARIO();

            // Actualizar los datos del usuario existente con los nuevos datos del modelo  
            usuarioExistente.IDUSUARIO = id;
            usuarioExistente.USUARIOMODIFICACION = GlobalAdmin.UserAdmin;    
            usuarioExistente.ESTADO = 2;
            usuarioExistente.FECHAMODIFICACION = DateTime.Now; // Actualizar la fecha de modificación
            usuarioExistente.IPMODIFICACION = UtilScripts.ObtenerIP();
            // Guardar los cambios en la base de datos
            try
            {
                UsuarioService.Modificar(3, usuarioExistente);
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

        public JsonResult Buscar(VIEW_Accesos Detalle)
        {
            //   ObjDetalle.ESTADO = 1;
            ViewBag.lstSalida = GlobalAdmin.lstSalida;
            var Lista = UsuarioService.Buscar(Detalle);
            object json = new { data = Lista };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}