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

namespace MasterDataBusiness.Weight
{
    public class WeightService
    {
        private MasterDataDbContext db;

        public WeightService()
        {
            db = new MasterDataDbContext();
        }

        public WeightService(MasterDataDbContext db)
        {
            this.db = db;
        }

        public List<WeightViewModel> weightdropdown(WeightViewModel data)
        {
            try
            {
                var result = new List<WeightViewModel>();

                var query = db.sy_Weight.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.Weight_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new WeightViewModel();

                    resultItem.weight_Index = item.Weight_Index;
                    resultItem.weight_Id = item.Weight_Id;
                    resultItem.weight_Name = item.Weight_Name;
                    resultItem.weight_Ratio = item.Weight_Ratio;

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
