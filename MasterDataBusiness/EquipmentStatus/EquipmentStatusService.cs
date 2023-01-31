using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static MasterDataBusiness.ViewModels.SearchEquipmentViewModel;

namespace MasterDataBusiness
{
    public class EquipmentStatusService
    {
        private MasterDataDbContext db;

        public EquipmentStatusService()
        {
            db = new MasterDataDbContext();
        }

        public EquipmentStatusService(MasterDataDbContext db)
        {
            this.db = db;
        }


        #region FindEquipment
        public EquipmentStatusViewModel find()
        {
            EquipmentStatusViewModel result = new EquipmentStatusViewModel();
            try
            {

                List<ms_EquipmentAisle> equipmentAisles = db.ms_EquipmentAisle.ToList();

                #region Enable
                var equipmentAisles_enable = equipmentAisles.Where(c => c.IsActive == 1).GroupBy(c => new
                {
                    c.Equipment_Index,
                    c.Equipment_Id,
                    c.Equipment_Name,
                    c.Update_By
                }).Select(c=> new
                {
                    c.Key.Equipment_Index,
                    c.Key.Equipment_Id,
                    c.Key.Equipment_Name,
                    c.Key.Update_By
                }).OrderBy(c => c.Equipment_Id).ToList();

                foreach (var item in equipmentAisles_enable)
                {
                    var update_date = equipmentAisles.FirstOrDefault(c => c.Equipment_Index == item.Equipment_Index);
                    EquipmentStatusViewModel model = new EquipmentStatusViewModel();
                    model.Equipment_Index = item.Equipment_Index;
                    model.Equipment_Id = item.Equipment_Id;
                    model.Equipment_Name = item.Equipment_Name;
                    model.isUser = false;
                    model.LocationAisle_Name = string.Join(",", equipmentAisles.Where(c => c.Equipment_Index == item.Equipment_Index).OrderBy(c => c.LocationAisle_Name).Select(c => c.LocationAisle_Name));
                    model.Update_By = item.Update_By;
                    model.Update_Date = update_date.Update_Date;

                    result.Crane_enable.Add(model);
                }
                #endregion

                #region Disable
                var equipmentAisles_disable = equipmentAisles.Where(c => c.IsActive == 0).GroupBy(c => new
                {
                    c.Equipment_Index,
                    c.Equipment_Id,
                    c.Equipment_Name,
                    c.Update_By
                }).Select(c => new
                {
                    c.Key.Equipment_Index,
                    c.Key.Equipment_Id,
                    c.Key.Equipment_Name,
                    c.Key.Update_By
                }).OrderBy(c => c.Equipment_Id).ToList();

                foreach (var item in equipmentAisles_disable)
                {
                    var update_date = equipmentAisles.FirstOrDefault(c => c.Equipment_Index == item.Equipment_Index);
                    EquipmentStatusViewModel model = new EquipmentStatusViewModel();
                    model.Equipment_Index = item.Equipment_Index;
                    model.Equipment_Id = item.Equipment_Id;
                    model.Equipment_Name = item.Equipment_Name;
                    model.isUser = false;
                    model.LocationAisle_Name = string.Join(",", equipmentAisles.Where(c => c.Equipment_Index == item.Equipment_Index).OrderBy(c=> c.LocationAisle_Name).Select(c => c.LocationAisle_Name));
                    model.Update_By = item.Update_By;
                    model.Update_Date = update_date.Update_Date;

                    result.Crane_disable.Add(model);
                }
                #endregion

                result.ResultIsUse = true;
                return result;
            }
            catch (Exception ex)
            {
                result.ResultIsUse = false;
                result.ResultMsg = "ไม่สามารถค้นหาได้ : "+ex.Message;
                return result;
            }
        }

        #endregion


        #region update Crane
        public EquipmentStatusViewModel update_Crane(EquipmentStatusViewModel model)
        {
            EquipmentStatusViewModel result = new EquipmentStatusViewModel();
            try {

                var update_IsActive = model.Crane_enable.Where(c => c.isUser == true).GroupBy(c=> c.Equipment_Index).Select(c=> c.Key).ToList();
                var update_UnActive = model.Crane_disable.Where(c => c.isUser == true).GroupBy(c => c.Equipment_Index).Select(c => c.Key).ToList();

                List<ms_EquipmentAisle> equipmentAisles_IsActive = db.ms_EquipmentAisle.Where(c=> update_IsActive.Contains(c.Equipment_Index)).ToList();
                List<ms_EquipmentAisle> equipmentAisles_UnActive = db.ms_EquipmentAisle.Where(c => update_UnActive.Contains(c.Equipment_Index)).ToList();

                foreach (var item in equipmentAisles_IsActive)
                {
                    item.IsActive = 1;
                    item.Update_By = model.Update_By;
                    item.Update_Date = DateTime.Now;

                    var location_Aisle = new SqlParameter("@location_Aisle", item.LocationAisle_Name);
                    var roweffect_CreateScanIn = db.Database.ExecuteSqlCommand("EXEC sp_updateBlockPutlocation_IsActive @location_Aisle", location_Aisle);
                }

                foreach (var item in equipmentAisles_UnActive)
                {
                    item.IsActive = 0;
                    item.Update_By = model.Update_By;
                    item.Update_Date = DateTime.Now;

                    var location_Aisle = new SqlParameter("@location_Aisle", item.LocationAisle_Name);
                    var roweffect_CreateScanIn = db.Database.ExecuteSqlCommand("EXEC sp_updateBlockPutlocation_UnActive @location_Aisle", location_Aisle);
                    
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
                result.ResultMsg = "ไม่สามารถบันทึกได้ : " + ex.Message;
                return result;
            }
        }

        #endregion

    }
}
