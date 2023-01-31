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
    public class ProcessService
    {
        public List<ProcessViewModel> Filter()
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.sy_Process.Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

                    var result = new List<ProcessViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new ProcessViewModel();

                        resultItem.ProcessIndex = item.Process_Index;
                        resultItem.ProcessId = item.Process_Id;
                        resultItem.ProcessName = item.Process_Name;
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
        public List<ProcessViewModel> search(ProcessViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    string pwhere = "";
                    string pwhereLike = "";
                    var result = new List<ProcessViewModel>();
                    var queryResult = context.sy_Process.FromSql("sp_GetProcess").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();
                    if (data.ProcessId != "" && data.ProcessId != null)
                    {
                        pwhere += " And Process_Id like N'%" + data.ProcessId + "%'";
                    }
                    else
                    {
                        pwhere += " ";
                    }
                    if (data.ProcessName != "" && data.ProcessName != null)
                    {
                        pwhere += " And Process_Name like N'%" + data.ProcessName + "%'";
                    }
                    else
                    {
                        pwhere += " ";
                    }
                    if (data.ProcessId != "" && data.ProcessId != null || data.ProcessName != "" && data.ProcessName != null)
                    {
                        pwhere += " And isActive = '" + 1 + "'";
                        pwhere += " And isDelete = '" + 0 + "'";
                        var strwhere = new SqlParameter("@strwhere", pwhere);
                        var query = context.sy_Process.FromSql("sp_GetProcess @strwhere ", strwhere).ToList();
                        foreach (var item in query)
                        {
                            var resultItem = new ProcessViewModel();
                            resultItem.ProcessIndex = item.Process_Index;
                            resultItem.ProcessId = item.Process_Id;
                            resultItem.ProcessName = item.Process_Name;
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

                    }

                    if (data.ProcessId == "" && data.ProcessName == "")
                    {
                        result = this.Filter();
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ProcessViewModel> getId(Guid id)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.sy_Process.FromSql("sp_GetProcess").Where(c => c.Process_Index == id).ToList();
                    var result = new List<ProcessViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new ProcessViewModel();
                        resultItem.ProcessIndex = item.Process_Index;
                        resultItem.ProcessId = item.Process_Id;
                        resultItem.ProcessName = item.Process_Name;
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

        public List<ProcessViewModel> processFix(ProcessViewModel model)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.sy_Process.Where(c => c.Process_Index == new Guid("6A877EA3-7FDD-43E8-A409-4B6BBE2BF199")
                    || c.Process_Index == new Guid("2AAB40FE-454E-4B61-8A74-37C7430A1E44")
                    || c.Process_Index == new Guid("FBEDC6EF-3871-4CDF-84EE-FE3FF618113D")).ToList();

                    var result = new List<ProcessViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new ProcessViewModel();
                        resultItem.ProcessIndex = item.Process_Index;
                        resultItem.ProcessId = item.Process_Id;
                        resultItem.ProcessName = item.Process_Name;
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
