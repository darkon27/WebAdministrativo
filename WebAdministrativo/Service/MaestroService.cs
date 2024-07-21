using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using WebAdministrativo.Clases;
using WebAdministrativo.Models;
using WebAdministrativo.ViewModels;

namespace WebAdministrativo.Service
{
    public class MaestroService
    {

        public static List<SCI_MAESTRO> BuscarMaestro(SCI_MAESTRO ViewMode)
        {
            List<SCI_MAESTRO> Model = new List<SCI_MAESTRO>();
            try
            {
                using (BDIntegrityEntities context = new BDIntegrityEntities())
                {
                    IQueryable<SCI_MAESTRO> consulta = context.SCI_MAESTRO.AsQueryable();
                    if (!string.IsNullOrEmpty(ViewMode.IDMAESTRO.ToString()) && ViewMode.IDMAESTRO > 0)
                    {
                        consulta = consulta.Where(c => c.IDMAESTRO == ViewMode.IDMAESTRO);
                    }
                    if (!string.IsNullOrEmpty(ViewMode.ESTADO.ToString()))
                    {
                        consulta = consulta.Where(c => c.ESTADO == ViewMode.ESTADO);
                    }
                    if (!string.IsNullOrEmpty(ViewMode?.CODIGO?.ToString()))
                    {
                        consulta = consulta.Where(c => c.CODIGO.ToUpper().Contains(ViewMode.CODIGO.ToUpper()));
                    }
                    Model = consulta.ToList();
                }

            }
            catch (DbEntityValidationException ex)
            {
                string msj = "Ocurrió un error al Buscar Garantias los datos. Inténtelo de nuevo.";
                string msjson = "";
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        // Mostrar el error en la consola o registrarlo en un log
                        msjson += $"  Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                    }
                }
                UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Error|BuscarMaestro =" + msj + msjson);
            }
            return Model;
        }

