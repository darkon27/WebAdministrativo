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
    
            if (Usu != null)
            {
                VIEW_Garantia vIEW_Garantia = new VIEW_Garantia();
                vIEW_Garantia.USUARIOCREACION = Usu.USUARIO.Trim();
                vIEW_Garantia.REFERENCIA = Usu.NOMBRE.Trim();
             var   ListGarantia = GarantiaService.BuscarGarantias(vIEW_Garantia);
                if (ListGarantia.Count > 0)
                {
                    int indicar = 0;
                    string fecFin = "";
                    foreach (var item in ListGarantia)
                    {
                        if (item.ESTADO == 1)
                        {
                            indicar = 1;
                            TempData["Message"] = "La Garantia se encuetra en Pendiente de aprobación :: " + fecFin;
                            TempData["MessageType"] = "warning";
                        }
                        if (item.ESTADO == 5)
                        {
                            fecFin = item.FECHAMODIFICACION.ToString();
                            indicar = 1;
                            TempData["Message"] = "La Garantia se encuetra Anulada :: " + fecFin;
                            TempData["MessageType"] = "dark";
                        }
                        else
                        {
                            if (item.FECHAFIN >= DateTime.Now)
                            {
                                fecFin = item.FECFINPLAZO;
                                indicar = 1;
                                TempData["Message"] = "La Garantia todavia esta vigente :: " + fecFin;
                                TempData["MessageType"] = "primary";
                            }
                        }
                        fecFin = item.FECHAMODIFICACION.ToString();
                    }
                    //if (indicar == 1)
                    //{
                    //    TempData["Message"] = "La Garantia todavia esta vigente :: " + fecFin;
                    //    TempData["MessageType"] = "primary";
                    //}
                    if (indicar == 0)
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