using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAdministrativo.Models;
using WebAdministrativo.Service;

namespace WebAdministrativo.Controllers
{
    public class PortalController : Controller
    {
        // GET: Portal
        public ActionResult Index()
        {
            ViewBag.Title = "Vista con Nuevo Menú";
            //  ViewBag.Layout = "~/Views/Shared/_NewMenu.cshtml";
            return View();
        }

        public ActionResult Nosotros()
        {
            ViewBag.Title = "Vista con Nuevo Menú Sistemas";
            ViewBag.Layout = "~/Views/Shared/_NewMenu.cshtml";
            return View();
        }
        public ActionResult Datacenter()
        {
            ViewBag.Title = "Vista con Nuevo Menú Sistemas";
            ViewBag.Layout = "~/Views/Shared/_NewMenu.cshtml";
            return View();
        }
        public ActionResult Redes()
        {
            ViewBag.Title = "Vista con Nuevo Menú Sistemas";
            ViewBag.Layout = "~/Views/Shared/_NewMenu.cshtml";
            return View();
        }
        public ActionResult Desarrollo()
        {
            ViewBag.Title = "Vista con Nuevo Menú Sistemas";
            ViewBag.Layout = "~/Views/Shared/_NewMenu.cshtml";
            return View();
        }
        public ActionResult Informatica()
        {
            ViewBag.Title = "Vista con Nuevo Menú Sistemas";
            ViewBag.Layout = "~/Views/Shared/_NewMenu.cshtml";
            return View();
        }
        public ActionResult Descargas()
        {
            ViewBag.Title = "Vista con Nuevo Menú Sistemas";
            ViewBag.Layout = "~/Views/Shared/_NewMenu.cshtml";
            return View();
        }
        public ActionResult Contacto()
        {
            ViewBag.Title = "Vista con Nuevo Menú Sistemas";
            ViewBag.Layout = "~/Views/Shared/_NewMenu.cshtml";
            return View();
        }


        public ActionResult Soluciones()
        {
            ViewBag.Title = "Vista con Nuevo Menú Soluciones";
            ViewBag.Layout = "~/Views/Shared/_NewMenu.cshtml";
            return View();
        }
        public ActionResult Sistemas()
        {
            ViewBag.Title = "Vista con Nuevo Menú Sistemas";
            ViewBag.Layout = "~/Views/Shared/_NewMenu.cshtml";
            return View();
        }
        public ActionResult Helpdesk()
        {
            ViewBag.Title = "Vista con Nuevo Menú Sistemas";
            ViewBag.Layout = "~/Views/Shared/_NewMenu.cshtml";
            return View();
        }
        public ActionResult Partners()
        {
            ViewBag.Title = "Vista con Nuevo Menú Sistemas";
            ViewBag.Layout = "~/Views/Shared/_NewMenu.cshtml";
            return View();
        }

        public ActionResult Garantia()
        {
            ViewBag.Title = "Vista con Nuevo Menú Sistemas";
            ViewBag.Layout = "~/Views/Shared/_NewMenu.cshtml";
            return View();
        }


        public ActionResult Consultar(VIEW_Accesos Usu)
        {
            List<VIEW_Garantia> ListGarantia = new List<VIEW_Garantia>();
            if (Usu != null)
            {
                VIEW_Garantia vIEW_Garantia = new VIEW_Garantia();
                vIEW_Garantia.USUARIOCREACION = Usu.USUARIO.Trim();
                vIEW_Garantia.REFERENCIA = Usu.NOMBRE.Trim();
                ListGarantia = GarantiaService.BuscarGarantias(vIEW_Garantia);
                if (ListGarantia.Count > 0)
                {
                    int indicar = 0;
                   string fecFin="";
                    foreach (var item in ListGarantia)
                    {
                        if (item.FECHAFIN >= DateTime.Now)
                        {                           
                            indicar = 1;
                        }
                        fecFin = item.FECFINPLAZO;
                    }
                    if (indicar == 1)
                    {
                        TempData["Message"] = "La Garantia todavia esta vigente :: " + fecFin;
                        TempData["MessageType"] = "primary";
                    }
                    else
                    {
                        TempData["Message"] = "La Garantia ya caduco :: " + fecFin;
                        TempData["MessageType"] = "danger";
                    }
                }
                else
                {
                    TempData["Message"] = "La Garantia NO Existe";
                    TempData["MessageType"] = "warning";
                }
            }
            return View("Garantia");
        }

    }
}