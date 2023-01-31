using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MasterDataBusiness.CargoType
{
    public class CargoTypeService
    {
        private MasterDataDbContext db;


        public CargoTypeService()
        {
            db = new MasterDataDbContext();
        }

        public CargoTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        public List<CargoTypeViewModel> cargoTypedropdown(CargoTypeViewModel data)
        {
            try
            {
                var result = new List<CargoTypeViewModel>();

                var query = db.ms_CargoType.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.CargoType_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new CargoTypeViewModel();

                    resultItem.cargoType_Index = item.CargoType_Index;
                    resultItem.cargoType_Id = item.CargoType_Id;
                    resultItem.cargoType_Name = item.CargoType_Name;

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
