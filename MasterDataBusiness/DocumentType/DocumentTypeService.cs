using Comone.Utils;
using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using Newtonsoft.Json;

namespace MasterDataBusiness
{
    public class DocumentTypeService
    {
        private MasterDataDbContext db;

        public DocumentTypeService()
        {
            db = new MasterDataDbContext();
        }

        public DocumentTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterDocumentType
        public actionResultDocumentTypeViewModel FilterInClause(string jsonData)
        {
            try
            {
                SearchDocumentTypeInClauseViewModel data = JsonConvert.DeserializeObject<SearchDocumentTypeInClauseViewModel>(jsonData);

                var query = db.View_DocumentType.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if ((data.List_DocumentType_Id?.Count ?? 0) > 0)
                {
                    query = query.Where(c => data.List_DocumentType_Id.Contains(c.DocumentType_Id));
                }

                if ((data.List_DocumentType_Name?.Count ?? 0) > 0)
                {
                    query = query.Where(c => data.List_DocumentType_Name.Contains(c.DocumentType_Name));
                }

                if ((data.List_DocumentType_Index?.Count ?? 0 ) > 0)
                {
                    query = query.Where(c => data.List_DocumentType_Index.Contains(c.DocumentType_Index));
                }

                if (data.Process_Index.HasValue)
                {
                    query = query.Where(c => c.Process_Index == data.Process_Index);
                }

                var Item = new List<View_DocumentType>();
                var TotalRow = new List<View_DocumentType>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.DocumentType_Id).ToList();

                var result = new List<SearchDocumentTypeViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchDocumentTypeViewModel();

                    resultItem.documentType_Index = item.DocumentType_Index;
                    resultItem.documentType_Id = item.DocumentType_Id;
                    resultItem.documentType_Name = item.DocumentType_Name;
                    resultItem.format_Text = item.Format_Text;
                    resultItem.format_Date = item.Format_Date;
                    resultItem.format_Running = item.Format_Running;
                    resultItem.format_Document = item.Format_Document;
                    resultItem.isResetByYear = item.IsResetByYear;
                    resultItem.isResetByMonth = item.IsResetByMonth;
                    resultItem.isResetByDay = item.IsResetByDay;
                    resultItem.isActive = item.IsActive;
                    resultItem.isDelete = item.IsDelete;

                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultDocumentTypeViewModel = new actionResultDocumentTypeViewModel();
                actionResultDocumentTypeViewModel.itemsDocumentType = result.ToList();
                actionResultDocumentTypeViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = null };

                return actionResultDocumentTypeViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public actionResultDocumentTypeViewModel filterDocumentType(SearchDocumentTypeViewModel data)
        {
            try
            {
                var query = db.View_DocumentType.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.DocumentType_Id.Contains(data.key)
                                        || c.DocumentType_Name.Contains(data.key));


                }


                var Item = new List<View_DocumentType>();
                var TotalRow = new List<View_DocumentType>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.DocumentType_Id).ToList();

                var result = new List<SearchDocumentTypeViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchDocumentTypeViewModel();

