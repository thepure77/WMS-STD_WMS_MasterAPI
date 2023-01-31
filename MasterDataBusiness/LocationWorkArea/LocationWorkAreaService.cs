using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.LocationWorkArea;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static MasterDataBusiness.LocationWorkArea.SearchLocationWorkAreaViewModel;

namespace MasterDataBusiness
{
    public class LocationWorkAreaService
    {
        private MasterDataDbContext db;

        public LocationWorkAreaService()
        {
            db = new MasterDataDbContext();
        }

        public LocationWorkAreaService(MasterDataDbContext db)
        {
            this.db = db;
        }
        #region BeforeCodeLocationWorkArea
        //public List<LocationWorkAreaViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {

        //            var queryResult = context.View_GetLocationWorkArea.FromSql("sp_GetLocationWorkArea").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<LocationWorkAreaViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new LocationWorkAreaViewModel();

        //                resultItem.LocationWorkAreaIndex = item.LocationWorkArea_Index;
        //                resultItem.LocationWorkAreaId = item.LocationWorkArea_Id;
        //                resultItem.LocationIndex = item.Location_Index;
        //                resultItem.LocationName = item.Location_Name;
        //                resultItem.WorkAreaIndex = item.WorkArea_Index;
        //                resultItem.WorkAreaName = item.WorkArea_Name;
        //                resultItem.IsActive = item.IsActive;
        //                resultItem.IsDelete = item.IsDelete;
        //                resultItem.StatusId = item.Status_Id;
        //                resultItem.IsSystem = item.IsSystem;
        //                resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                resultItem.CreateBy = item.Create_By;
        //                resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                resultItem.UpdateBy = item.Update_By;
        //                resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                resultItem.CancelBy = item.Cancel_By;

