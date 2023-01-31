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
using System.Linq;
using System.Text;

namespace MasterDataBusiness
{
    public class DocumentTypeItemStatusService
    {
        //public List<DocumentTypeItemStatusViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_DocumentTypeItemStatus.FromSql("sp_GetDocumentTypeItemStatus").ToList();

        //            var result = new List<DocumentTypeItemStatusViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new DocumentTypeItemStatusViewModel();
        //                resultItem.DocumentTypeIndex = item.DocumentType_Index;
        //                resultItem.DocumentTypeItemStatusIndex = item.DocumentTypeItemStatus_Index;
        //                resultItem.ItemStatusIndex = item.ItemStatus_Index;
        //                resultItem.IsActive = item.IsActive;
        //                resultItem.IsDelete = item.IsDelete;
        //                resultItem.IsSystem = item.IsSystem;
        //                resultItem.StatusId = item.Status_Id;
        //                resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                resultItem.CreateBy = item.Create_By;
        //                resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                resultItem.UpdateBy = item.Update_By;
        //                resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                resultItem.CancelBy = item.Cancel_By;

        //                result.Add(resultItem);
        //            }

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #region FindTaskGroupWorkArea

        public DocumentTypeItemStatusViewModel find(Guid id)
        {
            try
            {

                var query = from dtis in db.MS_DocumentTypeItemStatus
                            join dt in db.MS_DocumentType on dtis.DocumentType_Index equals dt.DocumentType_Index
                            join its in db.MS_ItemStatus on dtis.ItemStatus_Index equals its.ItemStatus_Index
                            select new
                            {
                                dtis.DocumentTypeItemStatus_Index,
                                dtis.DocumentType_Index,
                                dtis.ItemStatus_Index,
                                dtis.DocumentTypeItemStatus_Id,
                                dt.DocumentType_Id,
                                dt.DocumentType_Name,
                                its.ItemStatus_Id,
                                its.ItemStatus_Name,
                                dtis.IsActive,
                                dtis.IsDelete,
                                dtis.Create_Date
                            };

                var queryResult = query.Where(c => c.DocumentTypeItemStatus_Index == id).FirstOrDefault();

                var result = new DocumentTypeItemStatusViewModel();


                 result.documentType_Index = queryResult.DocumentType_Index;
                 result.documentType_Id = queryResult.DocumentType_Id;
                 result.documentType_Name = queryResult.DocumentType_Name;
                 result.documentTypeItemStatus_Index = queryResult.DocumentTypeItemStatus_Index;
                 result.documentTypeItemStatus_Id = queryResult.DocumentTypeItemStatus_Id;
                 result.itemStatus_Index = queryResult.ItemStatus_Index;
                 result.itemStatus_Id = queryResult.ItemStatus_Id;
                 result.itemStatus_Name = queryResult.ItemStatus_Name;
                 result.isActive = queryResult.IsActive;
                 result.isDelete = queryResult.IsDelete;               



                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region FilterDocumentTypeItemStatus
        //Filter
        private MasterDataDbContext db;

        public DocumentTypeItemStatusService()
        {
            db = new MasterDataDbContext();
        }

        public DocumentTypeItemStatusService(MasterDataDbContext db)
        {
            this.db = db;
        }


        public actionResultDocumentTypeItemStatusViewModel filter(SearchDocumentTypeItemStatusViewModel data)
        {
            try
            {
                var query = from dtis in db.MS_DocumentTypeItemStatus
                            join dt in db.MS_DocumentType on dtis.DocumentType_Index equals dt.DocumentType_Index
                            join its in db.MS_ItemStatus on dtis.ItemStatus_Index equals its.ItemStatus_Index                            
                            select new
                            {
                                dtis.DocumentTypeItemStatus_Index,
                                dtis.DocumentTypeItemStatus_Id,
                                dtis.DocumentType_Index,
                                dtis.ItemStatus_Index,
                                dt.DocumentType_Id,
                                dt.DocumentType_Name,
                                its.ItemStatus_Id,
                                its.ItemStatus_Name,
                                dtis.IsActive,
                                dtis.IsDelete,
                                dtis.Create_Date
                            };

                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.DocumentType_Name.Contains(data.key)
                                        || c.ItemStatus_Name.Contains(data.key));

                }

                
                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                var Item = query.OrderBy(o => o.DocumentTypeItemStatus_Id).ToList();

                var result = new List<SearchDocumentTypeItemStatusViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchDocumentTypeItemStatusViewModel();

                    resultItem.documentTypeItemStatus_Index = item.DocumentTypeItemStatus_Index;
                    resultItem.documentTypeItemStatus_Id = item.DocumentTypeItemStatus_Id;
                    resultItem.documentType_Index = item.DocumentType_Index;
                    resultItem.documentType_Id = item.DocumentType_Id;
                    resultItem.documentType_Name = item.DocumentType_Name;
                    resultItem.itemStatus_Index = item.ItemStatus_Index;
                    resultItem.itemStatus_Id = item.ItemStatus_Id;
                    resultItem.itemStatus_Name = item.ItemStatus_Name;
                    resultItem.isActive = item.IsActive;
                    resultItem.isDelete = item.IsDelete;
                    resultItem.create_Date = item.Create_Date.ToString();


                    result.Add(resultItem);
                }

                var count = query.ToList().Count;

                var actionResultDocumentTypeItemStatusViewModel = new actionResultDocumentTypeItemStatusViewModel();
                actionResultDocumentTypeItemStatusViewModel.itemsDocumentTypeItemStatus = result.ToList();
                actionResultDocumentTypeItemStatusViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, };

                return actionResultDocumentTypeItemStatusViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region DeleteDocumentTypeItemStatus
        public Boolean getDelete(DocumentTypeItemStatusViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var documentTypeItemStatus = db.MS_DocumentTypeItemStatus.Find(data.documentTypeItemStatus_Index);

                if (documentTypeItemStatus != null)
                {
                    documentTypeItemStatus.IsActive = 0;
                    documentTypeItemStatus.IsDelete = 1;


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
                        olog.logging("DeleteTaskGroupWorkArea", msglog);
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


        #region SaveChanges

        public String SaveChanges(DocumentTypeItemStatusViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var DocumentTypeItemStatusOld = db.MS_DocumentTypeItemStatus.Find(data.documentTypeItemStatus_Index);

                if (DocumentTypeItemStatusOld == null)
                {
                    if (!string.IsNullOrEmpty(data.documentTypeItemStatus_Id))
                    {
                        var query = db.MS_DocumentTypeItemStatus.FirstOrDefault(c => c.DocumentTypeItemStatus_Id == data.documentTypeItemStatus_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.documentTypeItemStatus_Id))
                    {
                        data.documentTypeItemStatus_Id = "DocumentTypeItemStatus_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_DocumentTypeItemStatus.FirstOrDefault(c => c.DocumentTypeItemStatus_Id == data.documentTypeItemStatus_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.DocumentTypeItemStatus_Id == data.documentTypeItemStatus_Id)
                                {
                                    data.documentTypeItemStatus_Id = "DocumentTypeItemStatus_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //data.documentTypeItemStatus_Id = "DocumentTypeItemStatus_Id".genAutonumber();

                    MS_DocumentTypeItemStatus Model = new MS_DocumentTypeItemStatus();

                    Model.DocumentTypeItemStatus_Index = Guid.NewGuid();
                    Model.DocumentTypeItemStatus_Id = data.documentTypeItemStatus_Id;
                    Model.DocumentType_Index = data.documentType_Index;
                    Model.ItemStatus_Index = data.itemStatus_Index;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_DocumentTypeItemStatus.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.documentTypeItemStatus_Id))
                    {
                        if (DocumentTypeItemStatusOld.DocumentTypeItemStatus_Id != "")
                        {
                            data.documentTypeItemStatus_Id = DocumentTypeItemStatusOld.DocumentTypeItemStatus_Id;
                        }
                    }
                    else
                    {
                        if (DocumentTypeItemStatusOld.DocumentTypeItemStatus_Id != data.documentTypeItemStatus_Id)
                        {
                            var query = db.MS_DocumentTypeItemStatus.FirstOrDefault(c => c.DocumentTypeItemStatus_Id == data.documentTypeItemStatus_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.documentTypeItemStatus_Id = DocumentTypeItemStatusOld.DocumentTypeItemStatus_Id;
                        }
                    }
                    DocumentTypeItemStatusOld.DocumentTypeItemStatus_Id = data.documentTypeItemStatus_Id;
                    DocumentTypeItemStatusOld.DocumentType_Index = data.documentType_Index;
                    DocumentTypeItemStatusOld.ItemStatus_Index = data.itemStatus_Index;
                    DocumentTypeItemStatusOld.IsActive = Convert.ToInt32(data.isActive);
                    DocumentTypeItemStatusOld.Update_By = data.update_By;
                    DocumentTypeItemStatusOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveTaskGroupWorkArea", msglog);
                    transactionx.Rollback();

                    throw exy;
                }

                return "Done"; ;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
