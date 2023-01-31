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

namespace MasterDataBusiness
{
    public class UnloadingTypeService
    {
        private MasterDataDbContext db;

        public UnloadingTypeService()
        {
            db = new MasterDataDbContext();
        }

        public UnloadingTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterUnloadingType
        public actionResultUnloadingTypeViewModel filter(SearchUnloadingTypeViewModel data)
        {
            try
            {
                var query = db.ms_UnloadingType.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.UnloadingType_Id.Contains(data.key)
                                         || c.UnloadingType_Name.Contains(data.key));
                }

                var Item = new List<ms_UnloadingType>();
                var TotalRow = new List<ms_UnloadingType>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.UnloadingType_Id).ToList();

                var result = new List<SearchUnloadingTypeViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchUnloadingTypeViewModel();

                    resultItem.unloadingType_Index = item.UnloadingType_Index;

                    resultItem.unloadingType_Id = item.UnloadingType_Id;

                    resultItem.unloadingType_Name = item.UnloadingType_Name;

                    resultItem.unloadingType_SecondName = item.UnloadingType_SecondName;

                    resultItem.unloadingType_ThirdName = item.UnloadingType_ThirdName;

                    resultItem.ref_No1 = item.Ref_No1;

                    resultItem.ref_No2 = item.Ref_No2;

                    resultItem.ref_No3 = item.Ref_No3;

                    resultItem.ref_No4 = item.Ref_No4;

                    resultItem.ref_No5 = item.Ref_No5;

                    resultItem.remark = item.Remark;

                    resultItem.uDF_1 = item.UDF_1;

                    resultItem.uDF_2 = item.UDF_2;

                    resultItem.uDF_3 = item.UDF_3;

                    resultItem.uDF_4 = item.UDF_4;

                    resultItem.uDF_5 = item.UDF_5;

