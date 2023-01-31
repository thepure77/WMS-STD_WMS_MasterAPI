using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ProductOwner
{
    public class View_GetProductOwnerViewModel
    {

        public Guid product_Index { get; set; }


        public string product_Id { get; set; }


        public string product_Name { get; set; }

        public Guid productConversion_Index { get; set; }

        public string productConversion_Id { get; set; }


        public string productConversion_Name { get; set; }


        public string product_SecondName { get; set; }


        public string product_ThirdName { get; set; }

        public Guid owner_Index { get; set; }


        public decimal? productConversion_Ratio { get; set; }

        public decimal? productConversion_Weight { get; set; }

        public decimal? productConversion_Width { get; set; }


        public decimal? productConversion_Length { get; set; }


        public decimal? productConversion_Height { get; set; }


        public decimal? productConversion_VolumeRatio { get; set; }


        public decimal? productConversion_Volume { get; set; }
    }
}
