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

namespace MasterDataBusiness
{
    public class ShipToTypeService
    {
        #region BeforeCodeShipToType
        //public List<ShipToTypeViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ShipToType.FromSql("sp_GetShipToType").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<ShipToTypeViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ShipToTypeViewModel();

        //                resultItem.ShipToTypeIndex = item.ShipToType_Index;
        //                resultItem.ShipToTypeId = item.ShipToType_Id;
        //                resultItem.ShipToTypeName = item.ShipToType_Name;
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

        //public String SaveChanges(ShipToTypeViewModel data)
        //{
        //    try
        //    {
        //        int isactive = 1;
        //        int isdelete = 0;
        //        int issystem = 0;
        //        int statusid = 0;

        //        using (var context = new MasterDataDbContext())
        //        {
        //            if (data.ShipToTypeIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.ShipToTypeIndex = Guid.NewGuid();
        //            }
        //            if (data.ShipToTypeId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "ShipToTypeID");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.ShipToTypeId = resultParameter.Value.ToString();
        //            }
        //            var ShipToType_Index = new SqlParameter("ShipToType_Index", data.ShipToTypeIndex);
        //            var ShipToType_Id = new SqlParameter("ShipToType_Id", data.ShipToTypeId);
        //            var ShipToType_Name = new SqlParameter("ShipToType_Name", data.ShipToTypeName);
        //            var IsActive = new SqlParameter("IsActive", isactive);
        //            var IsDelete = new SqlParameter("IsDelete", isdelete);
        //            var IsSystem = new SqlParameter("IsSystem", issystem);
        //            var Status_Id = new SqlParameter("Status_Id", statusid);
        //            var Create_By = new SqlParameter("Create_By", "");
        //            var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
        //            var Update_By = new SqlParameter("Update_By", "");
        //            var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
        //            var Cancel_By = new SqlParameter("Cancel_By", "");
        //            var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_ShipToType  @ShipToType_Index,@ShipToType_Id,@ShipToType_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", ShipToType_Index, ShipToType_Id, ShipToType_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //public List<ShipToTypeViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ShipToType.FromSql("sp_GetShipToType").Where(c => c.ShipToType_Index == id).ToList();

        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<ShipToTypeViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var ShipToType_Index = new SqlParameter("ShipToType_Index", item.ShipToType_Index);
        //                var ShipToType_Id = new SqlParameter("ShipToType_Id", item.ShipToType_Id);
        //                var ShipToType_Name = new SqlParameter("ShipToType_Name", item.ShipToType_Name);
        //                var IsActive = new SqlParameter("IsActive", isactive);
        //                var IsDelete = new SqlParameter("IsDelete", isdelete);
        //                var IsSystem = new SqlParameter("IsSystem", item.IsSystem);
        //                var Status_Id = new SqlParameter("Status_Id", item.Status_Id);
        //                var Create_By = new SqlParameter("Create_By", "");
        //                var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
        //                var Update_By = new SqlParameter("Update_By", "");
        //                var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
        //                var Cancel_By = new SqlParameter("Cancel_By", "");
        //                var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_ShipToType  @ShipToType_Index,@ShipToType_Id,@ShipToType_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", ShipToType_Index, ShipToType_Id, ShipToType_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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

        //public List<ShipToTypeViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ShipToType.FromSql("sp_GetShipToType").Where(c => c.ShipToType_Index == id).ToList();

        //            var result = new List<ShipToTypeViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ShipToTypeViewModel();
        //                resultItem.ShipToTypeIndex = item.ShipToType_Index;
        //                resultItem.ShipToTypeId = item.ShipToType_Id;
        //                resultItem.ShipToTypeName = item.ShipToType_Name;
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
        //public List<ShipToTypeViewModel> search(ShipToTypeViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhereFilter = "";
        //            string pwhereLike = "";
        //            var result = new List<ShipToTypeViewModel>();                    
        //            if (data.ShipToTypeId != "" && data.ShipToTypeId != null)
        //            {
        //                pwhereFilter = " And ShipToType_Id like N'%" + data.ShipToTypeId + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter = "";
        //            }