                    resultItem.documentType_Index = item.DocumentType_Index;
                    resultItem.documentType_Id = item.DocumentType_Id;
                    resultItem.documentType_Name = item.DocumentType_Name;
                    resultItem.format_Text = item.Format_Text;
                    resultItem.format_Date = item.Format_Date;
                    resultItem.format_Running = item.Format_Running;
                    resultItem.isResetByYear = item.IsResetByYear;
                    resultItem.isResetByMonth = item.IsResetByMonth;
                    resultItem.isResetByDay = item.IsResetByDay;
                    resultItem.isActive = item.IsActive;
                    resultItem.isDelete = item.IsDelete;
  
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultDocumentTypeViewModel = new actionResultDocumentTypeViewModel();
                actionResultDocumentTypeViewModel.itemsDocumentType = result.ToList();
                actionResultDocumentTypeViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultDocumentTypeViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChangesDocumentType
        public String SaveChangesDocumentType(DocumentTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var documentTypeOld = db.MS_DocumentType.Find(data.documentType_Index);
                                             

                if (documentTypeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.documentType_Id))
                    {
                        var query = db.MS_DocumentType.FirstOrDefault(c => c.DocumentType_Id == data.documentType_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.documentType_Id))
                    {
                        data.documentType_Id = "DocumentType_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_DocumentType.FirstOrDefault(c => c.DocumentType_Id == data.documentType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.DocumentType_Id == data.documentType_Id)
                                {
                                    data.documentType_Id = "DocumentType_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_DocumentType Model = new MS_DocumentType();


                    Model.DocumentType_Index = Guid.NewGuid();
                    Model.DocumentType_Id = data.documentType_Id;
                    Model.DocumentType_Name = data.documentType_Name;
                    Model.Process_Index = data.process_Index;
                    Model.Format_Text = data.format_Text;
                    Model.Format_Date = data.format_Date;
                    Model.Format_Running = data.format_Running;
                    Model.Format_Document = data.format_Document;
                    Model.IsResetByYear = data.isResetByYear;
                    Model.IsResetByMonth = data.isResetByMonth;
                    Model.IsResetByDay = data.isResetByDay;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_DocumentType.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.documentType_Id))
                    {
                        if (documentTypeOld.DocumentType_Id != "")
                        {
                            data.documentType_Id = documentTypeOld.DocumentType_Id;
                        }
                    }
                    else
                    {
                        if (documentTypeOld.DocumentType_Id != data.documentType_Id)
                        {
                            var query = db.MS_DocumentType.FirstOrDefault(c => c.DocumentType_Id == data.documentType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.documentType_Id = documentTypeOld.DocumentType_Id;
                        }
                    }
                    documentTypeOld.DocumentType_Name = data.documentType_Name;
                    documentTypeOld.DocumentType_Id = data.documentType_Id;
                    documentTypeOld.DocumentType_Name = data.documentType_Name;
                    documentTypeOld.Process_Index = data.process_Index;
                    documentTypeOld.Format_Text = data.format_Text;
                    documentTypeOld.Format_Date = data.format_Date;
                    documentTypeOld.Format_Running = data.format_Running;
                    documentTypeOld.Format_Document = data.format_Document;
                    documentTypeOld.IsResetByYear = data.isResetByYear;
                    documentTypeOld.IsResetByMonth = data.isResetByMonth;
                    documentTypeOld.IsResetByDay = data.isResetByDay;
                    documentTypeOld.IsActive = Convert.ToInt32(data.isActive);
                    documentTypeOld.Update_By = data.create_By;
                    documentTypeOld.Update_Date = DateTime.Now;
                }

                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("SaveDocumentType", msglog);
                    transactionx.Rollback();

