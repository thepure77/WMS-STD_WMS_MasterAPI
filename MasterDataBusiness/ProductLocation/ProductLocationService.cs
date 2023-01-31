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
    public class ProductLocationService
    {
        private MasterDataDbContext db;

        public ProductLocationService()
        {
            db = new MasterDataDbContext();
        }

        public ProductLocationService(MasterDataDbContext db)
        {
            this.db = db;
        }

 
        #region filterProductLocation
        public actionResultProductLocationViewModel filter(SearchProductLocationViewModel data)
        {
            try
            {
                var query = db.View_ProductLocation.AsQueryable();
                query = query.Where( c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.ProductLocation_Id.Contains(data.key)
                                        || c.Location_Name.Contains(data.key)
                                        || c.Product_Name.Contains(data.key)
                                         || c.Product_Id.Contains(data.key)
                                          || c.Location_Id.Contains(data.key));

                }

                var Item = new List<View_ProductLocation>();
                var TotalRow = new List<View_ProductLocation>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ProductLocation_Id).ToList();

                var result = new List<SearchProductLocationViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchProductLocationViewModel();

                    resultItem.productLocation_Index = item.ProductLocation_Index;
                    resultItem.productLocation_Id = item.ProductLocation_Id;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.Location_Id = item.Location_Id;
                    resultItem.Location_Name = item.Location_Name;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.isActive = item.IsActive;
                    resultItem.Qty = item.Qty;
                    resultItem.replenish_Qty = item.Replenish_Qty;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultProductLocationViewModel = new actionResultProductLocationViewModel();
                actionResultProductLocationViewModel.itemsProductLocation = result.ToList();
                actionResultProductLocationViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, };

                return actionResultProductLocationViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(ProductLocationViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var ProductLocationOld = db.MS_ProductLocation.Find(data.productLocation_Index);

  

                if (ProductLocationOld == null)
                {
                    var checkLocation = db.MS_ProductLocation.Where(c => c.Location_Index == data.Location_Index && c.IsActive == 1 && c.IsDelete == 0).FirstOrDefault();

                    if (checkLocation != null && data.Location_Index != Guid.Parse("A6A7CCA3-1D76-42D6-8EEF-83555FAB110D"))
                    {
                        return "LocationDup";
                    }
                    data.productLocation_Id = "ProductLocation_Id".genAutonumber();

                    MS_ProductLocation Model = new MS_ProductLocation();

                    Model.ProductLocation_Index = Guid.NewGuid();
                    Model.ProductLocation_Id = data.productLocation_Id;
                    Model.Location_Index = data.Location_Index;
                    Model.Product_Index = data.product_Index;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.Qty = data.Qty;
                    Model.Replenish_Qty = data.Replenish_Qty;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_ProductLocation.Add(Model);
                }
                else
                {
                    ProductLocationOld.Location_Index = data.Location_Index;
                    ProductLocationOld.Product_Index = data.product_Index;
                    ProductLocationOld.IsActive = Convert.ToInt32(data.isActive);
                    ProductLocationOld.Update_By = data.create_By;
                    ProductLocationOld.Qty = data.Qty;
                    ProductLocationOld.Replenish_Qty = data.Replenish_Qty;
                    ProductLocationOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveProductLocation", msglog);
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
        public ProductLocationViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_ProductLocation.Where(c => c.ProductLocation_Index == id).FirstOrDefault();

                var result = new ProductLocationViewModel();


                result.productLocation_Index = queryResult.ProductLocation_Index;
                result.productLocation_Id = queryResult.ProductLocation_Id;
                result.Location_Index = queryResult.Location_Index;
                result.Location_Name = queryResult.Location_Name;
                result.product_Index = queryResult.Product_Index;
                result.Qty = queryResult.Qty;
                result.Replenish_Qty = queryResult.Replenish_Qty;
                result.product_Name = queryResult.Product_Name;
                result.key = queryResult.Product_Id + " - " + queryResult.Product_Name;
                result.key2 = queryResult.Location_Id + " - " + queryResult.Location_Name;
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
        public Boolean getDelete(ProductLocationViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Location = db.MS_ProductLocation.Find(data.productLocation_Index);

                if (Location != null)
                {
                    Location.IsActive = 0;
                    Location.IsDelete = 1;


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
                        olog.logging("DeleteProductLocation" +
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
