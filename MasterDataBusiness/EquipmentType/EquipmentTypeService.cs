using Comone.Utils;
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

namespace MasterDataBusiness
{
    public class EquipmentTypeService
    {
        #region FindEquipmentType
        public EquipmentTypeViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_EquipmentType.Where(c => c.EquipmentType_Index == id).FirstOrDefault();

                var result = new EquipmentTypeViewModel();


                result.equipmentType_Id = queryResult.EquipmentType_Id;
                result.equipmentType_Name = queryResult.EquipmentType_Name;
                result.equipmentType_Index = queryResult.EquipmentType_Index;
                result.isActive = queryResult.IsActive;


                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region FilterEquipmentType
        //Filter
        private MasterDataDbContext db;

        public EquipmentTypeService()
        {
            db = new MasterDataDbContext();
        }

        public EquipmentTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        
        public actionResultEquipmentTypeViewModel filter(SearchEquipmentTypeViewModel data)
        {
            try
            {
                var query = db.MS_EquipmentType.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.EquipmentType_Name.Contains(data.key)
                                        || c.EquipmentType_Id.Contains(data.key));


                }

                var Item = new List<MS_EquipmentType>();
                var TotalRow = new List<MS_EquipmentType>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.EquipmentType_Id).ToList();

                var result = new List<SearchEquipmentTypeViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchEquipmentTypeViewModel();

                    resultItem.equipmentType_Index = item.EquipmentType_Index;
                    resultItem.equipmentType_Id = item.EquipmentType_Id;
                    resultItem.equipmentType_Name = item.EquipmentType_Name;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultEquipmentTypeViewModel = new actionResultEquipmentTypeViewModel();
                actionResultEquipmentTypeViewModel.itemsEquipmentType = result.ToList();
                actionResultEquipmentTypeViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, };

                return actionResultEquipmentTypeViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetDelete

        public Boolean getDelete(EquipmentTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var equipmentType = db.MS_EquipmentType.Find(data.equipmentType_Index);

                if (equipmentType != null)
                {
                    equipmentType.IsActive = 0;
                    equipmentType.IsDelete = 1;


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
                        olog.logging("DeleteEquipmentType", msglog);
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

        #region SaveChangesEquipmentType

        public String SaveChanges(EquipmentTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var EquipmentTypeOld = db.MS_EquipmentType.Find(data.equipmentType_Index);

                if (EquipmentTypeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.equipmentType_Id))
                    {
                        var query = db.MS_EquipmentType.FirstOrDefault(c => c.EquipmentType_Id == data.equipmentType_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.equipmentType_Id))
                    {
                        data.equipmentType_Id = "EquipmentType_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_EquipmentType.FirstOrDefault(c => c.EquipmentType_Id == data.equipmentType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.EquipmentType_Id == data.equipmentType_Id)
                                {
                                    data.equipmentType_Id = "EquipmentType_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //data.equipmentType_Id = "EquipmentType_Id".genAutonumber();

                    MS_EquipmentType Model = new MS_EquipmentType();

                    Model.EquipmentType_Index = Guid.NewGuid();
                    Model.EquipmentType_Id = data.equipmentType_Id;
                    Model.EquipmentType_Name = data.equipmentType_Name;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_EquipmentType.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.equipmentType_Id))
                    {
                        if (EquipmentTypeOld.EquipmentType_Id != "")
                        {
                            data.equipmentType_Id = EquipmentTypeOld.EquipmentType_Id;
                        }
                    }
                    else
                    {
                        if (EquipmentTypeOld.EquipmentType_Id != data.equipmentType_Id)
                        {
                            var query = db.MS_EquipmentType.FirstOrDefault(c => c.EquipmentType_Id == data.equipmentType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.equipmentType_Id = EquipmentTypeOld.EquipmentType_Id;
                        }
                    }
                    EquipmentTypeOld.EquipmentType_Id = data.equipmentType_Id;
                    EquipmentTypeOld.EquipmentType_Name = data.equipmentType_Name;
                    EquipmentTypeOld.IsActive = Convert.ToInt32(data.isActive);
                    EquipmentTypeOld.Update_By = data.update_By;
                    EquipmentTypeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveEquipmentType", msglog);
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

        #region SearchEquipmentType

        public List<ItemListViewModel> autoEquipmentType(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_EquipmentType.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.EquipmentType_Id.Contains(data.key)
                                                || c.EquipmentType_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.EquipmentType_Name, c.EquipmentType_Index, c.EquipmentType_Id }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.EquipmentType_Index,
                            id = item.EquipmentType_Id,
                            name = item.EquipmentType_Id + " - " + item.EquipmentType_Name,
                            key = item.EquipmentType_Id + " - " + item.EquipmentType_Name,
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


        #region SearchEquipmentTypeFilter

        public List<ItemListViewModel> AutoSearchEquipmentTypeFilter(ItemListViewModel data)
        {
                var items = new List<ItemListViewModel>();
                try
                {
                    if (!string.IsNullOrEmpty(data.key))
                    {
                        var query1 = db.MS_EquipmentType.Where(c => c.EquipmentType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                        {
                            name = s.EquipmentType_Name,
                            key = s.EquipmentType_Name
                        }).Distinct();

                        var query2 = db.MS_EquipmentType.Where(c => c.EquipmentType_Id.Contains(data.key)).Select(s => new ItemListViewModel
                        {
                            name = s.EquipmentType_Id,
                            key = s.EquipmentType_Id
                        }).Distinct();
                        var query = query1.Union(query2).Union(query2);

                        items = query.OrderBy(c => c.name).Take(10).ToList();
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion
    }
}

