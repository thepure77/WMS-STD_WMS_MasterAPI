using DataAccess;
using MasterDataBusiness.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterDataBusiness
{
    public class ProductBomService
    {
        private MasterDataDbContext db;

        public ProductBomService()
        {
            db = new MasterDataDbContext();
        }

        public ProductBomService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region autoProductBOM
        public List<ItemListViewModel> autoProductBOM(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    var query = context.ms_ProductBom.Where(c => c.IsActive == 1 && c.IsDelete == 0);
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Product_Id.Contains(data.key));

                    }

                    var result = query.Select(c => new { c.ProductBOM_Index, c.Product_Id, c.Product_Name,c.ProductConversion_Index,c.ProductConversion_Id,c.ProductConversion_Name,c.ProductConversion_Ratio,c.Product_Index }).Distinct().Take(10).ToList();

                    var items = new List<ItemListViewModel>();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.ProductBOM_Index,
                            id = item.Product_Id,
                            name = item.Product_Name,
                            value1 = item.ProductConversion_Index.ToString(),
                            value2 = item.ProductConversion_Id,
                            value3 = item.ProductConversion_Name,
                            value4 = item.ProductConversion_Ratio.ToString(),
                            value5 = item.Product_Index

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

        #region findProductBOMItem
        public List<ProductBOMItemViewModel> findProductBOMItem(ProductBOMItemViewModel data)
        {
            try
            {
                var query = db.ms_ProductBOMItem.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();


                if (!string.IsNullOrEmpty(data.productBOM_Index.ToString()) && data.productBOM_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                {
                    query = query.Where(c => c.ProductBOM_Index == data.productBOM_Index);
                }

                if (!string.IsNullOrEmpty(data.productBOM_No))
                {
                    query = query.Where(c => c.ProductBOM_No.Contains(data.productBOM_No));
                }


                var result = new List<ProductBOMItemViewModel>();

                var queryResult = query.ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new ProductBOMItemViewModel();

                    resultItem.productBOMItem_Index = item.ProductBOMItem_Index;
                    resultItem.productBOM_Index = item.ProductBOM_Index;
                    resultItem.productBOM_No = item.ProductBOM_No;
                    resultItem.product_Index = item.Product_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.product_SecondName = item.Product_SecondName;
                    resultItem.product_ThirdName = item.Product_ThirdName;
                    resultItem.qty = item.Qty;
                    resultItem.ratio = item.Ratio;
                    resultItem.totalQty = item.TotalQty;
                    resultItem.productConversion_Index = item.ProductConversion_Index;
                    resultItem.productConversion_Id = item.ProductConversion_Id;
                    resultItem.productConversion_Name = item.ProductConversion_Name;
                    resultItem.itemStatus_Index = item.ItemStatus_Index;
                    resultItem.itemStatus_Id = item.ItemStatus_Id;
                    resultItem.itemStatus_Name = item.ItemStatus_Name;
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
                    resultItem.productConversion_Weight_Index = item.ProductConversion_Weight_Index;
                    resultItem.productConversion_Volume_Index = item.ProductConversion_Volume_Index;


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

        public List<ProductViewModel> ProductBOM(ProductViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    var query = context.ms_ProductBom.AsQueryable();

                    if (!string.IsNullOrEmpty(data.product_Index.ToString()) && data.product_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.Product_Index == data.product_Index);
                    }

                    if (!string.IsNullOrEmpty(data.product_Id))
                    {
                        query = query.Where(c => c.Product_Id == data.product_Id);
                    }
                    if (data.is_promotion)
                    {
                        query = query.Where(c => c.ProductBOM_Type != "Promotion");
                    }

                    var items = new List<ProductViewModel>();

                    var result = query.ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ProductViewModel();

                        resultItem.product_SecondName = item.Product_SecondName;
                        resultItem.product_ThirdName = item.Product_ThirdName;

                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Index = item.Product_Index;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.product_SecondName = item.Product_SecondName;
                        resultItem.product_ThirdName = item.Product_ThirdName;
                 
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
                        resultItem.create_Date = item.Create_Date.GetValueOrDefault();
                        resultItem.create_By = item.Create_By;
                        resultItem.update_Date = item.Update_Date.GetValueOrDefault();
                        resultItem.update_By = item.Update_By;
                        resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
                        resultItem.cancel_By = item.Cancel_By;
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

    }
}
