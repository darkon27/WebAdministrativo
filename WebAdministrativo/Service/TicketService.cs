using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using WebAdministrativo.Clases;
using WebAdministrativo.Models;
using WebAdministrativo.ViewModels;

namespace WebAdministrativo.Service
{
    public class TicketService
    {
        public static List<VIEW_Ticket> Buscar(SCI_TICKET ViewMode)
        {
            List<VIEW_Ticket> Model = new List<VIEW_Ticket>();
            try
            {
                using (BDIntegrityEntities context = new BDIntegrityEntities())
                {
                    IQueryable<VIEW_Ticket> query = context.VIEW_Ticket.AsQueryable();
                    if (!string.IsNullOrEmpty(ViewMode.IDSOLICITANTE.ToString()))
                    {
                        query = query.Where(t => t.IDSOLICITANTE == ViewMode.IDSOLICITANTE);
                    }
                    if (!string.IsNullOrEmpty(ViewMode.IDRESPONSABLE.ToString()))
                    {
                        query = query.Where(t => t.IDRESPONSABLE == ViewMode.IDRESPONSABLE);
                    }
                    if (ViewMode.FECHAINI != null)
                    {
                        query = query.Where(t => t.FECHAREGISTRO >= ViewMode.FECHAINI);
                    }

                    if (ViewMode.FECHAFIN != null)
                    {
                        query = query.Where(t => t.FECHAREGISTRO <= ViewMode.FECHAFIN);
                    }

                    if (!string.IsNullOrEmpty(ViewMode.ESTADO.ToString()))
                    {
                        query = query.Where(t => t.ESTADO == ViewMode.ESTADO);
                    }
                    Model = query.ToList();
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

        public static int Modificar(int valor, SCI_TICKET ViewModels)
        {
            int idreturn = 0;
            using (BDIntegrityEntities context = new BDIntegrityEntities())
            {
                using (var transactionScope = context.Database.BeginTransaction())
                {
                    try
                    {
                        var Existente = context.SCI_TICKET.Find(ViewModels.IDTICKET);
                        if (valor == 2)
                        {
                            var ESTADO = ViewModels.ESTADO;
                            var FECHAMODIFICACION = Existente.FECHAMODIFICACION; // Actualizar la fecha de modificación
                            var USUARIODMODIFICACION = Existente.USUARIOCREACION;
                            var IPMODIFICACION = Existente.IPMODIFICACION;
                            var FECHAINI = ViewModels.FECHAINI;
                            var SOLUCION = ViewModels.SOLUCION;
                            var IDRESPONSABLE = ViewModels.IDRESPONSABLE;
                            var TIPOTICKET = ViewModels.TIPOTICKET;


                            ViewModels = Existente;
                            ViewModels.FECHAINI = FECHAINI;
                            ViewModels.FECHAMODIFICACION = FECHAMODIFICACION; // Actualizar la fecha de modificación
                            ViewModels.USUARIODMODIFICACION = USUARIODMODIFICACION;
                            ViewModels.IPMODIFICACION = IPMODIFICACION;
                            ViewModels.FECHAFIN = null;
                            ViewModels.TIPOTICKET = TIPOTICKET;
                            ViewModels.SOLUCION = SOLUCION;
                            ViewModels.IDRESPONSABLE = IDRESPONSABLE;
                        }
                        else
                        {
                            var ESTADO = ViewModels.ESTADO;
                            var FECHACREACION = Existente.FECHAMODIFICACION; // Actualizar la fecha de modificación
                            var SUARIOCREACION = Existente.USUARIOCREACION;
                            var IPCREACION = Existente.IPMODIFICACION;
                            var FECHAFIN = ViewModels.FECHAFIN;
                            ViewModels = Existente;
                            ViewModels.ESTADO = ESTADO;
                            ViewModels.FECHAMODIFICACION = FECHACREACION; // Actualizar la fecha de modificación
                            ViewModels.USUARIODMODIFICACION = SUARIOCREACION;
                            ViewModels.IPMODIFICACION = IPCREACION;
                            ViewModels.FECHAFIN = FECHAFIN;
                        }


                        // Actualiza las propiedades del usuario existente con los valores del usuario modificado
                        context.Entry(Existente).CurrentValues.SetValues(ViewModels);
                        // Guarda los cambios en la base de datos
                        context.SaveChanges();
                        // Confirma la transacción
                        transactionScope.Commit();
                        idreturn = ViewModels.IDTICKET;
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


        public static List<TicketEstadistico> TicketEstadistico(SCI_TICKET ViewMode)
        {
            List<TicketEstadistico> Model = new List<TicketEstadistico>();
            try
            {
                using (BDIntegrityEntities context = new BDIntegrityEntities())
                {
                    IQueryable<VIEW_Ticket> query = context.VIEW_Ticket.AsQueryable();
                    var result = query.Where(t => t.IDSOLICITANTE == ViewMode.IDSOLICITANTE)
                     .GroupBy(t => t.DESTIPOTICKET)
                     .Select(g => new
                     {
                         DESTIPOTICKET = g.Key,
                         TOTAL_REGISTROS = g.Count(),
                         FINALIZADO = g.Count(t => t.DESESTADO == "FINALIZADO"),
                         PENDIENTE = g.Count(t => t.DESESTADO == "PENDIENTE"),
                         ANULADO = g.Count(t => t.DESESTADO == "ANULADO"),
                         EN_PROCESO = g.Count(t => t.DESESTADO == "EN PROCESO"),
                         PORCENTAJE = (g.Count(t => t.DESESTADO == "FINALIZADO") * 100 / g.Count())
                     }).ToList();

                    foreach (var item in result)
                    {
                        TicketEstadistico repo = new TicketEstadistico();
                        repo.DESTIPOTICKET = item.DESTIPOTICKET;
                        repo.TOTAL_REGISTROS = item.TOTAL_REGISTROS;
                        repo.FINALIZADO = item.FINALIZADO;
                        repo.PENDIENTE = item.PENDIENTE;
                        repo.ANULADO = item.ANULADO;
                        repo.EN_PROCESO = item.EN_PROCESO;
                        repo.PORCENTAJE = item.PORCENTAJE;
                        Model.Add(repo);
                    }
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

    }
}