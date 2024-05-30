using System;
using System.Collections.Generic;
using System.Linq;
using WebAdministrativo.Models;
using WebAdministrativo.ViewModels;

namespace WebAdministrativo.Service
{
    public class MenuService
    {
        public static List<MenuGrupo> SetListSalida(string Perfil)
        {
            List<MenuGrupo> lstMenuGrupo = new List<MenuGrupo>();
            var listMenuAll = GetMenufile(Perfil);
            if (listMenuAll.Count > 0)
            {
                List<Menu> lstMenu = new List<Menu>();

                var listGrupo = listaMenuGrupo();
                var result = listMenuAll
                             .GroupBy(item => new { item.IDGRUPO, item.NOMBRE })
                             .Select(grouped => new GroupedResultType
                             {
                                 IDGRUPO = grouped.Key.IDGRUPO,
                                 NOMBRE = grouped.Key.NOMBRE
                             })
                             .ToList();
                int num = 0;
                foreach (var item in listGrupo)
                {
                    foreach (var gru in result)
                    {
                        if (gru.IDGRUPO == item.IDGRUPO)
                        {
                            num++;
                            MenuGrupo m = new MenuGrupo();
                            m.IDGRUPO = item.IDGRUPO;
                            m.DESCRIPCION = item.DESCRIPCION;
                            m.ESTADO = item.ESTADO;
                            m.ICON = item.ICON;
                            if (num == 1)
                            { 
                                m.IsActive = "nav-parent nav-expanded nav-active";                              
                            }
                            else
                            {
                                 m.IsActive = "nav-parent";
                            }
                            foreach (var item2 in listMenuAll)
                            {
                                if (gru.IDGRUPO == item2.IDGRUPO)
                                {
                                    Menu men = new Menu();
                                    men = item2;
                                    //men.IDMENU = item2.IDMENU;
                                    //men.IDGRUPO = item2.IDGRUPO;
                                    //men.CONCEPTO = item2.CONCEPTO;
                                    //men.ORDEN = item2.ORDEN;
                                    //men.OBJETO = item2.OBJETO;
                                    //men.IDPERFIL = item2.IDPERFIL;
                                    //men.URL = item2.URL;
                                    lstMenu.Add(men);
                                }
                            }
                            m.listaMenu_Items = lstMenu;
                            lstMenu = new List<Menu>();
                            lstMenuGrupo.Add(m);
                        }
                    }
                }
            }
            return lstMenuGrupo;
        }

        public static List<Menu> GetMenufile(string profileName)
        {
            try
            {
                using (var context = new BDIntegrityEntities())
                {
                    var result = (from perfil in context.SCI_PERFIL
                                  join menuAutorizacion in context.SCI_MENUAUTORIZACION on perfil.IDPERFIL equals menuAutorizacion.IDPERFIL
                                  join menu in context.SCI_MENU on new { menuAutorizacion.IDMENU, menuAutorizacion.IDGRUPO } equals new { menu.IDMENU, menu.IDGRUPO }
                                  join menuGrupo in context.SCI_MENUGRUPO on menuAutorizacion.IDGRUPO equals menuGrupo.IDGRUPO
                                  where perfil.NOMBRE == profileName
                                  select new Menu
                                  {
                                      IDPERFIL = perfil.IDPERFIL,
                                      IDGRUPO = menu.IDGRUPO,
                                      IDMENU = menu.IDMENU,
                                      NOMBRE = perfil.NOMBRE,
                                      CONCEPTO = menu.CONCEPTO,
                                      ORDEN = menu.ORDEN,
                                      OBJETO = menu.OBJETO,
                                      URL = menu.URL,
                                      DESCRIPCION = menuGrupo.DESCRIPCION,
                                      ICON = menuGrupo.ICON
                                  }).ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                // Manejo de la excepción (log, rethrow, etc.)
                throw new ApplicationException("Error al obtener la lista de menús", ex);
            }
        }

        public static List<SCI_MENUAUTORIZACION> Buscar(int IDPERFIL)
        {
            try
            {
                using (BDIntegrityEntities context = new BDIntegrityEntities())
                {
                    IQueryable<SCI_MENUAUTORIZACION> consulta = context.SCI_MENUAUTORIZACION.AsQueryable();
                    consulta = consulta.Where(c => c.IDPERFIL == IDPERFIL);
                    return consulta.ToList();
                }
            }
            catch (Exception ex)
            {
                // Manejo de la excepción (log, rethrow, etc.)
                throw new ApplicationException("Error al obtener la lista de menús", ex);
            }
        }

        public static List<MenuGrupo> listaMenuGrupo()
        {
            try
            {
                using (var context = new BDIntegrityEntities())
                {
                    return (from d in context.SCI_MENUGRUPO
                            select new MenuGrupo
                            {
                                IDGRUPO = d.IDGRUPO,
                                DESCRIPCION = d.DESCRIPCION,
                                ESTADO = d.ESTADO,
                                ICON = d.ICON
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                // Manejo de la excepción (log, rethrow, etc.)
                throw new ApplicationException("Error al obtener la lista de menús", ex);
            }
        }

        public List<Menu> listaMenu()
        {
            try
            {
                using (BDIntegrityEntities context = new BDIntegrityEntities())
                {
                    return (from d in context.SCI_MENU
                            select new Menu
                            {
                                IDGRUPO = d.IDGRUPO,
                                IDMENU = d.IDMENU,
                                CONCEPTO = d.CONCEPTO,
                                URL = d.URL
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                // Manejo de la excepción (log, rethrow, etc.)
                throw new ApplicationException("Error al obtener la lista de menús", ex);
            }

        }
    }
}