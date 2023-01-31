using DataAccess;
using Microsoft.EntityFrameworkCore;
using PlanGIBusiness.Route;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MasterDataBusiness
{
    public class RouteService
    {
        public List<RouteViewModel> FilterRoute()
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_Route.FromSql("sp_GetRoute").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

                    var result = new List<RouteViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new RouteViewModel();

                        resultItem.RouteIndex = item.Route_Index;
                        resultItem.SubRouteIndex = item.SubRoute_Index.GetValueOrDefault();
                        resultItem.RouteId = item.Route_Id;
                        resultItem.RouteName = item.Route_Name;
                        resultItem.IsActive = item.IsActive;
                        resultItem.IsDelete = item.IsDelete;
                        resultItem.IsSystem = item.IsSystem;
                        resultItem.StatusId = item.Status_Id;
                        resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
                        resultItem.CreateBy = item.Create_By;
                        resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
                        resultItem.UpdateBy = item.Update_By;
                        resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
                        resultItem.CancelBy = item.Cancel_By;

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
        public List<RouteViewModel> search(RouteViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    string pwhereFilter = "";
                    string pwhereLike = "";
                    var result = new List<RouteViewModel>();
                    if (data.RouteId != "" && data.RouteId != null)
                    {
                        pwhereFilter = " And Route_Id like N'%" + data.RouteId + "%'";
                    }
                    else
                    {
                        pwhereFilter += "";
                    }
                    if (data.RouteName != "" && data.RouteName != null)
                    {
                        pwhereFilter = " And Route_Name like N'%" + data.RouteName + "%'";
                    }
                    else
                    {
                        pwhereFilter += "";
                    }


                    pwhereFilter += " And isActive = '" + 1 + "'";
                    pwhereFilter += " And isDelete = '" + 0 + "'";
                    var strwhere = new SqlParameter("@strwhere", pwhereFilter);
                    var query = context.MS_Route.FromSql("sp_GetRoute @strwhere ", strwhere).ToList();
                    foreach (var item in query)
                    {
                        var resultItem = new RouteViewModel();
                        resultItem.RouteIndex = item.Route_Index;
                        resultItem.SubRouteIndex = item.SubRoute_Index.GetValueOrDefault();
                        resultItem.RouteId = item.Route_Id;
                        resultItem.RouteName = item.Route_Name;
                        resultItem.IsActive = item.IsActive;
                        resultItem.IsDelete = item.IsDelete;
                        resultItem.IsSystem = item.IsSystem;
                        resultItem.StatusId = item.Status_Id;
                        resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
                        resultItem.CreateBy = item.Create_By;
                        resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
                        resultItem.UpdateBy = item.Update_By;
                        resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
                        resultItem.CancelBy = item.Cancel_By;


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

        public List<RouteViewModelV2> routefilter(RouteViewModelV2 data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var result = new List<RouteViewModelV2>();

                    var query = context.MS_Route.AsQueryable();

                    if (!string.IsNullOrEmpty(data.route_Id))
                    {
                        query = query.Where(c => c.Route_Id == data.route_Id);
                    }

                    if (!string.IsNullOrEmpty(data.route_Name))
                    {
                        query = query.Where(c => c.Route_Name == data.route_Name);
                    }

                    var queryResult = query.OrderBy(o => o.Route_Id).ToList();

                    foreach (var item in queryResult)
                    {
                        var resultItem = new RouteViewModelV2();
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
