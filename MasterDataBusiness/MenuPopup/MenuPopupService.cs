using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MasterDataBusiness
{
    public class MenuPopupService
    {
        private MasterDataDbContext db;

        public MenuPopupService()
        {
            db = new MasterDataDbContext();
        }

        public MenuPopupService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterMenuPopup
        public List<MenuPopupViewModel> filter(MenuPopupViewModel data)
        {
            try
            {
                var query = db.sy_Menu.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
                
                if (!string.IsNullOrEmpty(data.menu_Id))
                {
                    query = query.Where(c => c.Menu_Id.Contains(data.menu_Id));

                }
                else if (!string.IsNullOrEmpty(data.menu_Name))
                {
                    query = query.Where(c => c.Menu_Name.Contains(data.menu_Name));

                }

                var result = new List<MenuPopupViewModel>();

                foreach (var item in query)
                {
                    var resultItem = new MenuPopupViewModel();

                    resultItem.menu_Index = item.Menu_Index;
                    resultItem.menu_Id = item.Menu_Id;
                    resultItem.menu_Name = item.Menu_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //public List<ProcessV2ViewModel> search(ProcessV2ViewModel data)
        //{
        //    try
        //    {
        //        var result = new List<ProcessV2ViewModel>();

        //        if (!string.IsNullOrEmpty(data.process_Id) || !string.IsNullOrEmpty(data.process_Name))
        //        {
        //            var query = db.sy_Process.Where(c => c.Process_Id.Contains(data.process_Id) || c.Process_Name.Contains(data.process_Name) && c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0).Distinct();
        //            foreach (var item in query)
        //            {
        //                var resultItem = new ProcessV2ViewModel();
        //                resultItem.process_Index = item.Process_Index;
        //                resultItem.process_Id = item.Process_Id;
        //                resultItem.process_Name = item.Process_Name;
        //                resultItem.isActive = item.IsActive;
        //                resultItem.isDelete = item.IsDelete;
        //                resultItem.isSystem = item.IsSystem;
        //                resultItem.status_Id = item.Status_Id;
        //                resultItem.create_Date = item.Create_Date.GetValueOrDefault();
        //                resultItem.create_By = item.Create_By;
        //                resultItem.update_Date = item.Update_Date.GetValueOrDefault();
        //                resultItem.update_By = item.Update_By;
        //                resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
        //                resultItem.cancel_By = item.Cancel_By;
        //                result.Add(resultItem);
        //            }
        //        }
        //        else
        //        {
        //            var query = db.sy_Process.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0).Distinct();
        //            foreach (var item in query)
        //            {
        //                var resultItem = new ProcessV2ViewModel();
        //                resultItem.process_Index = item.Process_Index;
        //                resultItem.process_Id = item.Process_Id;
        //                resultItem.process_Name = item.Process_Name;
        //                resultItem.isActive = item.IsActive;
        //                resultItem.isDelete = item.IsDelete;
        //                resultItem.isSystem = item.IsSystem;
        //                resultItem.status_Id = item.Status_Id;
        //                resultItem.create_Date = item.Create_Date.GetValueOrDefault();
        //                resultItem.create_By = item.Create_By;
        //                resultItem.update_Date = item.Update_Date.GetValueOrDefault();
        //                resultItem.update_By = item.Update_By;
        //                resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
        //                resultItem.cancel_By = item.Cancel_By;
        //                result.Add(resultItem);
        //            }
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<ProcessViewModel> Filter()
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.sy_Process.FromSql("sp_GetProcess").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

        //            var result = new List<ProcessViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ProcessViewModel();

        //                resultItem.ProcessIndex = item.Process_Index;
        //                resultItem.ProcessId = item.Process_Id;
        //                resultItem.ProcessName = item.Process_Name;
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
        //public List<ProcessViewModel> search(ProcessViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {
        //            if (data.ProcessId == null)
        //            {
        //                data.ProcessId = context.sy_Process.FromSql("sp_GetProcess").Where( c => c.Process_Name == data.ProcessName).Select( c => c.Process_Id).FirstOrDefault();
        //            }
        //            var sqlParam = new SqlParameter("@pName", data.ProcessId);
        //            var query = context.sy_Process.FromSql("sp_GetSearchProcess @pName = {0}", data.ProcessId, data.ProcessName).ToList();
        //            var result = new List<ProcessViewModel>();
        //            foreach (var item in query)
        //            {
        //                var resultItem = new ProcessViewModel();
        //                resultItem.ProcessIndex = item.Process_Index;
        //                resultItem.ProcessId = item.Process_Id;
        //                resultItem.ProcessName = item.Process_Name;
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

        //public List<ProcessViewModel> search(ProcessViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new MasterDataDbContext())
        //        {

        //            string pwhere = "";
        //            string pwhereLike = "";
        //            var result = new List<ProcessViewModel>();
        //            var queryResult = context.sy_Process.FromSql("sp_GetProcess").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();
        //            if (data.ProcessId != "" && data.ProcessId != null)
        //            {
        //                pwhere += " And Process_Id like N'%" + data.ProcessId + "%'";
        //            }
        //            else
        //            {
        //                pwhere += " ";
        //            }
        //            if (data.ProcessName != "" && data.ProcessName != null)
        //            {
        //                pwhere += " And Process_Name like N'%" + data.ProcessName + "%'";
        //            }
        //            else
        //            {
        //                pwhere += " ";
        //            }
        //            if (data.ProcessId != "" && data.ProcessId != null || data.ProcessName != "" && data.ProcessName != null)
        //            {
        //                pwhere += " And isActive = '" + 1 + "'";
        //                pwhere += " And isDelete = '" + 0 + "'";
        //                var strwhere = new SqlParameter("@strwhere", pwhere);
        //                var query = context.sy_Process.FromSql("sp_GetProcess @strwhere ", strwhere).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new ProcessViewModel();
        //                    resultItem.ProcessIndex = item.Process_Index;
        //                    resultItem.ProcessId = item.Process_Id;
        //                    resultItem.ProcessName = item.Process_Name;
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

        //            if (data.ProcessId == "" && data.ProcessName == "")
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
        //public List<ProcessViewModel> getId(Guid id)
        //{
        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var queryResult = context.sy_Process.FromSql("sp_GetProcess").Where(c => c.Process_Index == id).ToList();
        //            var result = new List<ProcessViewModel>();
        //            foreach (var item in queryResult)
        //            {
        //                var resultItem = new ProcessViewModel();
        //                resultItem.ProcessIndex = item.Process_Index;
        //                resultItem.ProcessId = item.Process_Id;
        //                resultItem.ProcessName = item.Process_Name;
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
    }
}
