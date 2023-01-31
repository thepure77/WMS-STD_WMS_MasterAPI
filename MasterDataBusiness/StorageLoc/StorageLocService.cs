using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MasterDataBusiness.StorageLoc
{
    public class StorageLocService
    {
        private MasterDataDbContext db;

        public StorageLocService()
        {
            db = new MasterDataDbContext();
        }

        public StorageLocService(MasterDataDbContext db)
        {
            this.db = db;
        }
        public List<StorageLocViewModel> storageLocfilter(StorageLocViewModel data)
        {
            try
            {
                var result = new List<StorageLocViewModel>();
                var query = db.ms_Storage_Loc.AsQueryable();

                if (data.warehouse_Index_To != new Guid("00000000-0000-0000-0000-000000000000".ToString()) && data.warehouse_Index_To != null)
                {
                    query = query.Where(c => c.Warehouse_Index == data.warehouse_Index_To);
                }

                if (!string.IsNullOrEmpty(data.storageLoc_Id))
                {
                    query = query.Where(c => c.StorageLoc_Id == data.storageLoc_Id);
                }

                var queryResult = query.OrderBy(o => o.StorageLoc_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new StorageLocViewModel
                    {
                        storageLoc_Index = item.StorageLoc_Index,
                        storageLoc_Id = item.StorageLoc_Id,
                        storageLoc_Name = item.StorageLoc_Name,

                    };

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
