using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ViewModels
{
    public class DocumentTypeExportViewModel
    {
        public Guid? documentType_Index { get; set; }

        public Guid? process_Index { get; set; }

        public string documentType_Id { get; set; }

        public string process_Name { get; set; }

        public string documentType_Name { get; set; }

        public string format_Text { get; set; }

        public string format_Date { get; set; }

        public string format_Running { get; set; }

        public string format_Document { get; set; }

        public int? isResetByYear { get; set; }

        public int? isResetByMonth { get; set; }

        public int? isResetByDay { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }

        public string create_By { get; set; }

        public string create_Date { get; set; }

        public string update_By { get; set; }

        public string update_Date { get; set; }

        public string cancel_By { get; set; }

        public string cancel_Date { get; set; }

        public int chk { get; set; }

        public string key { get; set; }

        public string name { get; set; }

        public string activeStatus { get; set; }
        
        public int numBerOf { get; set; }
    }

    public class DocumentTypeActionResultExportViewModel
    {
        public IList<DocumentTypeExportViewModel> itemsDocumentType { get; set; }
    }
}
