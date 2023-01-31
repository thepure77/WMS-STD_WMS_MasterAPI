using Comone.Utils;
using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using PlanGIBusiness.Round;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MasterDataBusiness
{
    public class RoundService
    {
        public List<RoundViewModel> FilterRound()
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_Round.FromSql("sp_GetRound").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

                    var orderbySeq = queryResult.Select(c => new { c.Round_Index, seq = Convert.ToInt64(c.Round_Id), c.Round_Name, c.IsActive, c.IsDelete, c.IsSystem, c.Status_Id, c.Create_Date, c.Create_By, c.Update_Date, c.Update_By, c.Cancel_Date, c.Cancel_By })
                        .OrderBy(o => o.seq).ToList();

                    var result = new List<RoundViewModel>();
                    foreach (var item in orderbySeq)
                    {
                        var resultItem = new RoundViewModel();

                        resultItem.RoundIndex = item.Round_Index;
                        resultItem.RoundId = item.seq.sParse("");
                        resultItem.RoundName = item.Round_Name;
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

        public List<RoundViewModel> searchRound(RoundViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    string pwhereFilter = "";
                    string pwhereLike = "";
                    var result = new List<RoundViewModel>();
                    var queryResult = context.MS_Round.FromSql("sp_GetRound").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();
                    if (data.RoundId != "" && data.RoundId != null)
                    {
                        pwhereFilter = " And Round_Id like N'%" + data.RoundId + "%'";
                    }
                    else
                    {
                        pwhereFilter += "";
                    }
                    if (data.RoundName != "" && data.RoundName != null)
                    {
                        pwhereFilter = " And Round_Name like N'%" + data.RoundName + "%'";
                    }
                    else
                    {
                        pwhereFilter += "";
                    }


                    pwhereFilter += " And isActive = '" + 1 + "'";
                    pwhereFilter += " And isDelete = '" + 0 + "'";
                    var strwhere = new SqlParameter("@strwhere", pwhereFilter);
                    var query = context.MS_Round.FromSql("sp_GetRound @strwhere ", strwhere).ToList();
                    foreach (var item in query)
                    {
                        var resultItem = new RoundViewModel();
                        resultItem.RoundIndex = item.Round_Index;
                        resultItem.RoundId = item.Round_Id;
                        resultItem.RoundName = item.Round_Name;
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

        public List<roundDocViewModel> roundfilter(roundDocViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var result = new List<roundDocViewModel>();

                    var query = context.MS_Round.AsQueryable();

                    if (!string.IsNullOrEmpty(data.round_Id))
                    {
                        query = query.Where(c => c.Round_Id == data.round_Id);
                    }

                    if (!string.IsNullOrEmpty(data.round_Name))
                    {
                        query = query.Where(c => c.Round_Name == data.round_Name);
                    }

                    var queryResult = query.OrderBy(o => o.Round_Id).ToList();

                    foreach (var item in queryResult)
                    {
                        var resultItem = new roundDocViewModel();
                        resultItem.round_Index = item.Round_Index;
                        resultItem.round_Id = item.Round_Id;
                        resultItem.round_Name = item.Round_Name;
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
