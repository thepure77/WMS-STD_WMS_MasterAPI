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

namespace MasterDataBusiness.Chute
{
    public class ChuteService
    {
        private MasterDataDbContext db;

        public ChuteService()
        {
            db = new MasterDataDbContext();
        }

        public ChuteService(MasterDataDbContext db)
        {
            this.db = db;
        }

        public List<ChuteViewModel> chutedropdown(ChuteViewModel data)
        {
            try
            {
                var result = new List<ChuteViewModel>();

                var query = db.ms_Chute.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.Chute_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new ChuteViewModel();

                    resultItem.chute_Index = item.Chute_Index;
                    resultItem.chute_Id = item.Chute_Id;
                    resultItem.chute_Name = item.Chute_Name;
                
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
