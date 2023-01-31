using Comone.Utils;
using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.ViewModels;
using MasterDataBusiness.ZonePutaway;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MasterDataBusiness
{
    public class ZonePutawayService
    {
        private MasterDataDbContext db;

        public ZonePutawayService()
        {
            db = new MasterDataDbContext();
        }

        public ZonePutawayService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterZone
        public actionResultZonePutawayViewModel filter(SearchZonePutawayViewModel data)
        {
            try
            {
                var query = db.MS_Zoneputaway.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Zoneputaway_Id.Contains(data.key)
                                         || c.Zoneputaway_Name.Contains(data.key));
                }
                if (!string.IsNullOrEmpty(data.create_date) && !string.IsNullOrEmpty(data.create_date_to))
                {
                    var dateStart = data.create_date.toBetweenDate();
                    var dateEnd = data.create_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);
                }
                var Item = new List<MS_Zoneputaway>();
                var TotalRow = new List<MS_Zoneputaway>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.Zoneputaway_Id).ToList();

                var result = new List<SearchZonePutawayViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchZonePutawayViewModel();

                    resultItem.zonePutaway_Index = item.Zoneputaway_Index;
                    resultItem.zonePutaway_Id = item.Zoneputaway_Id;
                    resultItem.zonePutaway_Name = item.Zoneputaway_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultZonePutawayViewModel = new actionResultZonePutawayViewModel();
                actionResultZonePutawayViewModel.itemsZonePutaway = result.ToList();
                actionResultZonePutawayViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage,Key = data.key };

                return actionResultZonePutawayViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveChanges
        public String SaveChanges(ZonePutawayViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var ZoneputawayOld = db.MS_Zoneputaway.Find(data.zonePutaway_Index);

                if (ZoneputawayOld == null)
                {
                    if (!string.IsNullOrEmpty(data.zonePutaway_Id))
                    {
                        var query = db.MS_Zoneputaway.FirstOrDefault(c => c.Zoneputaway_Id == data.zonePutaway_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.zonePutaway_Id))
                    {
                        data.zonePutaway_Id = "ZonePutaway_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_Zoneputaway.FirstOrDefault(c => c.Zoneputaway_Id == data.zonePutaway_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.Zoneputaway_Id == data.zonePutaway_Id)
                                {
                                    data.zonePutaway_Id = "ZonePutaway_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    MS_Zoneputaway Model = new MS_Zoneputaway();

                    Model.Zoneputaway_Index = Guid.NewGuid();
                    Model.Zoneputaway_Id = data.zonePutaway_Id;
                    Model.Zoneputaway_Name = data.zonePutaway_Name;
                    Model.IsActive = Convert.ToInt32(data.isActive);
                    Model.IsDelete = 0;
                    Model.IsSystem = 0;
                    Model.Status_Id = 0;
                    Model.Create_By = data.create_By;
                    Model.Create_Date = DateTime.Now;

                    db.MS_Zoneputaway.Add(Model);
                }
                else
                {
                    if (string.IsNullOrEmpty(data.zonePutaway_Id))
                    {
                        if (ZoneputawayOld.Zoneputaway_Id != "")
                        {
                            data.zonePutaway_Id = ZoneputawayOld.Zoneputaway_Id;
                        }
                    }
                    else
                    {
                        if (ZoneputawayOld.Zoneputaway_Id != data.zonePutaway_Id)
                        {
                            var query = db.MS_Zoneputaway.FirstOrDefault(c => c.Zoneputaway_Id == data.zonePutaway_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.zonePutaway_Id = ZoneputawayOld.Zoneputaway_Id;
                        }
                    }

                    ZoneputawayOld.Zoneputaway_Id = data.zonePutaway_Id;
                    ZoneputawayOld.Zoneputaway_Name = data.zonePutaway_Name;
                    ZoneputawayOld.IsActive = Convert.ToInt32(data.isActive);
                    ZoneputawayOld.Update_By = data.create_By;
                    ZoneputawayOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveZonePutaway", msglog);
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

        #region find
        public ZonePutawayViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.MS_Zoneputaway.Where(c => c.Zoneputaway_Index == id).FirstOrDefault();

                var result = new ZonePutawayViewModel();


                result.zonePutaway_Index = queryResult.Zoneputaway_Index;
                result.zonePutaway_Id = queryResult.Zoneputaway_Id;
                result.zonePutaway_Name = queryResult.Zoneputaway_Name;
                result.isActive = queryResult.IsActive;

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getDelete
        public Boolean getDelete(ZonePutawayViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var zoneputaway = db.MS_Zoneputaway.Find(data.zonePutaway_Index);

                if (zoneputaway != null)
                {
                    zoneputaway.IsActive = 0;
                    zoneputaway.IsDelete = 1;


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
                        olog.logging("DeleteZonePutaway", msglog);
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

        //public List<ZoneViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_Zone.FromSql("sp_GetZone").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<ZoneViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ZoneViewModel();

        //                resultItem.ZoneIndex = item.Zone_Index;
        //                resultItem.ZoneId = item.Zone_Id;
        //                resultItem.ZoneName = item.Zone_Name;
        //                resultItem.IsActive = item.IsActive;
        //                resultItem.IsDelete = item.IsDelete;
        //                resultItem.IsSystem = item.IsSystem;
        //                resultItem.StatusId = item.Status_Id;
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



        //public String SaveChanges(ZoneViewModel data)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {


        //            int isactive = 1;
        //            int isdelete = 0;
        //            int issystem = 0;
        //            int statusid = 0;


        //            if (data.ZoneIndex.ToString() == "00000000-0000-0000-0000-000000000000")
        //            {
        //                data.ZoneIndex = Guid.NewGuid();
        //            }

        //            if (data.ZoneId == null)
        //            {
        //                var Sys_Key = new SqlParameter("Sys_Key", "ZoneID");
        //                var resultParameter = new SqlParameter("@result", SqlDbType.Int);
        //                resultParameter.Size = 2000; // some meaningfull value
        //                resultParameter.Direction = ParameterDirection.Output;
        //                context.Database.ExecuteSqlCommand("EXEC @result = sp_Gen_AutoNumber @Sys_Key ", Sys_Key, resultParameter);
        //                //var result = resultParameter.Value;
        //                data.ZoneId = resultParameter.Value.ToString();
        //            }
        //            var Zone_Index = new SqlParameter("Zone_Index", data.ZoneIndex);
        //            var Zone_Id = new SqlParameter("Zone_Id", data.ZoneId);
        //            var Zone_Name = new SqlParameter("Zone_Name", data.ZoneName);
        //            var IsActive = new SqlParameter("IsActive", isactive);
        //            var IsDelete = new SqlParameter("IsDelete", isdelete);
        //            var IsSystem = new SqlParameter("IsSystem", issystem);
        //            var Status_Id = new SqlParameter("Status_Id", statusid);
        //            var Create_By = new SqlParameter("Create_By", "");
        //            var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
        //            var Update_By = new SqlParameter("Update_By", "");
        //            var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
        //            var Cancel_By = new SqlParameter("Cancel_By", "");
        //            var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
        //            var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_Zone  @Zone_Index,@Zone_Id,@Zone_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", Zone_Index, Zone_Id, Zone_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
        //            return rowsAffected.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<ZoneViewModel> getDelete(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.MS_Zone.FromSql("sp_GetZone").Where(c => c.Zone_Index == id).ToList();

        //            int isactive = 0;
        //            int isdelete = 1;
        //            var result = new List<ZoneViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var Zone_Index = new SqlParameter("Zone_Index", item.Zone_Index);
        //                var Zone_Id = new SqlParameter("Zone_Id", item.Zone_Id);
        //                var Zone_Name = new SqlParameter("Zone_Name", item.Zone_Name);
        //                var IsActive = new SqlParameter("IsActive", isactive);
        //                var IsDelete = new SqlParameter("IsDelete", isdelete);
        //                var IsSystem = new SqlParameter("IsSystem", item.IsSystem);
        //                var Status_Id = new SqlParameter("Status_Id", item.Status_Id);
        //                var Create_By = new SqlParameter("Create_By", "");
        //                var Create_Date = new SqlParameter("Create_Date", DateTime.Now.Date);
        //                var Update_By = new SqlParameter("Update_By", "");
        //                var Update_Date = new SqlParameter("Update_Date", DateTime.Now.Date);
        //                var Cancel_By = new SqlParameter("Cancel_By", "");
        //                var Cancel_Date = new SqlParameter("Cancel_Date", DateTime.Now.Date);
        //                var rowsAffected = context.Database.ExecuteSqlCommand("sp_Save_ms_Zone  @Zone_Index,@Zone_Id,@Zone_Name,@IsActive,@IsDelete,@IsSystem,@Status_Id,@Create_By,@Create_Date,@Update_By,@Update_Date,@Cancel_By,@Cancel_Date ", Zone_Index, Zone_Id, Zone_Name, IsActive, IsDelete, IsSystem, Status_Id, Create_By, Create_Date, Update_By, Update_Date, Cancel_By, Cancel_Date);
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

        //public List<ZoneViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            string pstring = " and Zone_Index ='" + id + "'";

        //            var queryResult = context.MS_Zone.FromSql("sp_GetZone  {0}", pstring).Where(c => c.Zone_Index == id).ToList();

        //            var result = new List<ZoneViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ZoneViewModel();
        //                resultItem.ZoneIndex = item.Zone_Index;
        //                resultItem.ZoneId = item.Zone_Id;
        //                resultItem.ZoneName = item.Zone_Name;
        //                resultItem.IsActive = item.IsActive;
        //                resultItem.IsDelete = item.IsDelete;
        //                resultItem.IsSystem = item.IsSystem;
        //                resultItem.StatusId = item.Status_Id;
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

        //public List<ZoneViewModel> search(ZoneViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhereFilter = "";
        //            string pwhereLike = "";
        //            var result = new List<ZoneViewModel>();
        //            if (data.ZoneId != "" && data.ZoneId != null)
        //            {
        //                pwhereFilter = " And Zone_Id like N'%" + data.ZoneId + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter = "";
        //            }

        //            if (data.ZoneName != "" && data.ZoneName != null)
        //            {
        //                pwhereFilter += " And Zone_Name like N'%" + data.ZoneName + "%'";
        //            }
        //            else
        //            {
        //                pwhereFilter += "";
        //            }


        //            if (data.ZoneId != "" && data.ZoneId != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_Zone.FromSql("sp_GetZone @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new ZoneViewModel();
        //                    resultItem.ZoneIndex = item.Zone_Index;
        //                    resultItem.ZoneId = item.Zone_Id;
        //                    resultItem.ZoneName = item.Zone_Name;
        //                    resultItem.IsActive = item.IsActive;
        //                    resultItem.IsDelete = item.IsDelete;
        //                    resultItem.IsSystem = item.IsSystem;
        //                    resultItem.StatusId = item.Status_Id;
        //                    resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                    resultItem.CreateBy = item.Create_By;
        //                    resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                    resultItem.UpdateBy = item.Update_By;
        //                    resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                    resultItem.CancelBy = item.Cancel_By;
        //                    result.Add(resultItem);
        //                }
        //            }
        //            else if (data.ZoneName != "" && data.ZoneName != null)
        //            {
        //                pwhereFilter += " And isActive = '" + 1 + "'";
        //                pwhereFilter += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhereFilter);
        //                var query = context.MS_Zone.FromSql("sp_GetZone @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new ZoneViewModel();
        //                    resultItem.ZoneIndex = item.Zone_Index;
        //                    resultItem.ZoneId = item.Zone_Id;
        //                    resultItem.ZoneName = item.Zone_Name;
        //                    resultItem.IsActive = item.IsActive;
        //                    resultItem.IsDelete = item.IsDelete;
        //                    resultItem.IsSystem = item.IsSystem;
        //                    resultItem.StatusId = item.Status_Id;
        //                    resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
        //                    resultItem.CreateBy = item.Create_By;
        //                    resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
        //                    resultItem.UpdateBy = item.Update_By;
        //                    resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
        //                    resultItem.CancelBy = item.Cancel_By;
        //                    result.Add(resultItem);
        //                }
        //            }

        //            if (data.ZoneId == "" && data.ZoneName == "")
        //            {
        //                result = this.Filter();
        //            }

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<ZoneViewModel> zoneFilter(ZoneViewModel model)
        //{
        //    try
        //    {
        //        var items = new List<ZoneViewModel>();
        //        var result = db.MS_Zone.Select(c => new { c.Zone_Index, c.Zone_Id, c.Zone_Name }).ToList();


        //        foreach (var item in result)
        //        {
        //            var resultItem = new ZoneViewModel
        //            {
        //                ZoneIndex = item.Zone_Index,
        //                ZoneId = item.Zone_Id,
        //                ZoneName = item.Zone_Name
        //            };

        //            items.Add(resultItem);
        //        }

        //        return items;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #region Export Excel
        public ResultZonePutawayViewModel Export(ResultZonePutawayExportViewModel data)
        {
            try
            {
                var query = db.MS_Zoneputaway.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Zoneputaway_Id.Contains(data.key)
                                         || c.Zoneputaway_Name.Contains(data.key));
                }
                if (!string.IsNullOrEmpty(data.create_date) && !string.IsNullOrEmpty(data.create_date_to))
                {
                    var dateStart = data.create_date.toBetweenDate();
                    var dateEnd = data.create_date_to.toBetweenDate();
                    query = query.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);
                }
                var Item = new List<MS_Zoneputaway>();
                var TotalRow = new List<MS_Zoneputaway>();

                TotalRow = query.ToList();

                Item = query.OrderBy(o => o.Zoneputaway_Id).ToList();

                var result = new List<ResultZonePutawayExportViewModel>();
                //var num = 0;
                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new ResultZonePutawayExportViewModel();
                    resultItem.numBerOf = num + 1;
                    resultItem.zonePutaway_Id = item.Zoneputaway_Id;
                    resultItem.zonePutaway_Name = item.Zoneputaway_Name;
                    resultItem.activeStatus = item.IsActive == 1 ? "เปิดใช้งาน" : "ปิดใช้งาน";
                    resultItem.create_By = item.Create_By == null ? "" : item.Create_By;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.update_By = item.Update_By == null ? "" : item.Update_By;
                    resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.cancel_By = item.Cancel_By == null ? "" : item.Cancel_By;
                    resultItem.cancel_Date = item.Cancel_Date != null ? item.Cancel_Date.Value.ToString("dd/MM/yyyy") : "";
                    result.Add(resultItem);
                    num++;


                }

                var count = TotalRow.Count;

                var zonePutawayExportViewModel = new ResultZonePutawayViewModel();
                zonePutawayExportViewModel.itemsZonePutaway = result.ToList();
                zonePutawayExportViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return zonePutawayExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
