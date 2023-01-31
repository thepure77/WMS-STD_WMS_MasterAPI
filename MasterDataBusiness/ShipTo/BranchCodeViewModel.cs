using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ViewModels
{
    public class BranchCodeViewModel
    {
        public Guid? shipTo_Index { get; set; }
        public string shipTo_Id { get; set; }
        public string format_Text { get; set; }

        public string status { get; set; }
        public string message { get; set; }
    }
}
