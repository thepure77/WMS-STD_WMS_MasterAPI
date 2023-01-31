using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public class SearchEquipmentViewModel : Pagination
    {
        public SearchEquipmentViewModel()
        {
            sort = new List<sortViewModel>();

            status = new List<statusViewModel>();

        }
        public int numOf { get; set; }
        public Guid equipment_Index { get; set; }

        [StringLength(50)]
        public string equipment_Id { get; set; }

        [StringLength(200)]
        public string equipment_Name { get; set; }

        public string equipment_status { get; set; }


        [StringLength(200)]
        public string equipmentType_Name { get; set; }

        [StringLength(200)]
        public string equipmentSubType_Name { get; set; }

        public Guid equipmentType_Index { get; set; }

        public Guid equipmentSubType_Index { get; set; }

        public string isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }


        [StringLength(200)]
        public string create_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public string create_Date { get; set; }

        [StringLength(200)]
        public string update_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public string update_Date { get; set; }

        [StringLength(200)]
        public string cancel_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? cancel_Date { get; set; }

        public string key { get; set; }

        public string createdateeq_date { get; set; }
        public string createdateeq_date_to { get; set; }

        public List<sortViewModel> sort { get; set; }
        public List<statusViewModel> status { get; set; }


        public class actionResultEquipmentViewModel
        {
            public IList<SearchEquipmentViewModel> itemsEquipment { get; set; }
            public Pagination pagination { get; set; }
        }

        public class sortViewModel
        {
            public string value { get; set; }
            public string display { get; set; }
            public int seq { get; set; }
        }

        public class statusViewModel
        {
            public int? value { get; set; }
            public string display { get; set; }
            public int seq { get; set; }
        }

        public class SortModel
        {
            public string ColId { get; set; }
            public string Sort { get; set; }

            public string PairAsSqlExpression
            {
                get
                {
                    return $"{ColId} {Sort}";
                }
            }
        }
    }
}
