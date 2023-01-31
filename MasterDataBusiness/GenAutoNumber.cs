using DataAccess;
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
using System.Security.Cryptography;
using System.Text;

namespace GenAutoNumber
{
    public static class GenAutoNumber
    {
        #region genAutonumber
        public static string genAutonumber(this String Sys_Key)
        {

            var db = new MasterDataDbContext();
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            String result = "";

            try
            {
                sy_AutoNumber model = new sy_AutoNumber();

                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                var query = db.sy_AutoNumber.Where(c => c.Sys_Key == Sys_Key).FirstOrDefault();
                if (query != null)
                {
                    var item = db.sy_AutoNumber.Find(query.Sys_Key);
                    item.Sys_Value = query.Sys_Value + 1;
                    item.Update_Date = DateTime.Now;
                    result = item.Sys_Value.ToString();

                }
                else
                {
                    model.Sys_Key = Sys_Key;
                    model.Sys_Text = "";
                    model.Sys_Value = 1;
                    model.IsActive = 1;
                    model.IsDelete = 0;
                    model.IsSystem = 1;
                    model.Status_Id = 0;
                    model.Create_By = "System";
                    model.Create_Date = DateTime.Now;
                    result = model.Sys_Value.ToString();
                    db.sy_AutoNumber.Add(model);
                }

                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("SaveRoom", msglog);
                    transactionx.Rollback();

                    throw exy;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion  

    }
}