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
using static MasterDataBusiness.ViewModels.ProductConversionViewModel;
using Comone.Utils;

namespace MasterDataBusiness
{
    public class ProductConversionService
    {
        private MasterDataDbContext db;

        public ProductConversionService()
        {
            db = new MasterDataDbContext();
        }

        public ProductConversionService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterProductConversion
        public actionResultProductConversionViewModel filter(SearchProductConversionViewModel data)
        {
            try
            {
                var query = db.View_ProductConversion.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (data.changeSet != "1")
                {
                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductConversion_Id.Contains(data.key)
                                             || c.ProductConversion_Name.Contains(data.key)
                                             || c.Product_Id.Contains(data.key)
                                             || c.Product_Name.Contains(data.key));
                    }

                    if (!string.IsNullOrEmpty(data.product_Id))
                    {
                        query = query.Where(c => c.Product_Id.Contains(data.product_Id));
                    }

                    if (!string.IsNullOrEmpty(data.create_date) && !string.IsNullOrEmpty(data.create_date_to))
                    {
                        var dateStart = data.create_date.toBetweenDate();
                        var dateEnd = data.create_date_to.toBetweenDate();
                        query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductConversion_Id.Contains(data.key)
                                             || c.ProductConversion_Name.Contains(data.key)
                                             || c.Product_Id.Contains(data.key)
                                             || c.Product_Name.Contains(data.key));
                    }

                    if (!string.IsNullOrEmpty(data.product_Id))
                    {
                        query = query.Where(c => c.Product_Id.Contains(data.product_Id));
                    }
                }
                               