                    throw exy;
                }

                return "Done"; 

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region find
        public DocumentTypeViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_DocumentType.Where(c => c.DocumentType_Index == id).FirstOrDefault();

                var result = new DocumentTypeViewModel();

                result.documentType_Index = queryResult.DocumentType_Index;
                result.documentType_Id = queryResult.DocumentType_Id;
                result.documentType_Name = queryResult.DocumentType_Name;
                result.process_Index = queryResult.Process_Index;
                result.format_Text = queryResult.Format_Text;
                result.format_Date = queryResult.Format_Date;
                result.format_Running = queryResult.Format_Running;
                result.format_Document = queryResult.Format_Document;
                result.isResetByYear = queryResult.IsResetByYear;
                result.isResetByMonth = queryResult.IsResetByMonth;
                result.isResetByDay = queryResult.IsResetByDay;
                result.key = queryResult.Process_Id + " - " + queryResult.Process_Name;

                result.isActive = queryResult.IsActive;

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getDelete
        public Boolean getDelete(DocumentTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Room = db.MS_DocumentType.Find(data.documentType_Index);

                if (Room != null)
                {
                    Room.IsActive = 0;
                    Room.IsDelete = 1;


                    var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                    try
                    {
                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }

                    catch (Exception exy)
                    {
                        msglog = State + " ex Rollback " + exy.Message.ToString();
                        olog.logging("DeleteDocumentType", msglog);
                        transaction.Rollback();
                        throw exy;
                    }

                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Export Excel
        public DocumentTypeActionResultExportViewModel Export(DocumentTypeExportViewModel data)
        {
            try
            {
                var query = db.MS_DocumentType.AsQueryable();


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.DocumentType_Id.Contains(data.key)
                                        || c.DocumentType_Name.Contains(data.key));

                }

                var Item = new List<MS_DocumentType>();
                var TotalRow = new List<MS_DocumentType>();

                TotalRow = query.ToList();

                Item = query.OrderBy(o => o.DocumentType_Id).ToList();

                var result = new List<DocumentTypeExportViewModel>();

                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new DocumentTypeExportViewModel();
                    resultItem.numBerOf = num + 1;
                    resultItem.documentType_Id = item.DocumentType_Id;
                    resultItem.documentType_Name = item.DocumentType_Name;
                    resultItem.documentType_Index = item.DocumentType_Index;
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.cancel_By = item.Cancel_By == null ? "" : item.Cancel_By;
                    resultItem.cancel_Date = item.Cancel_Date != null ? item.Cancel_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.isActive = item.IsActive;
                    resultItem.isDelete = item.IsDelete;
                    resultItem.activeStatus = item.IsActive == 1 ? "เปิดใช้งาน" : "ปิดใช้งาน";
                    result.Add(resultItem);
                    num++;
                }

                var count = TotalRow.Count;

                var DocumentTypeActionResultExportViewModel = new DocumentTypeActionResultExportViewModel();
                DocumentTypeActionResultExportViewModel.itemsDocumentType = result.ToList();

                return DocumentTypeActionResultExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion




        public List<DocumentTypeViewModel> Filter()
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_DocumentType.FromSql("sp_GetDocumentType").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

                    var result = new List<DocumentTypeViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new DocumentTypeViewModel();
                        resultItem.documentType_Index = item.DocumentType_Index;
                        resultItem.documentType_Id = item.DocumentType_Id;
                        resultItem.documentType_Name = item.DocumentType_Name;
                        resultItem.format_Text = item.Format_Text;
                        resultItem.format_Date = item.Format_Date;
                        resultItem.format_Running = item.Format_Running;
                        resultItem.isResetByYear = item.IsResetByYear;
                        resultItem.isResetByMonth = item.IsResetByMonth;
                        resultItem.isResetByDay = item.IsResetByDay;
                        resultItem.isActive = item.IsActive;
                        resultItem.isDelete = item.IsDelete;
                        resultItem.isSystem = item.IsSystem;
                        resultItem.status_Id = item.Status_Id;
                        resultItem.create_Date = item.Create_Date.GetValueOrDefault();
                        resultItem.create_By = item.Create_By;
                        resultItem.update_Date = item.Update_Date.GetValueOrDefault();
                        resultItem.update_By = item.Update_By;
                        resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
                        resultItem.cancel_By = item.Cancel_By;

                        result.Add(resultItem);
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentTypeViewModel> FilterGR(Guid id)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var result = new List<DocumentTypeViewModel>();
                    if (id.ToString() != "" && id.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        string pstring = "";


                        pstring = " and DocumentType_Index in (select DocumentType_Index_To from sy_DocumentTypeRef where DocumentType_Index = '" + id + "')";
                        var strwhere = new SqlParameter("@strwhere", pstring);
                        var queryResult = context.MS_DocumentType.FromSql("sp_GetDocumentType @strwhere", strwhere).Where(c => c.Process_Index == new Guid("5F147725-520C-4CA6-B1D2-2C0E65E7AAAA") && c.IsActive == 1 && c.IsDelete == 0).ToList();
                        foreach (var item in queryResult)
                        {
                            var resultItem = new DocumentTypeViewModel();

                            resultItem.documentType_Index = item.DocumentType_Index;
                            resultItem.documentType_Id = item.DocumentType_Id;
                            resultItem.documentType_Name = item.DocumentType_Name;
                            resultItem.isActive = item.IsActive;
                            resultItem.isDelete = item.IsDelete;
                            resultItem.isSystem = item.IsSystem;
                            resultItem.status_Id = item.Status_Id;
                            resultItem.create_Date = item.Create_Date.GetValueOrDefault();
                            resultItem.create_By = item.Create_By;
                            resultItem.update_Date = item.Update_Date.GetValueOrDefault();
                            resultItem.update_By = item.Update_By;
                            resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
                            resultItem.cancel_By = item.Cancel_By;

                            result.Add(resultItem);
                        }
                    }
                    else
                    {
                        var queryResult = context.MS_DocumentType.FromSql("sp_GetDocumentType").Where(c => c.Process_Index == new Guid("5F147725-520C-4CA6-B1D2-2C0E65E7AAAA") && c.IsActive == 1 && c.IsDelete == 0).ToList();
                        foreach (var item in queryResult)
                        {
                            var resultItem = new DocumentTypeViewModel();

                            resultItem.documentType_Index = item.DocumentType_Index;
                            resultItem.documentType_Id = item.DocumentType_Id;
                            resultItem.documentType_Name = item.DocumentType_Name;
                            resultItem.isActive = item.IsActive;
                            resultItem.isDelete = item.IsDelete;
                            resultItem.isSystem = item.IsSystem;
                            resultItem.status_Id = item.Status_Id;
                            resultItem.create_Date = item.Create_Date.GetValueOrDefault();
                            resultItem.create_By = item.Create_By;
                            resultItem.update_Date = item.Update_Date.GetValueOrDefault();
                            resultItem.update_By = item.Update_By;
                            resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
                            resultItem.cancel_By = item.Cancel_By;

                            result.Add(resultItem);
                        }
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentTypeViewModel> filterPI(DocumentTypeViewModel model)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var sql = "and Process_Index = '22744590-55D8-4448-88EF-5997C252111F' and IsActive = 1 and IsDelete = 0";
                    if (!string.IsNullOrEmpty(model.documentType_Id))
                    {
                        sql += " and DocumentType_Id like '%'" + model.documentType_Id + "'%'";
                    }

                    if (!string.IsNullOrEmpty(model.documentType_Name))
                    {
                        sql += " and DocumentType_Id like '%'" + model.documentType_Name + "'%'";
                    }

                    var strwhere = new SqlParameter("@strwhere", sql);

                    var queryResult = context.MS_DocumentType.FromSql("sp_GetDocumentType @strwhere", strwhere).ToList();
                    var result = new List<DocumentTypeViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new DocumentTypeViewModel();
                        resultItem.documentType_Index = item.DocumentType_Index;
                        resultItem.documentType_Id = item.DocumentType_Id;
                        resultItem.documentType_Name = item.DocumentType_Name;
                        resultItem.format_Text = item.Format_Text;
                        resultItem.format_Date = item.Format_Date;
                        resultItem.format_Running = item.Format_Running;
                        resultItem.isResetByYear = item.IsResetByYear;
                        resultItem.isResetByMonth = item.IsResetByMonth;
                        resultItem.isResetByDay = item.IsResetByDay;
                        resultItem.isActive = item.IsActive;
                        resultItem.isDelete = item.IsDelete;
                        resultItem.isSystem = item.IsSystem;
                        resultItem.status_Id = item.Status_Id;
                        resultItem.create_Date = item.Create_Date.GetValueOrDefault();
                        resultItem.create_By = item.Create_By;
                        resultItem.update_Date = item.Update_Date.GetValueOrDefault();
                        resultItem.update_By = item.Update_By;
                        resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
                        resultItem.cancel_By = item.Cancel_By;

                        result.Add(resultItem);
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DocumentTypeViewModel> getId(Guid id)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_DocumentType.FromSql("sp_GetDocumentType").Where(c => c.DocumentType_Index == id).ToList();
                    var result = new List<DocumentTypeViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new DocumentTypeViewModel();

                        if (item.Process_Index != null)
                        {
                            var itemList = context.sy_Process.FromSql("sp_GetProcess").Where(c => c.Process_Index == item.Process_Index).FirstOrDefault();
                            if (itemList != null)
                            {
                                resultItem.process_Index = item.Process_Index;
                                resultItem.process_Name = itemList.Process_Name;
                            }

                        }
                        resultItem.documentType_Index = item.DocumentType_Index;
                        resultItem.documentType_Id = item.DocumentType_Id;
                        resultItem.documentType_Name = item.DocumentType_Name;
                        resultItem.format_Text = item.Format_Text;
                        resultItem.format_Date = item.Format_Date;
                        resultItem.format_Running = item.Format_Running;
                        resultItem.format_Document = item.Format_Document;
                        resultItem.isResetByYear = item.IsResetByYear;
                        resultItem.isResetByMonth = item.IsResetByMonth;
                        resultItem.isResetByDay = item.IsResetByDay;
                        resultItem.isActive = item.IsActive;
                        resultItem.isDelete = item.IsDelete;
                        resultItem.isSystem = item.IsSystem;
                        resultItem.status_Id = item.Status_Id;
                        resultItem.create_Date = item.Create_Date.GetValueOrDefault();
                        resultItem.create_By = item.Create_By;
                        resultItem.update_Date = item.Update_Date.GetValueOrDefault();
                        resultItem.update_By = item.Update_By;
                        resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
                        resultItem.cancel_By = item.Cancel_By;

                        result.Add(resultItem);
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public String SaveChanges(DocumentTypeViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    if (data.documentType_Index.ToString() == "00000000-0000-0000-0000-000000000000")
                    {
                        data.documentType_Index = Guid.NewGuid();
                    }
                    if (data.documentType_Id == null)
                    {
                        var Sys_Key = new SqlParameter("Sys_Key", "documentType_Id");
                        var resultParameter = new SqlParameter("@result", SqlDbType.Int);
                        resultParameter.Size = 2000; // some meaningfull value
                        resultParameter.Direction = ParameterDirection.Output;
                        context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
                        //var result = resultParameter.Value;
                        data.documentType_Id = resultParameter.Value.ToString();
                    }
                    int isactive = 1;
                    int isdelete = 0;
                    int isSystem = 0;
                    int statusId = 0;
                    var DocumentType_Index = new SqlParameter("DocumentType_Index", data.documentType_Index);
                    var Process_Index = new SqlParameter("Process_Index", data.process_Index);
                    var DocumentType_Id = new SqlParameter("DocumentType_Id", data.documentType_Id);
                    var DocumentType_Name = new SqlParameter("DocumentType_Name", data.documentType_Name);
                    var Format_Text = new SqlParameter("Format_Text", data.format_Text);
                    var Format_Date = new SqlParameter("Format_Date", data.format_Date);
                    var Format_Running = new SqlParameter("Format_Running", data.format_Running);
                    var Format_Document = new SqlParameter("Format_Document", data.format_Document);
                    var IsResetByYear = new SqlParameter("IsResetByYear", data.isResetByYear);
                    var IsResetByMonth = new SqlParameter("IsResetByMonth", data.isResetByMonth);
                    var IsResetByDay = new SqlParameter("IsResetByDay", data.isResetByDay);
                    var IsActive = new SqlParameter("IsActive", isactive);
                    var IsDelete = new SqlParameter("IsDelete", isdelete);
                    var IsSystem = new SqlParameter("IsSystem", isSystem);
                    var Status_Id = new SqlParameter("Status_Id", statusId);
                    var Create_By = new SqlParameter("Create_By", "");
                    var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
                    var Update_By = new SqlParameter("Update_By", "");
                    var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
                    var Cancel_By = new SqlParameter("Cancel_By", "");
                    var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
                    var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_DocumentType  @DocumentType_Index,@Process_Index,@DocumentType_Id,@DocumentType_Name,@Format_Text,@Format_Date,@Format_Running,@Format_Document,@IsResetByYear,@IsResetByMonth,@IsResetByDay,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", DocumentType_Index, Process_Index, DocumentType_Id, DocumentType_Name, Format_Text, Format_Date, Format_Running, Format_Document, IsResetByYear, IsResetByMonth, IsResetByDay, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
                    return rowsAffected.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DocumentTypeViewModel> getDelete(Guid id)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_DocumentType.FromSql("sp_GetDocumentType").Where(c => c.DocumentType_Index == id).ToList();
                    int isactive = 0;
                    int isdelete = 1;
                    var result = new List<DocumentTypeViewModel>();
                    foreach (var item in queryResult)
                    {
                        var DocumentType_Index = new SqlParameter("DocumentType_Index", item.DocumentType_Index);
                        var Process_Index = new SqlParameter("Process_Index", item.Process_Index);
                        var DocumentType_Id = new SqlParameter("DocumentType_Id", item.DocumentType_Id);
                        var DocumentType_Name = new SqlParameter("DocumentType_Name", item.DocumentType_Name);
                        var Format_Text = new SqlParameter("Format_Text", item.Format_Text);
                        var Format_Date = new SqlParameter("Format_Date", item.Format_Date);
                        var Format_Running = new SqlParameter("Format_Running", item.Format_Running);
                        var Format_Document = new SqlParameter("Format_Document", item.Format_Document);
                        var IsResetByYear = new SqlParameter("IsResetByYear", item.IsResetByYear);
                        var IsResetByMonth = new SqlParameter("IsResetByMonth", item.IsResetByMonth);
                        var IsResetByDay = new SqlParameter("IsResetByDay", item.IsResetByDay);
                        var IsActive = new SqlParameter("IsActive", isactive);
                        var IsDelete = new SqlParameter("IsDelete", isdelete);
                        var IsSystem = new SqlParameter("IsSystem", item.IsSystem);
                        var Status_Id = new SqlParameter("Status_Id", item.Status_Id);
                        var Create_By = new SqlParameter("Create_By", "");
                        var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
                        var Update_By = new SqlParameter("Update_By", "");
                        var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
                        var Cancel_By = new SqlParameter("Cancel_By", "");
                        var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
                        var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_DocumentType  @DocumentType_Index,@Process_Index,@DocumentType_Id,@DocumentType_Name,@Format_Text,@Format_Date,@Format_Running,@Format_Document,@IsResetByYear,@IsResetByMonth,@IsResetByDay,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", DocumentType_Index, Process_Index, DocumentType_Id, DocumentType_Name, Format_Text, Format_Date, Format_Running, Format_Document, IsResetByYear, IsResetByMonth, IsResetByDay, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
                        context.SaveChanges();
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentTypeViewModel> FilterPlanGR()
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_DocumentType.FromSql("sp_GetDocumentType").Where(c => c.Process_Index == new Guid("C2A3F847-BAA6-46FE-B502-44F2D5826A1C") && c.IsActive == 1 && c.IsDelete == 0).ToList();
                    var result = new List<DocumentTypeViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new DocumentTypeViewModel();
                        resultItem.documentType_Index = item.DocumentType_Index;
                        resultItem.documentType_Id = item.DocumentType_Id;
                        resultItem.documentType_Name = item.DocumentType_Name;
                        resultItem.format_Text = item.Format_Text;
                        resultItem.format_Date = item.Format_Date;
                        resultItem.format_Running = item.Format_Running;
                        resultItem.isResetByYear = item.IsResetByYear;
                        resultItem.isResetByMonth = item.IsResetByMonth;
                        resultItem.isResetByDay = item.IsResetByDay;
                        resultItem.isActive = item.IsActive;
                        resultItem.isDelete = item.IsDelete;
                        resultItem.isSystem = item.IsSystem;
                        resultItem.status_Id = item.Status_Id;
                        resultItem.create_Date = item.Create_Date.GetValueOrDefault();
                        resultItem.create_By = item.Create_By;
                        resultItem.update_Date = item.Update_Date.GetValueOrDefault();
                        resultItem.update_By = item.Update_By;
                        resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
                        resultItem.cancel_By = item.Cancel_By;

                        result.Add(resultItem);
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentTypeViewModel> search(DocumentTypeViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    var result = new List<DocumentTypeViewModel>();

                    var name = new SqlParameter("@pName", data.documentType_Name);
                    if (data.documentType_Name == null)
                    {
                        name.SqlValue = DBNull.Value;
                    }
                    var id = new SqlParameter("@pId", data.documentType_Id);
                    if (data.documentType_Id == null)
                    {
                        id.SqlValue = DBNull.Value;
                    }
                    var query = context.MS_DocumentType.FromSql("sp_GetSearchDocumentType @pId ,@pName ", id, name).ToList();
                    foreach (var item in query)
                    {
                        var resultItem = new DocumentTypeViewModel();
                        resultItem.documentType_Id = item.DocumentType_Id;
                        resultItem.documentType_Name = item.DocumentType_Name;
                        result.Add(resultItem);
                    }


                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ItemListViewModel> autoGRSearch(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var query = context.MS_DocumentType.AsQueryable();
                    var items = new List<ItemListViewModel>();

                    var queryresult = new List<MS_DocumentType>();
                    string pwhereFilter = "";

                    if (data.chk == 1)
                    {

                        var GRProcess = Guid.Parse("5F147725-520C-4CA6-B1D2-2C0E65E7AAAA");
                        query = query.Where(c => c.DocumentType_Name.Contains(data.key) && c.Process_Index == GRProcess);

                    }
                    //if (data.chk == 2)
                    //{

                    //    if (data.documentType_Id != "" && data.documentType_Id != null)
                    //    {
                    //        pwhereFilter = " And DocumentType_Id like'%" + data.documentType_Id + "%'";
                    //    }
                    //    if (data.documentType_Name != "" && data.documentType_Name != null)
                    //    {
                    //        pwhereFilter += " And DocumentType_Name like'%" + data.documentType_Name + "%'";
                    //    }
                    //    pwhereFilter += " and DocumentType_Index in (select DocumentType_Index_To from sy_DocumentTypeRef where DocumentType_Index = '" + data.documentType_Index + "')";
                    //    var strwhere = new SqlParameter("@strwhere", pwhereFilter);
                    //    queryresult = context.MS_DocumentType.FromSql("sp_GetDocumentType @strwhere ", strwhere).Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.Process_Index == new Guid("5F147725-520C-4CA6-B1D2-2C0E65E7AAAA")).ToList();
                    //}
                    var result = query.Select(c => new { c.DocumentType_Index, c.DocumentType_Id, c.DocumentType_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.DocumentType_Index,
                            id = item.DocumentType_Id,
                            name = item.DocumentType_Name

                        };

                        items.Add(resultItem);
                    }

                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<DocumentTypeViewModel> documentTypefilter(DocumentTypeViewModel data)
        {
            var olog = new logtxt();
            String msglog = "";
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var result = new List<DocumentTypeViewModel>();

                    var query = context.MS_DocumentType.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                    if (!string.IsNullOrEmpty(data.documentType_Id))
                    {
                        msglog = "DocumentType_Id";
                        query = query.Where(c => c.DocumentType_Id == data.documentType_Id);
                    }

                    if (!string.IsNullOrEmpty(data.documentType_Name))
                    {
                        msglog = "DocumentType_Name";
                        query = query.Where(c => c.DocumentType_Name == data.documentType_Name);
                    }
                    if (data.process_Index != new Guid("00000000-0000-0000-0000-000000000000".ToString()))
                    {
                        msglog = "Process_Index";
                        query = query.Where(c => c.Process_Index == data.process_Index);
                    }
                    if (data.documentType_Index != null && data.documentType_Index != new Guid("00000000-0000-0000-0000-000000000000".ToString()))
                    {
                        msglog = "DocumentType_Index";
                        query = query.Where(c => c.DocumentType_Index == data.documentType_Index);
                    }

                    var queryResult = query.OrderBy(o => o.DocumentType_Id).ToList();

                    foreach (var item in queryResult)
                    {
                        msglog = "by values";
                        var resultItem = new DocumentTypeViewModel();
                        resultItem.documentType_Index = item.DocumentType_Index;
                        resultItem.documentType_Id = item.DocumentType_Id;
                        resultItem.documentType_Name = item.DocumentType_Name;
                        resultItem.format_Date = item.Format_Date;
                        resultItem.format_Document = item.Format_Document;
                        resultItem.format_Running = item.Format_Running;
                        resultItem.format_Text = item.Format_Text;
                        resultItem.isResetByDay = item.IsResetByDay;
                        resultItem.isResetByMonth = item.IsResetByMonth;
                        resultItem.isResetByYear = item.IsResetByYear;
                        resultItem.process_Index = item.Process_Index;
                        resultItem.create_Date = item.Create_Date;

                        result.Add(resultItem);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                olog.logging("documentTypefilter", msglog + " ex Rollback " + ex.Message.ToString());
                throw ex;
            }
        }

        public List<DocumentTypeViewModel> getFormat(DocumentTypeViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var result = new List<DocumentTypeViewModel>();

                    var query = context.MS_DocumentType.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                    if (!string.IsNullOrEmpty(data.documentType_Id))
                    {
                        query = query.Where(c => c.DocumentType_Id.Contains(data.documentType_Id));
                    }

                    if (!string.IsNullOrEmpty(data.documentType_Name))
                    {
                        query = query.Where(c => c.DocumentType_Name.Contains(data.documentType_Name));
                    }

                    if (!string.IsNullOrEmpty(data.documentType_Index.ToString()) && data.documentType_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.DocumentType_Index == data.documentType_Index);
                    }

                    var queryResult = query.OrderBy(o => o.DocumentType_Name).ToList();

                    foreach (var item in queryResult)
                    {
                        var resultItem = new DocumentTypeViewModel();
                        resultItem.documentType_Index = item.DocumentType_Index;
                        resultItem.documentType_Id = item.DocumentType_Id;
                        resultItem.documentType_Name = item.DocumentType_Name;
                        resultItem.format_Date = item.Format_Date;
                        resultItem.format_Document = item.Format_Document;
                        resultItem.format_Running = item.Format_Running;
                        resultItem.format_Text = item.Format_Text;

                        result.Add(resultItem);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
