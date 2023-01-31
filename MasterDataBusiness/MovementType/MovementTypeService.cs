using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.CostCenter;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MasterDataBusiness.MovementType
{
    public class MovementTypeService
    {
        private MasterDataDbContext db;

        public MovementTypeService()
        {
            db = new MasterDataDbContext();
        }

        public MovementTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region FilterMovementType
        //Filter
        public actionResultMovementTypeViewModel movementTypefilter(SearchMovementTypeViewModel data)
        {
            try
            {
                var query = db.ms_MovementType.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.MovementType_Name.Contains(data.key)
                                        || c.MovementType_Id.Contains(data.key));


                }

                var Item = new List<ms_MovementType>();
                var TotalRow = new List<ms_MovementType>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.MovementType_Id).ToList();

                var result = new List<SearchMovementTypeViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchMovementTypeViewModel();

                    resultItem.movementType_Index = item.MovementType_Index;
                    resultItem.movementType_Id = item.MovementType_Id;
                    resultItem.movementType_Name = item.MovementType_Name;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultMovementTypeViewModel = new actionResultMovementTypeViewModel();
                actionResultMovementTypeViewModel.itemsMovementType = result.ToList();
                actionResultMovementTypeViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultMovementTypeViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region findMovementType
        public MovementTypeViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.ms_MovementType.Where(c => c.MovementType_Index == id).FirstOrDefault();

                var result = new MovementTypeViewModel();


                result.movementType_Index = queryResult.MovementType_Index;
                result.movementType_Name = queryResult.MovementType_Name;
                result.movementType_Id = queryResult.MovementType_Id;
                result.isActive = queryResult.IsActive;


                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetDeleteMovementType
        public Boolean getDelete(MovementTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var costCenter = db.ms_MovementType.Find(data.movementType_Index);

                if (costCenter != null)
                {
                    costCenter.IsActive = 0;
                    costCenter.IsDelete = 1;


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
                        olog.logging("DeleteMovementType", msglog);
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

        #region SaveChangesMovementType

        public String SaveChanges(MovementTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var MovementTypeOld = db.ms_MovementType.Find(data.movementType_Index);

                if (MovementTypeOld == null)
                {
                    if (!string.IsNullOrEmpty(data.movementType_Id))
                    {
                        var query = db.ms_MovementType.FirstOrDefault(c => c.MovementType_Id == data.movementType_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.movementType_Id))
                    {
                        data.movementType_Id = "MovementType_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.ms_MovementType.FirstOrDefault(c => c.MovementType_Id == data.movementType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.MovementType_Id == data.movementType_Id)
                                {
                                    data.movementType_Id = "MovementType_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //data.movementType_Id = "MovementType_Id".genAutonumber();

                    ms_MovementType Model = new ms_MovementType();
                    Model.MovementType_Index = Guid.NewGuid();
                    Model.MovementType_Id = data.movementType_Id;
                    Model.MovementType_Name = data.movementType_Name;
                    Model.IsActive = 1;
                    Model.IsDelete = 0;
                    Model.IsSystem = 1;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.ms_MovementType.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.movementType_Id))
                    {
                        if (MovementTypeOld.MovementType_Id != "")
                        {
                            data.movementType_Id = MovementTypeOld.MovementType_Id;
                        }
                    }
                    else
                    {
                        if (MovementTypeOld.MovementType_Id != data.movementType_Id)
                        {
                            var query = db.ms_MovementType.FirstOrDefault(c => c.MovementType_Id == data.movementType_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.movementType_Id = MovementTypeOld.MovementType_Id;
                        }
                    }
                    MovementTypeOld.MovementType_Id = data.movementType_Id;
                    MovementTypeOld.MovementType_Index = data.movementType_Index;
                    MovementTypeOld.MovementType_Name = data.movementType_Name;
                    MovementTypeOld.IsActive = Convert.ToInt32(data.isActive);
                    MovementTypeOld.Update_By = data.update_By;
                    MovementTypeOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveMovementType" +
                        "", msglog);
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

        #region SearchMovementTypeFilter

        public List<ItemListViewModel> autoSearchMovementTypeFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.ms_MovementType.Where(c => c.MovementType_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.MovementType_Id,
                        key = s.MovementType_Id
                    }).Distinct();

                    var query2 = db.ms_MovementType.Where(c => c.MovementType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.MovementType_Name,
                        key = s.MovementType_Name
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
