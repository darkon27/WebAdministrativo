using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Transactions;
using WebAdministrativo.Clases;
using WebAdministrativo.Models;
using WebAdministrativo.ViewModels;


namespace WebAdministrativo.Service
{
    public class GarantiaService
    {

        public static List<VIEW_Garantia> BuscarGarantias(VIEW_Garantia vIEW_Garantia)
        {
            List<VIEW_Garantia> garantias = new List<VIEW_Garantia>();
            try
            {
                using (BDIntegrityEntities context = new BDIntegrityEntities())
                {
                    IQueryable<VIEW_Garantia> consulta = context.VIEW_Garantia.AsQueryable();
                    //if (!string.IsNullOrEmpty(vIEW_Garantia.FECHAINICIO.ToString()))
                    //{
                    //    consulta = consulta.Where(c => c.FECHAINICIO >= vIEW_Garantia.FECHAINICIO);
                    //}

                    //if (!string.IsNullOrEmpty(vIEW_Garantia.FECHAFIN.ToString()))
                    //{
                    //    consulta = consulta.Where(c => c.FECHAFIN <= vIEW_Garantia.FECHAFIN);
                    //}
                    if (!string.IsNullOrEmpty(vIEW_Garantia.ESTADO.ToString()))
                    {
                        consulta = consulta.Where(c => c.ESTADO == vIEW_Garantia.ESTADO);
                    }
                    if (!string.IsNullOrEmpty(vIEW_Garantia?.DESCRIPCION?.ToString()))
                    {
                        consulta = consulta.Where(c => c.DESCRIPCION.ToUpper().Contains(vIEW_Garantia.DESCRIPCION.ToUpper()));
                    }
                    if (!string.IsNullOrEmpty(vIEW_Garantia?.REFERENCIA?.ToString()))
                    {
                        consulta = consulta.Where(c => c.REFERENCIA.ToUpper().Contains(vIEW_Garantia.REFERENCIA.ToUpper()));
                    }
                    if (!string.IsNullOrEmpty(vIEW_Garantia?.CONTACTO?.ToString()))
                    {
                        consulta = consulta.Where(c => c.CONTACTO.ToUpper().Contains(vIEW_Garantia.CONTACTO.ToUpper()));
                    }
                    garantias = consulta.ToList();
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
                UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Error|BuscarGarantias =" + msj + msjson);
            }
            return garantias;
        }

        public static List<VIEW_GarantiaDetalle> BuscarGarantiasDetalle(VIEW_Garantia vIEW_Garantia)
        {
            List<VIEW_GarantiaDetalle> garantias = new List<VIEW_GarantiaDetalle>();
            try
            {
                using (BDIntegrityEntities context = new BDIntegrityEntities())
                {
                    IQueryable<VIEW_GarantiaDetalle> consulta = context.VIEW_GarantiaDetalle.AsQueryable();
                    if (!string.IsNullOrEmpty(vIEW_Garantia.ESTADO.ToString()))
                    {
                        consulta = consulta.Where(c => c.ESTADO == vIEW_Garantia.ESTADO);
                    }

                    if (!string.IsNullOrEmpty(vIEW_Garantia?.DESCRIPCION?.ToString()))
                    {
                        consulta = consulta.Where(c => c.DESCRIPCION.ToUpper().Contains(vIEW_Garantia.DESCRIPCION.ToUpper()));
                    }

                    if (!string.IsNullOrEmpty(vIEW_Garantia.IDGARANTIA.ToString()))
                    {
                        consulta = consulta.Where(c => c.IDGARANTIA == vIEW_Garantia.IDGARANTIA);
                    }

                    garantias = consulta.ToList();
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
                UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Error|BuscarGarantiasDetalle =" + msj + msjson);
            }
            return garantias;
        }


        public static int InsertarGarantias(GarantiaViewModels ViewModels)
        {
            ViewModels.Garantia.IDGARANTIA = 0;
            int Idsecuencia = 0;
            using (BDIntegrityEntities context = new BDIntegrityEntities())
            {
                using (var transactionScope = new TransactionScope())
                {
                    // Guardar los cambios en la base de datos
                    try
                    {
                        var listado = context.SCI_GARANTIA;
                        if (listado.Count() > 0)
                        {
                            Idsecuencia = listado.Max(C => C.IDGARANTIA);
                        }
                        else
                        {
                            Idsecuencia = 0;
                        }
                        ViewModels.Garantia.IDGARANTIA = Idsecuencia + 1;
                        context.SCI_GARANTIA.Add(ViewModels.Garantia);
                        context.SaveChanges();
                        int linea = 0;
                        foreach (var odj in ViewModels.ListaDetalle)
                        {
                            SCI_GARANTIADETALLE EntyObj = new SCI_GARANTIADETALLE();
                            EntyObj.IDGARANTIA = ViewModels.Garantia.IDGARANTIA;
                            EntyObj.ITEM = odj.ITEM;
                            EntyObj.CANTIDAD = odj.CANTIDAD;
                            EntyObj.MARCA = odj.MARCA;
                            EntyObj.MODELO = odj.MODELO;
                            EntyObj.DESCRIPCION = odj.DESCRIPCION;
                            EntyObj.ESTADO = 1;
                            EntyObj.FECHACREACION = DateTime.Now; // Actualizar la fecha de modificación
                            EntyObj.USUARIOCREACION = GlobalAdmin.UserAdmin; // Actualizar la fecha de modificación
                            linea++;
                            EntyObj.ID = linea;
                            context.SCI_GARANTIADETALLE.Add(EntyObj);
                            context.SaveChanges();
                        }
                        //ViewBag.Message = "Registro Insertado";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        // Manejar el caso de concurrencia optimista, por ejemplo, recargando los datos y volviendo a intentar
                        context.Entry(ViewModels.Garantia).Reload();
                        context.SaveChanges();
                        //ViewBag.Message = "Registro Forzado ";
                    }
                    transactionScope.Complete();
                }
            }
            return ViewModels.Garantia.IDGARANTIA;

        }

        public static Response Modificar(int valor, GarantiaViewModels ViewModels)
        {
            Response Resp = new Response();
            using (BDIntegrityEntities context = new BDIntegrityEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var existingDetalle = context.SCI_GARANTIA.Find(ViewModels.VIEW.IDGARANTIA);
                        if (existingDetalle != null)
                        {
                            if (valor == 3)
                            {   // Anula el detalle existente
                                existingDetalle.ESTADO = ViewModels.VIEW.ESTADO;
                                existingDetalle.USUARIODMODIFICACION = ViewModels.VIEW.USUARIODMODIFICACION;
                                existingDetalle.FECHAMODIFICACION = ViewModels.VIEW.FECHAMODIFICACION;
                                existingDetalle.IPMODIFICACION = ViewModels.VIEW.IPMODIFICACION;
                            }
                            else
                            { 
                               // Actualizar el detalle existente
                                existingDetalle.IDGARANTIA = ViewModels.VIEW.IDGARANTIA;
                                existingDetalle.IDEMPRESA = ViewModels.VIEW.IDEMPRESA;
                                existingDetalle.FECHAREGISTRO = ViewModels.VIEW.FECHAREGISTRO;
                                existingDetalle.CODIGO = ViewModels.VIEW.CODIGO;                            
                                existingDetalle.DESCRIPCION = ViewModels.VIEW.DESCRIPCION;
                                existingDetalle.IDCONTACTO = ViewModels.VIEW.IDCONTACTO;
                                existingDetalle.EMAIL = ViewModels.VIEW.EMAIL;
                                existingDetalle.FECHAINICIO = ViewModels.VIEW.FECHAINICIO;
                                existingDetalle.FECHAFIN = ViewModels.VIEW.FECHAFIN;
                                existingDetalle.REFERENCIA = ViewModels.VIEW.REFERENCIA;
                                existingDetalle.OBSERVACION = ViewModels.VIEW.OBSERVACION;
                                existingDetalle.ESTADO = ViewModels.VIEW.ESTADO;
                                existingDetalle.USUARIODMODIFICACION = ViewModels.VIEW.USUARIODMODIFICACION;
                                existingDetalle.FECHAMODIFICACION = ViewModels.VIEW.FECHAMODIFICACION;
                                existingDetalle.IPMODIFICACION = ViewModels.VIEW.IPMODIFICACION;
                            }
                            Resp.Data = ViewModels.VIEW;
                            Resp.Message = "Registro exitoso. ";
                            Resp.IsSucces = true;
                            context.SaveChanges();
                            transaction.Commit();
                        }
                        else
                        {
                            Resp.Data = ViewModels.VIEW;
                            Resp.Message = "Registro no Existe. ";
                            Resp.IsSucces = false;
                        }
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