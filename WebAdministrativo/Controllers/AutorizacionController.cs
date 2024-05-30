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
    public class AutorizacionController : Controller
    {
        // GET: Autorizacion
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
    
            ViewBag.lstSalida = GlobalAdmin.lstSalida;
            return View();
        }
    }
}