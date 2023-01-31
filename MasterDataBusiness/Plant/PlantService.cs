using Comone.Utils;
using DataAccess;
using MasterDataAPI.Controllers;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MasterDataBusiness
{
    public class PlantService
    {
        private MasterDataDbContext db;

        public PlantService()
        {
            db = new MasterDataDbContext();
        }

        public PlantService(MasterDataDbContext db)
        {
            this.db = db;
        }
        #region FindPlant
        public PlantViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.ms_Plant.Where(c => c.Plant_Index == id).FirstOrDefault();
                var plantType = db.ms_PlantType.Where(c => c.PlantType_Index == queryResult.PlantType_Index).FirstOrDefault();
                var result = new PlantViewModel();

                result.plant_Index = queryResult.Plant_Index;
                result.plant_Id = queryResult.Plant_Id;
                result.plantType_Index = queryResult.PlantType_Index;
                result.plant_Name = queryResult.Plant_Name;

                result.plantType_Id = plantType.PlantType_Id;
                result.plantType_Name = plantType.PlantType_Name;

                result.ref_No1 = queryResult.Ref_No1;
                result.ref_No2 = queryResult.Ref_No2;
                result.ref_No3 = queryResult.Ref_No3;
                result.ref_No4 = queryResult.Ref_No4;
                result.ref_No5 = queryResult.Ref_No5;
    
                result.udf_1 = queryResult.UDF_1;
                result.udf_2 = queryResult.UDF_2;
                result.udf_3 = queryResult.UDF_3;
                result.udf_4 = queryResult.UDF_4;
                result.udf_5 = queryResult.UDF_5;
                result.isActive = queryResult.IsActive;

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FilterPlant
        public actionResultPlantViewModel filterPlant(SearchPlantViewModel data)
        {
            try
            {
                var query = db.ms_Plant.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Plant_Id.Contains(data.key)
                                        || c.Plant_Name.Contains(data.key));


                }

                
                var Item = new List<ms_Plant>();
                var TotalRow = new List<ms_Plant>();
                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.Plant_Id).ToList();

                var result = new List<SearchPlantViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchPlantViewModel();


                    resultItem.plant_Index = item.Plant_Index;
                    resultItem.plant_Id = item.Plant_Id;
                    resultItem.plant_Name = item.Plant_Name;
                    resultItem.ref_No1 = item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3;
                    resultItem.ref_No4 = item.Ref_No4;
                    resultItem.ref_No5 = item.Ref_No5;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.udf_5 = item.UDF_5;
                    resultItem.isActive = item.IsActive;
                    resultItem.isDelete = item.IsDelete;
                    resultItem.create_By = item.Create_By;
                    resultItem.activeStatus = item.IsActive == 1 ? "Active" : "Not Active";
                    //resultItem.create_Date = item.Create_Date;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.update_By = item.Update_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""; 
                    resultItem.cancel_By = item.Cancel_By;
                    resultItem.cancel_Date = item.Cancel_Date != null ? item.Cancel_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""; 
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultPlantViewModel = new actionResultPlantViewModel();
                actionResultPlantViewModel.itemsPlant = result.ToList();
                actionResultPlantViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultPlantViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetDeleteShipTo

        public Boolean getDelete(PlantViewModel data)
        {
            String State = "Start";
            String msglog = "";
            //var olog = new logtxt();

            try
            {
                var plant = db.ms_Plant.Find(data.plant_Index);

                if (plant != null)
                {
                    plant.IsActive = 0;
                    plant.IsDelete = 1;
                    plant.Cancel_By = data.cancel_By;
                    plant.Cancel_Date = DateTime.Now;
                   


                    var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                    try
                    {
                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }

                    catch (Exception exy)
                    {
                        msglog = State + " ex Rollback " + exy.Message.ToString();
                        //olog.logging("DeleteShipTo", msglog);
                        transaction.Rollback();
                        throw exy;
                    }

                }


                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region SaveChanges
        public String SaveChanges(PlantViewModel data)
        {
            String State = "Start";
            String msglog = "";
            //var olog = new logtxt();

            try
            {

                var PlantOld = db.ms_Plant.Find(data.plant_Index);

                if (PlantOld == null)
                {
                    if (!string.IsNullOrEmpty(data.plant_Id))
                    {
                        var query = db.ms_Plant.FirstOrDefault(c => c.Plant_Id == data.plant_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    //data.soldToShipTo_Id = "SoldToShipTo_Id".genAutonumber();

                    ms_Plant Model = new ms_Plant();

                    Model.Plant_Index = Guid.NewGuid();
                    Model.Plant_Id = data.plant_Id;
                    Model.PlantType_Index = data.plantType_Index.sParse<Guid>();
                    Model.Plant_Name = data.plant_Name;
                    Model.Ref_No1 = data.ref_No1;
                    Model.Ref_No2 = data.ref_No2;
                    Model.Ref_No3 = data.ref_No3;
                    Model.Ref_No4 = data.ref_No4;
                    Model.Ref_No5 = data.ref_No5;
                    Model.UDF_1 = data.udf_1;
                    Model.UDF_2 = data.udf_2;
                    Model.UDF_3 = data.udf_3;
                    Model.UDF_4 = data.udf_4;
                    Model.UDF_5 = data.udf_5;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;
                   
                    db.ms_Plant.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.plant_Id))
                    {
                        if (PlantOld.Plant_Id != "")
                        {
                            data.plant_Id = PlantOld.Plant_Id;
                        }
                    }
                    else
                    {
                        if (PlantOld.Plant_Id != data.plant_Id)
                        {
                            var query = db.ms_Plant.FirstOrDefault(c => c.Plant_Id == data.plant_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.plant_Id = PlantOld.Plant_Id;
                        }
                    }
                    PlantOld.Plant_Id = data.plant_Id;
                    PlantOld.Plant_Name = data.plant_Name;
                    PlantOld.PlantType_Index = data.plantType_Index.sParse<Guid>();
                    PlantOld.Plant_Name = data.plant_Name;
                    PlantOld.Ref_No1 = data.ref_No1;
                    PlantOld.Ref_No2 = data.ref_No2;
                    PlantOld.Ref_No3 = data.ref_No3;
                    PlantOld.Ref_No4 = data.ref_No4;
                    PlantOld.Ref_No5 = data.ref_No5;
                    PlantOld.UDF_1 = data.udf_1;
                    PlantOld.UDF_2 = data.udf_2;
                    PlantOld.UDF_3 = data.udf_3;
                    PlantOld.UDF_4 = data.udf_4;
                    PlantOld.UDF_5 = data.udf_5;
                    PlantOld.IsActive = Convert.ToInt32(data.isActive);
                    PlantOld.Update_By = data.create_By;
                    PlantOld.Update_Date = DateTime.Now;
                }

                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    //olog.logging("SaveSoldToShipTo", msglog);
                    transactionx.Rollback();

                    throw exy;
                }

                return "Done"; ;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoPlant
        public List<ItemListViewModel> autoSearchPlantFilter(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.ms_Plant.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Plant_Id.Contains(data.key)
                                                || c.Plant_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.Plant_Index, c.Plant_Id, c.Plant_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Plant_Index,
                            id = item.Plant_Id,
                            name = item.Plant_Name
                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoPlantType
        public List<ItemListViewModel> autoPlantType(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.ms_PlantType.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.PlantType_Id.Contains(data.key)
                                                || c.PlantType_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.PlantType_Name, c.PlantType_Index, c.PlantType_Id }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.PlantType_Index,
                            id = item.PlantType_Id,
                            name = item.PlantType_Id + " - " + item.PlantType_Name,
                            key = item.PlantType_Id + " - " + item.PlantType_Name,
                        };

                        items.Add(resultItem);
                    }
                    return items;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Export Excel
        public ResultPlantViewModel Export(PlantExportViewModel data)
        {
            try
            {
                var query = db.ms_Plant.AsQueryable();
                
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Plant_Id.Contains(data.key)
                                        || c.Plant_Name.Contains(data.key));

                }

                var Item = new List<ms_Plant>();
                var TotalRow = new List<ms_Plant>();

                TotalRow = query.ToList();

                Item = query.OrderBy(o => o.Plant_Id).ToList();

                var result = new List<PlantExportViewModel>();
                //var num = 0;
                int num = 0;
                foreach (var item in Item)
                {
                        var resultItem = new PlantExportViewModel();
                        resultItem.numBerOf = num + 1;
                        resultItem.plant_Index = item.Plant_Index;
                        resultItem.plant_Id = item.Plant_Id;
                        resultItem.plant_Name = item.Plant_Name;
                        resultItem.ref_No1 = item.Ref_No1;
                        resultItem.ref_No2 = item.Ref_No2;
                        resultItem.ref_No3 = item.Ref_No3;
                        resultItem.ref_No4 = item.Ref_No4;
                        resultItem.ref_No5 = item.Ref_No5;
                        resultItem.udf_1 = item.UDF_1;
                        resultItem.udf_2 = item.UDF_2;
                        resultItem.udf_3 = item.UDF_3;
                        resultItem.udf_4 = item.UDF_4;
                        resultItem.udf_5 = item.UDF_5;
                        resultItem.isActive = item.IsActive;
                        resultItem.isDelete = item.IsDelete;
                        resultItem.activeStatus = item.IsActive == 1 ? "เปิดใช้งาน" : "ปิดใช้งาน";
                        resultItem.create_By = item.Create_By;
                        //resultItem.create_Date = item.Create_Date;
                        resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                        resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                        resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                        resultItem.cancel_By = item.Cancel_By == null ? "" : item.Cancel_By;
                        resultItem.cancel_Date = item.Cancel_Date != null ? item.Cancel_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                        result.Add(resultItem);
                        num++;
                   
                    
                }

                var count = TotalRow.Count;

                var plantExportViewModel = new ResultPlantViewModel();
                plantExportViewModel.itemsPlant = result.ToList();

                return plantExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