                var Item = new List<View_ProductConversion>();
                var TotalRow = new List<View_ProductConversion>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ProductConversion_Id).ToList();

                var result = new List<SearchProductConversionViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchProductConversionViewModel();

                    resultItem.productConversion_Index = item.ProductConversion_Index;
                    resultItem.productConversion_Id = item.ProductConversion_Id;
                    resultItem.productConversion_Name = item.ProductConversion_Name;
                    resultItem.productConversion_SecondName = item.ProductConversion_SecondName;
                    resultItem.productConversion_Volume = item.ProductConversion_Volume;
                    resultItem.productConversion_SecondName = item.ProductConversion_SecondName;
                    resultItem.productConversion_Ratio = item.ProductConversion_Ratio;
                    resultItem.productConversion_VolumeRatio = item.ProductConversion_VolumeRatio;
                    resultItem.productConversion_Weight = item.ProductConversion_Weight;
                    resultItem.productConversion_Width = item.ProductConversion_Width;
                    resultItem.productConversion_Length = item.ProductConversion_Length;
                    resultItem.productConversion_Height = item.ProductConversion_Height;
                    resultItem.product_Index = item.Product_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.businessUnit_Index = item.BusinessUnit_Index;
                    resultItem.businessUnit_Id = item.BusinessUnit_Id;
                    resultItem.businessUnit_Name = item.BusinessUnit_Name;
                    resultItem.ref_No1 = item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3;
                    resultItem.ref_No4 = item.Ref_No4;
                    resultItem.ref_No5 = item.Ref_No5;
                    resultItem.remark = item.Remark;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.udf_5 = item.UDF_5;
                    //resultItem.sale_UNIT = item.SALE_UNIT;
                    //resultItem.in_UNIT = item.IN_UNIT;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultProductConversionViewModel = new actionResultProductConversionViewModel();
                actionResultProductConversionViewModel.itemsProductConversion = result.ToList();
                actionResultProductConversionViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultProductConversionViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public actionResultProductConversionViewModel FilterInClause(string jsonData)
        {
            try
            {
                SearchProductConversionInClauseViewModel model = JsonConvert.DeserializeObject<SearchProductConversionInClauseViewModel>(jsonData);

                var query = db.View_ProductConversion.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (!(model.List_Product_Index is null) && model.List_Product_Index.Count > 0)
                {
                    query = query.Where(c => model.List_Product_Index.Contains((Guid)c.Product_Index));
                }

                if (!(model.List_ProductConversion_Id is null) && model.List_ProductConversion_Id.Count > 0)
                {
                    query = query.Where(c => model.List_ProductConversion_Id.Contains(c.ProductConversion_Id));
                }

                if (!(model.List_ProductConversion_Index is null) && model.List_ProductConversion_Index.Count > 0)
                {
                    query = query.Where(c => model.List_ProductConversion_Index.Contains(c.ProductConversion_Index));
                }

                var Item = new List<View_ProductConversion>();
                var TotalRow = new List<View_ProductConversion>();

                TotalRow = query.ToList();


                if (model.CurrentPage != 0 && model.PerPage != 0)
                {
                    query = query.Skip(((model.CurrentPage - 1) * model.PerPage));
                }

                if (model.PerPage != 0)
                {
                    query = query.Take(model.PerPage);

                }

                Item = query.OrderBy(o => o.ProductConversion_Id).ToList();

                var result = new List<SearchProductConversionViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchProductConversionViewModel();

                    resultItem.productConversion_Index = item.ProductConversion_Index;
                    resultItem.productConversion_Id = item.ProductConversion_Id;
                    resultItem.productConversion_Name = item.ProductConversion_Name;
                    resultItem.productConversion_Ratio = item.ProductConversion_Ratio;

                    resultItem.productConversion_GrsWeight = item.ProductConversion_GrsWeight;
                    resultItem.productConversion_GrsWeight_Index = item.ProductConversion_GrsWeight_Index;
                    resultItem.productConversion_GrsWeight_Id = item.ProductConversion_GrsWeight_Id;
                    resultItem.productConversion_GrsWeight_Name = item.ProductConversion_GrsWeight_Name;
                    resultItem.productConversion_GrsWeightRatio = item.ProductConversion_GrsWeightRatio;

                    resultItem.productConversion_Weight = item.ProductConversion_Weight;
                    resultItem.productConversion_Weight_Index = item.ProductConversion_Weight_Index;
                    resultItem.productConversion_Weight_Id = item.ProductConversion_Weight_Id;
                    resultItem.productConversion_Weight_Name = item.ProductConversion_Weight_Name;
                    resultItem.productConversion_WeightRatio = item.ProductConversion_WeightRatio;

                    resultItem.productConversion_Volume = item.ProductConversion_Volume;
                    resultItem.productConversion_Volume_Index = item.ProductConversion_Volume_Index;
                    resultItem.productConversion_Volume_Id = item.ProductConversion_Volume_Id;
                    resultItem.productConversion_Volume_Name = item.ProductConversion_Volume_Name;
                    resultItem.productConversion_VolumeRatio = item.ProductConversion_VolumeRatio;

                    resultItem.productConversion_Width = item.ProductConversion_Width;
                    resultItem.productConversion_Width_Index = item.ProductConversion_Width_Index;
                    resultItem.productConversion_Width_Id = item.ProductConversion_Width_Id;
                    resultItem.productConversion_Width_Name = item.ProductConversion_Width_Name;
                    resultItem.productConversion_WidthRatio = item.ProductConversion_WidthRatio;

                    resultItem.productConversion_Length = item.ProductConversion_Length;
                    resultItem.productConversion_Length_Index = item.ProductConversion_Length_Index;
                    resultItem.productConversion_Length_Id = item.ProductConversion_Length_Id;
                    resultItem.productConversion_Length_Name = item.ProductConversion_Length_Name;
                    resultItem.productConversion_LengthRatio = item.ProductConversion_LengthRatio;

                    resultItem.productConversion_Height = item.ProductConversion_Height;
                    resultItem.productConversion_Height_Index = item.ProductConversion_Height_Index;
                    resultItem.productConversion_Height_Id = item.ProductConversion_Height_Id;
                    resultItem.productConversion_Height_Name = item.ProductConversion_Height_Name;
                    resultItem.productConversion_HeightRatio = item.ProductConversion_HeightRatio;

                    resultItem.product_Index = item.Product_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultProductConversionViewModel = new actionResultProductConversionViewModel();
                actionResultProductConversionViewModel.itemsProductConversion = result.ToList();
                actionResultProductConversionViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = model.CurrentPage, PerPage = model.PerPage, Key = null };

                return actionResultProductConversionViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(ProductConversionViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var ProductConversionOld = db.MS_ProductConversion.Find(data.productConversion_Index);

                

                if (ProductConversionOld == null)
                {
                    if (!string.IsNullOrEmpty(data.productConversion_Id))
                    {
                        var query = db.MS_ProductConversion.FirstOrDefault(c => c.ProductConversion_Id == data.productConversion_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (!string.IsNullOrEmpty(data.productConversion_Name))
                    {
                        var query = db.MS_ProductConversion.FirstOrDefault(c => c.ProductConversion_Name == data.productConversion_Name && c.IsActive == 1 && c.Product_Index == data.product_Index);
                        if (query != null)
                        {
                            return "ProductConversion_Name " + data.productConversion_Name + " Is Used";
                        }
                    }
                    if (string.IsNullOrEmpty(data.productConversion_Id))
                    {
                        data.productConversion_Id = "ProductConversion_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_ProductConversion.FirstOrDefault(c => c.ProductConversion_Id == data.productConversion_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.ProductConversion_Id == data.productConversion_Id)
                                {
                                    data.productConversion_Id = "ProductConversion_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_ProductConversion Model = new MS_ProductConversion();

                    if (data.sale_UNIT == 1)
                    {
                        var productcon_sale = db.MS_ProductConversion.FirstOrDefault(c => c.Product_Index == data.product_Index && c.ProductConversion_Name != data.productConversion_Name && c.SALE_UNIT == 1);
                        if (productcon_sale != null)
                        {
                            return productcon_sale.ProductConversion_Name + " is Sale Unit";
                        }
                    }
                    if (data.in_UNIT == 1)
                    {
                        var productcon_in = db.MS_ProductConversion.FirstOrDefault(c => c.Product_Index == data.product_Index && c.ProductConversion_Name != data.productConversion_Name && c.IN_UNIT == 1);
                        if (productcon_in != null)
                        {
                            return productcon_in.ProductConversion_Name + " is In Unit";
                        }
                    }

                    Model.ProductConversion_Index = Guid.NewGuid();
                    Model.ProductConversion_Id = data.productConversion_Id;
                    Model.ProductConversion_Name = data.productConversion_Name;
                    Model.ProductConversion_SecondName = data.productConversion_SecondName;
                    Model.ProductConversion_Ratio = data.productConversion_Ratio.GetValueOrDefault();

                    Model.ProductConversion_Weight = data.productConversion_Weight;
                    Model.ProductConversion_Weight_Index = data.productConversion_Weight_Index;
                    Model.ProductConversion_Weight_Id = data.productConversion_Weight_Id;
                    Model.ProductConversion_Weight_Name = data.productConversion_Weight_Name;
                    Model.ProductConversion_WeightRatio = data.productConversion_WeightRatio;

                    Model.ProductConversion_GrsWeight = data.productConversion_GrsWeight;
                    Model.ProductConversion_GrsWeight_Index = data.productConversion_GrsWeight_Index;
                    Model.ProductConversion_GrsWeight_Id = data.productConversion_GrsWeight_Id;
                    Model.ProductConversion_GrsWeight_Name = data.productConversion_GrsWeight_Name;
                    Model.ProductConversion_GrsWeightRatio = data.productConversion_GrsWeightRatio;

                    Model.ProductConversion_Width = data.productConversion_Width;
                    Model.ProductConversion_Width_Index = data.productConversion_Volume_Index;
                    Model.ProductConversion_Width_Id = data.productConversion_Volume_Id;
                    Model.ProductConversion_Width_Name = data.productConversion_Volume_Name;
                    Model.ProductConversion_WidthRatio = data.productConversion_VolumeRatio;


                    Model.ProductConversion_Length = data.productConversion_Length;
                    Model.ProductConversion_Length_Index = data.productConversion_Volume_Index;
                    Model.ProductConversion_Length_Id = data.productConversion_Volume_Id;
                    Model.ProductConversion_Length_Name = data.productConversion_Volume_Name;
                    Model.ProductConversion_LengthRatio = data.productConversion_VolumeRatio;

                    Model.ProductConversion_Height = data.productConversion_Height;
                    Model.ProductConversion_Height_Index = data.productConversion_Volume_Index;
                    Model.ProductConversion_Height_Id = data.productConversion_Volume_Id;
                    Model.ProductConversion_Height_Name = data.productConversion_Volume_Name;
                    Model.ProductConversion_HeightRatio = data.productConversion_VolumeRatio;

                    Model.ProductConversion_Volume = (data.productConversion_Width * data.productConversion_Length * data.productConversion_Height);
                    Model.ProductConversion_Volume_Index = data.productConversion_Volume_Index;
                    Model.ProductConversion_Volume_Id = data.productConversion_Volume_Id;
                    Model.ProductConversion_Volume_Name = data.productConversion_Volume_Name;
                    Model.ProductConversion_VolumeRatio = data.productConversion_VolumeRatio ?? 0;

                    Model.Product_Index = data.product_Index.GetValueOrDefault();
                    Model.Product_Id = data.product_Id;
                    Model.Product_Name = data.product_Name;
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
                    Model.SALE_UNIT = Convert.ToInt32(data.sale_UNIT);
                    Model.IN_UNIT = Convert.ToInt32(data.in_UNIT);
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_ProductConversion.Add(Model);

                    if (data.documents != null)
                    {
                        foreach (var d in data.documents.Where(c => !c.isDelete))
                        {
                            im_DocumentFile documents = new im_DocumentFile();

                            documents.DocumentFile_Index = Guid.NewGuid(); ;
                            documents.DocumentFile_Name = d.filename;
                            documents.DocumentFile_Path = d.path;
                            documents.DocumentFile_Url = d.urlAttachFile;
                            documents.DocumentFile_Type = d.type;
                            documents.DocumentFile_Status = 0;
                            documents.Create_By = data.create_By;
                            documents.Create_Date = DateTime.Now;
                            documents.Ref_Index = data.productConversion_Index;
                            documents.Ref_No = data.productConversion_Id;
                            db.im_DocumentFile.Add(documents);
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(data.productConversion_Name))
                    {
                        var query = db.MS_ProductConversion.FirstOrDefault(c => c.ProductConversion_Name == data.productConversion_Name && c.IsActive == 1 && c.ProductConversion_Index != ProductConversionOld.ProductConversion_Index && c.Product_Index == data.product_Index );
                        if (query != null)
                        {
                            return "ProductConversion_Name " + data.productConversion_Name + " Is Used";
                        }
                    }
                    if (data.sale_UNIT == 1)
                    {
                        var productcon_sale = db.MS_ProductConversion.FirstOrDefault(c => c.Product_Index == data.product_Index && c.ProductConversion_Name != data.productConversion_Name && c.SALE_UNIT == 1  );
                        if (productcon_sale != null)
                        {
                            return productcon_sale.ProductConversion_Name + " is Sale Unit";
                        }
                    }
                    if (data.in_UNIT == 1)
                    {
                        var productcon_in = db.MS_ProductConversion.FirstOrDefault(c => c.Product_Index == data.product_Index && c.ProductConversion_Name != data.productConversion_Name && c.IN_UNIT == 1);
                        if (productcon_in != null)
                        {
                            return productcon_in.ProductConversion_Name + " is In Unit";
                        }
                    }
                    

                    if (string.IsNullOrEmpty(data.productConversion_Id))
                    {
                        if (ProductConversionOld.ProductConversion_Id != "")
                        {
                            data.productConversion_Id = ProductConversionOld.ProductConversion_Id;
                        }
                    }
                    else
                    {
                        if (ProductConversionOld.ProductConversion_Id != data.productConversion_Id)
                        {
                            var query = db.MS_ProductConversion.FirstOrDefault(c => c.ProductConversion_Id == data.productConversion_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.productConversion_Id = ProductConversionOld.ProductConversion_Id;
                        }
                    }

                    ProductConversionOld.ProductConversion_Id = data.productConversion_Id;
                    ProductConversionOld.ProductConversion_Name = data.productConversion_Name;
                    ProductConversionOld.ProductConversion_SecondName = data.productConversion_SecondName;
                    ProductConversionOld.ProductConversion_Ratio = data.productConversion_Ratio.GetValueOrDefault();

                    ProductConversionOld.ProductConversion_Weight = data.productConversion_Weight;
                    ProductConversionOld.ProductConversion_Weight_Index = data.productConversion_Weight_Index;
                    ProductConversionOld.ProductConversion_Weight_Id = data.productConversion_Weight_Id;
                    ProductConversionOld.ProductConversion_Weight_Name = data.productConversion_Weight_Name;
                    ProductConversionOld.ProductConversion_WeightRatio = data.productConversion_WeightRatio;

                    ProductConversionOld.ProductConversion_GrsWeight = data.productConversion_GrsWeight;
                    ProductConversionOld.ProductConversion_GrsWeight_Index = data.productConversion_GrsWeight_Index;
                    ProductConversionOld.ProductConversion_GrsWeight_Id = data.productConversion_GrsWeight_Id;
                    ProductConversionOld.ProductConversion_GrsWeight_Name = data.productConversion_GrsWeight_Name;
                    ProductConversionOld.ProductConversion_GrsWeightRatio = data.productConversion_GrsWeightRatio;

                    ProductConversionOld.ProductConversion_Width = data.productConversion_Width;
                    ProductConversionOld.ProductConversion_Width_Index = data.productConversion_Volume_Index;
                    ProductConversionOld.ProductConversion_Width_Id = data.productConversion_Volume_Id;
                    ProductConversionOld.ProductConversion_Width_Name = data.productConversion_Volume_Name;
                    ProductConversionOld.ProductConversion_WidthRatio = data.productConversion_VolumeRatio;


                    ProductConversionOld.ProductConversion_Length = data.productConversion_Length;
                    ProductConversionOld.ProductConversion_Length_Index = data.productConversion_Volume_Index;
                    ProductConversionOld.ProductConversion_Length_Id = data.productConversion_Volume_Id;
                    ProductConversionOld.ProductConversion_Length_Name = data.productConversion_Volume_Name;
                    ProductConversionOld.ProductConversion_LengthRatio = data.productConversion_VolumeRatio;

                    ProductConversionOld.ProductConversion_Height = data.productConversion_Height;
                    ProductConversionOld.ProductConversion_Height_Index = data.productConversion_Volume_Index;
                    ProductConversionOld.ProductConversion_Height_Id = data.productConversion_Volume_Id;
                    ProductConversionOld.ProductConversion_Height_Name = data.productConversion_Volume_Name;
                    ProductConversionOld.ProductConversion_HeightRatio = data.productConversion_VolumeRatio;

                    ProductConversionOld.ProductConversion_Volume = (data.productConversion_Width * data.productConversion_Length * data.productConversion_Height);
                    ProductConversionOld.ProductConversion_Volume_Index = data.productConversion_Volume_Index;
                    ProductConversionOld.ProductConversion_Volume_Id = data.productConversion_Volume_Id;
                    ProductConversionOld.ProductConversion_Volume_Name = data.productConversion_Volume_Name;
                    ProductConversionOld.ProductConversion_VolumeRatio = data.productConversion_VolumeRatio ?? 0;


                    ProductConversionOld.Product_Index = data.product_Index.GetValueOrDefault();
                    ProductConversionOld.Product_Id = data.product_Id;
                    ProductConversionOld.Product_Name = data.product_Name;
                    ProductConversionOld.Ref_No1 = data.ref_No1;
                    ProductConversionOld.Ref_No2 = data.ref_No2;
                    ProductConversionOld.Ref_No3 = data.ref_No3;
                    ProductConversionOld.Ref_No4 = data.ref_No4;
                    ProductConversionOld.Ref_No5 = data.ref_No5;
                    ProductConversionOld.Remark = data.remark;
                    ProductConversionOld.UDF_1 = data.udf_1;
                    ProductConversionOld.UDF_2 = data.udf_2;
                    ProductConversionOld.UDF_3 = data.udf_3;
                    ProductConversionOld.UDF_4 = data.udf_4;
                    ProductConversionOld.UDF_5 = data.udf_5;
                    ProductConversionOld.SALE_UNIT = Convert.ToInt32(data.sale_UNIT);
                    ProductConversionOld.IN_UNIT = Convert.ToInt32(data.in_UNIT);
                    ProductConversionOld.IsActive = Convert.ToInt32(data.isActive);
                    ProductConversionOld.Update_By = data.create_By;
                    ProductConversionOld.Update_Date = DateTime.Now;

                    if (data.documents != null)
                    {
                        foreach (var d in data.documents)
                        {
                            if (d.index == null || d.index == Guid.Empty)
                            {
                                im_DocumentFile documents = new im_DocumentFile();

                                documents.DocumentFile_Index = Guid.NewGuid(); ;
                                documents.DocumentFile_Name = d.filename;
                                documents.DocumentFile_Path = d.path;
                                documents.DocumentFile_Url = d.urlAttachFile;
                                documents.DocumentFile_Type = d.type;
                                documents.DocumentFile_Status = 0;
                                documents.Create_By = data.create_By;
                                documents.Create_Date = DateTime.Now;
                                documents.Ref_Index = data.productConversion_Index;
                                documents.Ref_No = data.productConversion_Id;
                                db.im_DocumentFile.Add(documents);
                            }
                            else if ((d.index != null || d.index != Guid.Empty) && d.isDelete)
                            {
                                var Documents = db.im_DocumentFile.FirstOrDefault(c => c.DocumentFile_Index == d.index && c.Ref_Index == ProductConversionOld.ProductConversion_Index && c.DocumentFile_Status == 0);
                                Documents.DocumentFile_Status = -1;
                                Documents.Update_By = data.create_By;
                                Documents.Update_Date = DateTime.Now;
                            }
                        }
                    }
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
                    olog.logging("SaveProductConversion", msglog);
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
        public ProductConversionViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_ProductConversion.Where(c => c.ProductConversion_Index == id).FirstOrDefault();

                var result = new ProductConversionViewModel();


                result.productConversion_Index = queryResult.ProductConversion_Index;
                result.productConversion_Id = queryResult.ProductConversion_Id;
                result.productConversion_Name = queryResult.ProductConversion_Name;
                result.productConversion_SecondName = queryResult.ProductConversion_SecondName;
                result.productConversion_Ratio = queryResult.ProductConversion_Ratio;

                result.productConversion_Volume = (queryResult.ProductConversion_Volume ?? 0)/1000000;
                result.productConversion_VolumeRatio = queryResult.ProductConversion_VolumeRatio;
                result.productConversion_Volume_Index = queryResult.ProductConversion_Volume_Index;
                result.productConversion_Volume_Id = queryResult.ProductConversion_Volume_Id;
                result.productConversion_Volume_Name = queryResult.ProductConversion_Volume_Name;

                result.productConversion_Weight = queryResult.ProductConversion_Weight;
                result.productConversion_Weight_Index = queryResult.ProductConversion_Weight_Index;
                result.productConversion_Weight_Id = queryResult.ProductConversion_Weight_Id;
                result.productConversion_Weight_Name = queryResult.ProductConversion_Weight_Name;
                result.productConversion_WeightRatio = queryResult.ProductConversion_WeightRatio;
                result.productConversion_GrsWeight = queryResult.ProductConversion_GrsWeight;
                result.productConversion_GrsWeight_Index = queryResult.ProductConversion_GrsWeight_Index;
                result.productConversion_GrsWeight_Id = queryResult.ProductConversion_GrsWeight_Id;
                result.productConversion_GrsWeight_Name = queryResult.ProductConversion_GrsWeight_Name;
                result.productConversion_GrsWeightRatio = queryResult.ProductConversion_GrsWeightRatio;

                result.productConversion_Width = queryResult.ProductConversion_Width;
                result.productConversion_Length = queryResult.ProductConversion_Length;
                result.productConversion_Height = queryResult.ProductConversion_Height;

                result.product_Index = queryResult.Product_Index;
                result.product_Id = queryResult.Product_Id;
                result.product_Name = queryResult.Product_Name;
                result.key = queryResult.Product_Id + " - " + queryResult.Product_Name;
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
                result.sale_UNIT = queryResult.SALE_UNIT;
                result.in_UNIT = queryResult.IN_UNIT;
                result.isActive = queryResult.IsActive;

                var Listdocuments = new List<document>();
                var DocumentFile = db.im_DocumentFile.Where(c => c.Ref_Index == queryResult.ProductConversion_Index && c.DocumentFile_Status == 0).ToList();
                foreach (var d in DocumentFile)
                {
                    var documents = new document();
                    documents.index = d.DocumentFile_Index;
                    documents.filename = d.DocumentFile_Name;
                    documents.path = d.DocumentFile_Path;
                    documents.urlAttachFile = d.DocumentFile_Url;
                    Listdocuments.Add(documents);
                }
                result.documents = Listdocuments;
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getDelete
        public Boolean getDelete(ProductConversionViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Owner = db.MS_ProductConversion.Find(data.productConversion_Index);

                if (Owner != null)
                {
                    Owner.IsActive = 0;
                    Owner.IsDelete = 1;


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
                        olog.logging("DeleteProductConversion" +
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

        #region filterV2
        public actionResultProductConversionViewModel filterV2(SearchProductConversionViewModel data)
        {
            try
            {
                var query = db.MS_ProductConversion.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (data.product_Index != null)
                {
                    query = query.Where(c => c.Product_Index == data.product_Index);
                }

                if (!string.IsNullOrEmpty(data.value1))
                {
                    query = query.Where(c => c.ProductConversion_Id.Contains(data.value1)
                                         || c.ProductConversion_Name.Contains(data.value1));
                }

                var Item = new List<MS_ProductConversion>();
                var TotalRow = new List<MS_ProductConversion>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.ProductConversion_Id).ToList();

                var result = new List<SearchProductConversionViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchProductConversionViewModel();

                    resultItem.productConversion_Index = item.ProductConversion_Index;
                    resultItem.productConversion_Id = item.ProductConversion_Id;
                    resultItem.productConversion_Name = item.ProductConversion_Name;
                    resultItem.productConversion_SecondName = item.ProductConversion_SecondName;
                    resultItem.productConversion_SecondName = item.ProductConversion_SecondName;
                    resultItem.productConversion_Ratio = item.ProductConversion_Ratio;

                    resultItem.productConversion_Volume = item.ProductConversion_Volume;
                    resultItem.productConversion_VolumeRatio = item.ProductConversion_VolumeRatio;
                    resultItem.productConversion_Volume_Index = item.ProductConversion_Volume_Index;
                    resultItem.productConversion_Volume_Id = item.ProductConversion_Volume_Id;
                    resultItem.productConversion_Volume_Name = item.ProductConversion_Volume_Name;

                    resultItem.productConversion_Weight = item.ProductConversion_Weight;
                    resultItem.productConversion_Weight_Index = item.ProductConversion_Weight_Index;
                    resultItem.productConversion_Weight_Id = item.ProductConversion_Weight_Id;
                    resultItem.productConversion_Weight_Name = item.ProductConversion_Weight_Name;
                    resultItem.productConversion_WeightRatio = item.ProductConversion_WeightRatio;
                    resultItem.productConversion_GrsWeight = item.ProductConversion_GrsWeight;
                    resultItem.productConversion_GrsWeight_Index = item.ProductConversion_GrsWeight_Index;
                    resultItem.productConversion_GrsWeight_Id = item.ProductConversion_GrsWeight_Id;
                    resultItem.productConversion_GrsWeight_Name = item.ProductConversion_GrsWeight_Name;
                    resultItem.productConversion_GrsWeightRatio = item.ProductConversion_GrsWeightRatio;

                    resultItem.productConversion_Width = item.ProductConversion_Width;
                    resultItem.productConversion_Length = item.ProductConversion_Length;
                    resultItem.productConversion_Height = item.ProductConversion_Height;

                    resultItem.product_Index = item.Product_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
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
                    resultItem.sale_UNIT = item.SALE_UNIT;
                    resultItem.in_UNIT = item.IN_UNIT;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultProductConversionViewModel = new actionResultProductConversionViewModel();
                actionResultProductConversionViewModel.itemsProductConversion = result.ToList();
                actionResultProductConversionViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultProductConversionViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region dropdown
        public List<ProductConversionViewModel> productConversionDropdown(ProductConversionViewModel data)
        {
            try
            {
                var result = new List<ProductConversionViewModel>();

                var query = db.MS_ProductConversion.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.ProductConversion_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new ProductConversionViewModel();
                    resultItem.productConversion_Index = item.ProductConversion_Index;
                    resultItem.productConversion_Id = item.ProductConversion_Id;
                    resultItem.productConversion_Name = item.ProductConversion_Name;
                    resultItem.productConversion_SecondName = item.ProductConversion_SecondName;
                    resultItem.productConversion_Volume = item.ProductConversion_Volume;
                    resultItem.productConversion_SecondName = item.ProductConversion_SecondName;
                    resultItem.productConversion_Ratio = item.ProductConversion_Ratio;
                    resultItem.productConversion_VolumeRatio = item.ProductConversion_VolumeRatio;
                    resultItem.productConversion_Weight = item.ProductConversion_Weight;
                    resultItem.productConversion_Width = item.ProductConversion_Width;
                    resultItem.productConversion_Length = item.ProductConversion_Length;
                    resultItem.productConversion_Height = item.ProductConversion_Height;
                    resultItem.product_Index = item.Product_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
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
                    resultItem.sale_UNIT = item.SALE_UNIT;
                    resultItem.in_UNIT = item.IN_UNIT;

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


        #region export
        public actionResultProductConversionViewModel export(SearchProductConversionViewModel data)
        {
            try
            {
                var query = db.View_ProductConversion.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                if (data.changeSet != "1")
                {
                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductConversion_Id.Contains(data.key)
                                             || c.ProductConversion_Name.Contains(data.key)
                                             || c.Product_Id.Contains(data.key)
                                             || c.Product_Name.Contains(data.key));
                    }

                    if (!string.IsNullOrEmpty(data.product_Id))
                    {
                        query = query.Where(c => c.Product_Id.Contains(data.product_Id));
                    }

                    if (!string.IsNullOrEmpty(data.create_date) && !string.IsNullOrEmpty(data.create_date_to))
                    {
                        var dateStart = data.create_date.toBetweenDate();
                        var dateEnd = data.create_date_to.toBetweenDate();
                        query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductConversion_Id.Contains(data.key)
                                             || c.ProductConversion_Name.Contains(data.key)
                                             || c.Product_Id.Contains(data.key)
                                             || c.Product_Name.Contains(data.key));
                    }

                    if (!string.IsNullOrEmpty(data.product_Id))
                    {
                        query = query.Where(c => c.Product_Id.Contains(data.product_Id));
                    }
                }

                var Item = new List<View_ProductConversion>();
                var TotalRow = new List<View_ProductConversion>();

                TotalRow = query.ToList();

                Item = query.OrderBy(o => o.ProductConversion_Id).ToList();

                var result = new List<SearchProductConversionViewModel>();
                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new SearchProductConversionViewModel();
                    resultItem.rowNum = num + 1;
                    resultItem.productConversion_Index = item.ProductConversion_Index;
                    resultItem.productConversion_Id = item.ProductConversion_Id;
                    resultItem.productConversion_Name = item.ProductConversion_Name;
                    resultItem.productConversion_SecondName = item.ProductConversion_SecondName == null ? "" : item.ProductConversion_SecondName;
                    resultItem.productConversion_Volume = item.ProductConversion_Volume;
                    resultItem.productConversion_Ratio = item.ProductConversion_Ratio;
                    resultItem.productConversion_VolumeRatio = item.ProductConversion_VolumeRatio;
                    resultItem.productConversion_Weight = item.ProductConversion_Weight;
                    resultItem.productConversion_Width = item.ProductConversion_Width;
                    resultItem.productConversion_Length = item.ProductConversion_Length;
                    resultItem.productConversion_Height = item.ProductConversion_Height;
                    resultItem.product_Index = item.Product_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.businessUnit_Index = item.BusinessUnit_Index;
                    resultItem.businessUnit_Id = item.BusinessUnit_Id == null ? "" : item.BusinessUnit_Id;
                    resultItem.businessUnit_Name = item.BusinessUnit_Name == null ? "" : item.BusinessUnit_Name;
                    resultItem.ref_No1 = item.Ref_No1 == null ? "" : item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2 == null ? "" : item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3 == null ? "" : item.Ref_No3;
                    resultItem.ref_No4 = item.Ref_No4 == null ? "" : item.Ref_No4;
                    resultItem.ref_No5 = item.Ref_No5 == null ? "" : item.Ref_No5;
                    resultItem.remark = item.Remark == null ? "" : item.Remark;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.udf_5 = item.UDF_5;
                    //resultItem.sale_UNIT = item.SALE_UNIT;
                    //resultItem.in_UNIT = item.IN_UNIT;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                    num++;
                }

                var count = TotalRow.Count;

                var actionResultProductConversionViewModel = new actionResultProductConversionViewModel();
                actionResultProductConversionViewModel.itemsProductConversion = result.ToList();

                return actionResultProductConversionViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        //#region SaveProductConversionList

        //public String SaveProductConversionList(ProductConversionViewModel data)
        //{
        //    String State = "Start";
        //    String msglog = "";
        //    var olog = new logtxt();

        //    try
        //    {
        //        foreach (var item in data.listProductConversionViewModel)
        //        {
        //            MS_ProductConversion Model = new MS_ProductConversion();

        //            Model.ProductConversion_Index = Guid.NewGuid();

        //            data.productConversion_Id = "ProductConversion_Id".genAutonumber();
        //            int i = 1;
        //            while (i > 0)
        //            {
        //                var query = db.MS_SoldToShipTo.FirstOrDefault(c => c.SoldToShipTo_Id == data.soldToShipTo_Id && c.IsActive == 1);
        //                if (query != null)
        //                {
        //                    if (query.SoldToShipTo_Id == data.soldToShipTo_Id)
        //                    {
        //                        data.soldToShipTo_Id = "SoldToShipTo_Id".genAutonumber();
        //                    }
        //                }
        //                else
        //                {
        //                    break;
        //                }
        //            }

        //            Model.SoldToShipTo_Id = data.soldToShipTo_Id;
        //            Model.SoldTo_Index = data.soldTo_Index;
        //            Model.ShipTo_Index = item.shipTo_Index;
        //            Model.IsActive = 1;
        //            Model.IsDelete = 0;
        //            Model.IsSystem = 0;
        //            Model.Status_Id = 0;
        //            Model.Create_By = data.create_By;
        //            Model.Create_Date = DateTime.Now;

        //            db.MS_SoldToShipTo.Add(Model);
        //        }

        //        var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
        //        try
        //        {
        //            db.SaveChanges();
        //            transactionx.Commit();
        //        }

        //        catch (Exception exy)
        //        {
        //            msglog = State + " ex Rollback " + exy.Message.ToString();
        //            olog.logging("SaveSoldToShipTo", msglog);
        //            transactionx.Rollback();

        //            throw exy;
        //        }

        //        return "Done";

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //#endregion

        //public actionResultProductConversionViewModel Filter(ProductConversionViewModel data)
        //{
        //    try
        //    {
        //        var res = new actionResultProductConversionViewModel();
        //        var count = 0;
        //        var pwhereFilter = "";
        //        using (var context = new MasterDataDbContext())
        //        {
        //            if (data.ProductConversionId != "" && data.ProductConversionId != null)
        //                pwhereFilter = " And ms_ProductConversion.ProductConversion_Id like N'%" + data.ProductConversionId + "%'";

        //            if (data.ProductConversionName != "" && data.ProductConversionName != null)
        //                pwhereFilter += " And ms_ProductConversion.ProductConversion_Name like N'%" + data.ProductConversionName + "%'";



        //            if (data.ProductName != "" && data.ProductName != null)
        //            {
        //                string SqlProduct = " and Convert(Nvarchar(50),Product_Name) like N'%" + data.ProductName + "%'";
        //                var strwhereProduct = new SqlParameter("@strwhere", SqlProduct);
        //                var queryProduct = context.MS_Product.FromSql("sp_GetProduct @strwhere", strwhereProduct).FirstOrDefault();
        //                if (queryProduct != null)
        //                {
        //                    pwhereFilter += " And ms_ProductConversion.ProductConversion_Index like N'%" + queryProduct.ProductConversion_Index + "%'";
        //                }
        //                else
        //                {
        //                    pwhereFilter += " And ms_ProductConversion.ProductConversion_Name ='00000000000000'";
        //                }


        //            }





        //            var strwheres = new SqlParameter("@strwhere", pwhereFilter);
        //            var PageNumber = new SqlParameter("@PageNumber", 1);
        //            var RowspPage = new SqlParameter("@RowspPage", 10000);

        //            count = context.MS_ProductConversionPaging.FromSql("sp_GetProductConversionPaging @strwhere , @PageNumber , @RowspPage ", strwheres, PageNumber, RowspPage).Count();

        //            var strwhere1 = new SqlParameter("@strwhere", pwhereFilter);
        //            var PageNumber1 = new SqlParameter("@PageNumber", data.CurrentPage);
        //            var RowspPage1 = new SqlParameter("@RowspPage", data.PerPage);
        //            var queryResultTotal = context.MS_ProductConversionPaging.FromSql("sp_GetProductConversionPaging @strwhere , @PageNumber , @RowspPage ", strwhere1, PageNumber1, RowspPage1).ToList();

        //            //var queryResult = context.MS_ProductConversion.FromSql("sp_GetProductConversion").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();
        //            //var queryResult = data.PerPage == 0 
        //            //    ? context.MS_ProductConversion.FromSql("sp_GetProductConversion").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList() 
        //            //    : context.MS_ProductConversion.FromSql("sp_GetProductConversion").Where(c => c.IsActive == 1 && c.IsDelete == 0).Skip((data.CurrentPage - 1) * data.PerPage).Take(data.PerPage).ToList();

        //            var result = new List<ProductConversionViewModel>();
        //            foreach (var item in queryResultTotal)
        //            {
        //                var resultItem = new ProductConversionViewModel();

        //                resultItem.ProductConversionIndex = item.ProductConversion_Index;

        //                //if (item.Product_Index != null)
        //                //{
        //                //    var itemList = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.Product_Index == item.Product_Index).FirstOrDefault();
        //                //    if (itemList != null)
        //                //    {

        //                //    }

        //                //}
        //                resultItem.ProductIndex = item.Product_Index;
        //                resultItem.ProductName = item.product_name;
        //                resultItem.ProductConversionId = item.ProductConversion_Id;
        //                resultItem.ProductConversionName = item.ProductConversion_Name;
        //                resultItem.ProductConversionRatio = item.ProductConversion_Ratio;
        //                resultItem.ProductConversionWeight = item.ProductConversion_Weight;
        //                resultItem.ProductConversionWidth = item.ProductConversion_Width;
        //                resultItem.ProductConversionLength = item.ProductConversion_Length;
        //                resultItem.ProductConversionHeight = item.ProductConversion_Height;
        //                resultItem.ProductConversionVolumeRatio = item.ProductConversion_VolumeRatio;
        //                resultItem.ProductConversionVolume = item.ProductConversion_Volume;
        //                resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                resultItem.CreateBy = item.Create_By;
        //                resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                resultItem.UpdateBy = item.Update_By;
        //                resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                resultItem.CancelBy = item.Cancel_By;

        //                result.Add(resultItem);
        //            }

        //            res.items = result;
        //            res.pagination = new Pagination { PerPage = data.PerPage, NumPerPage = data.PerPage, CurrentPage = data.CurrentPage, TotalRow = count };

        //            return res;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public String SaveChanges(ProductConversionViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            if (data.ProductConversionIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.ProductConversionIndex = Guid.NewGuid();
        //            }
        //            int isactive = 1;
        //            int isdelete = 0;
        //            int isSystem = 0;
        //            int statusId = 0;

        //            var Product_Index = new SqlParameter("Product_Index", System.Data.SqlDbType.UniqueIdentifier);

        //            if (data.ProductIndex != null)
        //            {
        //                Product_Index.SqlValue = data.ProductIndex;
        //            }
        //            else
        //            {
        //                Product_Index.SqlValue = DBNull.Value;
        //            }
        //            if (data.ProductConversionId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "ProductConversionID");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.ProductConversionId = resultParameter.Value.ToString();
        //            }

        //            var ProductConversion_Index = new SqlParameter("ProductConversion_Index", data.ProductConversionIndex);
        //            var ProductConversion_Id = new SqlParameter("ProductConversion_Id", data.ProductConversionId);
        //            var ProductConversion_Name = new SqlParameter("ProductConversion_Name", data.ProductConversionName);
        //            var ProductConversion_Ratio = new SqlParameter("ProductConversion_Ratio", data.ProductConversionRatio);
        //            var ProductConversion_Weight = new SqlParameter("ProductConversion_Weight", data.ProductConversionWeight);
        //            var ProductConversion_Width = new SqlParameter("ProductConversion_Width", data.ProductConversionWidth);
        //            var ProductConversion_Length = new SqlParameter("ProductConversion_Length", data.ProductConversionLength);
        //            var ProductConversion_Height = new SqlParameter("ProductConversion_Height", data.ProductConversionHeight);
        //            var ProductConversion_VolumeRatio = new SqlParameter("ProductConversion_VolumeRatio", data.ProductConversionVolumeRatio);
        //            var ProductConversion_Volume = new SqlParameter("ProductConversion_Volume", data.ProductConversionVolume);
        //            var IsActive = new SqlParameter("IsActive", isactive);
        //            var IsDelete = new SqlParameter("IsDelete", isdelete);
        //            var IsSystem = new SqlParameter("IsSystem", isSystem);
        //            var Status_Id = new SqlParameter("Status_Id", statusId);
        //            var Create_By = new SqlParameter("Create_By", "");
        //            var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
        //            var Update_By = new SqlParameter("Update_By", "");
        //            var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
        //            var Cancel_By = new SqlParameter("Cancel_By", "");
        //            var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_ProductConversion  @ProductConversion_Index,@Product_Index,@ProductConversion_Id,@ProductConversion_Name,@ProductConversion_Ratio,@ProductConversion_Weight,@ProductConversion_Width,@ProductConversion_Length,@ProductConversion_Height,@ProductConversion_VolumeRatio,@ProductConversion_Volume,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", ProductConversion_Index, Product_Index, ProductConversion_Id, ProductConversion_Name, ProductConversion_Ratio, ProductConversion_Weight, ProductConversion_Width, ProductConversion_Length, ProductConversion_Height, ProductConversion_VolumeRatio, ProductConversion_Volume, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<ProductConversionViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var strwhere = new SqlParameter("@strwhere", " and ProductConversion_Index = '" + id + "'");
        //            var queryResult = context.MS_ProductConversion.FromSql("sp_GetProductConversion @strwhere",strwhere).ToList();

        //            var result = new List<ProductConversionViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ProductConversionViewModel();
        //                resultItem.ProductConversionIndex = item.ProductConversion_Index;
        //                if (item.Product_Index != null)
        //                {
        //                    var itemList = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.Product_Index == item.Product_Index).FirstOrDefault();
        //                    if (itemList != null)
        //                    {
        //                        resultItem.ProductIndex = itemList.Product_Index;
        //                        resultItem.ProductName = itemList.Product_Name;
        //                    }

        //                }
        //                resultItem.ProductConversionId = item.ProductConversion_Id;
        //                resultItem.ProductConversionName = item.ProductConversion_Name;
        //                resultItem.ProductConversionRatio = item.ProductConversion_Ratio;
        //                resultItem.ProductConversionWeight = item.ProductConversion_Weight;
        //                resultItem.ProductConversionWidth = item.ProductConversion_Width;
        //                resultItem.ProductConversionLength = item.ProductConversion_Length;
        //                resultItem.ProductConversionHeight = item.ProductConversion_Height;
        //                resultItem.ProductConversionVolumeRatio = item.ProductConversion_VolumeRatio;
        //                resultItem.ProductConversionVolume = item.ProductConversion_Volume;
        //                resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                resultItem.CreateBy = item.Create_By;
        //                resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                resultItem.UpdateBy = item.Update_By;
        //                resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                resultItem.CancelBy = item.Cancel_By;
        //                result.Add(resultItem);
        //            }

        //            if (queryResult.Count == 0)
        //            {
        //                //var findItem = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.Product_Index == id).FirstOrDefault();
        //                var strwhere1 = new SqlParameter("@strwhere", " and Product_Index = '" + id + "'"); 
        //                var itemList = context.MS_ProductConversion.FromSql("sp_GetProductConversion @strwhere",strwhere1).ToList();
        //                foreach (var item in itemList)
        //                {
        //                    var resultItem = new ProductConversionViewModel();
        //                    resultItem.ProductConversionIndex = item.ProductConversion_Index;
        //                    resultItem.ProductConversionId = item.ProductConversion_Id;
        //                    resultItem.ProductConversionName = item.ProductConversion_Name;
        //                    resultItem.ProductConversionRatio = item.ProductConversion_Ratio;
        //                    resultItem.ProductConversionWeight = item.ProductConversion_Weight;
        //                    resultItem.ProductConversionWidth = item.ProductConversion_Width;
        //                    resultItem.ProductConversionLength = item.ProductConversion_Length;
        //                    resultItem.ProductConversionHeight = item.ProductConversion_Height;
        //                    resultItem.ProductConversionVolumeRatio = item.ProductConversion_VolumeRatio;
        //                    resultItem.ProductConversionVolume = item.ProductConversion_Volume;
        //                    resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                    resultItem.CreateBy = item.Create_By;
        //                    resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                    resultItem.UpdateBy = item.Update_By;
        //                    resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                    resultItem.CancelBy = item.Cancel_By;
        //                    result.Add(resultItem);
        //                }
        //            }


        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<ProductConversionViewModel> getItem(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {

        //            var itemList = context.MS_ProductConversion.FromSql("sp_GetProductConversion").Where(c => c.Product_Index == id).ToList();
        //            var result = new List<ProductConversionViewModel>();
        //            if (itemList != null)
        //            {
        //                foreach (var item in itemList)
        //                {
        //                    var resultItem = new ProductConversionViewModel();                            
        //                    if (item.ProductConversion_Index != null)
        //                    {
        //                        if (item.Product_Index != null)
        //                        {
        //                            var dataList = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.Product_Index == item.Product_Index).FirstOrDefault();
        //                            if (dataList != null)
        //                            {
        //                                resultItem.ProductIndex = dataList.Product_Index;
        //                                resultItem.ProductName = dataList.Product_Name;
        //                            }

        //                        }
        //                        resultItem.ProductConversionIndex = item.ProductConversion_Index;
        //                        resultItem.ProductConversionId = item.ProductConversion_Id;
        //                        resultItem.ProductConversionName = item.ProductConversion_Name;
        //                        resultItem.ProductConversionRatio = item.ProductConversion_Ratio;
        //                        resultItem.ProductConversionWeight = item.ProductConversion_Weight;
        //                        resultItem.ProductConversionWidth = item.ProductConversion_Width;
        //                        resultItem.ProductConversionLength = item.ProductConversion_Length;
        //                        resultItem.ProductConversionHeight = item.ProductConversion_Height;
        //                        resultItem.ProductConversionVolumeRatio = item.ProductConversion_VolumeRatio;
        //                        resultItem.ProductConversionVolume = item.ProductConversion_Volume;
        //                        resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                        resultItem.CreateBy = item.Create_By;
        //                        resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                        resultItem.UpdateBy = item.Update_By;
        //                        resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                        resultItem.CancelBy = item.Cancel_By;
        //                        result.Add(resultItem);


        //                    }

        //                }
        //            }


        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<ProductConversionViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ProductConversion.FromSql("sp_GetProductConversion").Where(c => c.ProductConversion_Index == id).ToList();

        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<ProductConversionViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var ProductConversion_Index = new SqlParameter("ProductConversion_Index", item.ProductConversion_Index);
        //                var Product_Index = new SqlParameter("Product_Index", item.Product_Index);
        //                var ProductConversion_Id = new SqlParameter("ProductConversion_Id", item.ProductConversion_Id);
        //                var ProductConversion_Name = new SqlParameter("ProductConversion_Name", item.ProductConversion_Name);
        //                var ProductConversion_Ratio = new SqlParameter("ProductConversion_Ratio", item.ProductConversion_Ratio);
        //                var ProductConversion_Weight = new SqlParameter("ProductConversion_Weight", item.ProductConversion_Weight);
        //                var ProductConversion_Width = new SqlParameter("ProductConversion_Width", item.ProductConversion_Width);
        //                var ProductConversion_Length = new SqlParameter("ProductConversion_Length", item.ProductConversion_Length);
        //                var ProductConversion_Height = new SqlParameter("ProductConversion_Height", item.ProductConversion_Height);
        //                var ProductConversion_VolumeRatio = new SqlParameter("ProductConversion_VolumeRatio", item.ProductConversion_VolumeRatio);
        //                var ProductConversion_Volume = new SqlParameter("ProductConversion_Volume", item.ProductConversion_Volume);
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
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_ProductConversion  @ProductConversion_Index,@Product_Index,@ProductConversion_Id,@ProductConversion_Name,@ProductConversion_Ratio,@ProductConversion_Weight,@ProductConversion_Width,@ProductConversion_Length,@ProductConversion_Height,@ProductConversion_VolumeRatio,@ProductConversion_Volume,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", ProductConversion_Index, Product_Index, ProductConversion_Id, ProductConversion_Name, ProductConversion_Ratio, ProductConversion_Weight, ProductConversion_Width, ProductConversion_Length, ProductConversion_Height, ProductConversion_VolumeRatio, ProductConversion_Volume, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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
        //public actionResultProductConversionViewModel search(ProductConversionViewModel data)
        //{
        //    try
        //    {
        //        var res = new actionResultProductConversionViewModel();
        //        using (var context = new MasterDataDbContext())
        //        {
        //            if (data.ProductIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                string pwhereFilter = "";
        //                string pwhereLike = "";
        //                var result = new List<ProductConversionViewModel>();
        //                var queryResult = context.MS_ProductConversion.FromSql("sp_GetProductConversion").Where(c => c.IsActive == 1 && c.IsDelete == 0)
        //                                                .ToList();


        //                if (data.ProductConversionId != "" && data.ProductConversionId != null)
        //                {
        //                    pwhereFilter = " And ProductConversion_Id like N'%" + data.ProductConversionId + "%'";
        //                }
        //                else
        //                {
        //                    pwhereFilter = "";
        //                }

        //                if (data.ProductConversionName != "" && data.ProductConversionName != null)
        //                {
        //                    pwhereFilter += " And ProductConversion_Name like N'%" + data.ProductConversionName + "%'";
        //                }
        //                else
        //                {
        //                    pwhereFilter += "";
        //                }


        //                if (data.ProductConversionId != "" && data.ProductConversionId != null)
        //                {
        //                    pwhereFilter += " And isActive = '" + 1 + "'";
        //                    pwhereFilter += " And isDelete = '" + 0 + "'";
        //                    var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                    var query = context.MS_ProductConversion.FromSql("sp_GetProductConversion @strwhere ", strwhere).ToList();
        //                    var perpages = data.PerPage == 0 ? query.ToList() : query.Skip((data.CurrentPage - 1) * data.PerPage).Take(data.PerPage).ToList();
        //                    foreach (var item in query)
        //                    {
        //                        var resultItem = new ProductConversionViewModel();

        //                        resultItem.ProductConversionIndex = item.ProductConversion_Index;

        //                        if (item.Product_Index != null)
        //                        {
        //                            var itemList = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.Product_Index == item.Product_Index).FirstOrDefault();
        //                            if (itemList != null)
        //                            {
        //                                resultItem.ProductIndex = itemList.Product_Index;
        //                                resultItem.ProductName = itemList.Product_Name;
        //                            }

        //                        }
        //                        resultItem.ProductConversionId = item.ProductConversion_Id;
        //                        resultItem.ProductConversionName = item.ProductConversion_Name;
        //                        resultItem.ProductConversionRatio = item.ProductConversion_Ratio;
        //                        resultItem.ProductConversionWeight = item.ProductConversion_Weight;
        //                        resultItem.ProductConversionWidth = item.ProductConversion_Width;
        //                        resultItem.ProductConversionLength = item.ProductConversion_Length;
        //                        resultItem.ProductConversionHeight = item.ProductConversion_Height;
        //                        resultItem.ProductConversionVolumeRatio = item.ProductConversion_VolumeRatio;
        //                        resultItem.ProductConversionVolume = item.ProductConversion_Volume;
        //                        resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                        resultItem.CreateBy = item.Create_By;
        //                        resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                        resultItem.UpdateBy = item.Update_By;
        //                        resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                        resultItem.CancelBy = item.Cancel_By;

        //                        result.Add(resultItem);
        //                    }
        //                }
        //                else if (data.ProductConversionName != "" && data.ProductConversionName != null)
        //                {
        //                    pwhereFilter += " And isActive = '" + 1 + "'";
        //                    pwhereFilter += " And isDelete = '" + 0 + "'";
        //                    var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                    var query = context.MS_ProductConversion.FromSql("sp_GetProductConversion @strwhere ", strwhere).ToList();
        //                    foreach (var item in query)
        //                    {
        //                        var resultItem = new ProductConversionViewModel();

        //                        resultItem.ProductConversionIndex = item.ProductConversion_Index;

        //                        if (item.Product_Index != null)
        //                        {
        //                            var itemList = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.Product_Index == item.Product_Index).FirstOrDefault();
        //                            if (itemList != null)
        //                            {
        //                                resultItem.ProductIndex = itemList.Product_Index;
        //                                resultItem.ProductName = itemList.Product_Name;
        //                            }

        //                        }
        //                        resultItem.ProductConversionId = item.ProductConversion_Id;
        //                        resultItem.ProductConversionName = item.ProductConversion_Name;
        //                        resultItem.ProductConversionRatio = item.ProductConversion_Ratio;
        //                        resultItem.ProductConversionWeight = item.ProductConversion_Weight;
        //                        resultItem.ProductConversionWidth = item.ProductConversion_Width;
        //                        resultItem.ProductConversionLength = item.ProductConversion_Length;
        //                        resultItem.ProductConversionHeight = item.ProductConversion_Height;
        //                        resultItem.ProductConversionVolumeRatio = item.ProductConversion_VolumeRatio;
        //                        resultItem.ProductConversionVolume = item.ProductConversion_Volume;
        //                        resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                        resultItem.CreateBy = item.Create_By;
        //                        resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                        resultItem.UpdateBy = item.Update_By;
        //                        resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                        resultItem.CancelBy = item.Cancel_By;

        //                        result.Add(resultItem);
        //                    }
        //                }
        //                else if (data.ProductName != "" && data.ProductName != null)
        //                {
        //                    pwhereLike += " And isActive = '" + 1 + "'";
        //                    pwhereLike += " And isDelete = '" + 0 + "'";
        //                    pwhereLike = " And Product_Name like N'%" + data.ProductName + "%'";
        //                    var pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                    var dataList = context.MS_Product.FromSql("sp_GetProduct @strwhere ", pstrwhere1).ToList();
        //                    foreach (var item in queryResult)
        //                    {
        //                        var resultItem = new ProductConversionViewModel();
        //                        foreach (var ItemList in dataList)
        //                        {
        //                            if (item.Product_Index == ItemList.Product_Index)
        //                            {
        //                                resultItem.ProductConversionIndex = item.ProductConversion_Index;

        //                                if (item.Product_Index != null)
        //                                {
        //                                    var itemList = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.Product_Index == item.Product_Index).FirstOrDefault();
        //                                    if (itemList != null)
        //                                    {
        //                                        resultItem.ProductIndex = itemList.Product_Index;
        //                                        resultItem.ProductName = itemList.Product_Name;
        //                                    }

        //                                }
        //                                resultItem.ProductConversionId = item.ProductConversion_Id;
        //                                resultItem.ProductConversionName = item.ProductConversion_Name;
        //                                resultItem.ProductConversionRatio = item.ProductConversion_Ratio;
        //                                resultItem.ProductConversionWeight = item.ProductConversion_Weight;
        //                                resultItem.ProductConversionWidth = item.ProductConversion_Width;
        //                                resultItem.ProductConversionLength = item.ProductConversion_Length;
        //                                resultItem.ProductConversionHeight = item.ProductConversion_Height;
        //                                resultItem.ProductConversionVolumeRatio = item.ProductConversion_VolumeRatio;
        //                                resultItem.ProductConversionVolume = item.ProductConversion_Volume;
        //                                resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                                resultItem.CreateBy = item.Create_By;
        //                                resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                                resultItem.UpdateBy = item.Update_By;
        //                                resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                                resultItem.CancelBy = item.Cancel_By;

        //                                result.Add(resultItem);
        //                            }

        //                        }

        //                    }
        //                }
        //                else
        //                {
        //                    res = this.Filter(data);
        //                }

        //                //if (data.ProductConversionId == "" && data.ProductConversionName == "" && data.ProductName == "")
        //                //{

        //                //}

        //                return res;
        //            }
        //            else
        //             {
        //                string pwhereFilter = "";
        //                string pwhereLike = "";
        //                var result = new List<ProductConversionViewModel>();
        //                var queryResult = context.MS_ProductConversion.FromSql("sp_GetProductConversion").Where(c => c.Product_Index == data.ProductIndex && c.IsActive == 1 && c.IsDelete == 0)
        //                                                .ToList();
        //                if (data.ProductConversionId != "" && data.ProductConversionId != null)
        //                {
        //                    pwhereFilter = " And ProductConversion_Id like N'%" + data.ProductConversionId + "%'";
        //                }
        //                else
        //                {
        //                    pwhereFilter = "";
        //                }

        //                if (data.ProductConversionName != "" && data.ProductConversionName != null)
        //                {
        //                    pwhereFilter += " And ProductConversion_Name like N'%" + data.ProductConversionName + "%'";
        //                }
        //                else
        //                {
        //                    pwhereFilter += "";
        //                }


        //                if (data.ProductConversionId != "" && data.ProductConversionId != null)
        //                {
        //                    pwhereFilter += " And isActive = '" + 1 + "'";
        //                    pwhereFilter += " And isDelete = '" + 0 + "'";
        //                    var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                    var query = context.MS_ProductConversion.FromSql("sp_GetProductConversion @strwhere ", strwhere).ToList();
        //                    foreach (var item in query)
        //                    {
        //                        var resultItem = new ProductConversionViewModel();

        //                        resultItem.ProductConversionIndex = item.ProductConversion_Index;

        //                        if (item.Product_Index != null)
        //                        {
        //                            var itemList = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.Product_Index == item.Product_Index).FirstOrDefault();
        //                            if (itemList != null)
        //                            {
        //                                resultItem.ProductIndex = itemList.Product_Index;
        //                                resultItem.ProductName = itemList.Product_Name;
        //                            }

        //                        }
        //                        resultItem.ProductConversionId = item.ProductConversion_Id;
        //                        resultItem.ProductConversionName = item.ProductConversion_Name;
        //                        resultItem.ProductConversionRatio = item.ProductConversion_Ratio;
        //                        resultItem.ProductConversionWeight = item.ProductConversion_Weight;
        //                        resultItem.ProductConversionWidth = item.ProductConversion_Width;
        //                        resultItem.ProductConversionLength = item.ProductConversion_Length;
        //                        resultItem.ProductConversionHeight = item.ProductConversion_Height;
        //                        resultItem.ProductConversionVolumeRatio = item.ProductConversion_VolumeRatio;
        //                        resultItem.ProductConversionVolume = item.ProductConversion_Volume;
        //                        resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                        resultItem.CreateBy = item.Create_By;
        //                        resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                        resultItem.UpdateBy = item.Update_By;
        //                        resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                        resultItem.CancelBy = item.Cancel_By;

        //                        result.Add(resultItem);
        //                    }
        //                }
        //                else if (data.ProductConversionName != "" && data.ProductConversionName != null)
        //                {
        //                    pwhereFilter += " And isActive = '" + 1 + "'";
        //                    pwhereFilter += " And isDelete = '" + 0 + "'";
        //                    var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                    var query = context.MS_ProductConversion.FromSql("sp_GetProductConversion @strwhere ", strwhere).ToList();
        //                    foreach (var item in query)
        //                    {
        //                        var resultItem = new ProductConversionViewModel();

        //                        resultItem.ProductConversionIndex = item.ProductConversion_Index;

        //                        if (item.Product_Index != null)
        //                        {
        //                            var itemList = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.Product_Index == item.Product_Index).FirstOrDefault();
        //                            if (itemList != null)
        //                            {
        //                                resultItem.ProductIndex = itemList.Product_Index;
        //                                resultItem.ProductName = itemList.Product_Name;
        //                            }

        //                        }
        //                        resultItem.ProductConversionId = item.ProductConversion_Id;
        //                        resultItem.ProductConversionName = item.ProductConversion_Name;
        //                        resultItem.ProductConversionRatio = item.ProductConversion_Ratio;
        //                        resultItem.ProductConversionWeight = item.ProductConversion_Weight;
        //                        resultItem.ProductConversionWidth = item.ProductConversion_Width;
        //                        resultItem.ProductConversionLength = item.ProductConversion_Length;
        //                        resultItem.ProductConversionHeight = item.ProductConversion_Height;
        //                        resultItem.ProductConversionVolumeRatio = item.ProductConversion_VolumeRatio;
        //                        resultItem.ProductConversionVolume = item.ProductConversion_Volume;
        //                        resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                        resultItem.CreateBy = item.Create_By;
        //                        resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                        resultItem.UpdateBy = item.Update_By;
        //                        resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                        resultItem.CancelBy = item.Cancel_By;

        //                        result.Add(resultItem);
        //                    }
        //                }
        //                else if (data.ProductName != "" && data.ProductName != null)
        //                {
        //                    pwhereLike += " And isActive = '" + 1 + "'";
        //                    pwhereLike += " And isDelete = '" + 0 + "'";
        //                    pwhereLike = " And Product_Name like N'%" + data.ProductName + "%'";
        //                    var pstrwhere1 = new SqlParameter("@strwhere", pwhereLike);
        //                    var dataList = context.MS_Product.FromSql("sp_GetProduct @strwhere ", pstrwhere1).ToList();
        //                    foreach (var item in queryResult)
        //                    {
        //                        var resultItem = new ProductConversionViewModel();
        //                        foreach (var ItemList in dataList)
        //                        {
        //                            if (item.Product_Index == ItemList.Product_Index)
        //                            {
        //                                resultItem.ProductConversionIndex = item.ProductConversion_Index;

        //                                if (item.Product_Index != null)
        //                                {
        //                                    var itemList = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.Product_Index == item.Product_Index).FirstOrDefault();
        //                                    if (itemList != null)
        //                                    {
        //                                        resultItem.ProductIndex = itemList.Product_Index;
        //                                        resultItem.ProductName = itemList.Product_Name;
        //                                    }

        //                                }
        //                                resultItem.ProductConversionId = item.ProductConversion_Id;
        //                                resultItem.ProductConversionName = item.ProductConversion_Name;
        //                                resultItem.ProductConversionRatio = item.ProductConversion_Ratio;
        //                                resultItem.ProductConversionWeight = item.ProductConversion_Weight;
        //                                resultItem.ProductConversionWidth = item.ProductConversion_Width;
        //                                resultItem.ProductConversionLength = item.ProductConversion_Length;
        //                                resultItem.ProductConversionHeight = item.ProductConversion_Height;
        //                                resultItem.ProductConversionVolumeRatio = item.ProductConversion_VolumeRatio;
        //                                resultItem.ProductConversionVolume = item.ProductConversion_Volume;
        //                                resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                                resultItem.CreateBy = item.Create_By;
        //                                resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                                resultItem.UpdateBy = item.Update_By;
        //                                resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                                resultItem.CancelBy = item.Cancel_By;

        //                                result.Add(resultItem);
        //                            }

        //                        }

        //                    }
        //                }

        //                if (data.ProductConversionId == "" && data.ProductConversionName == "" && data.ProductName == "")
        //                {
        //                    res = this.Filter(data);
        //                }

        //                return res;
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public List<ProductConversionViewModel> FilterPopup()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_ProductConversion.FromSql("sp_GetProductConversion").Where(c =>  c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<ProductConversionViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ProductConversionViewModel();

        //                resultItem.ProductConversionIndex = item.ProductConversion_Index;

        //                if (item.Product_Index != null)
        //                {
        //                    var itemList = context.MS_Product.FromSql("sp_GetProduct").Where(c => c.Product_Index == item.Product_Index).FirstOrDefault();
        //                    if (itemList != null)
        //                    {
        //                        resultItem.ProductIndex = itemList.Product_Index;
        //                        resultItem.ProductName = itemList.Product_Name;
        //                    }

        //                }
        //                resultItem.ProductConversionId = item.ProductConversion_Id;
        //                resultItem.ProductConversionName = item.ProductConversion_Name;
        //                resultItem.ProductConversionRatio = item.ProductConversion_Ratio;
        //                resultItem.ProductConversionWeight = item.ProductConversion_Weight;
        //                resultItem.ProductConversionWidth = item.ProductConversion_Width;
        //                resultItem.ProductConversionLength = item.ProductConversion_Length;
        //                resultItem.ProductConversionHeight = item.ProductConversion_Height;
        //                resultItem.ProductConversionVolumeRatio = item.ProductConversion_VolumeRatio;
        //                resultItem.ProductConversionVolume = item.ProductConversion_Volume;
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
        //public List<ProductConversionViewModel> productConversionPopupSearch(ProductConversionViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            string pstring = "";
        //            pstring += " and Product_Index = '" + data.ProductIndex + "'";
        //            pstring += " and IsActive = 1 and isDelete = 0 ";
        //            var strwhere = new SqlParameter("@strwhere", pstring);
        //            var query = context.MS_ProductConversion.FromSql("sp_GetProductConversion @strwhere",strwhere).AsQueryable();

        //            if (!string.IsNullOrEmpty(data.ProductConversionId))
        //                query = query.Where(c => c.ProductConversion_Id.Contains(data.ProductConversionId));

        //            if (!string.IsNullOrEmpty(data.ProductConversionName))
        //                query = query.Where(c => c.ProductConversion_Name.ToUpper().Contains(data.ProductConversionName.ToUpper()));

        //            var queryResult = query.ToList();

        //            var result = new List<ProductConversionViewModel>();

        //            //var queryResult2 = context.MS_Vendor.FromSql("sp_GetVendor").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            //var queryResult3 = queryResult2.Where(x => queryResult.Select(a => a.Vendor_Index).Contains(x.Vendor_Index));


        //            foreach (var item in queryResult)
        //            {

        //                var resultItem = new ProductConversionViewModel();

        //                resultItem.ProductConversionId = item.ProductConversion_Id;
        //                resultItem.ProductConversionName = item.ProductConversion_Name;
        //                resultItem.ProductConversionRatio = item.ProductConversion_Ratio;
        //                resultItem.ProductConversionWeight = item.ProductConversion_Weight;
        //                resultItem.ProductConversionWidth = item.ProductConversion_Width;
        //                resultItem.ProductConversionLength = item.ProductConversion_Length;
        //                resultItem.ProductConversionHeight = item.ProductConversion_Height;
        //                resultItem.ProductConversionVolumeRatio = item.ProductConversion_VolumeRatio;
        //                resultItem.ProductConversionVolume = item.ProductConversion_Volume;
        //                resultItem.IsActive = item.IsActive;
        //                resultItem.IsDelete = item.IsDelete;
        //                resultItem.IsSystem = item.IsSystem;
        //                resultItem.StatusId = item.Status_Id;
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

        //public List<ProductConversionViewModelDoc> PopupSearch(ProductConversionViewModelDoc data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var query = context.MS_ProductConversion.AsQueryable();



        //            if (!string.IsNullOrEmpty(data.productConversion_Id))
        //            {
        //                query = query.Where(c => c.ProductConversion_Id.Contains(data.productConversion_Id));
        //            }
        //            if (!string.IsNullOrEmpty(data.productConversion_Name))
        //            {
        //                query = query.Where(c => c.ProductConversion_Name.Contains(data.productConversion_Name));
        //            }
        //            if (!string.IsNullOrEmpty(data.product_Index.ToString()))
        //            {
        //                query = query.Where(c => c.Product_Index == data.product_Index);
        //            }

        //            var queryResult = query.Distinct().Take(10).ToList();

        //            var result = new List<ProductConversionViewModelDoc>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ProductConversionViewModelDoc();

        //                resultItem.productConversion_Index = item.ProductConversion_Index;
        //                resultItem.productConversion_Id = item.ProductConversion_Id;
        //                resultItem.productConversion_Name = item.ProductConversion_Name;
        //                resultItem.productconversion_Ratio = item.ProductConversion_Ratio;
        //                resultItem.productconversion_Weight = item.ProductConversion_Weight;
        //                resultItem.productconversion_Width = item.ProductConversion_Width;
        //                resultItem.productconversion_Length = item.ProductConversion_Length;
        //                resultItem.productconversion_Height = item.ProductConversion_Height;
        //                resultItem.productconversion_VolumeRatio = item.ProductConversion_VolumeRatio;


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

        public List<ProductConversionViewModelDoc> productConversionfilter(ProductConversionViewModelDoc data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var result = new List<ProductConversionViewModelDoc>();

                    var query = context.MS_ProductConversion.AsQueryable();
                    query = query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                    if (data.product_Index != new Guid("00000000-0000-0000-0000-000000000000".ToString()) && data.product_Index != null)
                    {
                        query = query.Where(c => c.Product_Index == data.product_Index);
                    }

                    if (!string.IsNullOrEmpty(data.productConversion_Name))
                    {
                        query = query.Where(c => c.ProductConversion_Name == data.productConversion_Name);
                    }

                    if (data.productConversion_Index != new Guid("00000000-0000-0000-0000-000000000000".ToString()) && data.productConversion_Index != null)
                    {
                        query = query.Where(c => c.ProductConversion_Index == data.productConversion_Index);
                    }

                    var queryResult = query.OrderBy(o => o.ProductConversion_Id).Take(10).ToList();

                    foreach (var item in queryResult)
                    {
                        var resultItem = new ProductConversionViewModelDoc();
                        resultItem.productConversion_Index = item.ProductConversion_Index;
                        resultItem.productConversion_Id = item.ProductConversion_Id;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.productconversion_Ratio = item.ProductConversion_Ratio;

                        #region Weight
                        if (item.ProductConversion_Weight == null)
                        {
                            resultItem.productConversion_Weight = 0;
                        }
                        else
                        {
                            resultItem.productConversion_Weight = item.ProductConversion_Weight;
                        }
                        resultItem.productConversion_Weight_Index = item.ProductConversion_Weight_Index;
                        resultItem.productConversion_Weight_Id = item.ProductConversion_Weight_Id;
                        resultItem.productConversion_Weight_Name = item.ProductConversion_Weight_Name;

                        if (item.ProductConversion_WeightRatio == null)
                        {
                            resultItem.productConversion_WeightRatio = 1;
                        }
                        else
                        {
                            resultItem.productConversion_WeightRatio = item.ProductConversion_WeightRatio;
                        }

                        #endregion

                        #region Width

                        if (item.ProductConversion_Width == null)
                        {
                            resultItem.productConversion_Width = 0;
                        }
                        else
                        {
                            resultItem.productConversion_Width = item.ProductConversion_Width;
                        }


                        if (item.ProductConversion_WidthRatio == null)
                        {
                            resultItem.productConversion_WidthRatio = 1;
                        }
                        else
                        {
                            resultItem.productConversion_WidthRatio = item.ProductConversion_WidthRatio;
                        }

                        resultItem.productConversion_Width_Index = item.ProductConversion_Width_Index;
                        resultItem.productConversion_Width_Id = item.ProductConversion_Width_Id;
                        resultItem.productConversion_Width_Name = item.ProductConversion_Width_Name;

                        #endregion

                        #region Volume
                        if (item.ProductConversion_Volume == null)
                        {
                            resultItem.productConversion_Volume = 0;
                        }
                        else
                        {
                            resultItem.productConversion_Volume = item.ProductConversion_Volume;
                        }

                        if (item.ProductConversion_VolumeRatio == 0)
                        {
                            resultItem.productConversion_VolumeRatio = 1;
                        }
                        else
                        {
                            resultItem.productConversion_VolumeRatio = item.ProductConversion_VolumeRatio;
                        }

                        resultItem.productConversion_Volume_Index = item.ProductConversion_Volume_Index;
                        resultItem.productConversion_Volume_Id = item.ProductConversion_Volume_Id;
                        resultItem.productConversion_Volume_Name = item.ProductConversion_Volume_Name;
                        #endregion

                        #region Length
                        if (item.ProductConversion_Length == null)
                        {
                            resultItem.productConversion_Length = 0;
                        }
                        else
                        {
                            resultItem.productConversion_Length = item.ProductConversion_Length;
                        }

                        if (item.ProductConversion_LengthRatio == null)
                        {
                            resultItem.productConversion_LengthRatio = 1;
                        }
                        else
                        {
                            resultItem.productConversion_LengthRatio = item.ProductConversion_LengthRatio;
                        }

                        resultItem.productConversion_Length_Index = item.ProductConversion_Length_Index;
                        resultItem.productConversion_Length_Id = item.ProductConversion_Length_Id;
                        resultItem.productConversion_Length_Name = item.ProductConversion_Length_Name;
                        #endregion

                        #region Height 
                        if (item.ProductConversion_Height == null)
                        {
                            resultItem.productConversion_Height = 0;
                        }
                        else
                        {
                            resultItem.productConversion_Height = item.ProductConversion_Height;
                        }

                        if (item.ProductConversion_HeightRatio == null)
                        {
                            resultItem.productConversion_HeightRatio = 1;
                        }
                        else
                        {
                            resultItem.productConversion_HeightRatio = item.ProductConversion_HeightRatio;
                        }

                        resultItem.productConversion_Height_Index = item.ProductConversion_Height_Index;
                        resultItem.productConversion_Height_Id = item.ProductConversion_Height_Id;
                        resultItem.productConversion_Height_Name = item.ProductConversion_Height_Name;
                        #endregion

                        #region Grs
                        if (item.ProductConversion_GrsWeight == null)
                        {
                            resultItem.productConversion_GrsWeight = 0;
                        }
                        else
                        {
                            resultItem.productConversion_GrsWeight = item.ProductConversion_GrsWeight;
                        }

                        if (item.ProductConversion_GrsWeightRatio == null)
                        {
                            resultItem.productConversion_GrsWeightRatio = 1;
                        }
                        else
                        {
                            resultItem.productConversion_GrsWeightRatio = item.ProductConversion_GrsWeightRatio;
                        }

                        resultItem.productConversion_GrsWeight_Index = item.ProductConversion_GrsWeight_Index;
                        resultItem.productConversion_GrsWeight_Id = item.ProductConversion_GrsWeight_Id;
                        resultItem.productConversion_GrsWeight_Name = item.ProductConversion_GrsWeight_Name;
                        #endregion

                        resultItem.uDF_1 = item.UDF_1;
                        resultItem.uDF_2 = item.UDF_2;
                        resultItem.uDF_3 = item.UDF_3;
                        resultItem.uDF_4 = item.UDF_4;
                        resultItem.uDF_5 = item.UDF_5;
                        resultItem.sale_UNIT = item.SALE_UNIT;
                        resultItem.in_UNIT = item.IN_UNIT;
                        resultItem.ref_No1 = item.Ref_No1;
                        resultItem.ref_No2 = item.Ref_No2;
                        resultItem.ref_No3 = item.Ref_No3;
                        resultItem.ref_No4 = item.Ref_No4;
                        resultItem.ref_No5 = item.Ref_No5;



                        resultItem.remark = item.Remark;


                        result.Add(resultItem);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<SearchProductConversionViewModel> conversion_sale_unit(SearchProductConversionViewModel data)
        {
            try
            {
                var query = db.MS_ProductConversion.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0 );
                query = query.Where(c => c.SALE_UNIT == 1);
                
                var result = new List<SearchProductConversionViewModel>();

                foreach (var item in query)
                {
                    var resultItem = new SearchProductConversionViewModel();
                    resultItem.product_Index = item.Product_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.sale_UNIT = item.SALE_UNIT;
                    resultItem.productConversion_Width = item.ProductConversion_Width;
                    resultItem.productConversion_Length = item.ProductConversion_Length;
                    resultItem.productConversion_Height = item.ProductConversion_Height;
                    resultItem.productConversion_Volume = (item.ProductConversion_Width ?? 0) * (item.ProductConversion_Length ?? 0) * (item.ProductConversion_Height ?? 0);
                    resultItem.productConversion_Name = item.ProductConversion_Name;
                    resultItem.productConversion_Ratio = item.ProductConversion_Ratio;
                    resultItem.productConversion_Index = item.ProductConversion_Index;
                    resultItem.ref_No1 = item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3;
                    resultItem.ref_No4 = item.Ref_No4;
                    resultItem.ref_No5 = item.Ref_No5;

                    result.Add(resultItem);
                }
                
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductConversionViewModelDoc> productConversionfilterV2(ProductConversionViewModelDoc data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var result = new List<ProductConversionViewModelDoc>();

                    var query = context.MS_ProductConversion.AsQueryable();
                    query = query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                    if (data.product_Index != new Guid("00000000-0000-0000-0000-000000000000".ToString()) && data.product_Index != null)
                    {
                        query = query.Where(c => c.Product_Index == data.product_Index);
                    }

                    if (data.productConversion_Index != new Guid("00000000-0000-0000-0000-000000000000".ToString()) && data.productConversion_Index != null)
                    {
                        query = query.Where(c => c.ProductConversion_Index == data.productConversion_Index);
                    }

                    var queryResult = query.OrderBy(o => o.ProductConversion_Id).ToList();

                    foreach (var item in queryResult)
                    {
                        var resultItem = new ProductConversionViewModelDoc();
                        resultItem.productConversion_Index = item.ProductConversion_Index;
                        resultItem.productConversion_Id = item.ProductConversion_Id;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.productconversion_Ratio = item.ProductConversion_Ratio;

                        resultItem.product_Index = item.Product_Index;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;



                        #region Weight
                        if (item.ProductConversion_Weight == null)
                        {
                            resultItem.productConversion_Weight = 0;
                        }
                        else
                        {
                            resultItem.productConversion_Weight = item.ProductConversion_Weight;
                        }
                        resultItem.productConversion_Weight_Index = item.ProductConversion_Weight_Index;
                        resultItem.productConversion_Weight_Id = item.ProductConversion_Weight_Id;
                        resultItem.productConversion_Weight_Name = item.ProductConversion_Weight_Name;

                        if (item.ProductConversion_WeightRatio == null)
                        {
                            resultItem.productConversion_WeightRatio = 1;
                        }
                        else
                        {
                            resultItem.productConversion_WeightRatio = item.ProductConversion_WeightRatio;
                        }

                        #endregion

                        #region Width

                        if (item.ProductConversion_Width == null)
                        {
                            resultItem.productConversion_Width = 0;
                        }
                        else
                        {
                            resultItem.productConversion_Width = item.ProductConversion_Width;
                        }


                        if (item.ProductConversion_WidthRatio == null)
                        {
                            resultItem.productConversion_WidthRatio = 1;
                        }
                        else
                        {
                            resultItem.productConversion_WidthRatio = item.ProductConversion_WidthRatio;
                        }

                        resultItem.productConversion_Width_Index = item.ProductConversion_Width_Index;
                        resultItem.productConversion_Width_Id = item.ProductConversion_Width_Id;
                        resultItem.productConversion_Width_Name = item.ProductConversion_Width_Name;

                        #endregion

                        #region Volume
                        if (item.ProductConversion_Volume == null)
                        {
                            resultItem.productConversion_Volume = 0;
                        }
                        else
                        {
                            resultItem.productConversion_Volume = item.ProductConversion_Volume;
                        }

                        if (item.ProductConversion_VolumeRatio == 0)
                        {
                            resultItem.productConversion_VolumeRatio = 1;
                        }
                        else
                        {
                            resultItem.productConversion_VolumeRatio = item.ProductConversion_VolumeRatio;
                        }

                        resultItem.productConversion_Volume_Index = item.ProductConversion_Volume_Index;
                        resultItem.productConversion_Volume_Id = item.ProductConversion_Volume_Id;
                        resultItem.productConversion_Volume_Name = item.ProductConversion_Volume_Name;
                        #endregion

                        #region Length
                        if (item.ProductConversion_Length == null)
                        {
                            resultItem.productConversion_Length = 0;
                        }
                        else
                        {
                            resultItem.productConversion_Length = item.ProductConversion_Length;
                        }

                        if (item.ProductConversion_LengthRatio == null)
                        {
                            resultItem.productConversion_LengthRatio = 1;
                        }
                        else
                        {
                            resultItem.productConversion_LengthRatio = item.ProductConversion_LengthRatio;
                        }

                        resultItem.productConversion_Length_Index = item.ProductConversion_Length_Index;
                        resultItem.productConversion_Length_Id = item.ProductConversion_Length_Id;
                        resultItem.productConversion_Length_Name = item.ProductConversion_Length_Name;
                        #endregion

                        #region Height 
                        if (item.ProductConversion_Height == null)
                        {
                            resultItem.productConversion_Height = 0;
                        }
                        else
                        {
                            resultItem.productConversion_Height = item.ProductConversion_Height;
                        }

                        if (item.ProductConversion_HeightRatio == null)
                        {
                            resultItem.productConversion_HeightRatio = 1;
                        }
                        else
                        {
                            resultItem.productConversion_HeightRatio = item.ProductConversion_HeightRatio;
                        }

                        resultItem.productConversion_Height_Index = item.ProductConversion_Height_Index;
                        resultItem.productConversion_Height_Id = item.ProductConversion_Height_Id;
                        resultItem.productConversion_Height_Name = item.ProductConversion_Height_Name;
                        #endregion

                        #region Grs
                        if (item.ProductConversion_GrsWeight == null)
                        {
                            resultItem.productConversion_GrsWeight = 0;
                        }
                        else
                        {
                            resultItem.productConversion_GrsWeight = item.ProductConversion_GrsWeight;
                        }

                        if (item.ProductConversion_GrsWeightRatio == null)
                        {
                            resultItem.productConversion_GrsWeightRatio = 1;
                        }
                        else
                        {
                            resultItem.productConversion_GrsWeightRatio = item.ProductConversion_GrsWeightRatio;
                        }

                        resultItem.productConversion_GrsWeight_Index = item.ProductConversion_GrsWeight_Index;
                        resultItem.productConversion_GrsWeight_Id = item.ProductConversion_GrsWeight_Id;
                        resultItem.productConversion_GrsWeight_Name = item.ProductConversion_GrsWeight_Name;
                        #endregion

                        resultItem.uDF_1 = item.UDF_1;
                        resultItem.uDF_2 = item.UDF_2;
                        resultItem.uDF_3 = item.UDF_3;
                        resultItem.uDF_4 = item.UDF_4;
                        resultItem.uDF_5 = item.UDF_5;
                        resultItem.sale_UNIT = item.SALE_UNIT;
                        resultItem.in_UNIT = item.IN_UNIT;
                        resultItem.ref_No1 = item.Ref_No1;
                        resultItem.ref_No2 = item.Ref_No2;
                        resultItem.ref_No3 = item.Ref_No3;
                        resultItem.ref_No4 = item.Ref_No4;
                        resultItem.ref_No5 = item.Ref_No5;



                        resultItem.remark = item.Remark;


                        result.Add(resultItem);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region SaveChanges
        public String SaveChangesV2(ProductConversionViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var ProductConversionOld = db.MS_ProductConversion.Find(data.productConversion_Index);
                var Product = db.MS_Product.Find(data.product_Index);



                if (ProductConversionOld == null)
                {
                    if (!string.IsNullOrEmpty(data.productConversion_Id))
                    {
                        var query = db.MS_ProductConversion.FirstOrDefault(c => c.ProductConversion_Id == data.productConversion_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (!string.IsNullOrEmpty(data.productConversion_Name))
                    {
                        var query = db.MS_ProductConversion.FirstOrDefault(c => c.ProductConversion_Name == data.productConversion_Name && c.IsActive == 1 && c.Product_Index == data.product_Index);
                        if (query != null)
                        {
                            return "ProductConversion_Name " + data.productConversion_Name + " Is Used";
                        }
                    }
                    if (string.IsNullOrEmpty(data.productConversion_Id))
                    {
                        data.productConversion_Id = "ProductConversion_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_ProductConversion.FirstOrDefault(c => c.ProductConversion_Id == data.productConversion_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.ProductConversion_Id == data.productConversion_Id)
                                {
                                    data.productConversion_Id = "ProductConversion_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_ProductConversion Model = new MS_ProductConversion();

                    if (data.sale_UNIT == 1)
                    {
                        var productcon_sale = db.MS_ProductConversion.FirstOrDefault(c => c.Product_Index == data.product_Index && c.ProductConversion_Name != data.productConversion_Name && c.SALE_UNIT == 1);
                        if (productcon_sale != null)
                        {
                            return productcon_sale.ProductConversion_Name + " is Sale Unit";
                        }
                    }
                    if (data.in_UNIT == 1)
                    {
                        var productcon_in = db.MS_ProductConversion.FirstOrDefault(c => c.Product_Index == data.product_Index && c.ProductConversion_Name != data.productConversion_Name && c.IN_UNIT == 1);
                        if (productcon_in != null)
                        {
                            return productcon_in.ProductConversion_Name + " is In Unit";
                        }
                    }

                    Model.ProductConversion_Index = Guid.NewGuid();
                    Model.ProductConversion_Id = data.productConversion_Id;
                    Model.ProductConversion_Name = data.productConversion_Name;
                    Model.ProductConversion_SecondName = data.productConversion_SecondName;
                    Model.ProductConversion_Ratio = data.productConversion_Ratio.GetValueOrDefault();

                    Model.ProductConversion_Weight = data.productConversion_Weight;
                    Model.ProductConversion_Weight_Index = data.productConversion_Weight_Index;
                    Model.ProductConversion_Weight_Id = data.productConversion_Weight_Id;
                    Model.ProductConversion_Weight_Name = data.productConversion_Weight_Name;
                    Model.ProductConversion_WeightRatio = data.productConversion_WeightRatio;

                    Model.ProductConversion_GrsWeight = data.productConversion_GrsWeight;
                    Model.ProductConversion_GrsWeight_Index = data.productConversion_GrsWeight_Index;
                    Model.ProductConversion_GrsWeight_Id = data.productConversion_GrsWeight_Id;
                    Model.ProductConversion_GrsWeight_Name = data.productConversion_GrsWeight_Name;
                    Model.ProductConversion_GrsWeightRatio = data.productConversion_GrsWeightRatio;

                    Model.ProductConversion_Width = data.productConversion_Width;
                    Model.ProductConversion_Width_Index = data.productConversion_Volume_Index;
                    Model.ProductConversion_Width_Id = data.productConversion_Volume_Id;
                    Model.ProductConversion_Width_Name = data.productConversion_Volume_Name;
                    Model.ProductConversion_WidthRatio = data.productConversion_VolumeRatio;


                    Model.ProductConversion_Length = data.productConversion_Length;
                    Model.ProductConversion_Length_Index = data.productConversion_Volume_Index;
                    Model.ProductConversion_Length_Id = data.productConversion_Volume_Id;
                    Model.ProductConversion_Length_Name = data.productConversion_Volume_Name;
                    Model.ProductConversion_LengthRatio = data.productConversion_VolumeRatio;

                    Model.ProductConversion_Height = data.productConversion_Height;
                    Model.ProductConversion_Height_Index = data.productConversion_Volume_Index;
                    Model.ProductConversion_Height_Id = data.productConversion_Volume_Id;
                    Model.ProductConversion_Height_Name = data.productConversion_Volume_Name;
                    Model.ProductConversion_HeightRatio = data.productConversion_VolumeRatio;

                    Model.ProductConversion_Volume = (data.productConversion_Width * data.productConversion_Length * data.productConversion_Height);
                    Model.ProductConversion_Volume_Index = data.productConversion_Volume_Index;
                    Model.ProductConversion_Volume_Id = data.productConversion_Volume_Id;
                    Model.ProductConversion_Volume_Name = data.productConversion_Volume_Name;
                    Model.ProductConversion_VolumeRatio = data.productConversion_VolumeRatio ?? 0;

                    Model.Product_Index = data.product_Index.GetValueOrDefault();
                    Model.Product_Id = data.product_Id;
                    Model.Product_Name = data.product_Name;
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
                    Model.SALE_UNIT = Convert.ToInt32(data.sale_UNIT);
                    Model.IN_UNIT = Convert.ToInt32(data.in_UNIT);
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_ProductConversion.Add(Model);

                    if (data.in_UNIT != null)
                    {
                        Product.Qty_Per_Tag = data.qty_Per_Tag;
                    }

                    if (data.documents != null)
                    {
                        foreach (var d in data.documents.Where(c => !c.isDelete))
                        {
                            im_DocumentFile documents = new im_DocumentFile();

                            documents.DocumentFile_Index = Guid.NewGuid(); ;
                            documents.DocumentFile_Name = d.filename;
                            documents.DocumentFile_Path = d.path;
                            documents.DocumentFile_Url = d.urlAttachFile;
                            documents.DocumentFile_Type = d.type;
                            documents.DocumentFile_Status = 0;
                            documents.Create_By = data.create_By;
                            documents.Create_Date = DateTime.Now;
                            documents.Ref_Index = data.productConversion_Index;
                            documents.Ref_No = data.productConversion_Id;
                            db.im_DocumentFile.Add(documents);
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(data.productConversion_Name))
                    {
                        var query = db.MS_ProductConversion.FirstOrDefault(c => c.ProductConversion_Name == data.productConversion_Name && c.IsActive == 1 && c.ProductConversion_Index != ProductConversionOld.ProductConversion_Index && c.Product_Index == data.product_Index );
                        if (query != null)
                        {
                            return "ProductConversion_Name " + data.productConversion_Name + " Is Used";
                        }
                    }
                    if (data.sale_UNIT == 1)
                    {
                        var productcon_sale = db.MS_ProductConversion.FirstOrDefault(c => c.Product_Index == data.product_Index && c.ProductConversion_Name != data.productConversion_Name && c.SALE_UNIT == 1  );
                        if (productcon_sale != null)
                        {
                            return productcon_sale.ProductConversion_Name + " is Sale Unit";
                        }
                    }
                    if (data.in_UNIT == 1)
                    {
                        var productcon_in = db.MS_ProductConversion.FirstOrDefault(c => c.Product_Index == data.product_Index && c.ProductConversion_Name != data.productConversion_Name && c.IN_UNIT == 1);
                        if (productcon_in != null)
                        {
                            return productcon_in.ProductConversion_Name + " is In Unit";
                        }
                    }


                    if (string.IsNullOrEmpty(data.productConversion_Id))
                    {
                        if (ProductConversionOld.ProductConversion_Id != "")
                        {
                            data.productConversion_Id = ProductConversionOld.ProductConversion_Id;
                        }
                    }
                    else
                    {
                        if (ProductConversionOld.ProductConversion_Id != data.productConversion_Id)
                        {
                            var query = db.MS_ProductConversion.FirstOrDefault(c => c.ProductConversion_Id == data.productConversion_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.productConversion_Id = ProductConversionOld.ProductConversion_Id;
                        }
                    }

                    ProductConversionOld.ProductConversion_Id = data.productConversion_Id;
                    ProductConversionOld.ProductConversion_Name = data.productConversion_Name;
                    ProductConversionOld.ProductConversion_SecondName = data.productConversion_SecondName;
                    ProductConversionOld.ProductConversion_Ratio = data.productConversion_Ratio.GetValueOrDefault();

                    ProductConversionOld.ProductConversion_Weight = data.productConversion_Weight;
                    ProductConversionOld.ProductConversion_Weight_Index = data.productConversion_Weight_Index;
                    ProductConversionOld.ProductConversion_Weight_Id = data.productConversion_Weight_Id;
                    ProductConversionOld.ProductConversion_Weight_Name = data.productConversion_Weight_Name;
                    ProductConversionOld.ProductConversion_WeightRatio = data.productConversion_WeightRatio;

                    ProductConversionOld.ProductConversion_GrsWeight = data.productConversion_GrsWeight;
                    ProductConversionOld.ProductConversion_GrsWeight_Index = data.productConversion_GrsWeight_Index;
                    ProductConversionOld.ProductConversion_GrsWeight_Id = data.productConversion_GrsWeight_Id;
                    ProductConversionOld.ProductConversion_GrsWeight_Name = data.productConversion_GrsWeight_Name;
                    ProductConversionOld.ProductConversion_GrsWeightRatio = data.productConversion_GrsWeightRatio;

                    ProductConversionOld.ProductConversion_Width = data.productConversion_Width;
                    ProductConversionOld.ProductConversion_Width_Index = data.productConversion_Volume_Index;
                    ProductConversionOld.ProductConversion_Width_Id = data.productConversion_Volume_Id;
                    ProductConversionOld.ProductConversion_Width_Name = data.productConversion_Volume_Name;
                    ProductConversionOld.ProductConversion_WidthRatio = data.productConversion_VolumeRatio;


                    ProductConversionOld.ProductConversion_Length = data.productConversion_Length;
                    ProductConversionOld.ProductConversion_Length_Index = data.productConversion_Volume_Index;
                    ProductConversionOld.ProductConversion_Length_Id = data.productConversion_Volume_Id;
                    ProductConversionOld.ProductConversion_Length_Name = data.productConversion_Volume_Name;
                    ProductConversionOld.ProductConversion_LengthRatio = data.productConversion_VolumeRatio;

                    ProductConversionOld.ProductConversion_Height = data.productConversion_Height;
                    ProductConversionOld.ProductConversion_Height_Index = data.productConversion_Volume_Index;
                    ProductConversionOld.ProductConversion_Height_Id = data.productConversion_Volume_Id;
                    ProductConversionOld.ProductConversion_Height_Name = data.productConversion_Volume_Name;
                    ProductConversionOld.ProductConversion_HeightRatio = data.productConversion_VolumeRatio;

                    ProductConversionOld.ProductConversion_Volume = (data.productConversion_Width * data.productConversion_Length * data.productConversion_Height);
                    ProductConversionOld.ProductConversion_Volume_Index = data.productConversion_Volume_Index;
                    ProductConversionOld.ProductConversion_Volume_Id = data.productConversion_Volume_Id;
                    ProductConversionOld.ProductConversion_Volume_Name = data.productConversion_Volume_Name;
                    ProductConversionOld.ProductConversion_VolumeRatio = data.productConversion_VolumeRatio ?? 0;


                    ProductConversionOld.Product_Index = data.product_Index.GetValueOrDefault();
                    ProductConversionOld.Product_Id = data.product_Id;
                    ProductConversionOld.Product_Name = data.product_Name;
                    ProductConversionOld.Ref_No1 = data.ref_No1;
                    ProductConversionOld.Ref_No2 = data.ref_No2;
                    ProductConversionOld.Ref_No3 = data.ref_No3;
                    ProductConversionOld.Ref_No4 = data.ref_No4;
                    ProductConversionOld.Ref_No5 = data.ref_No5;
                    ProductConversionOld.Remark = data.remark;
                    ProductConversionOld.UDF_1 = data.udf_1;
                    ProductConversionOld.UDF_2 = data.udf_2;
                    ProductConversionOld.UDF_3 = data.udf_3;
                    ProductConversionOld.UDF_4 = data.udf_4;
                    ProductConversionOld.UDF_5 = data.udf_5;
                    ProductConversionOld.SALE_UNIT = Convert.ToInt32(data.sale_UNIT);
                    ProductConversionOld.IN_UNIT = Convert.ToInt32(data.in_UNIT);
                    ProductConversionOld.IsActive = Convert.ToInt32(data.isActive);
                    ProductConversionOld.Update_By = data.create_By;
                    ProductConversionOld.Update_Date = DateTime.Now;

                    //Update Qty_Per_Tag ms_Product
                    if (data.in_UNIT != null)
                    {
                        Product.Qty_Per_Tag = data.qty_Per_Tag;
                    }

                    if (data.documents != null)
                    {
                        foreach (var d in data.documents)
                        {
                            if (d.index == null || d.index == Guid.Empty)
                            {
                                im_DocumentFile documents = new im_DocumentFile();

                                documents.DocumentFile_Index = Guid.NewGuid(); ;
                                documents.DocumentFile_Name = d.filename;
                                documents.DocumentFile_Path = d.path;
                                documents.DocumentFile_Url = d.urlAttachFile;
                                documents.DocumentFile_Type = d.type;
                                documents.DocumentFile_Status = 0;
                                documents.Create_By = data.create_By;
                                documents.Create_Date = DateTime.Now;
                                documents.Ref_Index = data.productConversion_Index;
                                documents.Ref_No = data.productConversion_Id;
                                db.im_DocumentFile.Add(documents);
                            }
                            else if ((d.index != null || d.index != Guid.Empty) && d.isDelete)
                            {
                                var Documents = db.im_DocumentFile.FirstOrDefault(c => c.DocumentFile_Index == d.index && c.Ref_Index == ProductConversionOld.ProductConversion_Index && c.DocumentFile_Status == 0);
                                Documents.DocumentFile_Status = -1;
                                Documents.Update_By = data.create_By;
                                Documents.Update_Date = DateTime.Now;
                            }
                        }
                    }
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
                    olog.logging("SaveProductConversion", msglog);
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
    }
}
