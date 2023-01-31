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
    public class TransportService
    {
        public List<TransportViewModel> transportfilter(TransportViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var result = new List<TransportViewModel>();

                    var query = context.ms_Transport.AsQueryable();

                    var queryResult = query.OrderBy(o => o.Transport_Name).ToList();

                    foreach (var item in queryResult)
                    {
                        var resultItem = new TransportViewModel();
                        resultItem.transport_Index = item.Transport_Index;
                        resultItem.transport_Id = item.Transport_Id;
                        resultItem.transport_Name = item.Transport_Name;
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
