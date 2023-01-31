using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{
    public  class GetConfigFromBaseViewModel
    {
        public string Config_Name { get; set; }
        public string Config_Key { get; set; }
        public string Config_Value { get; set; }
    }
}
