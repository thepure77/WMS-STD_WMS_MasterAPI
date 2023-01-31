using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MasterDataBusiness
{
    public class ZonePopupService
    {
        private MasterDataDbContext db;

        public ZonePopupService()
        {
            db = new MasterDataDbContext();
        }

        public ZonePopupService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterZonePopup
        public List<ZonePopupViewModel> filter(ZonePopupViewModel data)
        {
            try
            {
                var query = db.MS_Zone.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                
                if (!string.IsNullOrEmpty(data.zone_Id))
                {
                    query = query.Where(c => c.Zone_Id.Contains(data.zone_Id));

                }
                else if (!string.IsNullOrEmpty(data.zone_Name))
                {
                    query = query.Where(c => c.Zone_Name.Contains(data.zone_Name));

                }

                var result = new List<ZonePopupViewModel>();

                foreach (var item in query)
                {
                    var resultItem = new ZonePopupViewModel();

                    resultItem.zone_Index = item.Zone_Index;
                    resultItem.zone_Id = item.Zone_Id;
                    resultItem.zone_Name = item.Zone_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
