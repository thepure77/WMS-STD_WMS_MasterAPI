using DataAccess;
using MasterDataBusiness.ViewModels;
using System;
using MasterBusiness;
using Business.Commons;
using MasterDataDataAccess.Models;
using GenAutoNumber;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MasterDataBusiness
{
    public class DynamicSlottingService
    {
        private MasterDataDbContext db;

        public DynamicSlottingService()
        {
            db = new MasterDataDbContext();
        }

        public DynamicSlottingService(MasterDataDbContext db)
        {
            this.db = db;
        }

        

        public actionResultDynamicSlottingViewModel filterDynamicSlotting(DynamicSlottingViewModel data)
        {
            try
            {
                var actionResultDynamicSlotting = new actionResultDynamicSlottingViewModel();
                var dynamicSlottings = db.ms_DynamicSlotting.Where(c => c.IsActive == 1).AsQueryable();

                if (!string.IsNullOrEmpty(data.Crane_Name))
                {
                    dynamicSlottings = dynamicSlottings.Where(c => c.Crane_Name == data.Crane_Name);
                }

                if (!string.IsNullOrEmpty(data.Product_Id))
                {
                    dynamicSlottings = dynamicSlottings.Where(c => c.Product_Id == data.Product_Id);
                }

                List<SearchDynamicSlottingViewModel> result = new List<SearchDynamicSlottingViewModel>();
                var data_dynamicSlottings = dynamicSlottings.ToList();
                

                foreach (var item in data_dynamicSlottings)
                {
                    SearchDynamicSlottingViewModel model = new SearchDynamicSlottingViewModel();

                    model.DynamicSlotting_Index = item.DynamicSlotting_Index;
                    model.DynamicSlotting_Id = item.DynamicSlotting_Id;
                    model.DynamicSlotting_Remark = item.DynamicSlotting_Remark;
                    model.Crane_Name = item.Crane_Name;
                    model.Product_Index = item.Product_Index;
                    model.Product_Id = item.Product_Id;
                    model.Product_Name = item.Product_Name;
                    model.Zoneputaway_Index = item.Zoneputaway_Index;
                    model.Zoneputaway_Id = item.Zoneputaway_Id;
                    model.Zoneputaway_Name = item.Zoneputaway_Name;
                    model.Trigger_Time = item.Trigger_Time;
                    model.Trigger_Time_End = item.Trigger_Time_End;
                    model.Trigger_Date = item.Trigger_Date;
                    model.Trigger_Date_End = item.Trigger_Date_End;
                    model.IsMonday = item.IsMonday;
                    model.IsTuesday = item.IsTuesday;
                    model.IsWednesday = item.IsWednesday;
                    model.IsThursday = item.IsThursday;
                    model.IsFriday = item.IsFriday;
                    model.IsSaturday = item.IsSaturday;
                    model.IsSunday = item.IsSunday;
                    model.Plan_By_Product = item.Plan_By_Product;
                    model.Plan_By_Location = item.Plan_By_Location;
                    model.Plan_By_Status = item.Plan_By_Status;
                    model.IsActive = item.IsActive == 1 ? true : false;
                    model.IsDelete = item.IsDelete;
                    model.IsSystem = item.IsSystem;
                    model.Create_By = item.Create_By;
                    model.Create_Date = item.Create_Date;
                    model.Update_By = item.Update_By;
                    model.Update_Date = item.Update_Date;
                    model.Cancel_By = item.Cancel_By;
                    model.Cancel_Date = item.Cancel_Date;
                    model.Last_Trigger_Date = item.Last_Trigger_Date;
                    result.Add(model);
                }


                actionResultDynamicSlotting.itemsDynamicSlotting = result.ToList();

                return actionResultDynamicSlotting;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Result createDynamicSlotting(DynamicSlottingViewModel data)
        {
            logtxt logtxt = new logtxt();
            Result result = new Result();
            try
            {
                if (string.IsNullOrEmpty(data.Crane_Name))
                {
                    result.ResultIsUse = false;
                    result.ResultMsg = "please select Crane";
                    return result;
                }

                MS_Product product = db.MS_Product.FirstOrDefault(c => c.Product_Index == data.Product_Index);

                if (product == null)
                {
                    result.ResultIsUse = false;
                    result.ResultMsg = "product not found";
                    return result;
                }

                List<ms_DynamicSlotting> dynamicSlotting = db.ms_DynamicSlotting.Where(c => c.Product_Index == product.Product_Index && c.Crane_Name == data.Crane_Name  && c.IsActive == 1).ToList();

                if (dynamicSlotting.Count > 1)
                {
                    result.ResultIsUse = false;
                    result.ResultMsg = "Config product already";
                    return result;
                }

                MS_RuleputawayCondition ruleputawayCondition = db.MS_RuleputawayCondition.FirstOrDefault(c => c.RuleputawayCondition_Param == product.Product_Id);

                if (ruleputawayCondition == null)
                {
                    result.ResultIsUse = false;
                    result.ResultMsg = "Config putaway not found";
                    return result;
                }

                MS_Zoneputaway zoneputaway = db.MS_Zoneputaway.FirstOrDefault(c => c.Zoneputaway_Index == ruleputawayCondition.Zoneputaway_Index);

                if (zoneputaway == null)
                {
                    result.ResultIsUse = false;
                    result.ResultMsg = "Config zoneputaway not found";
                    return result;
                }

                ms_DynamicSlotting model = new ms_DynamicSlotting {
                    DynamicSlotting_Index = Guid.NewGuid(),
                    DynamicSlotting_Id = "DynamicSlotting_Id".genAutonumber(),
                    DynamicSlotting_Remark = data.DynamicSlotting_Remark,
                    Crane_Name = data.Crane_Name,
                    Product_Index = product.Product_Index,
                    Product_Id = product.Product_Id,
                    Product_Name = product.Product_Name,
                    Zoneputaway_Index = zoneputaway.Zoneputaway_Index,
                    Zoneputaway_Id = zoneputaway.Zoneputaway_Id,
                    Zoneputaway_Name = zoneputaway.Zoneputaway_Name,
                    Trigger_Time = null,
                    Trigger_Time_End = null,
                    Trigger_Date = null,
                    Trigger_Date_End = null,
                    IsMonday = data.IsMonday,
                    IsTuesday = data.IsTuesday,
                    IsWednesday = data.IsWednesday,
                    IsThursday = data.IsThursday,
                    IsFriday = data.IsFriday,
                    IsSaturday = data.IsSaturday,
                    IsSunday = data.IsSunday,
                    Plan_By_Product = null,
                    Plan_By_Location = null,
                    Plan_By_Status = null,
                    IsActive = 1,
                    IsDelete = 0,
                    IsSystem = 0,
                    Status_Id = 1,
                    Create_By = data.Create_By,
                    Create_Date = DateTime.Now,
                    Update_By = null,
                    Update_Date = null,
                    Cancel_By = null,
                    Cancel_Date = null,
                    Last_Trigger_Date = null
                };
                db.ms_DynamicSlotting.Add(model);

                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                    result.ResultIsUse = true;
                }

                catch (Exception exy)
                {
                    transactionx.Rollback();

                    result.ResultIsUse = false;
                    result.ResultMsg = exy.Message;
                    return result;
                }


                return result;
            }
            catch (Exception ex)
            {
                result.ResultIsUse = false;
                result.ResultMsg = ex.Message;
                return result;
            }
        }

        public Result deleteDynamicSlotting(DynamicSlottingViewModel data)
        {
            logtxt logtxt = new logtxt();
            Result result = new Result();
            try
            {
                ms_DynamicSlotting dynamicSlotting = db.ms_DynamicSlotting.FirstOrDefault(c => c.DynamicSlotting_Index == data.DynamicSlotting_Index && c.IsActive == 1);

                if (dynamicSlotting != null)
                {
                    dynamicSlotting.IsActive = 0;
                    dynamicSlotting.IsDelete = 1;
                    dynamicSlotting.Cancel_By = data.Update_By;
                    dynamicSlotting.Cancel_Date = DateTime.Now;
                }
                else {
                    result.ResultIsUse = false;
                    result.ResultMsg = "ไม่สามารภยกเลิกได้";
                    return result;
                }

                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                    result.ResultIsUse = true;
                }

                catch (Exception exy)
                {
                    transactionx.Rollback();

                    result.ResultIsUse = false;
                    result.ResultMsg = exy.Message;
                    return result;
                }


                return result;
            }
            catch (Exception ex)
            {
                result.ResultIsUse = false;
                result.ResultMsg = ex.Message;
                return result;
            }
        }

    }
}
