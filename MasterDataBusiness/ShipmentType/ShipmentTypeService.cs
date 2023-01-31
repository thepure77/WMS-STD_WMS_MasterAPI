using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.CostCenter;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MasterDataBusiness.ShipmentType
{
    public class ShipmentTypeService
    {
        private MasterDataDbContext db;

        public ShipmentTypeService()
        {
            db = new MasterDataDbContext();
        }

        public ShipmentTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        public actionResultShipmentTypeViewModel ShipmentTypeFilter(SearchShipmentTypeViewModel data)
        {
            try
            {
                var result = new List<ShipmentTypeViewModel>();
                var query = db.ms_ShipmentType.AsQueryable();

                query = query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                if (data.shipmentType_Index != Guid.Empty && data.shipmentType_Index != null)
                {
                    query = query.Where(c => c.ShipmentType_Index == data.shipmentType_Index);
                }
                if (!string.IsNullOrEmpty(data.shipmentType_Id))
                {
                    query = query.Where(c => c.ShipmentType_Id == data.shipmentType_Id);
                }
                if (!string.IsNullOrEmpty(data.shipmentType_Name))
                {
                    query = query.Where(c => c.ShipmentType_Name == data.shipmentType_Name);
                }
                if (!string.IsNullOrEmpty(data.shipmentType_SecondName))
                {
                    query = query.Where(c => c.ShipmentType_SecondName == data.shipmentType_SecondName);
                }
                if (!string.IsNullOrEmpty(data.shipmentType_ThirdName))
                {
                    query = query.Where(c => c.ShipmentType_ThirdName == data.shipmentType_ThirdName);
                }
                            

                var queryResult = query.OrderBy(o => o.ShipmentType_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new ShipmentTypeViewModel();

                    resultItem.shipmentType_Index = item.ShipmentType_Index;
                    resultItem.shipmentType_Id = item.ShipmentType_Id;
                    resultItem.shipmentType_Name = item.ShipmentType_Name;
                    resultItem.shipmentType_SecondName = item.ShipmentType_SecondName;
                    resultItem.shipmentType_ThirdName = item.ShipmentType_ThirdName;
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

                    result.Add(resultItem);
                }


                var actionResult = new actionResultShipmentTypeViewModel();
                actionResult.items = result.ToList();
                actionResult.pagination = new Pagination() { TotalRow = queryResult.Count(), CurrentPage = data.CurrentPage, PerPage = data.PerPage, };


                return actionResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region SaveChanges
        public String SaveChanges(ShipmentTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var ShipmentTypeOld = db.ms_ShipmentType.Find(data.shipmentType_Index);

                if (ShipmentTypeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.shipmentType_Id))
                    {
                        var query = db.ms_ShipmentType.FirstOrDefault(c => c.ShipmentType_Id == data.shipmentType_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.shipmentType_Id))
                    {
                        data.shipmentType_Id = "ShipmentType_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.ms_ShipmentType.FirstOrDefault(c => c.ShipmentType_Id == data.shipmentType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.ShipmentType_Id == data.shipmentType_Id)
                                {
                                    data.shipmentType_Id = "ShipmentType_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    ms_ShipmentType Model = new ms_ShipmentType();


                    Model.ShipmentType_Index = Guid.NewGuid();
                    Model.ShipmentType_Id = data.shipmentType_Id;
                    Model.ShipmentType_Name = data.shipmentType_Name;
                    Model.ShipmentType_SecondName = data.shipmentType_SecondName;
                    Model.ShipmentType_ThirdName = data.shipmentType_ThirdName;
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

                    db.ms_ShipmentType.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.shipmentType_Id))
                    {
                        if (ShipmentTypeOld.ShipmentType_Id != "")
                        {
                            data.shipmentType_Id = ShipmentTypeOld.ShipmentType_Id;
                        }
                    }
                    else
                    {
                        if (ShipmentTypeOld.ShipmentType_Id != data.shipmentType_Id)
                        {
                            var query = db.ms_ShipmentType.FirstOrDefault(c => c.ShipmentType_Id == data.shipmentType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.shipmentType_Id = ShipmentTypeOld.ShipmentType_Id;
                        }
                    }

                    ShipmentTypeOld.ShipmentType_Id = data.shipmentType_Id;
                    ShipmentTypeOld.ShipmentType_Name = data.shipmentType_Name;
                    ShipmentTypeOld.ShipmentType_SecondName = data.shipmentType_SecondName;
                    ShipmentTypeOld.ShipmentType_ThirdName = data.shipmentType_ThirdName;
                    ShipmentTypeOld.Ref_No1 = data.ref_No1;
                    ShipmentTypeOld.Ref_No2 = data.ref_No2;
                    ShipmentTypeOld.Ref_No3 = data.ref_No3;
                    ShipmentTypeOld.Ref_No4 = data.ref_No4;
                    ShipmentTypeOld.Ref_No5 = data.ref_No5;
                    ShipmentTypeOld.Remark = data.remark;
                    ShipmentTypeOld.UDF_1 = data.udf_1;
                    ShipmentTypeOld.UDF_2 = data.udf_2;
                    ShipmentTypeOld.UDF_3 = data.udf_3;
                    ShipmentTypeOld.UDF_4 = data.udf_4;
                    ShipmentTypeOld.UDF_5 = data.udf_5;
                    ShipmentTypeOld.IsActive = Convert.ToInt32(data.isActive);
                    ShipmentTypeOld.Update_By = data.create_By;
                    ShipmentTypeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveShipmentType", msglog);
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
        public ShipmentTypeViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.ms_ShipmentType.Where(c => c.ShipmentType_Index == id).FirstOrDefault();

                var result = new ShipmentTypeViewModel();

                result.shipmentType_Index = queryResult.ShipmentType_Index;
                result.shipmentType_Id = queryResult.ShipmentType_Id;
                result.shipmentType_Name = queryResult.ShipmentType_Name;
                result.shipmentType_SecondName = queryResult.ShipmentType_SecondName;
                result.shipmentType_ThirdName = queryResult.ShipmentType_ThirdName;
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
        public Boolean getDelete(ShipmentTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var ShipmentType = db.ms_ShipmentType.Find(data.shipmentType_Index);

                if (ShipmentType != null)
                {
                    ShipmentType.IsActive = 0;
                    ShipmentType.IsDelete = 1;


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
                        olog.logging("DeleteShipmentType", msglog);
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
        public List<ShipmentTypeViewModel> ShipmentTypeDropdown(ShipmentTypeViewModel data)
        {
            try
            {
                var result = new List<ShipmentTypeViewModel>();

                var query = db.ms_ShipmentType.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.ShipmentType_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new ShipmentTypeViewModel();

                    resultItem.shipmentType_Index = item.ShipmentType_Index;
                    resultItem.shipmentType_Id = item.ShipmentType_Id;
                    resultItem.shipmentType_Name = item.ShipmentType_Name;

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
