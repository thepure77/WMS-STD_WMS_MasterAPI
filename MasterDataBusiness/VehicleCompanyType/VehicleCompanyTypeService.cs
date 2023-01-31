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

namespace MasterDataBusiness.VehicleCompanyType
{
    public class VehicleCompanyTypeService
    {
        private MasterDataDbContext db;

        public VehicleCompanyTypeService()
        {
            db = new MasterDataDbContext();
        }

        public VehicleCompanyTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        public List<VehicleCompanyTypeViewModel> vehicleCompanyTypedropdown(VehicleCompanyTypeViewModel data)
        {
            try
            {
                var result = new List<VehicleCompanyTypeViewModel>();

                var query = db.ms_VehicleCompanyType.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.VehicleCompanyType_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new VehicleCompanyTypeViewModel();

                    resultItem.vehicleCompanyType_Index = item.VehicleCompanyType_Index;
                    resultItem.vehicleCompanyType_Id = item.VehicleCompanyType_Id;
                    resultItem.vehicleCompanyType_Name = item.VehicleCompanyType_Name;
                
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