        public static List<VIEW_MaestroDetalle> Buscar(VIEW_MaestroDetalle ViewMode)
        {
            List<VIEW_MaestroDetalle> Model = new List<VIEW_MaestroDetalle>();
            try
            {
                using (BDIntegrityEntities context = new BDIntegrityEntities())
                {
                    IQueryable<VIEW_MaestroDetalle> consulta = context.VIEW_MaestroDetalle.AsQueryable();
                    if (!string.IsNullOrEmpty(ViewMode?.ID.ToString()) && ViewMode.ID > 0)
                    {
                        consulta = consulta.Where(c => c.ID == ViewMode.ID);
                    }
                    if (!string.IsNullOrEmpty(ViewMode?.ESTADO.ToString()))
                    {
                        consulta = consulta.Where(c => c.ESTADO == ViewMode.ESTADO);
                    }
                    if (!string.IsNullOrEmpty(ViewMode?.CODIGO?.ToString()))
                    {
                        consulta = consulta.Where(c => c.CODIGO.ToUpper().Contains(ViewMode.CODIGO.ToUpper()));
                    }
                    if (!string.IsNullOrEmpty(ViewMode?.IDMAESTRO.ToString()) && ViewMode.IDMAESTRO > 0)
                    {
                        consulta = consulta.Where(c => c.IDMAESTRO == ViewMode.IDMAESTRO);
                    }
                    Model = consulta.ToList();
                }

            }
            catch (DbEntityValidationException ex)
            {
                string msj = "Ocurrió un error al Buscar Garantias los datos. Inténtelo de nuevo.";
                string msjson = "";
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        // Mostrar el error en la consola o registrarlo en un log
                        msjson += $"  Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                    }
                }
                UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Error|Buscar =" + msj + msjson);
            }
            return Model;
        }

        public static List<SCI_MAESTRODETALLE> ListarMaestroActivos()
        {
            List<SCI_MAESTRODETALLE> Model = new List<SCI_MAESTRODETALLE>();
            try
            {
                using (BDIntegrityEntities context = new BDIntegrityEntities())
                {
                    IQueryable<SCI_MAESTRODETALLE> consulta = context.SCI_MAESTRODETALLE.AsQueryable();
                    consulta = consulta.Where(c => c.ESTADO == 1);                   
                    Model = consulta.ToList();
                }
            }
            catch (DbEntityValidationException ex)
            {
                string msj = "Ocurrió un error en la ListarMaestroActivos de los datos. Inténtelo de nuevo.";
                string msjson = "";
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        // Mostrar el error en la consola o registrarlo en un log
                        msjson += $"  Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                    }
                }
                UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Error|ListarMaestroActivos =" + msj + msjson);
            }
            return Model;
        }

        public static List<SCI_MAESTRODETALLE> Validacion(SCI_MAESTRODETALLE ViewMode)
        {
            List<SCI_MAESTRODETALLE> Model = new List<SCI_MAESTRODETALLE>();
            try
            {
                using (BDIntegrityEntities context = new BDIntegrityEntities())
                {
                    IQueryable<SCI_MAESTRODETALLE> consulta = context.SCI_MAESTRODETALLE.AsQueryable();
                    if (!string.IsNullOrEmpty(ViewMode.ID.ToString()))
                    {
                        consulta = consulta.Where(c => c.ID != ViewMode.ID);
                    }
                    if (!string.IsNullOrEmpty(ViewMode?.IDMAESTRO.ToString()))
                    {
                        consulta = consulta.Where(c => c.IDMAESTRO == ViewMode.IDMAESTRO);
                    }
                    if (!string.IsNullOrEmpty(ViewMode?.CODIGO?.ToString()))
                    {
                        consulta = consulta.Where(c => c.CODIGO.ToUpper() == ViewMode.CODIGO.ToUpper());
                    }
                    if (!string.IsNullOrEmpty(ViewMode?.NOMBRE?.ToString()))
                    {
                        consulta = consulta.Where(c => c.NOMBRE.ToUpper() == ViewMode.NOMBRE.ToUpper());
                    }
                    Model = consulta.ToList();
                }
            }
            catch (DbEntityValidationException ex)
            {
                string msj = "Ocurrió un error en la Validacion SCI_MAESTRODETALLE de los datos. Inténtelo de nuevo.";
                string msjson = "";
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        // Mostrar el error en la consola o registrarlo en un log
                        msjson += $"  Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                    }
                }
                UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Error|Validacion =" + msj + msjson);
            }
            return Model;
        }

        public static Response  Insertar(MaestroDetalleViewModels ViewModels)
        {
            ViewModels.Detalle.ID = 0;
            int Idsecuencia = 0;
            Response Resp = new Response();
            using (BDIntegrityEntities context = new BDIntegrityEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Idsecuencia = context.SCI_MAESTRODETALLE.Max(C => C.ID);
                        ViewModels.Detalle.ID = Idsecuencia + 1;
                        context.SCI_MAESTRODETALLE.Add(ViewModels.Detalle);              
                        Resp.Data = ViewModels.Detalle;
                        Resp.Message = "Registro exitoso. ";
                        Resp.IsSucces = true;
                        context.SaveChanges();
                        transaction.Commit();

                    }
                    catch (DbEntityValidationException ex)
                    {
                        string msj = "Ocurrió un error al registrar los datos. Inténtelo de nuevo.";
                        string msjson = "";
                        foreach (var validationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                // Mostrar el error en la consola o registrarlo en un log
                                msjson += $"  Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                            }
                        }
                        transaction.Rollback();
                        Resp.Data = ViewModels.VIEW;
                        Resp.Message = msj + msjson;
                        Resp.IsSucces = false;
                    }
                }
            }
            return Resp;
        }

        public static Response Modificar(int valor ,MaestroDetalleViewModels ViewModels)
        {
            Response Resp = new Response();
            using (BDIntegrityEntities context = new BDIntegrityEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var existingDetalle = context.SCI_MAESTRODETALLE.Find(ViewModels.VIEW.ID);
                        if (existingDetalle != null)
                        {
                            if (valor == 3)
                            {   // Anula el detalle existente
                                existingDetalle.ESTADO = ViewModels.VIEW.ESTADO;
                            }
                            else
                            {
                                // Actualizar el detalle existente
                                existingDetalle.IDMAESTRO = ViewModels.VIEW.IDMAESTRO;
                                existingDetalle.CODIGO = ViewModels.VIEW.CODIGO;
                                existingDetalle.NOMBRE = ViewModels.VIEW.NOMBRE;
                                existingDetalle.DESCRIPCION = ViewModels.VIEW.DESCRIPCION;
                                existingDetalle.VERSION++;
                                existingDetalle.ESTADO = ViewModels.VIEW.ESTADO;
                            }
                        }
                        else
                        {
                            // Insertar nuevo detalle si no existe
                            ViewModels.Detalle.VERSION = 1;                  
                            context.SCI_MAESTRODETALLE.Add(existingDetalle);
                        }
                        Resp.Data = ViewModels.VIEW;
                        Resp.Message = "Registro exitoso. ";
                        Resp.IsSucces = true;
                        context.SaveChanges();
                        transaction.Commit();
            
                    }
                    catch (DbEntityValidationException ex)
                    {
                        string msj = "Ocurrió un error al registrar los datos. Inténtelo de nuevo.";
                        string msjson = "";
                        foreach (var validationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                // Mostrar el error en la consola o registrarlo en un log
                                msjson += $"  Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                            }
                        }
                        transaction.Rollback();
                        Resp.Data = ViewModels.VIEW;
                        Resp.Message = msj + msjson;
                        Resp.IsSucces = false;
                    }
                }
            }
            return Resp;
        }
    }
}