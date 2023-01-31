using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MasterDataBusiness
{
    public class TypeCarService
    {
        public List<TypeCarViewModel> TypeCarilter(TypeCarViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var result = new List<TypeCarViewModel>();

                    var query = context.ms_TypeCar.AsQueryable();

                    var queryResult = query.OrderBy(o => o.TypeCar_Id).ToList();

                    foreach (var item in queryResult)
                    {
                        var resultItem = new TypeCarViewModel();
                        resultItem.typeCar_Index = item.TypeCar_Index;
                        resultItem.typeCar_Id = item.TypeCar_Id;
                        resultItem.typeCar_Name = item.TypeCar_Name;
                        result.Add(resultItem);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