                    resultItem.isActive = item.IsActive;

                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultUnloadingTypeViewModel = new actionResultUnloadingTypeViewModel();
                actionResultUnloadingTypeViewModel.itemsUnloadingType = result.ToList();
                actionResultUnloadingTypeViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultUnloadingTypeViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region SaveChanges
        public String SaveChanges(UnloadingTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var UnloadingTypeOld = db.ms_UnloadingType.Find(data.unloadingType_Index);

                if (UnloadingTypeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.unloadingType_Id))
                    {
                        var query = db.ms_UnloadingType.FirstOrDefault(c => c.UnloadingType_Id == data.unloadingType_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.unloadingType_Id))
                    {
                        data.unloadingType_Id = "UnloadingType_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.ms_UnloadingType.FirstOrDefault(c => c.UnloadingType_Id == data.unloadingType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.UnloadingType_Id == data.unloadingType_Id)
                                {
                                    data.unloadingType_Id = "UnloadingType_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    ms_UnloadingType Model = new ms_UnloadingType();


                    Model.UnloadingType_Index = Guid.NewGuid();
                    Model.UnloadingType_Id = data.unloadingType_Id;
                    Model.UnloadingType_Name = data.unloadingType_Name;
                    Model.UnloadingType_SecondName = data.unloadingType_SecondName;
                    Model.UnloadingType_ThirdName = data.unloadingType_ThirdName;
                    Model.Ref_No1 = data.ref_No1;
                    Model.Ref_No2 = data.ref_No2;
                    Model.Ref_No3 = data.ref_No3;
                    Model.Ref_No4 = data.ref_No4;
                    Model.Ref_No5 = data.ref_No5;
                    Model.Remark = data.remark;
                    Model.UDF_1 = data.uDF_1;
                    Model.UDF_2 = data.uDF_2;
                    Model.UDF_3 = data.uDF_3;
                    Model.UDF_4 = data.uDF_4;
                    Model.UDF_5 = data.uDF_5;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.ms_UnloadingType.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.unloadingType_Id))
                    {
                        if (UnloadingTypeOld.UnloadingType_Id != "")
                        {
                            data.unloadingType_Id = UnloadingTypeOld.UnloadingType_Id;
                        }
                    }
                    else
                    {
                        if (UnloadingTypeOld.UnloadingType_Id != data.unloadingType_Id)
                        {
                            var query = db.ms_UnloadingType.FirstOrDefault(c => c.UnloadingType_Id == data.unloadingType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.unloadingType_Id = UnloadingTypeOld.UnloadingType_Id;
                        }
                    }

                    UnloadingTypeOld.UnloadingType_Id = data.unloadingType_Id;
                    UnloadingTypeOld.UnloadingType_Name = data.unloadingType_Name;
                    UnloadingTypeOld.UnloadingType_SecondName = data.unloadingType_SecondName;
                    UnloadingTypeOld.UnloadingType_ThirdName = data.unloadingType_ThirdName;
                    UnloadingTypeOld.Ref_No1 = data.ref_No1;
                    UnloadingTypeOld.Ref_No2 = data.ref_No2;
                    UnloadingTypeOld.Ref_No3 = data.ref_No3;
                    UnloadingTypeOld.Ref_No4 = data.ref_No4;
                    UnloadingTypeOld.Ref_No5 = data.ref_No5;
                    UnloadingTypeOld.Remark = data.remark;
                    UnloadingTypeOld.UDF_1 = data.uDF_1;
                    UnloadingTypeOld.UDF_2 = data.uDF_2;
                    UnloadingTypeOld.UDF_3 = data.uDF_3;
                    UnloadingTypeOld.UDF_4 = data.uDF_4;
                    UnloadingTypeOld.UDF_5 = data.uDF_5;
                    UnloadingTypeOld.IsActive = Convert.ToInt32(data.isActive);
                    UnloadingTypeOld.Update_By = data.create_By;
                    UnloadingTypeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveUnloadingType", msglog);
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
        public UnloadingTypeViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.ms_UnloadingType.Where(c => c.UnloadingType_Index == id).FirstOrDefault();

                var result = new UnloadingTypeViewModel();

                result.unloadingType_Index = queryResult.UnloadingType_Index;
                result.unloadingType_Id = queryResult.UnloadingType_Id;
                result.unloadingType_Name = queryResult.UnloadingType_Name;
                result.unloadingType_SecondName = queryResult.UnloadingType_SecondName;
                result.unloadingType_ThirdName = queryResult.UnloadingType_ThirdName;
                result.ref_No1 = queryResult.Ref_No1;
                result.ref_No2 = queryResult.Ref_No2;
                result.ref_No3 = queryResult.Ref_No3;
                result.ref_No4 = queryResult.Ref_No4;
                result.ref_No5 = queryResult.Ref_No5;
                result.remark = queryResult.Remark;
                result.uDF_1 = queryResult.UDF_1;
                result.uDF_2 = queryResult.UDF_2;
                result.uDF_3 = queryResult.UDF_3;
                result.uDF_4 = queryResult.UDF_4;
                result.uDF_5 = queryResult.UDF_5;
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
        public Boolean getDelete(UnloadingTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var UnloadingType = db.ms_UnloadingType.Find(data.unloadingType_Index);

                if (UnloadingType != null)
                {
                    UnloadingType.IsActive = 0;
                    UnloadingType.IsDelete = 1;


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
                        olog.logging("DeleteUnloadingType", msglog);
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
        public List<UnloadingTypeViewModel> unloadingTypedropdown(UnloadingTypeViewModel data)
        {
            try
            {
                var result = new List<UnloadingTypeViewModel>();

                var query = db.ms_UnloadingType.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.UnloadingType_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new UnloadingTypeViewModel();

                    resultItem.unloadingType_Index = item.UnloadingType_Index;
                    resultItem.unloadingType_Id = item.UnloadingType_Id;
                    resultItem.unloadingType_Name = item.UnloadingType_Name;

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