        //            if (data.ShipToTypeName != "" && data.ShipToTypeName != null)
        //            {
        //                pwhereFilter += " And ShipToType_Name like N'%" + data.ShipToTypeName + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter += "";
        //            }


        //            if (data.ShipToTypeId != "" && data.ShipToTypeId != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_ShipToType.FromSql("sp_GetShipToType @strwhere ", strwhere).ToList();                      
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new ShipToTypeViewModel();

        //                    resultItem.ShipToTypeIndex = item.ShipToType_Index;
        //                    resultItem.ShipToTypeId = item.ShipToType_Id;
        //                    resultItem.ShipToTypeName = item.ShipToType_Name;
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
        //            else if (data.ShipToTypeName != "" && data.ShipToTypeName != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_ShipToType.FromSql("sp_GetShipToType @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new ShipToTypeViewModel();

        //                    resultItem.ShipToTypeIndex = item.ShipToType_Index;
        //                    resultItem.ShipToTypeId = item.ShipToType_Id;
        //                    resultItem.ShipToTypeName = item.ShipToType_Name;
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

        //            if (data.ShipToTypeId == ""  && data.ShipToTypeName == "")
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


        //public List<ItemListViewModel> AutoShipToType(ItemListViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var query = context.MS_ShipToType.AsQueryable();

        //            if (!string.IsNullOrEmpty(data.key))
        //            {
        //                query = query.Where(c => c.ShipToType_Name.Contains(data.key));
        //            }

        //            var items = new List<ItemListViewModel>();

        //            var result = query.Select(c => new { c.ShipToType_Name, c.ShipToType_Index, c.ShipToType_Id }).Distinct().Take(10).ToList();

        //            foreach (var item in result)
        //            {
        //                var resultItem = new ItemListViewModel
        //                {
        //                    //index = new Guid(item.User_Name),
        //                    index = item.ShipToType_Index,
        //                    id = item.ShipToType_Id,
        //                    name = item.ShipToType_Name
        //                };

        //                items.Add(resultItem);
        //            }
        //            return items;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #endregion

        #region FindShipToType
        public ShipToTypeViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_ShipToType.Where(c => c.ShipToType_Index == id).FirstOrDefault();

                var result = new ShipToTypeViewModel();


                result.shipToType_Id = queryResult.ShipToType_Id;
                result.shipToType_Index = queryResult.ShipToType_Index;
                result.shipToType_Name = queryResult.ShipToType_Name;
                result.shipToType_SecondName = queryResult.ShipToType_SecondName;
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


                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region FilterShipToType
        //Filter
        private MasterDataDbContext db;

        public ShipToTypeService()
        {
            db = new MasterDataDbContext();
        }

        public ShipToTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        
        public actionResultShipToTypeViewModel filter(SearchShipToTypeViewModel data)
        {
            try
            {
                var query = db.MS_ShipToType.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.ShipToType_Name.Contains(data.key)
                                        || c.ShipToType_Id.Contains(data.key));


                }
                var Item = new List<MS_ShipToType>();
                var TotalRow = new List<MS_ShipToType>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ShipToType_Id).ToList();

                var result = new List<SearchShipToTypeViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchShipToTypeViewModel();

