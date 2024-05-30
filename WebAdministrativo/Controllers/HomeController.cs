using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebAdministrativo.Models;
using WebAdministrativo.ViewModels;

namespace WebAdministrativo.Controllers
{
    public class HomeController : Controller
    {
        private BDIntegrityEntities context = new BDIntegrityEntities();
        // GET: Home
        public ActionResult Index()
        {
            if (GlobalAdmin.IdUsuarioAdmin==0)
            {
                return RedirectToAction("Index", "Login");
            }

            if (Session["VIEW_Accesos"] != null)
            {
                List<VIEW_Accesos>  UsuAccesos = (List<VIEW_Accesos>)Session["VIEW_Accesos"];
                foreach ( var intem in  UsuAccesos)
                {
                    ViewBag.UserNameAdmin = intem.NOMBRECOMPLETO;
                    ViewBag.TipoUsuarioAdmin = intem.DESTIPOUSUARIO;
                }
                GlobalAdmin.FechaRegistro = DateTime.Now.ToShortDateString();
                ViewBag.FechaRegistro = GlobalAdmin.FechaRegistro;
            }
            ViewBag.lstSalida = GlobalAdmin.lstSalida;
            ////return View(ViewBag.lstSalida);
             return View();
        }

        public ActionResult Logout()
        {
            // Cerrar sesión del usuario
            Session["VIEW_Accesos"] = null;
            Session["MAESTRODETALLE"] = null;

            GlobalAdmin xon = new GlobalAdmin();  
            GlobalAdmin.UserNameAdmin = null;
            GlobalAdmin.TipoUserAdmin = null;
            GlobalAdmin.IdUsuarioAdmin = 0;
            GlobalAdmin.IdMenuCap = 0;
            GlobalAdmin.NombreMenuGrupo = null;
            GlobalAdmin.NombreGrupo = null;
            GlobalAdmin.AccionOpenSetList = null;

            // Redirigir al usuario a la página de login
            return RedirectToAction("Index", "Login");
        }



    }
}