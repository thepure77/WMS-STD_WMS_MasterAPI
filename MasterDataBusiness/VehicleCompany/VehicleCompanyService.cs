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

namespace MasterDataBusiness.VehicleCompany
{
    public class VehicleCompanyService
    {
        private MasterDataDbContext db;

        public VehicleCompanyService()
        {
            db = new MasterDataDbContext();
        }

        public VehicleCompanyService(MasterDataDbContext db)
        {
            this.db = db;
        }

        public List<VehicleCompanyViewModel> vehicleCompanydropdown(VehicleCompanyViewModel data)
        {
            try
            {
                var result = new List<VehicleCompanyViewModel>();

                var query = db.ms_VehicleCompany.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.VehicleCompany_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new VehicleCompanyViewModel();

                    resultItem.vehicleCompany_Index = item.VehicleCompany_Index;
                    resultItem.vehicleCompany_Id = item.VehicleCompany_Id;
                    resultItem.vehicleCompany_Name = item.VehicleCompany_Name;
                
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
