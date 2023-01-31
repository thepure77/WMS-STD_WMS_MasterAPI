using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.ProductHierarchy5;
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
    public class ProductHierarchy5Service
    {
        private MasterDataDbContext db;

        public ProductHierarchy5Service()
        {
            db = new MasterDataDbContext();
        }

        public ProductHierarchy5Service(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterProductHierarchy5
        public actionResultProductHierarchy5ViewModel filter(SearchProductHierarchy5ViewModel data)
        {
            try
            {

                var query = db.ms_ProductHierarchy5.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.ProductHierarchy5_Id.Contains(data.key)
                                         || c.ProductHierarchy5_Name.Contains(data.key));
                }

                var Item = new List<ms_ProductHierarchy5>();
                var TotalRow = new List<ms_ProductHierarchy5>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ProductHierarchy5_Id).ToList();

                var result = new List<SearchProductHierarchy5ViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchProductHierarchy5ViewModel();

                    resultItem.productHierarchy5_Index = item.ProductHierarchy5_Index;
                    resultItem.productHierarchy5_Id = item.ProductHierarchy5_Id;
                    resultItem.productHierarchy5_Name = item.ProductHierarchy5_Name;
                    resultItem.productHierarchy5_SecondName = item.ProductHierarchy5_SecondName;
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

                var actionResultProductHierarchy5ViewModel = new actionResultProductHierarchy5ViewModel();
                actionResultProductHierarchy5ViewModel.itemsProductHierarchy5 = result.ToList();
                actionResultProductHierarchy5ViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultProductHierarchy5ViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(ProductHierarchy5ViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var ProductHierarchy5Old = db.ms_ProductHierarchy5.Find(data.productHierarchy5_Index);

                if (ProductHierarchy5Old == null)
                {
                    if (!string.IsNullOrEmpty(data.productHierarchy5_Id))
                    {
                        var query = db.ms_ProductHierarchy5.FirstOrDefault(c => c.ProductHierarchy5_Id == data.productHierarchy5_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.productHierarchy5_Id))
                    {
                        data.productHierarchy5_Id = "ProductHierarchy5_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.ms_ProductHierarchy5.FirstOrDefault(c => c.ProductHierarchy5_Id == data.productHierarchy5_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.ProductHierarchy5_Id == data.productHierarchy5_Id)
                                {
                                    data.productHierarchy5_Id = "ProductHierarchy5_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    ms_ProductHierarchy5 Model = new ms_ProductHierarchy5();

                    Model.ProductHierarchy5_Index = Guid.NewGuid();
                    Model.ProductHierarchy5_Id = data.productHierarchy5_Id;
                    Model.ProductHierarchy5_Name = data.productHierarchy5_Name;
                    Model.ProductHierarchy5_SecondName = data.productHierarchy5_SecondName;
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

                    db.ms_ProductHierarchy5.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.productHierarchy5_Id))
                    {
                        if (ProductHierarchy5Old.ProductHierarchy5_Id != "")
                        {
                            data.productHierarchy5_Id = ProductHierarchy5Old.ProductHierarchy5_Id;
                        }
                    }
                    else
                    {
                        if (ProductHierarchy5Old.ProductHierarchy5_Id != data.productHierarchy5_Id)
                        {
                            var query = db.ms_ProductHierarchy5.FirstOrDefault(c => c.ProductHierarchy5_Id == data.productHierarchy5_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.productHierarchy5_Id = ProductHierarchy5Old.ProductHierarchy5_Id;
                        }
                    }

                    ProductHierarchy5Old.ProductHierarchy5_Id = data.productHierarchy5_Id;
                    ProductHierarchy5Old.ProductHierarchy5_Name = data.productHierarchy5_Name;
                    ProductHierarchy5Old.ProductHierarchy5_SecondName = data.productHierarchy5_SecondName;
                    ProductHierarchy5Old.Ref_No1 = data.ref_No1;
                    ProductHierarchy5Old.Ref_No2 = data.ref_No2;
                    ProductHierarchy5Old.Ref_No3 = data.ref_No3;
                    ProductHierarchy5Old.Ref_No4 = data.ref_No4;
                    ProductHierarchy5Old.Ref_No5 = data.ref_No5;
                    ProductHierarchy5Old.Remark = data.remark;
                    ProductHierarchy5Old.UDF_1 = null;
                    ProductHierarchy5Old.UDF_2 = null;
                    ProductHierarchy5Old.UDF_3 = null;
                    ProductHierarchy5Old.UDF_4 = null;
                    ProductHierarchy5Old.UDF_5 = null;
                    ProductHierarchy5Old.IsActive = Convert.ToInt32(data.isActive);
                    ProductHierarchy5Old.Update_By = data.create_By;
                    ProductHierarchy5Old.Update_Date = DateTime.Now;
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
                    olog.logging("SaveProductHierarchy5", msglog);
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
        public ProductHierarchy5ViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.ms_ProductHierarchy5.Where(c => c.ProductHierarchy5_Index == id).FirstOrDefault();

                var result = new ProductHierarchy5ViewModel();


                result.productHierarchy5_Index = queryResult.ProductHierarchy5_Index;
                result.productHierarchy5_Id = queryResult.ProductHierarchy5_Id;
                result.productHierarchy5_Name = queryResult.ProductHierarchy5_Name;
                result.productHierarchy5_SecondName = queryResult.ProductHierarchy5_SecondName;
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
        public Boolean getDelete(ProductHierarchy5ViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Product = db.ms_ProductHierarchy5.Find(data.productHierarchy5_Index);

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
                        olog.logging("DeleteProductHierarchy5" +
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
