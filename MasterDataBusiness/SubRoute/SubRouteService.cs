using DataAccess;
using Microsoft.EntityFrameworkCore;
using PlanGIBusiness.Route;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MasterDataBusiness
{
    public class SubRouteService
    {

        public List<SubRouteViewModel> subRoutefilter(SubRouteViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var result = new List<SubRouteViewModel>();

                    var query = context.MS_SubRoute.AsQueryable();

                    if (!string.IsNullOrEmpty(data.subRoute_Index.ToString()))
                    {
                        query = query.Where(c => c.SubRoute_Index == data.subRoute_Index);
                    }

                    if (!string.IsNullOrEmpty(data.subRoute_Id))
                    {
                        query = query.Where(c => c.SubRoute_Id == data.subRoute_Id);
                    }

                    if (!string.IsNullOrEmpty(data.subRoute_Name))
                    {
                        query = query.Where(c => c.SubRoute_Name == data.subRoute_Name);
                    }

                    var queryResult = query.OrderBy(o => o.SubRoute_Id).ToList();

                    foreach (var item in queryResult)
                    {
                        var resultItem = new SubRouteViewModel();
                        resultItem.subRoute_Index = item.SubRoute_Index;
                        resultItem.subRoute_Id = item.SubRoute_Id;
                        resultItem.subRoute_Name = item.SubRoute_Name;
                        resultItem.sla_Day = item.SLA_Day;
                        resultItem.route_Index = item.Route_Index;
                        resultItem.route_Id = item.Route_Id;
                        resultItem.route_Name = item.Route_Name;
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
