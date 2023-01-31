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
    public class GetConfigFromBaseService
    {
        public string GetConfigFromBase(GetConfigFromBaseViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var items = new List<ItemStatusDocViewModel>();

                    var query = context.sy_Config.AsQueryable();

                    if (!string.IsNullOrEmpty(data.Config_Key))
                    {
                        query = query.Where(c => c.Config_Key == data.Config_Key);
                    }
                    if (!string.IsNullOrEmpty(data.Config_Name))
                    {
                        query = query.Where(c => c.Config_Key == data.Config_Name);
                    }
                    if (!string.IsNullOrEmpty(data.Config_Value))
                    {
                        query = query.Where(c => c.Config_Key == data.Config_Value);
                    }

                    return query.FirstOrDefault().Config_Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetConfigListString(GetConfigFromBaseViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {

                    var items = new List<ItemStatusDocViewModel>();

                    var query = context.sy_Config.AsQueryable();

                    if (!string.IsNullOrEmpty(data.Config_Key))
                    {
                        query = query.Where(c => c.Config_Key == data.Config_Key);
                    }
                    if (!string.IsNullOrEmpty(data.Config_Name))
                    {
                        query = query.Where(c => c.Config_Key == data.Config_Name);
                    }
                    if (!string.IsNullOrEmpty(data.Config_Value))
                    {
                        query = query.Where(c => c.Config_Key == data.Config_Value);
                    }

                    return query.FirstOrDefault().Config_Value.Split(',').ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
