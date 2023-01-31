using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SearchProductConversionViewModel : Pagination
    {
        public Guid productConversion_Index { get; set; }
        public string productConversion_Id { get; set; }
        public string productConversion_Name { get; set; }
        public string productConversion_SecondName { get; set; }
        public decimal? productConversion_Ratio { get; set; }

        public decimal? productConversion_Weight { get; set; }
        public Guid? productConversion_Weight_Index { get; set; }
        public string productConversion_Weight_Id { get; set; }
        public string productConversion_Weight_Name { get; set; }
        public decimal? productConversion_WeightRatio { get; set; }
        public decimal? productConversion_GrsWeight { get; set; }
        public Guid? productConversion_GrsWeight_Index { get; set; }
        public string productConversion_GrsWeight_Id { get; set; }
        public string productConversion_GrsWeight_Name { get; set; }
        public decimal? productConversion_GrsWeightRatio { get; set; }

        public decimal? productConversion_Width { get; set; }
        public Guid? productConversion_Width_Index { get; set; }
        public string productConversion_Width_Id { get; set; }
        public string productConversion_Width_Name { get; set; }
        public decimal? productConversion_WidthRatio { get; set; }

        public decimal? productConversion_Length { get; set; }
        public Guid? productConversion_Length_Index { get; set; }
        public string productConversion_Length_Id { get; set; }
        public string productConversion_Length_Name { get; set; }
        public decimal? productConversion_LengthRatio { get; set; }

        public decimal? productConversion_Height { get; set; }
        public Guid? productConversion_Height_Index { get; set; }
        public string productConversion_Height_Id { get; set; }
        public string productConversion_Height_Name { get; set; }
        public decimal? productConversion_HeightRatio { get; set; }

        public Guid? productConversion_Volume_Index { get; set; }
        public string productConversion_Volume_Id { get; set; }
        public string productConversion_Volume_Name { get; set; }

        public decimal? productConversion_VolumeRatio { get; set; }
        public decimal? productConversion_Volume { get; set; }

        public Guid? product_Index { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }

        public Guid? businessUnit_Index { get; set; }
        public string businessUnit_Id { get; set; }
        public string businessUnit_Name { get; set; }

        public string ref_No1 { get; set; }

        public string ref_No2 { get; set; }

        public string ref_No3 { get; set; }

        public string ref_No4 { get; set; }

        public string ref_No5 { get; set; }

        public string remark { get; set; }

        public string udf_1 { get; set; }

        public string udf_2 { get; set; }

        public string udf_3 { get; set; }

        public string udf_4 { get; set; }

        public string udf_5 { get; set; }

        public int? sale_UNIT { get; set; }
        public int? in_UNIT { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }
        
        public string create_By { get; set; }
        
        public string create_Date { get; set; }
        
        public string update_By { get; set; }
        
        public string update_Date { get; set; }
        
        public string cancel_By { get; set; }
        
        public DateTime? cancel_Date { get; set; }

        public string key { get; set; }

        public string value1 { get; set; }

        public int rowNum { get; set; }

        public string changeSet { get; set; }

        public string create_date { get; set; }

        public string create_date_to { get; set; }
    }
    public class actionResultProductConversionViewModel
    {
        public IList<SearchProductConversionViewModel> itemsProductConversion { get; set; }
        public Pagination pagination { get; set; }
    }

    public class SearchProductConversionInClauseViewModel : Pagination
    {
        public List<Guid> List_ProductConversion_Index { get; set; }

        public List<string> List_ProductConversion_Id { get; set; }

        public List<Guid> List_Product_Index { get; set; }
    }
}
