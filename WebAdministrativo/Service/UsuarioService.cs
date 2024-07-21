using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using WebAdministrativo.Clases;
using WebAdministrativo.Models;

namespace WebAdministrativo.Service
{
    public class UsuarioService
    {
        public static List<VIEW_Accesos> Accesos(VIEW_Accesos ViewMode)
        {
            List<VIEW_Accesos> Model = new List<VIEW_Accesos>();
            try
            {
                using (BDIntegrityEntities context = new BDIntegrityEntities())
                {
                    IQueryable<VIEW_Accesos> consulta = context.VIEW_Accesos.AsQueryable();
                    if (!string.IsNullOrEmpty(ViewMode.PASSWORD))
                    {
                        consulta = consulta.Where(c => c.PASSWORD == ViewMode.PASSWORD.Trim());
                    }
                    if (!string.IsNullOrEmpty(ViewMode.USUARIO))
                    {
                        consulta = consulta.Where(c => c.USUARIO == ViewMode.USUARIO.Trim());
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
                UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Error|Accesos =" + msj + msjson);
            }
            return Model;
        }

        public static List<VIEW_Accesos> Buscar(VIEW_Accesos ViewMode)
        {
            List<VIEW_Accesos> Model = new List<VIEW_Accesos>();
            try
            {
                using (BDIntegrityEntities context = new BDIntegrityEntities())
                {
                    IQueryable<VIEW_Accesos> consulta = context.VIEW_Accesos.AsQueryable();
                    if (!string.IsNullOrEmpty(ViewMode?.IDPERSONA.ToString()) && ViewMode.IDPERSONA > 0)
                    {
                        consulta = consulta.Where(c => c.IDPERSONA == ViewMode.IDPERSONA);
                    }
                    if (!string.IsNullOrEmpty(ViewMode?.ESTADO.ToString()))
                    {
                        consulta = consulta.Where(c => c.ESTADO == ViewMode.ESTADO);
                    }
                    if (!string.IsNullOrEmpty(ViewMode?.USUARIO?.ToString()))
                    {
                        consulta = consulta.Where(c => c.USUARIO.ToUpper().Contains(ViewMode.USUARIO.ToUpper()));
                    }
                    if (!string.IsNullOrEmpty(ViewMode?.TIPOUSUARIO.ToString()) && ViewMode.TIPOUSUARIO > 0)
                    {
                        consulta = consulta.Where(c => c.TIPOUSUARIO == ViewMode.TIPOUSUARIO);
                    }
                    Model = consulta.ToList();
                }

            }
            catch (DbEntityValidationException ex)
            {
                string msj = "Ocurrió un error en la Buscar de los datos. Inténtelo de nuevo.";
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

        public static SCI_USUARIO Obtener(VIEW_Accesos ViewMode)
        {
            List<SCI_USUARIO> Model = new List<SCI_USUARIO>();
            SCI_USUARIO Entyti = new SCI_USUARIO();
            try
            {
                using (BDIntegrityEntities context = new BDIntegrityEntities())
                {
                    IQueryable<SCI_USUARIO> consulta = context.SCI_USUARIO.AsQueryable();
                    if (!string.IsNullOrEmpty(ViewMode?.IDUSUARIO.ToString()) && ViewMode.IDUSUARIO > 0)
                    {
                        consulta = consulta.Where(c => c.IDUSUARIO == ViewMode.IDUSUARIO);
                    }
                    Model = consulta.ToList();
                    foreach (var obj in Model)
                    {
                        Entyti = obj;
                    }
                }
            }
            catch (Exception ex)
            {
                Entyti.PASSWORD = ex.Message;
            }
            return Entyti;
        }

        public static int Modificar(int valor, SCI_USUARIO ViewModels)
        {
            int idreturn = 0;
            using (BDIntegrityEntities context = new BDIntegrityEntities())
            {
                using (  var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var Existente = context.SCI_USUARIO.Find(ViewModels.IDUSUARIO);
                        ViewModels.FECHACREACION = Existente.FECHACREACION; // Actualizar la fecha de modificación
                        ViewModels.USUARIOCREACION = Existente.USUARIOCREACION;
                        ViewModels.IPCREACION = Existente.IPCREACION;
                        // Actualiza las propiedades del usuario existente con los valores del usuario modificado
                        context.Entry(Existente).CurrentValues.SetValues(ViewModels);

                        // Guarda los cambios en la base de datos
                        context.SaveChanges();

                        // Confirma la transacción
                        dbContextTransaction.Commit();
                        idreturn = ViewModels.IDUSUARIO;

                    }
                    catch (DbEntityValidationException ex)
                    {
                        dbContextTransaction.Rollback();
                        string msj = "Ocurrió un error al Modificar de los datos. Inténtelo de nuevo.";
                        string msjson = "";
                        foreach (var validationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                // Mostrar el error en la consola o registrarlo en un log
                                msjson += $"  Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                            }
                        }
                        UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Error|Modificar =" + msj + msjson);
                    }
                }
            }

            return idreturn;
        }
    }
}


