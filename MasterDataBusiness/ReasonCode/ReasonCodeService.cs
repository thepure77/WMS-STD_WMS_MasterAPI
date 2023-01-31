using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterDataBusiness
{
    public class ReasonCodeService
    {
        private MasterDataDbContext db;

        public ReasonCodeService()
        {
            db = new MasterDataDbContext();
        }

        public ReasonCodeService(MasterDataDbContext db)
        {
            this.db = db;
        }
        public List<ReasonCodeViewModel> GetReasonCode(ReasonCodeFilterViewModel model)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = db.MS_ReasonCode.Where(c => c.Process_Index == model.process_Index).ToList();

                    var result = new List<ReasonCodeViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new ReasonCodeViewModel();

                        resultItem.ReasonCodeId = item.ReasonCode_Id;
                        resultItem.ReasonCodeIndex = item.ReasonCode_Index;
                        resultItem.ReasonCodeName = item.ReasonCode_Name;
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