        //                result.Add(resultItem);
        //            }

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public String SaveChanges(LocationWorkAreaViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {

        //            int isactive = 1;
        //            int isdelete = 0;
        //            int issystem = 0;
        //            int statusid = 0;

        //            if (data.LocationWorkAreaIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.LocationWorkAreaIndex = Guid.NewGuid();
        //            }
        //            if (data.LocationWorkAreaId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "LocationWorkAreaId");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.LocationWorkAreaId = resultParameter.Value.ToString();
        //            }
        //            var LocationWorkArea_Index = new SqlParameter("LocationWorkArea_Index", data.LocationWorkAreaIndex);
        //            var LocationWorkArea_Id = new SqlParameter("LocationWorkArea_Id", data.LocationWorkAreaId);
        //            var Location_Index = new SqlParameter("Location_Index", data.LocationIndex);
        //            var WorkArea_Index = new SqlParameter("WorkArea_Index", data.WorkAreaIndex);
        //            var IsActive = new SqlParameter("IsActive", isactive);
        //            var IsDelete = new SqlParameter("IsDelete", isdelete);
        //            var Status_Id = new SqlParameter("Status_Id", issystem);
        //            var IsSystem = new SqlParameter("IsSystem", statusid);
        //            var Create_By = new SqlParameter("Create_By", "");
        //            var Create_Date = new SqlParameter("Create_Date", DateTime.Now);
        //            var Update_By = new SqlParameter("Update_By", "");
        //            var Update_Date = new SqlParameter("Update_Date", DateTime.Now);
        //            var Cancel_By = new SqlParameter("Cancel_By", "");
        //            var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now);
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_LocationWorkArea  @LocationWorkArea_Index,@LocationWorkArea_Id,@Location_Index,@WorkArea_Index,@IsActive,@IsDelete,@Status_Id,@IsSystem,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", LocationWorkArea_Index, LocationWorkArea_Id, Location_Index, WorkArea_Index, IsActive, IsDelete, Status_Id, IsSystem, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<LocationWorkAreaViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {

        //            var strwhere = ("@strwhere", " and LocationWorkArea_Index = '" + id + "'");
        //            var queryResult = context.MS_LocationWorkArea.FromSql("sp_GetLocationWorkArea @strwhere",strwhere).ToList();

        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<LocationWorkAreaViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var LocationWorkArea_Index = new SqlParameter("LocationWorkArea_Index", item.LocationWorkArea_Index);
        //                var LocationWorkArea_Id = new SqlParameter("LocationWorkArea_Id", item.LocationWorkArea_Id);
        //                var Location_Index = new SqlParameter("Location_Index", item.Location_Index);
        //                var WorkArea_Index = new SqlParameter("WorkArea_Index", item.WorkArea_Index);
        //                var IsActive = new SqlParameter("IsActive", isactive);
        //                var IsDelete = new SqlParameter("IsDelete", isdelete);
        //                var Status_Id = new SqlParameter("Status_Id", item.Status_Id);
        //                var IsSystem = new SqlParameter("IsSystem", item.IsSystem);
        //                var Create_By = new SqlParameter("Create_By", "");
        //                var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
        //                var Update_By = new SqlParameter("Update_By", "");
        //                var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
        //                var Cancel_By = new SqlParameter("Cancel_By", "");
        //                var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_LocationWorkArea  @LocationWorkArea_Index,@LocationWorkArea_Id,@Location_Index,@WorkArea_Index,@IsActive,@IsDelete,@Status_Id,@IsSystem,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", LocationWorkArea_Index, LocationWorkArea_Id, Location_Index, WorkArea_Index, IsActive, IsDelete, Status_Id, IsSystem, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //                context.SaveChanges();
        //            }

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<LocationWorkAreaViewModelPagination> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var strwhere = new SqlParameter("@strwhere", " and LocationWorkArea_Index = '" + id + "'");
        //            var queryResult = context.View_GetLocationWorkArea.FromSql("sp_GetLocationWorkAreaPagination @strwhere", strwhere).ToList();

        //            var result = new List<LocationWorkAreaViewModelPagination>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new LocationWorkAreaViewModelPagination();
        //                resultItem.LocationWorkAreaIndex = item.LocationWorkArea_Index;
        //                resultItem.LocationWorkAreaId = item.LocationWorkArea_Id;
        //                resultItem.LocationIndex = item.Location_Index;
        //                resultItem.LocationName = item.Location_Name;
        //                resultItem.WorkAreaIndex = item.WorkArea_Index;
        //                resultItem.WorkAreaName = item.WorkArea_Name;
        //                resultItem.IsActive = item.IsActive;
        //                resultItem.IsDelete = item.IsDelete;
        //                resultItem.StatusId = item.Status_Id;
        //                resultItem.IsSystem = item.IsSystem;
        //                resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                resultItem.CreateBy = item.Create_By;
        //                resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                resultItem.UpdateBy = item.Update_By;
        //                resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                resultItem.CancelBy = item.Cancel_By;
        //                result.Add(resultItem);
        //            }

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public actionResultViewModel search(LocationWorkAreaViewModelPagination data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pstring = "";

        //            if(!string.IsNullOrEmpty(data.LocationWorkAreaId))
        //            {
        //                pstring += " And LocationWorkArea_Id like N'%" + data.LocationWorkAreaId + "%'";
        //            }
        //            else if(!string.IsNullOrEmpty(data.LocationName))
        //            {
        //                pstring += " And Location_Name like N'%" + data.LocationName + "%'";
        //            }
        //            else if(!string.IsNullOrEmpty(data.WorkAreaName))
        //            {
        //                pstring += " And WorkArea_Name like N'%" + data.WorkAreaName + "%'";
        //            }

        //            pstring += " And isActive = '" + 1 + "'";
        //            pstring += " And isDelete = '" + 0 + "'";                  

        //            var strwhere = new SqlParameter("@strwhere", pstring);
        //            var PageNumber = new SqlParameter("@PageNumber", 1);
        //            var RowspPage = new SqlParameter("@RowspPage", 10000);
        //            var queryResultTotal = context.View_GetLocationWorkArea.FromSql("sp_GetLocationWorkAreaPagination @strwhere , @PageNumber , @RowspPage ", strwhere, PageNumber, RowspPage).ToList();

        //            var strwhere1 = new SqlParameter("@strwhere", pstring);
        //            var PageNumber1 = new SqlParameter("@PageNumber", data.CurrentPage);
        //            var RowspPage1 = new SqlParameter("@RowspPage", data.PerPage);
        //            var query = context.View_GetLocationWorkArea.FromSql("sp_GetLocationWorkAreaPagination @strwhere , @PageNumber , @RowspPage ", strwhere, PageNumber1, RowspPage1).ToList();

        //            var result = new List<LocationWorkAreaViewModelPagination>();
        //            foreach (var item in query)
        //            {
        //                var resultItem = new LocationWorkAreaViewModelPagination();


        //                resultItem.LocationWorkAreaIndex = item.LocationWorkArea_Index;
        //                resultItem.LocationWorkAreaId = item.LocationWorkArea_Id;
        //                resultItem.LocationIndex = item.Location_Index;
        //                resultItem.LocationName = item.Location_Name;
        //                resultItem.WorkAreaIndex = item.WorkArea_Index;
        //                resultItem.WorkAreaName = item.WorkArea_Name;
        //                resultItem.IsActive = item.IsActive;
        //                resultItem.IsDelete = item.IsDelete;
        //                resultItem.StatusId = item.Status_Id;
        //                resultItem.IsSystem = item.IsSystem;
        //                resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                resultItem.CreateBy = item.Create_By;
        //                resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                resultItem.UpdateBy = item.Update_By;
        //                resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                resultItem.CancelBy = item.Cancel_By;
        //                result.Add(resultItem);

        //            }

        //            var count = queryResultTotal.Count;
        //            var actionResult = new actionResultViewModel();
        //            actionResult.items = result.ToList();
        //            actionResult.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage };

        //            return actionResult;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion

        #region FindLocationWorkArea

        public LocationWorkAreaViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_LocationWorkArea.Where(c => c.LocationWorkArea_Index == id).FirstOrDefault();

                var result = new LocationWorkAreaViewModel();


                result.locationWorkArea_Index = queryResult.LocationWorkArea_Index;
                result.locationWorkArea_Id = queryResult.LocationWorkArea_Id;
                result.location_Index = queryResult.Location_Index;
                result.location_Name = queryResult.Location_Name;
                result.workArea_Index = queryResult.WorkArea_Index;
                result.workArea_Name = queryResult.WorkArea_Name;
                result.isActive = queryResult.IsActive;


                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region FilterLocationWorkArea
        //Filter

        public actionResultLocationWorkAreaViewModel filter(SearchLocationWorkAreaViewModel data)
        {
            try
            {
                var query = db.View_LocationWorkArea.AsQueryable();
                query = query.Where(c =>  c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Location_Name.Contains(data.key)
                                        || c.WorkArea_Name.Contains(data.key)
                                        || c.LocationWorkArea_Id.Contains(data.key));


                }

                var Item = new List<View_LocationWorkArea>();
                var TotalRow = new List<View_LocationWorkArea>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.LocationWorkArea_Id).ToList();

                var result = new List<SearchLocationWorkAreaViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchLocationWorkAreaViewModel();

                    resultItem.locationWorkArea_Index = item.LocationWorkArea_Index;
                    resultItem.locationWorkArea_Id = item.LocationWorkArea_Id;
                    resultItem.location_Index = item.Location_Index;
                    resultItem.location_Name = item.Location_Name;
                    resultItem.workArea_Index = item.WorkArea_Index;
                    resultItem.workArea_Name = item.WorkArea_Name;


                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultLocationWorkAreaViewModel = new actionResultLocationWorkAreaViewModel();
                actionResultLocationWorkAreaViewModel.itemsLocationWorkArea = result.ToList();
                actionResultLocationWorkAreaViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultLocationWorkAreaViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetDelete
        public Boolean getDelete(LocationWorkAreaViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var locationWorkArea = db.MS_LocationWorkArea.Find(data.locationWorkArea_Index);

                if (locationWorkArea != null)
                {
                    locationWorkArea.IsActive = 0;
                    locationWorkArea.IsDelete = 1;


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
                        olog.logging("DeleteLocationWorkArea", msglog);
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

        public String SaveChanges(LocationWorkAreaViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var LocationWorkAreaOld = db.MS_LocationWorkArea.Find(data.locationWorkArea_Index);

                if (LocationWorkAreaOld == null)
                {
                    if (!string.IsNullOrEmpty(data.locationWorkArea_Id))
                    {
                        var query = db.MS_LocationWorkArea.FirstOrDefault(c => c.LocationWorkArea_Id == data.locationWorkArea_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.locationWorkArea_Id))
                    {
                        data.locationWorkArea_Id = "LocationWorkArea_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_LocationWorkArea.FirstOrDefault(c => c.LocationWorkArea_Id == data.locationWorkArea_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.LocationWorkArea_Id == data.locationWorkArea_Id)
                                {
                                    data.locationWorkArea_Id = "LocationWorkArea_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //data.locationWorkArea_Id = "LocationWorkArea_Id".genAutonumber();

                    //MS_LocationWorkArea Model = new MS_LocationWorkArea();

                    //Model.LocationWorkArea_Index = Guid.NewGuid();
                    //Model.LocationWorkArea_Id = data.locationWorkArea_Id;
                    //Model.Location_Index = data.location_Index;
                    //Model.WorkArea_Index = data.workArea_Index;
                    //Model.IsActive = 1;
                    //Model.IsDelete = 0;
                    //Model.IsSystem = 0;
                    //Model.Status_Id = 0;
                    //Model.Create_By = data.create_By;
                    //Model.Create_Date = DateTime.Now;

                    //db.MS_LocationWorkArea.Add(Model);

                    foreach (var item in data.listLocationWorkAreaItemViewModel)
                    {
                        MS_LocationWorkArea resultItem = new MS_LocationWorkArea();

                        resultItem.LocationWorkArea_Index = Guid.NewGuid();
                        resultItem.LocationWorkArea_Id = data.locationWorkArea_Id;
                        resultItem.Location_Index = data.location_Index;
                        resultItem.WorkArea_Index = item.workArea_Index;
                        resultItem.IsActive = 1;
                        resultItem.IsDelete = 0;
                        resultItem.IsSystem = 0;
                        resultItem.Status_Id = 0;
                        resultItem.Create_By = data.create_By;
                        resultItem.Create_Date = DateTime.Now;
                        db.MS_LocationWorkArea.Add(resultItem);

                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(data.locationWorkArea_Id))
                    {
                        if (LocationWorkAreaOld.LocationWorkArea_Id != "")
                        {
                            data.locationWorkArea_Id = LocationWorkAreaOld.LocationWorkArea_Id;
                        }
                    }
                    else
                    {
                        if (LocationWorkAreaOld.LocationWorkArea_Id != data.locationWorkArea_Id)
                        {
                            var query = db.MS_LocationWorkArea.FirstOrDefault(c => c.LocationWorkArea_Id == data.locationWorkArea_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.locationWorkArea_Id = LocationWorkAreaOld.LocationWorkArea_Id;
                        }
                    }
                    LocationWorkAreaOld.LocationWorkArea_Id = data.locationWorkArea_Id;
                    LocationWorkAreaOld.Location_Index = data.location_Index;
                    LocationWorkAreaOld.WorkArea_Index = data.workArea_Index;
                    LocationWorkAreaOld.IsActive = Convert.ToInt32(data.isActive);
                    LocationWorkAreaOld.Update_By = data.update_By;
                    LocationWorkAreaOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveLocationWorkArea", msglog);
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

        //public List<ItemListViewModel> autoSearch(ItemListViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())

        //        {
        //            var query = context.MS_LocationWorkArea.AsQueryable();

        //            if (!string.IsNullOrEmpty(data.key))
        //            {
        //                query = query.Where(c => c.LocationWorkArea_Id.Contains(data.key));
        //            }


        //            var items = new List<ItemListViewModel>();

        //            var result = query.Select(c => new { c.LocationWorkArea_Index, c.LocationWorkArea_Id}).Distinct().Take(10).ToList();

        //            foreach (var item in result)
        //            {
        //                var resultItem = new ItemListViewModel
        //                {
        //                    index = item.LocationWorkArea_Index,
        //                    id = item.LocationWorkArea_Id
        //                };

        //                items.Add(resultItem);
        //            }



        //            return items;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #region AutoSearchLocationWorkAreaFilter

        public List<ItemListViewModel> autoLocationSearchFilter(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_LocationWorkArea.Where(c => c.Location_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Location_Name,
                        key = s.Location_Name
                    }).Distinct();

                    var query2 = db.View_LocationWorkArea.Where(c => c.WorkArea_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.WorkArea_Name,
                        key = s.WorkArea_Name
                    }).Distinct();

                    var query3 = db.View_LocationWorkArea.Where(c => c.LocationWorkArea_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.LocationWorkArea_Id,
                        key = s.LocationWorkArea_Id
                    }).Distinct();



                    var query = query1.Union(query2).Union(query2).Union(query3);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {

            }

            return items;
        }
        #endregion

        #region FilterLocationWorkArea
        //Filter

        public actionResultLocationWorkAreaViewModel filterWorkArea(SearchLocationWorkAreaViewModel data)
        {
            try
            {
                var query = db.View_LocationWorkArea.AsQueryable();
                query = query.Where(c => c.Location_Index == data.location_Index && c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Location_Name.Contains(data.key)
                                        || c.WorkArea_Name.Contains(data.key)
                                        || c.LocationWorkArea_Id.Contains(data.key));


                }

                var Item = new List<View_LocationWorkArea>();
                var TotalRow = new List<View_LocationWorkArea>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.LocationWorkArea_Id).ToList();

                var result = new List<SearchLocationWorkAreaViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchLocationWorkAreaViewModel();

                    resultItem.locationWorkArea_Index = item.LocationWorkArea_Index;
                    resultItem.locationWorkArea_Id = item.LocationWorkArea_Id;
                    resultItem.location_Index = item.Location_Index;
                    resultItem.location_Id = item.Location_Id;
                    resultItem.workArea_Id = item.WorkArea_Id;
                    resultItem.location_Name = item.Location_Name;
                    resultItem.workArea_Index = item.WorkArea_Index;
                    resultItem.workArea_Name = item.WorkArea_Name;


                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultLocationWorkAreaViewModel = new actionResultLocationWorkAreaViewModel();
                actionResultLocationWorkAreaViewModel.itemsLocationWorkArea = result.ToList();
                actionResultLocationWorkAreaViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultLocationWorkAreaViewModel;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
