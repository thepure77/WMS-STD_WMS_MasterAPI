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

namespace MasterDataBusiness.RollCageType
{
    public class RollCageTypeService
    {
        private MasterDataDbContext db;

        public RollCageTypeService()
        {
            db = new MasterDataDbContext();
        }

        public RollCageTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        public List<RollCageTypeViewModel> rollCageTypedropdown(RollCageTypeViewModel data)
        {
            try
            {
                var result = new List<RollCageTypeViewModel>();

                var query = db.ms_RollCageType.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.RollCageType_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new RollCageTypeViewModel();

                    resultItem.rollCageType_Index = item.RollCageType_Index;
                    resultItem.rollCageType_Id = item.RollCageType_Id;
                    resultItem.rollCageType_Name = item.RollCageType_Name;
                
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
