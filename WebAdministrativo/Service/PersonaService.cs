using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using WebAdministrativo.Clases;
using WebAdministrativo.Models;

namespace WebAdministrativo.Service
{
    public class PersonaService
    {
        public static List<VIEW_Persona> Buscar(VIEW_Persona ViewMode)
        {
            List<VIEW_Persona> Model = new List<VIEW_Persona>();
            try
            {
                using (BDIntegrityEntities context = new BDIntegrityEntities())
                {
                    IQueryable<VIEW_Persona> consulta = context.VIEW_Persona.AsQueryable();
                    if (!string.IsNullOrEmpty(ViewMode?.IDPERSONA.ToString()) && ViewMode.IDPERSONA > 0)
                    {
                        consulta = consulta.Where(c => c.IDPERSONA == ViewMode.IDPERSONA );
                    }
                    if (!string.IsNullOrEmpty(ViewMode?.ESTADO.ToString()))
                    {
                        consulta = consulta.Where(c => c.ESTADO == ViewMode.ESTADO);
                    }
                    if (!string.IsNullOrEmpty(ViewMode?.TIPODOCUMENTO))
                    {
                        consulta = consulta.Where(c => c.TIPODOCUMENTO == ViewMode.TIPODOCUMENTO);
                    }
                    if (!string.IsNullOrEmpty(ViewMode?.TIPOPERSONA) )
                    {
                        consulta = consulta.Where(c => c.TIPOPERSONA == ViewMode.TIPOPERSONA);
                    }
                    if (!string.IsNullOrEmpty(ViewMode?.DOCUMENTO))
                    {
                        consulta = consulta.Where(c => c.DOCUMENTO == ViewMode.DOCUMENTO);
                    }
                    if (!string.IsNullOrEmpty(ViewMode?.NOMBRECOMPLETO))
                    {
                        consulta = consulta.Where(c => c.NOMBRECOMPLETO.ToUpper().Contains(ViewMode.NOMBRECOMPLETO.ToUpper()));
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

        public static SCI_PERSONA Obtener(VIEW_Persona ViewMode)
        {
            List<SCI_PERSONA> Model = new List<SCI_PERSONA>();
            SCI_PERSONA Entyti = new SCI_PERSONA();
            try
            {
                using (BDIntegrityEntities context = new BDIntegrityEntities())
                {
                    IQueryable<SCI_PERSONA> consulta = context.SCI_PERSONA.AsQueryable();
                    if (!string.IsNullOrEmpty(ViewMode?.IDPERSONA.ToString()) && ViewMode.IDPERSONA > 0)
                    {
                        consulta = consulta.Where(c => c.IDPERSONA == ViewMode.IDPERSONA);
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
                Entyti.TIPODOCUMENTO = ex.Message;
            }
            return Entyti;
        }

        public static int Modificar(int valor, SCI_PERSONA ViewModels)
        {
            int idreturn = 0;
            using (BDIntegrityEntities context = new BDIntegrityEntities())
            {
                using (var transactionScope = context.Database.BeginTransaction())
                {
                    try
                    {
                        var Existente = context.SCI_PERSONA.Find(ViewModels.IDPERSONA);
                        if (valor == 2)
                        {
                            ViewModels.FECHACREACION = Existente.FECHACREACION; // Actualizar la fecha de modificación
                            ViewModels.USUARIOCREACION = Existente.USUARIOCREACION;
                            ViewModels.IPCREACION = Existente.IPCREACION;
                        }
                        else
                        {
                            var ESTADO = ViewModels.ESTADO;
                            var FECHACREACION = Existente.FECHAMODIFICACION; // Actualizar la fecha de modificación
                            var SUARIOCREACION = Existente.USUARIOMODIFICACION;
                            var IPCREACION = Existente.IPMODIFICACION;

                            ViewModels = Existente;
                            ViewModels.ESTADO = ESTADO;
                            ViewModels.FECHAMODIFICACION = FECHACREACION; // Actualizar la fecha de modificación
                            ViewModels.USUARIOMODIFICACION = SUARIOCREACION;
                            ViewModels.IPMODIFICACION = IPCREACION;
                        }
                   

                        // Actualiza las propiedades del usuario existente con los valores del usuario modificado
                        context.Entry(Existente).CurrentValues.SetValues(ViewModels);
                        // Guarda los cambios en la base de datos
                        context.SaveChanges();
                        // Confirma la transacción
                        transactionScope.Commit();
                        idreturn = ViewModels.IDPERSONA;
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var validationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                // Mostrar el error en la consola o registrarlo en un log
                                Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                            }
                        }
                        transactionScope.Rollback();
                        //throw new Exception("Error al modificar el registro.", ex);
                        idreturn = -1;

                    }
                }
            }

            return idreturn;
        }

        public static int Nuevo(int valor, SCI_PERSONA ViewModels)
        {
            int idreturn = 0;
            using (BDIntegrityEntities context = new BDIntegrityEntities())
            {
                using (var transactionScope = context.Database.BeginTransaction())
                {
                    int Idsecuencia = 0;
                    try
                    {
                        var listado = context.SCI_PERSONA;
                        if (listado.Count() > 0)
                        {
                            Idsecuencia = listado.Max(C => C.IDPERSONA);
                        }
                        else
                        {
                            Idsecuencia = 0;
                        }
                        ViewModels.IDPERSONA = Idsecuencia + 1;
                        context.SCI_PERSONA.Add(ViewModels);                       
                        context.SaveChanges();
                        transactionScope.Commit();
                        idreturn = ViewModels.IDPERSONA;
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var validationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                // Mostrar el error en la consola o registrarlo en un log
                                Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                            }
                        }
                        transactionScope.Rollback();
                        //throw new Exception("Error al modificar el registro.", ex);
                        idreturn = -1;

                    }
                }
            }

            return idreturn;
        }

    }

}