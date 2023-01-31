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

namespace MasterDataBusiness.VehicleType
{
    public class VehicleTypeService
    {
        private MasterDataDbContext db;


        public VehicleTypeService()
        {
            db = new MasterDataDbContext();
        }

        public VehicleTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        public List<VehicleTypeViewModel> vehicleTypedropdown(VehicleTypeViewModel data)
        {
            try
            {
                var result = new List<VehicleTypeViewModel>();

                var query = db.MS_VehicleType.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.VehicleType_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new VehicleTypeViewModel();

                    resultItem.vehicleType_Index = item.VehicleType_Index;
                    resultItem.vehicleType_Id = item.VehicleType_Id;
                    resultItem.vehicleType_Name = item.VehicleType_Name;

                    result.Add(resultItem);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region filter
        public actionResultVehicleTypeViewModel filter(SearchVehicleTypeViewModel data)
        {
            try
            {
                var query = db.MS_VehicleType.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.VehicleType_Id.Contains(data.key)
                                         || c.VehicleType_Name.Contains(data.key));
                }

                var Item = new List<MS_VehicleType>();
                var TotalRow = new List<MS_VehicleType>();

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

                Item = query.OrderBy(o => o.VehicleType_Index).ToList();

                var result = new List<SearchVehicleTypeViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchVehicleTypeViewModel();

                    resultItem.vehicleType_Index = item.VehicleType_Index;
                    resultItem.vehicleType_Id = item.VehicleType_Id;
                    resultItem.vehicleType_Name = item.VehicleType_Name;
                    resultItem.vehicleType_SecondName = item.VehicleType_SecondName;
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

                var actionResultVehicleTypeViewModel = new actionResultVehicleTypeViewModel();
                actionResultVehicleTypeViewModel.itemsVehicleType = result.ToList();
                actionResultVehicleTypeViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultVehicleTypeViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(VehicleTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var VehicleTypeOld = db.MS_VehicleType.Find(data.vehicleType_Index);

                if (VehicleTypeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.vehicleType_Id))
                    {
                        var query = db.MS_VehicleType.FirstOrDefault(c => c.VehicleType_Id == data.vehicleType_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.vehicleType_Id))
                    {
                        data.vehicleType_Id = "VehicleType_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_VehicleType.FirstOrDefault(c => c.VehicleType_Id == data.vehicleType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.VehicleType_Id == data.vehicleType_Id)
                                {
                                    data.vehicleType_Id = "VehicleType_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_VehicleType Model = new MS_VehicleType();

                    Model.VehicleType_Index = Guid.NewGuid();
                    Model.VehicleType_Id = data.vehicleType_Id;
                    Model.VehicleType_Name = data.vehicleType_Name;
                    Model.VehicleType_SecondName = data.vehicleType_SecondName;
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

                    db.MS_VehicleType.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.vehicleType_Id))
                    {
                        if (VehicleTypeOld.VehicleType_Id != "")
                        {
                            data.vehicleType_Id = VehicleTypeOld.VehicleType_Id;
                        }
                    }
                    else
                    {
                        if (VehicleTypeOld.VehicleType_Id != data.vehicleType_Id)
                        {
                            var query = db.MS_VehicleType.FirstOrDefault(c => c.VehicleType_Id == data.vehicleType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.vehicleType_Id = VehicleTypeOld.VehicleType_Id;
                        }
                    }

                    VehicleTypeOld.VehicleType_Id = data.vehicleType_Id;
                    VehicleTypeOld.VehicleType_Name = data.vehicleType_Name;
                    VehicleTypeOld.VehicleType_SecondName = data.vehicleType_SecondName;
                    VehicleTypeOld.Ref_No1 = data.ref_No1;
                    VehicleTypeOld.Ref_No2 = data.ref_No2;
                    VehicleTypeOld.Ref_No3 = data.ref_No3;
                    VehicleTypeOld.Ref_No4 = data.ref_No4;
                    VehicleTypeOld.Ref_No5 = data.ref_No5;
                    VehicleTypeOld.Remark = data.remark;
                    VehicleTypeOld.UDF_1 = data.udf_1;
                    VehicleTypeOld.UDF_2 = data.udf_2;
                    VehicleTypeOld.UDF_3 = data.udf_3;
                    VehicleTypeOld.UDF_4 = data.udf_4;
                    VehicleTypeOld.UDF_5 = data.udf_5;
                    VehicleTypeOld.IsActive = Convert.ToInt32(data.isActive);
                    VehicleTypeOld.Update_By = data.create_By;
                    VehicleTypeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveVehicleType", msglog);
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
        public VehicleTypeViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_VehicleType.Where(c => c.VehicleType_Index == id).FirstOrDefault();

                var result = new VehicleTypeViewModel();


                result.vehicleType_Index = queryResult.VehicleType_Index;
                result.vehicleType_Id = queryResult.VehicleType_Id;
                result.vehicleType_Name = queryResult.VehicleType_Name;
                result.vehicleType_SecondName = queryResult.VehicleType_SecondName;
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

        #region getDelete
        public Boolean getDelete(VehicleTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var warehouse = db.MS_VehicleType.Find(data.vehicleType_Index);

                if (warehouse != null)
                {
                    warehouse.IsActive = 0;
                    warehouse.IsDelete = 1;


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
                        olog.logging("DeleteVehicleType", msglog);
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
        public VehicleTypetypeActionResultExportViewModel Export(VehicleTypeExportViewModel data)
        {
            try
            {
                var query = db.MS_VehicleType.AsQueryable();


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.VehicleType_Id.Contains(data.key)
                                        || c.VehicleType_Name.Contains(data.key));

                }

                var Item = new List<MS_VehicleType>();
                var TotalRow = new List<MS_VehicleType>();

                TotalRow = query.ToList();

                Item = query.OrderBy(o => o.VehicleType_Id).ToList();

                var result = new List<VehicleTypeExportViewModel>();

                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new VehicleTypeExportViewModel();
                    //resultItem.count = i;
                    resultItem.numBerOf = num + 1;
                    resultItem.vehicleType_Id = item.VehicleType_Id;
                    resultItem.vehicleType_Name = item.VehicleType_Name;
                    resultItem.vehicleType_Index = item.VehicleType_Index;
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

                var VehicleTypetypeActionResultExportViewModel = new VehicleTypetypeActionResultExportViewModel();
                VehicleTypetypeActionResultExportViewModel.itemsVehicletype = result.ToList();

                return VehicleTypetypeActionResultExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoVehicleType
        public List<ItemListViewModel> autoVehicleType(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_VehicleType.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.VehicleType_Name.Contains(data.key));

                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.VehicleType_Index, c.VehicleType_Id, c.VehicleType_Name }).Distinct().Take(10).ToList();


                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.VehicleType_Index,
                            id = item.VehicleType_Id,
                            name = item.VehicleType_Name,

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

        /*#region autoVehicleType
        public List<ItemListViewModel> autoVehicleType(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.MS_VehicleType.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.VehicleType_Id.Contains(data.key)
                                                || c.VehicleType_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.VehicleType_Id, c.VehicleType_Name, }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            id = item.VehicleType_Id,
                            name = item.VehicleType_Name,

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

        #endregion*/
    }
}
