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
    public class VendorTypeService
    {

        private MasterDataDbContext db;

        public VendorTypeService()
        {
            db = new MasterDataDbContext();
        }

        public VendorTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterVendorType
        public actionResultVendorTypeViewModel filter(SearchVendorTypeViewModel data)
        {
            try
            {
                var query = db.MS_VendorType.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.VendorType_Id.Contains(data.key)
                                         || c.VendorType_Name.Contains(data.key));
                }

                var Item = new List<MS_VendorType>();
                var TotalRow = new List<MS_VendorType>();

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

                Item = query.OrderBy(o => o.VendorType_Index).ToList();

                var result = new List<SearchVendorTypeViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchVendorTypeViewModel();

                    resultItem.vendorType_Index = item.VendorType_Index;
                    resultItem.vendorType_Id = item.VendorType_Id;
                    resultItem.vendorType_Name = item.VendorType_Name;
                    resultItem.vendorType_SecondName = item.VendorType_SecondName;
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

                var actionResultVendorTypeViewModel = new actionResultVendorTypeViewModel();
                actionResultVendorTypeViewModel.itemsVendorType = result.ToList();
                actionResultVendorTypeViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultVendorTypeViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region SaveChanges
        public String SaveChanges(VendorTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var VendorTypeOld = db.MS_VendorType.Find(data.vendorType_Index);

                if (VendorTypeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.vendorType_Id))
                    {
                        var query = db.MS_VendorType.FirstOrDefault(c => c.VendorType_Id == data.vendorType_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.vendorType_Id))
                    {
                        data.vendorType_Id = "VendorType_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_VendorType.FirstOrDefault(c => c.VendorType_Id == data.vendorType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.VendorType_Id == data.vendorType_Id)
                                {
                                    data.vendorType_Id = "VendorType_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_VendorType Model = new MS_VendorType();

                    Model.VendorType_Index = Guid.NewGuid();
                    Model.VendorType_Id = data.vendorType_Id;
                    Model.VendorType_Name = data.vendorType_Name;
                    Model.VendorType_SecondName = data.vendorType_SecondName;
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

                    db.MS_VendorType.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.vendorType_Id))
                    {
                        if (VendorTypeOld.VendorType_Id != "")
                        {
                            data.vendorType_Id = VendorTypeOld.VendorType_Id;
                        }
                    }
                    else
                    {
                        if (VendorTypeOld.VendorType_Id != data.vendorType_Id)
                        {
                            var query = db.MS_VendorType.FirstOrDefault(c => c.VendorType_Id == data.vendorType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.vendorType_Id = VendorTypeOld.VendorType_Id;
                        }
                    }

                    VendorTypeOld.VendorType_Id = data.vendorType_Id;
                    VendorTypeOld.VendorType_Name = data.vendorType_Name;
                    VendorTypeOld.VendorType_SecondName = data.vendorType_SecondName;
                    VendorTypeOld.Ref_No1 = data.ref_No1;
                    VendorTypeOld.Ref_No2 = data.ref_No2;
                    VendorTypeOld.Ref_No3 = data.ref_No3;
                    VendorTypeOld.Ref_No4 = data.ref_No4;
                    VendorTypeOld.Ref_No5 = data.ref_No5;
                    VendorTypeOld.Remark = data.remark;
                    VendorTypeOld.UDF_1 = data.udf_1;
                    VendorTypeOld.UDF_2 = data.udf_2;
                    VendorTypeOld.UDF_3 = data.udf_3;
                    VendorTypeOld.UDF_4 = data.udf_4;
                    VendorTypeOld.UDF_5 = data.udf_5;
                    VendorTypeOld.IsActive = Convert.ToInt32(data.isActive);
                    VendorTypeOld.Update_By = data.create_By;
                    VendorTypeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveVendorType", msglog);
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
        public VendorTypeViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_VendorType.Where(c => c.VendorType_Index == id).FirstOrDefault();

                var result = new VendorTypeViewModel();


                result.vendorType_Index = queryResult.VendorType_Index;
                result.vendorType_Id = queryResult.VendorType_Id;
                result.vendorType_Name = queryResult.VendorType_Name;
                result.vendorType_SecondName = queryResult.VendorType_SecondName;
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
        public Boolean getDelete(VendorTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var warehouse = db.MS_VendorType.Find(data.vendorType_Index);

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
                        olog.logging("DeleteVendorType", msglog);
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
    }
}
