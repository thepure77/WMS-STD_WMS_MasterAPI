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
using System.Text;

namespace MasterDataBusiness.ContainerType
{
    public class ContainerTypeService
    {
        private MasterDataDbContext db;

        public ContainerTypeService()
        {
            db = new MasterDataDbContext();
        }

        public ContainerTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }
        public List<ContainerTypeViewModel> Filter()
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_ContainerType.FromSql("sp_GetContainerType").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

                    var result = new List<ContainerTypeViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new ContainerTypeViewModel();
                        resultItem.ContainerTypeIndex = item.ContainerType_Index;
                        resultItem.ContainerTypeId = item.ContainerType_Id;
                        resultItem.ContainerTypeName = item.ContainerType_Name;
                        resultItem.IsActive = item.IsActive;
                        resultItem.IsDelete = item.IsDelete;
                        resultItem.IsSystem = item.IsSystem;
                        resultItem.StatusId = item.Status_Id;
                        resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
                        resultItem.CreateBy = item.Create_By;
                        resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
                        resultItem.UpdateBy = item.Update_By;
                        resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
                        resultItem.CancelBy = item.Cancel_By;
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

        public List<ContainerTypeViewModel> search(ContainerTypeViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    string pwhereFilter = "";
                    string pwhereLike = "";
                    var result = new List<ContainerTypeViewModel>();
                    if (data.ContainerTypeId != "" && data.ContainerTypeId != null)
                    {
                        pwhereFilter = " And ContainerType_Id like N'%" + data.ContainerTypeId + "%'";
                    }
                    else
                    {
                        pwhereFilter = "";
                    }

                    if (data.ContainerTypeName != "" && data.ContainerTypeName != null)
                    {
                        pwhereFilter += " And ContainerType_Name like N'%" + data.ContainerTypeName + "%'";
                    }
                    else
                    {
                        pwhereFilter += "";
                    }


                    pwhereFilter += " And isActive = '" + 1 + "'";
                    pwhereFilter += " And isDelete = '" + 0 + "'";
                    var strwhere = new SqlParameter("@strwhere", pwhereFilter);
                    var query = context.MS_ContainerType.FromSql("sp_GetContainerType @strwhere ", strwhere).ToList();
                    foreach (var item in query)
                    {
                        var resultItem = new ContainerTypeViewModel();

                        resultItem.ContainerTypeIndex = item.ContainerType_Index;
                        resultItem.ContainerTypeId = item.ContainerType_Id;
                        resultItem.ContainerTypeName = item.ContainerType_Name;
                        resultItem.IsActive = item.IsActive;
                        resultItem.IsDelete = item.IsDelete;
                        resultItem.IsSystem = item.IsSystem;
                        resultItem.StatusId = item.Status_Id;
                        resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
                        resultItem.CreateBy = item.Create_By;
                        resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
                        resultItem.UpdateBy = item.Update_By;
                        resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
                        resultItem.CancelBy = item.Cancel_By;

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

        #region filter
        public actionResultContainerTypeViewModel filter(SearchContainerTypeViewModel data)
        {
            try
            {
                var query = db.MS_ContainerType.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.ContainerType_Id.Contains(data.key)
                                         || c.ContainerType_Name.Contains(data.key));
                }

                var Item = new List<MS_ContainerType>();
                var TotalRow = new List<MS_ContainerType>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ContainerType_Id).ToList();

                var result = new List<SearchContainerTypeViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchContainerTypeViewModel();

                    resultItem.containerType_Index = item.ContainerType_Index;
                    resultItem.containerType_Id = item.ContainerType_Id;
                    resultItem.containerType_Name = item.ContainerType_Name;
                    resultItem.isActive = item.IsActive;
                    resultItem.isDelete = item.IsDelete;
                    resultItem.isSystem = item.IsSystem;
                    resultItem.status_Id = item.Status_Id;
                    resultItem.create_By = item.Create_By;
                    resultItem.create_Date = item.Create_Date;
                    resultItem.update_By = item.Update_By;
                    resultItem.update_Date = item.Update_Date;
                    resultItem.cancel_By = item.Cancel_By;
                    resultItem.cancel_Date = item.Cancel_Date;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultContainerTypeViewModel = new actionResultContainerTypeViewModel();
                actionResultContainerTypeViewModel.itemsContainerType = result.ToList();
                actionResultContainerTypeViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultContainerTypeViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(ContainerTypeViewModelV2 data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var ContainerTypeOld = db.MS_ContainerType.Find(data.containerType_Index);

                if (ContainerTypeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.containerType_Id))
                    {
                        var query = db.MS_ContainerType.FirstOrDefault(c => c.ContainerType_Id == data.containerType_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.containerType_Id))
                    {
                        data.containerType_Id = "ContainerType_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_ContainerType.FirstOrDefault(c => c.ContainerType_Id == data.containerType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.ContainerType_Id == data.containerType_Id)
                                {
                                    data.containerType_Id = "ContainerType_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_ContainerType Model = new MS_ContainerType();


                    Model.ContainerType_Index = Guid.NewGuid();
                    Model.ContainerType_Id = data.containerType_Id;
                    Model.ContainerType_Name = data.containerType_Name;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_ContainerType.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.containerType_Id))
                    {
                        if (ContainerTypeOld.ContainerType_Id != "")
                        {
                            data.containerType_Id = ContainerTypeOld.ContainerType_Id;
                        }
                    }
                    else
                    {
                        if (ContainerTypeOld.ContainerType_Id != data.containerType_Id)
                        {
                            var query = db.MS_ContainerType.FirstOrDefault(c => c.ContainerType_Id == data.containerType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.containerType_Id = ContainerTypeOld.ContainerType_Id;
                        }
                    }

                    ContainerTypeOld.ContainerType_Id = data.containerType_Id;
                    ContainerTypeOld.ContainerType_Name = data.containerType_Name;
                    ContainerTypeOld.IsActive = Convert.ToInt32(data.isActive);
                    ContainerTypeOld.Update_By = data.create_By;
                    ContainerTypeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveContainerType", msglog);
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

        #region find
        public ContainerTypeViewModelV2 find(Guid id)
        {
            try
            {

                var queryResult = db.MS_ContainerType.Where(c => c.ContainerType_Index == id).FirstOrDefault();

                var result = new ContainerTypeViewModelV2();

                result.containerType_Index = queryResult.ContainerType_Index;
                result.containerType_Id = queryResult.ContainerType_Id;
                result.containerType_Name = queryResult.ContainerType_Name;
                result.isActive = queryResult.IsActive;
                result.isDelete = queryResult.IsDelete;
                result.isSystem = queryResult.IsSystem;
                result.status_Id = queryResult.Status_Id;
                result.create_By = queryResult.Create_By;
                result.create_Date = queryResult.Create_Date;
                result.update_By = queryResult.Update_By;
                result.update_Date = queryResult.Update_Date;
                result.cancel_By = queryResult.Cancel_By;
                result.cancel_Date = queryResult.Cancel_Date;

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getDelete
        public Boolean getDelete(ContainerTypeViewModelV2 data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var ContainerType = db.MS_ContainerType.Find(data.containerType_Index);

                if (ContainerType != null)
                {
                    ContainerType.IsActive = 0;
                    ContainerType.IsDelete = 1;


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
                        olog.logging("DeleteContainerType", msglog);
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

        #region dropdown
        public List<ContainerTypeViewModelV2> containerTypeDropdown(ContainerTypeViewModelV2 data)
        {
            try
            {
                var result = new List<ContainerTypeViewModelV2>();

                var query = db.MS_ContainerType.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.ContainerType_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new ContainerTypeViewModelV2();

                    resultItem.containerType_Index = item.ContainerType_Index;
                    resultItem.containerType_Id = item.ContainerType_Id;
                    resultItem.containerType_Name = item.ContainerType_Name;

                    result.Add(resultItem);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
