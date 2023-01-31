using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.CostCenter;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MasterDataBusiness.Volume
{
    public class VolumeService
    {
        private MasterDataDbContext db;

        public VolumeService()
        {
            db = new MasterDataDbContext();
        }

        public VolumeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        public List<VolumeViewModel> volumedropdown(VolumeViewModel data)
        {
            try
            {
                var result = new List<VolumeViewModel>();

                var query = db.sy_Volume.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.Volume_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new VolumeViewModel();

                    resultItem.volume_Index = item.Volume_Index;
                    resultItem.volume_Id = item.Volume_Id;
                    resultItem.volume_Name = item.Volume_Name;
                    resultItem.volume_Ratio = item.Volume_Ratio;

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
