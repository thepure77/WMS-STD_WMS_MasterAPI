using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MasterDataBusiness.DocumentPriority
{
    public class DocumentPriorityService
    {
        private MasterDataDbContext db;


        public DocumentPriorityService()
        {
            db = new MasterDataDbContext();
        }

        public DocumentPriorityService(MasterDataDbContext db)
        {
            this.db = db;
        }

        public List<DocumentPriorityViewModel> DocumentPrioritydropdown(DocumentPriorityViewModel data)
        {
            try
            {
                var result = new List<DocumentPriorityViewModel>();

                var query = db.ms_DocumentPriority.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.DocumentPriority_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new DocumentPriorityViewModel();

                    resultItem.documentPriority_Index = item.DocumentPriority_Index;
                    resultItem.documentPriority_Id = item.DocumentPriority_Id;
                    resultItem.documentPriority_Name = item.DocumentPriority_Name;

                    result.Add(resultItem);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
