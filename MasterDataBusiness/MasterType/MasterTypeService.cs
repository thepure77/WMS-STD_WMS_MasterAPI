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

namespace MasterDataBusiness.MasterType
{
    public class MasterTypeService
    {
        private MasterDataDbContext db;

        public MasterTypeService()
        {
            db = new MasterDataDbContext();
        }

        public MasterTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }
        #region filterMasterType
        public actionResultMasterTypeViewModel filter(SearchMasterTypeViewModel data)
        {
            try
            {

                var query = db.ms_MasterType.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.MasterType_Id.Contains(data.key)
                                         || c.MasterType_Name.Contains(data.key));
                }

                var Item = new List<ms_MasterType>();
                var TotalRow = new List<ms_MasterType>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.MasterType_Id).ToList();

                var result = new List<SearchMasterTypeViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchMasterTypeViewModel();

                    resultItem.masterType_Index = item.MasterType_Index;
                    resultItem.masterType_Id = item.MasterType_Id;
                    resultItem.masterType_Name = item.MasterType_Name;
                    resultItem.masterType_SecondName = item.MasterType_SecondName;
                    resultItem.ref_No1 = item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3;
                    resultItem.ref_No4 = item.Ref_No4;
                    resultItem.ref_No5 = item.Ref_No5;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.udf_5 = item.UDF_5;
                    resultItem.remark = item.Remark;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultMasterTypeViewModel = new actionResultMasterTypeViewModel();
                actionResultMasterTypeViewModel.itemsMasterType = result.ToList();
                actionResultMasterTypeViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultMasterTypeViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(MasterTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var MasterTypeOld = db.ms_MasterType.Find(data.masterType_Index);

                if (MasterTypeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.masterType_Id))
                    {
                        var query = db.ms_MasterType.FirstOrDefault(c => c.MasterType_Id == data.masterType_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.masterType_Id))
                    {
                        data.masterType_Id = "MasterType_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.ms_MasterType.FirstOrDefault(c => c.MasterType_Id == data.masterType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.MasterType_Id == data.masterType_Id)
                                {
                                    data.masterType_Id = "MasterType_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    ms_MasterType Model = new ms_MasterType();

                    Model.MasterType_Index = Guid.NewGuid();
                    Model.MasterType_Id = data.masterType_Id;
                    Model.MasterType_Name = data.masterType_Name;
                    Model.MasterType_SecondName = data.masterType_SecondName;
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
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.ms_MasterType.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.masterType_Id))
                    {
                        if (MasterTypeOld.MasterType_Id != "")
                        {
                            data.masterType_Id = MasterTypeOld.MasterType_Id;
                        }
                    }
                    else
                    {
                        if (MasterTypeOld.MasterType_Id != data.masterType_Id)
                        {
                            var query = db.ms_MasterType.FirstOrDefault(c => c.MasterType_Id == data.masterType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.masterType_Id = MasterTypeOld.MasterType_Id;
                        }
                    }

                    MasterTypeOld.MasterType_Id = data.masterType_Id;
                    MasterTypeOld.MasterType_Name = data.masterType_Name;
                    MasterTypeOld.MasterType_SecondName = data.masterType_SecondName;
                    MasterTypeOld.Ref_No1 = data.ref_No1;
                    MasterTypeOld.Ref_No2 = data.ref_No2;
                    MasterTypeOld.Ref_No3 = data.ref_No3;
                    MasterTypeOld.Ref_No4 = data.ref_No4;
                    MasterTypeOld.Ref_No5 = data.ref_No5;
                    MasterTypeOld.Remark = data.remark;
                    MasterTypeOld.UDF_1 = null;
                    MasterTypeOld.UDF_2 = null;
                    MasterTypeOld.UDF_3 = null;
                    MasterTypeOld.UDF_4 = null;
                    MasterTypeOld.UDF_5 = null;
                    MasterTypeOld.IsActive = Convert.ToInt32(data.isActive);
                    MasterTypeOld.Update_By = data.create_By;
                    MasterTypeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveMasterType", msglog);
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
        public MasterTypeViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.ms_MasterType.Where(c => c.MasterType_Index == id).FirstOrDefault();

                var result = new MasterTypeViewModel();


                result.masterType_Index = queryResult.MasterType_Index;
                result.masterType_Id = queryResult.MasterType_Id;
                result.masterType_Name = queryResult.MasterType_Name;
                result.masterType_SecondName = queryResult.MasterType_SecondName;
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

        #region getDelete
        public Boolean getDelete(MasterTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Product = db.ms_MasterType.Find(data.masterType_Index);

                if (Product != null)
                {
                    Product.IsActive = 0;
                    Product.IsDelete = 1;


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
                        olog.logging("DeleteMasterType" +
                            "" +
                            "", msglog);
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
