using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterDataBusiness
{
    public class RuleZoneService
    {
        public List<RuleZoneViewModel> Filter()
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_RuleZone.FromSql("sp_GetRuleZone").ToList();

                    var result = new List<RuleZoneViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new RuleZoneViewModel();

                        resultItem.RuleIndex = item.Rule_Index;
                        resultItem.RuleZoneIndex = item.RuleZone_Index;
                        resultItem.ZoneIndex= item.Zone_Index;
                        resultItem.RuleZoneSeq = item.RuleZone_Seq;
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
    }
}
