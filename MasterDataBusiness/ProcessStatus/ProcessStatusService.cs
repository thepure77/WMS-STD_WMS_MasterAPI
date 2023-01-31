using DataAccess;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MasterDataBusiness
{
    public class ProcessStatusService
    {
        public List<ProcessStatusViewModel> Filter()
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.sy_ProcessStatus.FromSql("sp_GetProcessStatus").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

                    var result = new List<ProcessStatusViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new ProcessStatusViewModel();

                        resultItem.processStatus_Index = item.ProcessStatus_Index;
                        resultItem.processStatus_Id = item.ProcessStatus_Id;
                        resultItem.processStatus_Name = item.ProcessStatus_Name;
                        resultItem.isActive = item.IsActive;
                        resultItem.isDelete = item.IsDelete;
                        resultItem.isSystem = item.IsSystem;
                        resultItem.status_Id = item.Status_Id;
                        resultItem.create_Date = item.Create_Date.GetValueOrDefault();
                        resultItem.create_By = item.Create_By;
                        resultItem.update_Date = item.Update_Date.GetValueOrDefault();
                        resultItem.update_By = item.Update_By;
                        resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
                        resultItem.cancel_By = item.Cancel_By;

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
        public List<ProcessStatusViewModel> search(ProcessStatusViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    string pwhere = "";
                    string pwhereLike = "";
                    var result = new List<ProcessStatusViewModel>();
                    var queryResult = context.sy_ProcessStatus.FromSql("sp_GetProcessStatus").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();
                    if (data.processStatus_Id != "" && data.processStatus_Id != null)
                    {
                        pwhere = " And ProcessStatus_Id like N'%" + data.processStatus_Id + "%'";
                    }
                    else
                    {
                        pwhere = " ";
                    }
                    if (data.processStatus_Id != "" && data.processStatus_Id != null || data.processStatus_Name != "" && data.processStatus_Name != null)
                    {
                        pwhere += " And isActive = '" + 1 + "'";
                        pwhere += " And isDelete = '" + 0 + "'";
                        var strwhere = new SqlParameter("@strwhere", pwhere);
                        var query = context.sy_ProcessStatus.FromSql("sp_GetProcessStatus @strwhere ", strwhere).ToList();
                        foreach (var item in query)
                        {
                            var resultItem = new ProcessStatusViewModel();

                            resultItem.processStatus_Index = item.ProcessStatus_Index;
                            resultItem.processStatus_Id = item.ProcessStatus_Id;
                            resultItem.processStatus_Name = item.ProcessStatus_Name;
                            resultItem.isActive = item.IsActive;
                            resultItem.isDelete = item.IsDelete;
                            resultItem.isSystem = item.IsSystem;
                            resultItem.status_Id = item.Status_Id;
                            resultItem.create_Date = item.Create_Date.GetValueOrDefault();
                            resultItem.create_By = item.Create_By;
                            resultItem.update_Date = item.Update_Date.GetValueOrDefault();
                            resultItem.update_By = item.Update_By;
                            resultItem.cancel_Date = item.Cancel_Date.GetValueOrDefault();
                            resultItem.cancel_By = item.Cancel_By;
                            result.Add(resultItem);
                        }
                    }

                    if (data.processStatus_Id == "" && data.processStatus_Name == "")
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


        public List<ProcessStatusViewModel> statusfilter(ProcessStatusViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var result = new List<ProcessStatusViewModel>();

                    var query = context.sy_ProcessStatus.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                    if (!string.IsNullOrEmpty(data.process_Index.ToString()))
                    {
                        query = query.Where(c => c.Process_Index == data.process_Index);
                    }

                    var queryResult = query.OrderBy(o => o.ProcessStatus_Name).ToList();

                    foreach (var item in queryResult)
                    {
                        var resultItem = new ProcessStatusViewModel();
                        resultItem.processStatus_Index = item.ProcessStatus_Index;
                        resultItem.processStatus_Id = item.ProcessStatus_Id;
                        resultItem.processStatus_Name = item.ProcessStatus_Name;
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

        public List<ItemListViewModel> ProcessStatusDropDown(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var query = context.sy_ProcessStatus.AsQueryable();
                    var items = new List<ItemListViewModel>();
                    string pwhereFilter = "";

                  
                    query = query.Where(c => c.ProcessStatus_Name.Contains(data.key) && c.Process_Index == data.index);


                    var result = query.Select(c => new { c.ProcessStatus_Index, c.ProcessStatus_Id, c.ProcessStatus_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.ProcessStatus_Index,
                            id = item.ProcessStatus_Id,
                            name = item.ProcessStatus_Name

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

        public List<ProcessStatusViewModel> ProcessStatus(ProcessStatusViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var query = context.sy_ProcessStatus.AsQueryable();
                    var items = new List<ProcessStatusViewModel>();
                    string pwhereFilter = "";

                    if (data.process_Index != new Guid("00000000-0000-0000-0000-000000000000".ToString()))
                    {
                        query = query.Where(c => c.Process_Index == data.process_Index);
                    }
                    if (!string.IsNullOrEmpty(data.processStatus_Id))
                    {
                        query = query.Where(c => c.ProcessStatus_Id == data.processStatus_Id);
                    }
                    if (!string.IsNullOrEmpty(data.processStatus_Name))
                    {
                        query = query.Where(c => c.ProcessStatus_Name == data.processStatus_Name);
                    }

                    var result = query.OrderBy(o => o.ProcessStatus_Id).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ProcessStatusViewModel();

                        resultItem.processStatus_Index = item.ProcessStatus_Index;
                        resultItem.processStatus_Id = item.ProcessStatus_Id;
                        resultItem.processStatus_Name = item.ProcessStatus_Name;

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


    }
}

