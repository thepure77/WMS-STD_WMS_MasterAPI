using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class userViewModelV2
    {

        public Guid user_Index { get; set; }

        public Guid userGroup_Index { get; set; }
        public string userGroup_Id { get; set; }
        public string userGroup_Name { get; set; }


        public string user_Id { get; set; }


        public string user_Name { get; set; }


        public string user_Password { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }


        public string create_By { get; set; }


        public DateTime? create_Date { get; set; }


        public string update_By { get; set; }


        public DateTime? update_Date { get; set; }


        public string cancel_By { get; set; }


        public DateTime? cancel_Date { get; set; }

        public string first_Name { get; set; }
        public string last_Name { get; set; }
        public string position_Name { get; set; }
        public string position_Code { get; set; }
        public string station_Code { get; set; }
        public string station_Name { get; set; }
        public string branch_Code { get; set; }
        public string branch_Name { get; set; }
        public string status_Emp { get; set; }
        public string emp_Code { get; set; }
        public bool checkupdate { get; set; }
        
    }


}
