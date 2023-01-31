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
    public class SoldToTypeService
    {

        private MasterDataDbContext db;

        public SoldToTypeService()
        {
            db = new MasterDataDbContext();
        }

        public SoldToTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }
        #region BeforeCodeSoldToType
        //public List<SoldToTypeViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_SoldToType.FromSql("sp_GetSoldToType").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<SoldToTypeViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new SoldToTypeViewModel();

        //                resultItem.SoldToTypeIndex = item.SoldToType_Index;
        //                resultItem.SoldToTypeId = item.SoldToType_Id;
        //                resultItem.SoldToTypeName = item.SoldToType_Name;
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
        //public List<SoldToTypeViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_SoldToType.FromSql("sp_GetSoldToType").Where(c => c.SoldToType_Index == id).ToList();
        //            var result = new List<SoldToTypeViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new SoldToTypeViewModel();
        //                resultItem.SoldToTypeIndex = item.SoldToType_Index;
        //                resultItem.SoldToTypeId = item.SoldToType_Id;
        //                resultItem.SoldToTypeName = item.SoldToType_Name;
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
        //public String SaveChanges(SoldToTypeViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            if (data.SoldToTypeIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.SoldToTypeIndex = Guid.NewGuid();
        //            }
        //            if (data.SoldToTypeId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "SoldToTypeID");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.SoldToTypeId = resultParameter.Value.ToString();
        //            }
        //            int isactive = 1;
        //            int isdelete = 0;
        //            int isSystem = 0;
        //            int status_Id = 0;
        //            var SoldToType_Index = new SqlParameter("SoldToType_Index", data.SoldToTypeIndex);
        //            var SoldToType_Id = new SqlParameter("SoldToType_Id", data.SoldToTypeId);
        //            var SoldToType_Name = new SqlParameter("SoldToType_Name", data.SoldToTypeName);
        //            var IsActive = new SqlParameter("IsActive", isactive);
        //            var IsDelete = new SqlParameter("IsDelete", isdelete);
        //            var IsSystem = new SqlParameter("IsSystem", isSystem);
        //            var Status_Id = new SqlParameter("Status_Id", status_Id);
        //            var Create_By = new SqlParameter("Create_By", "");
        //            var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
        //            var Update_By = new SqlParameter("Update_By", "");
        //            var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
        //            var Cancel_By = new SqlParameter("Cancel_By", "");
        //            var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_SoldToType  @SoldToType_Index,@SoldToType_Id,@SoldToType_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", SoldToType_Index, SoldToType_Id, SoldToType_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<SoldToTypeViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_SoldToType.FromSql("sp_GetSoldToType").Where(c => c.SoldToType_Index == id).ToList();
        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<SoldToTypeViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var SoldToType_Index = new SqlParameter("SoldToType_Index", item.SoldToType_Index);
        //                var SoldToType_Id = new SqlParameter("SoldToType_Id", item.SoldToType_Id);
        //                var SoldToType_Name = new SqlParameter("SoldToType_Name", item.SoldToType_Name);
        //                var IsActive = new SqlParameter("IsActive", isactive);
        //                var IsDelete = new SqlParameter("IsDelete", isdelete);
        //                var IsSystem = new SqlParameter("IsSystem", item.IsSystem);
        //                var Status_Id = new SqlParameter("Status_Id", item.Status_Id);
        //                var Create_By = new SqlParameter("Create_By", item.Create_By);
        //                var Create_Date = new SqlParameter("Create_Date", item.Create_Date);
        //                var Update_By = new SqlParameter("Update_By", item.Update_By);
        //                var Update_Date = new SqlParameter("Update_Date", item.Update_Date);
        //                var Cancel_By = new SqlParameter("Cancel_By", item.Cancel_By);
        //                var Cancel_Date = new SqlParameter("Cancel_Date", item.Cancel_Date);
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_SoldToType  @SoldToType_Index,@SoldToType_Id,@SoldToType_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", SoldToType_Index, SoldToType_Id, SoldToType_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //                context.SaveChanges();
        //            }

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<SoldToTypeViewModel> search(SoldToTypeViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhere = "";
        //            string pwhereLike = "";

        //            var result = new List<SoldToTypeViewModel>();
        //            var queryResult = context.MS_SoldToType.FromSql("sp_GetSoldToType").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            if (data.SoldToTypeId != "" && data.SoldToTypeId != null)
        //            {
        //                pwhere = " And SoldToType_Id like N'%" + data.SoldToTypeId + "%'";
        //            }
        //            else
        //            {
        //                pwhere += " ";
        //            }
        //            if (data.SoldToTypeName != "" && data.SoldToTypeName != null)
        //            {
        //                pwhere = " And SoldToType_Name like N'%" + data.SoldToTypeName + "%'";
        //            }
        //            else
        //            {
        //                pwhere += " ";
        //            }

        //            if (data.SoldToTypeName != "" && data.SoldToTypeName != null)
        //            {
        //                pwhere += " And isActive = '" + 1 + "'";
        //                pwhere += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhere);
        //                var query = context.MS_SoldToType.FromSql("sp_GetSoldToType @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new SoldToTypeViewModel();
        //                    resultItem.SoldToTypeIndex = item.SoldToType_Index;
        //                    resultItem.SoldToTypeId = item.SoldToType_Id;
        //                    resultItem.SoldToTypeName = item.SoldToType_Name;
        //                    resultItem.IsActive = item.IsActive;
        //                    resultItem.IsDelete = item.IsDelete;
        //                    resultItem.IsSystem = item.IsSystem;
        //                    resultItem.StatusId = item.Status_Id;
        //                    resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                    resultItem.CreateBy = item.Create_By;
        //                    resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                    resultItem.UpdateBy = item.Update_By;
        //                    resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                    resultItem.CancelBy = item.Cancel_By;
        //                    result.Add(resultItem);
        //                }

        //            }
        //            else if (data.SoldToTypeId != "" && data.SoldToTypeId != null)
        //            {
        //                pwhere += " And isActive = '" + 1 + "'";
        //                pwhere += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhere);
        //                var query = context.MS_SoldToType.FromSql("sp_GetSoldToType @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new SoldToTypeViewModel();
        //                    resultItem.SoldToTypeIndex = item.SoldToType_Index;
        //                    resultItem.SoldToTypeId = item.SoldToType_Id;
        //                    resultItem.SoldToTypeName = item.SoldToType_Name;
        //                    resultItem.IsActive = item.IsActive;
        //                    resultItem.IsDelete = item.IsDelete;
        //                    resultItem.IsSystem = item.IsSystem;
        //                    resultItem.StatusId = item.Status_Id;
        //                    resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                    resultItem.CreateBy = item.Create_By;
        //                    resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                    resultItem.UpdateBy = item.Update_By;
        //                    resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                    resultItem.CancelBy = item.Cancel_By;
        //                    result.Add(resultItem);
        //                }
        //            }

        //            if (data.SoldToTypeId == "" && data.SoldToTypeName == "")
        //            {
        //                result = this.Filter();
        //            }

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion

        #region FindSoldToType
        public SoldToTypeViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_SoldToType.Where(c => c.SoldToType_Index == id).FirstOrDefault();

                var result = new SoldToTypeViewModel();


                result.soldToType_Id = queryResult.SoldToType_Id;
                result.soldToType_Index = queryResult.SoldToType_Index;
                result.soldToType_Name = queryResult.SoldToType_Name;
                result.soldToType_SecondName = queryResult.SoldToType_SecondName;
                result.ref_No1 = queryResult.Ref_No1;
                result.ref_No2 = queryResult.Ref_No2;
                result.ref_No3 = queryResult.Ref_No3;
                result.ref_No4 = queryResult.Ref_No4;
                result.ref_No5 = queryResult.Ref_No5;
                result.remark = queryResult.Remark;
                result.udf_1 = queryResult.UDF_1;
                result.udf_2 = queryResult.UDF_2;
                result.udf_3 = queryResult.UDF_3;
                result.udf_4 = queryResult.UDF_4;
                result.udf_5 = queryResult.UDF_5;
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

        #region FilterSoldToType
        //Filter
        public actionResultSoldToTypeViewModel filter(SearchSoldToTypeViewModel data)
        {
            try
            {
                var query = db.MS_SoldToType.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.SoldToType_Name.Contains(data.key)
                                        || c.SoldToType_Id.Contains(data.key));


                }
                var Item = new List<MS_SoldToType>();
                var TotalRow = new List<MS_SoldToType>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                var num = 1;
                if (data.PerPage == 100)
                {
                    for (int i = 1; i < data.CurrentPage; i++)
                    {
                        num = num + 100;
                    }
                }
                if (data.PerPage == 50)
                {
                    for (int i = 1; i < data.CurrentPage; i++)
                    {
                        num = num + 50;
                    }
                }
                int rowCount = num;

                Item = query.OrderBy(o => o.SoldToType_Id).ToList();

                var result = new List<SearchSoldToTypeViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchSoldToTypeViewModel();

                    resultItem.soldToType_Index = item.SoldToType_Index;
                    resultItem.soldToType_Id = item.SoldToType_Id;
                    resultItem.soldToType_Name = item.SoldToType_Name;
                    resultItem.soldToType_SecondName = item.SoldToType_SecondName;
                    resultItem.ref_No1 = item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3;
                    resultItem.ref_No4 = item.Ref_No4;
                    resultItem.ref_No5 = item.Ref_No5;
                    resultItem.remark = item.Remark;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.udf_5 = item.UDF_5;
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
                    resultItem.row_Count = rowCount;
                    result.Add(resultItem);
                    rowCount++;
                }

                var count = TotalRow.Count;

                var actionResultSoldToTypeViewModel = new actionResultSoldToTypeViewModel();
                actionResultSoldToTypeViewModel.itemsSoldToType = result.ToList();
                actionResultSoldToTypeViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultSoldToTypeViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetDeleteSoldToType
        public Boolean getDelete(SoldToTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var soldToType = db.MS_SoldToType.Find(data.soldToType_Index);

                if (soldToType != null)
                {
                    soldToType.IsActive = 0;
                    soldToType.IsDelete = 1;


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
                        olog.logging("DeleteSoldToType", msglog);
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
        
        #region SaveChangesSoldToType
        public String SaveChanges(SoldToTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var SoldToTypeOld = db.MS_SoldToType.Find(data.soldToType_Index);

                if (SoldToTypeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.soldToType_Id))
                    {
                        var query = db.MS_SoldToType.FirstOrDefault(c => c.SoldToType_Id == data.soldToType_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.soldToType_Id))
                    {
                        data.soldToType_Id = "SoldToType_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_SoldToType.FirstOrDefault(c => c.SoldToType_Id == data.soldToType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.SoldToType_Id == data.soldToType_Id)
                                {
                                    data.soldToType_Id = "SoldToType_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_SoldToType Model = new MS_SoldToType();

                    Model.SoldToType_Index = Guid.NewGuid();
                    Model.SoldToType_Id = data.soldToType_Id;
                    Model.SoldToType_Name = data.soldToType_Name;
                    Model.SoldToType_SecondName = data.soldToType_SecondName;
                    Model.Ref_No1 = data.ref_No1;
                    Model.Ref_No2 = data.ref_No2;
                    Model.Ref_No3 = data.ref_No3;
                    Model.Ref_No4 = data.ref_No4;
                    Model.Ref_No5 = data.ref_No5;
                    Model.Remark = data.remark;
                    Model.UDF_1 = data.udf_1;
                    Model.UDF_2 = data.udf_2;
                    Model.UDF_3 = data.udf_3;
                    Model.UDF_4 = data.udf_4;
                    Model.UDF_5 = data.udf_5;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_SoldToType.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.soldToType_Id))
                    {
                        if (SoldToTypeOld.SoldToType_Id != "")
                        {
                            data.soldToType_Id = SoldToTypeOld.SoldToType_Id;
                        }
                    }
                    else
                    {
                        if (SoldToTypeOld.SoldToType_Id != data.soldToType_Id)
                        {
                            var query = db.MS_SoldToType.FirstOrDefault(c => c.SoldToType_Id == data.soldToType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.soldToType_Id = SoldToTypeOld.SoldToType_Id;
                        }
                    }

                    SoldToTypeOld.SoldToType_Id = data.soldToType_Id;
                    SoldToTypeOld.SoldToType_Name = data.soldToType_Name;
                    SoldToTypeOld.SoldToType_SecondName = data.soldToType_SecondName;
                    SoldToTypeOld.Ref_No1 = data.ref_No1;
                    SoldToTypeOld.Ref_No2 = data.ref_No2;
                    SoldToTypeOld.Ref_No3 = data.ref_No3;
                    SoldToTypeOld.Ref_No4 = data.ref_No4;
                    SoldToTypeOld.Ref_No5 = data.ref_No5;
                    SoldToTypeOld.Remark = data.remark;
                    SoldToTypeOld.UDF_1 = data.udf_1;
                    SoldToTypeOld.UDF_2 = data.udf_2;
                    SoldToTypeOld.UDF_3 = data.udf_3;
                    SoldToTypeOld.UDF_4 = data.udf_4;
                    SoldToTypeOld.UDF_5 = data.udf_5;
                    SoldToTypeOld.IsActive = Convert.ToInt32(data.isActive);
                    SoldToTypeOld.Update_By = data.create_By;
                    SoldToTypeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveSoldToType", msglog);
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

        #region SearchSoldToType


        public List<ItemListViewModel> AutoSoldToType(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_SoldToType.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.SoldToType_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.SoldToType_Name, c.SoldToType_Index, c.SoldToType_Id }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.SoldToType_Index,
                            id = item.SoldToType_Id,
                            name = item.SoldToType_Name
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
        #endregion

        #region SearchSoldToTypeFilter
        public List<ItemListViewModel> AutoSearchSoldToTypeFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_SoldToType.Where(c => c.SoldToType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.SoldToType_Name,
                        key = s.SoldToType_Name
                    }).Distinct();

                    var query2 = db.MS_SoldToType.Where(c => c.SoldToType_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.SoldToType_Id,
                        key = s.SoldToType_Id
                    }).Distinct();
                    var query = query1.Union(query2).Union(query2);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion
    }

}