                    resultItem.shipToType_Index = item.ShipToType_Index;
                    resultItem.shipToType_Id = item.ShipToType_Id;
                    resultItem.shipToType_Name = item.ShipToType_Name;
                    resultItem.shipToType_SecondName = item.ShipToType_SecondName;
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
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultShipToTypeViewModel = new actionResultShipToTypeViewModel();
                actionResultShipToTypeViewModel.itemsShipToType = result.ToList();
                actionResultShipToTypeViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultShipToTypeViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetDeleteShipToType
        public Boolean getDelete(ShipToTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var shipToType = db.MS_ShipToType.Find(data.shipToType_Index);

                if (shipToType != null)
                {
                    shipToType.IsActive = 0;
                    shipToType.IsDelete = 1;


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
                        olog.logging("DeleteShipToType", msglog);
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
             
        #region SaveChangesShipToType
        public String SaveChanges(ShipToTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var ShipToTypeOld = db.MS_ShipToType.Find(data.shipToType_Index);

                if (ShipToTypeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.shipToType_Id))
                    {
                        var query = db.MS_ShipToType.FirstOrDefault(c => c.ShipToType_Id == data.shipToType_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.shipToType_Id))
                    {
                        data.shipToType_Id = "ShipToType_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_ShipToType.FirstOrDefault(c => c.ShipToType_Id == data.shipToType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.ShipToType_Id == data.shipToType_Id)
                                {
                                    data.shipToType_Id = "ShipToType_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_ShipToType Model = new MS_ShipToType();

                    Model.ShipToType_Index = Guid.NewGuid();
                    Model.ShipToType_Id = data.shipToType_Id;
                    Model.ShipToType_Name = data.shipToType_Name;
                    Model.ShipToType_SecondName = data.shipToType_SecondName;
                    Model.Ref_No1 = data.ref_No1;
                    Model.Ref_No2 = data.ref_No2;
                    Model.Ref_No3 = data.ref_No3;
                    Model.Ref_No4 = data.ref_No4;
                    Model.Ref_No5 = data.ref_No5;
                    Model.Remark = data.remark;
                    Model.UDF_1 = null;
                    Model.UDF_2 = null;
                    Model.UDF_3 = null;
                    Model.UDF_4 = null;
                    Model.UDF_5 = null;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_ShipToType.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.shipToType_Id))
                    {
                        if (ShipToTypeOld.ShipToType_Id != "")
                        {
                            data.shipToType_Id = ShipToTypeOld.ShipToType_Id;
                        }
                    }
                    else
                    {
                        if (ShipToTypeOld.ShipToType_Id != data.shipToType_Id)
                        {
                            var query = db.MS_ShipToType.FirstOrDefault(c => c.ShipToType_Id == data.shipToType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.shipToType_Id = ShipToTypeOld.ShipToType_Id;
                        }
                    }

                    ShipToTypeOld.ShipToType_Id = data.shipToType_Id;
                    ShipToTypeOld.ShipToType_Name = data.shipToType_Name;
                    ShipToTypeOld.ShipToType_SecondName = data.shipToType_SecondName;
                    ShipToTypeOld.Ref_No1 = data.ref_No1;
                    ShipToTypeOld.Ref_No2 = data.ref_No2;
                    ShipToTypeOld.Ref_No3 = data.ref_No3;
                    ShipToTypeOld.Ref_No4 = data.ref_No4;
                    ShipToTypeOld.Ref_No5 = data.ref_No5;
                    ShipToTypeOld.UDF_1 = null;
                    ShipToTypeOld.UDF_2 = null;
                    ShipToTypeOld.UDF_3 = null;
                    ShipToTypeOld.UDF_4 = null;
                    ShipToTypeOld.UDF_5 = null;
                    ShipToTypeOld.IsActive = Convert.ToInt32(data.isActive);
                    ShipToTypeOld.Update_By = data.update_By;
                    ShipToTypeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveShipToType", msglog);
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

        #region SearchShipToType


        public List<ItemListViewModel> AutoShipToType(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_ShipToType.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ShipToType_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.ShipToType_Name, c.ShipToType_Index, c.ShipToType_Id }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.ShipToType_Index,
                            id = item.ShipToType_Id,
                            name = item.ShipToType_Name
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

        #region SearchShipToTypeFilter
        public List<ItemListViewModel> AutoSearchShipToTypeFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.MS_ShipToType.Where(c => c.ShipToType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ShipToType_Name,
                        key = s.ShipToType_Name
                    }).Distinct();

                    var query2 = db.MS_ShipToType.Where(c => c.ShipToType_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.ShipToType_Id,
                        key = s.ShipToType_Id
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
