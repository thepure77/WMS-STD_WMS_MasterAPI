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

namespace MasterDataBusiness.RollCage
{
    public class RollCageService
    {
        private MasterDataDbContext db;

        public RollCageService()
        {
            db = new MasterDataDbContext();
        }

        public RollCageService(MasterDataDbContext db)
        {
            this.db = db;
        }

        public List<RollCageViewModel> rollCagedropdown(RollCageViewModel data)
        {
            try
            {
                var result = new List<RollCageViewModel>();

                var query = db.ms_RollCage.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.RollCage_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new RollCageViewModel();

                    resultItem.rollCage_Index = item.RollCage_Index;
                    resultItem.rollCage_Id = item.RollCage_Id;
                    resultItem.rollCage_Name = item.RollCage_Name;
                
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
