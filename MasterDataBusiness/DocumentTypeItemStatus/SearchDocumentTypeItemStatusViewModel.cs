using MasterDataAPI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class SearchDocumentTypeItemStatusViewModel : Pagination
    {

        public Guid documentTypeItemStatus_Index { get; set; }

        public Guid documentType_Index { get; set; }
        public string documentType_Name { get; set; }
        public string documentType_Id { get; set; }
        public Guid itemStatus_Index { get; set; }

        public string itemStatus_Name { get; set; }
        public string itemStatus_Id { get; set; }

        public int? isActive { get; set; }

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
        public DateTime? update_Date { get; set; }

        [StringLength(200)]
        public string cancel_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? cancel_Date { get; set; }

        public string key { get; set; }

        public string documentTypeItemStatus_Id { get; set; }




    }
    public class actionResultDocumentTypeItemStatusViewModel
    {
        public IList<SearchDocumentTypeItemStatusViewModel> itemsDocumentTypeItemStatus { get; set; }
        public Pagination pagination { get; set; }
    }
}
