using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.MaterialClass;
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
    public class MaterialClassService
    {
        private MasterDataDbContext db;

        public MaterialClassService()
        {
            db = new MasterDataDbContext();
        }

        public MaterialClassService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterMaterialClass
        public actionResultMaterialClassViewModel filter(SearchMaterialClassViewModel data)
        {
            try
            {

                var query = db.ms_MaterialClass.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.MaterialClass_Id.Contains(data.key)
                                         || c.MaterialClass_Name.Contains(data.key));
                }

                var Item = new List<ms_MaterialClass>();
                var TotalRow = new List<ms_MaterialClass>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.MaterialClass_Id).ToList();

                var result = new List<SearchMaterialClassViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchMaterialClassViewModel();

                    resultItem.materialClass_Index = item.MaterialClass_Index;
                    resultItem.materialClass_Id = item.MaterialClass_Id;
                    resultItem.materialClass_Name = item.MaterialClass_Name;
                    resultItem.materialClass_SecondName = item.MaterialClass_SecondName;
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

                var actionResultMaterialClassViewModel = new actionResultMaterialClassViewModel();
                actionResultMaterialClassViewModel.itemsMaterialClass = result.ToList();
                actionResultMaterialClassViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultMaterialClassViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(MaterialClassViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var MaterialClassOld = db.ms_MaterialClass.Find(data.materialClass_Index);

                if (MaterialClassOld == null)
                {
                    if (!string.IsNullOrEmpty(data.materialClass_Id))
                    {
                        var query = db.ms_MaterialClass.FirstOrDefault(c => c.MaterialClass_Id == data.materialClass_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.materialClass_Id))
                    {
                        data.materialClass_Id = "MaterialClass_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.ms_MaterialClass.FirstOrDefault(c => c.MaterialClass_Id == data.materialClass_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.MaterialClass_Id == data.materialClass_Id)
                                {
                                    data.materialClass_Id = "MaterialClass_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    ms_MaterialClass Model = new ms_MaterialClass();

                    Model.MaterialClass_Index = Guid.NewGuid();
                    Model.MaterialClass_Id = data.materialClass_Id;
                    Model.MaterialClass_Name = data.materialClass_Name;
                    Model.MaterialClass_SecondName = data.materialClass_SecondName;
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

                    db.ms_MaterialClass.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.materialClass_Id))
                    {
                        if (MaterialClassOld.MaterialClass_Id != "")
                        {
                            data.materialClass_Id = MaterialClassOld.MaterialClass_Id;
                        }
                    }
                    else
                    {
                        if (MaterialClassOld.MaterialClass_Id != data.materialClass_Id)
                        {
                            var query = db.ms_MaterialClass.FirstOrDefault(c => c.MaterialClass_Id == data.materialClass_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.materialClass_Id = MaterialClassOld.MaterialClass_Id;
                        }
                    }

                    MaterialClassOld.MaterialClass_Id = data.materialClass_Id;
                    MaterialClassOld.MaterialClass_Name = data.materialClass_Name;
                    MaterialClassOld.MaterialClass_SecondName = data.materialClass_SecondName;
                    MaterialClassOld.Ref_No1 = data.ref_No1;
                    MaterialClassOld.Ref_No2 = data.ref_No2;
                    MaterialClassOld.Ref_No3 = data.ref_No3;
                    MaterialClassOld.Ref_No4 = data.ref_No4;
                    MaterialClassOld.Ref_No5 = data.ref_No5;
                    MaterialClassOld.Remark = data.remark;
                    MaterialClassOld.UDF_1 = null;
                    MaterialClassOld.UDF_2 = null;
                    MaterialClassOld.UDF_3 = null;
                    MaterialClassOld.UDF_4 = null;
                    MaterialClassOld.UDF_5 = null;
                    MaterialClassOld.IsActive = Convert.ToInt32(data.isActive);
                    MaterialClassOld.Update_By = data.create_By;
                    MaterialClassOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveMaterialClass", msglog);
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
        public MaterialClassViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.ms_MaterialClass.Where(c => c.MaterialClass_Index == id).FirstOrDefault();

                var result = new MaterialClassViewModel();


                result.materialClass_Index = queryResult.MaterialClass_Index;
                result.materialClass_Id = queryResult.MaterialClass_Id;
                result.materialClass_Name = queryResult.MaterialClass_Name;
                result.materialClass_SecondName = queryResult.MaterialClass_SecondName;
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
        public Boolean getDelete(MaterialClassViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Product = db.ms_MaterialClass.Find(data.materialClass_Index);

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
                        olog.logging("DeleteMaterialClass" +
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
