using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterDataBusiness
{
    public class PalletTypeService
    {
        public List<PalletTypeViewModel> Filter()
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_PalletType.FromSql("sp_GetPalletType").ToList();

                    var result = new List<PalletTypeViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new PalletTypeViewModel();

                        resultItem.PalletTypeIndex = item.PalletType_Index;
                        resultItem.PalletTypeId = item.PalletType_Id;
                        resultItem.PalletTypeName = item.PalletType_Name;
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
