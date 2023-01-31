using Comone.Utils;
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
using System.Threading;

namespace MasterDataBusiness.Rebuild
{
    public class RebuildService
    {
        private MasterDataDbContext db;

        public RebuildService()
        {
            db = new MasterDataDbContext();
        }

        public RebuildService(MasterDataDbContext db)
        {
            this.db = db;
        }

        public CloseWaveViewModel RebuildIndex(CloseWaveViewModel data)
        {
            var olog = new logtxt();
            try
            {
                String msglog = "";
                olog.logging("RebuildIndex", "1");
                CloseWaveViewModel result = new CloseWaveViewModel();
                result.ResultIsUse = false;
                db.Database.SetCommandTimeout(1800);
                var temp_datetime = DateTime.Now.ToString("yyyyMMdd");
                var Starti = temp_datetime.toBetweenDate();
                var Endi = temp_datetime.toBetweenDate();
                var queryCheck = db.sy_LogRebuild.AsQueryable();
                queryCheck = queryCheck.Where(c => c.Rebuild_Date_Start >= Starti.start && c.Rebuild_Date_End <= Endi.end);
                if (queryCheck.Count() == 1) {
                    result.ResultMsg = "กดไปแล้ววันนี้";
                    result.ResultIsUse = false;
                }
                else { 
                    Guid Rebuild_Index = new Guid();
                    Rebuild_Index = Guid.NewGuid();
                    DateTime Rebuild_Date_Start = DateTime.Now;

                    sy_LogRebuild itemi = new sy_LogRebuild();

                    itemi.Rebuild_Index = Rebuild_Index;
                    itemi.Rebuild_By = data.Rebuild_By;
                    itemi.Rebuild_Date_Start = Rebuild_Date_Start;
                    itemi.Rebuild_Date_End = null;

                    db.sy_LogRebuild.Add(itemi);
                    db.SaveChanges();

                    olog.logging("RebuildIndex", "2");
                    ////////////////////////////////////
                    //Thread.Sleep(10000);
                    var GoodsIssue_Index = new SqlParameter("@GoodsIssue_In", "");
                    var rebuild = db.Database.ExecuteSqlCommand("EXEC sp_Rebuild @GoodsIssue_In", GoodsIssue_Index);
                    ////////////////////////////////////
                    olog.logging("RebuildIndex", "3");
                    DateTime Rebuild_Date_End = DateTime.Now;

                    var query = db.sy_LogRebuild.Where(c => c.Rebuild_Index == itemi.Rebuild_Index && c.Rebuild_By == itemi.Rebuild_By);

                    var Item = db.sy_LogRebuild.Find(itemi.Rebuild_Index);
                    Item.Rebuild_Date_End = Rebuild_Date_End;
                    db.SaveChanges();
                    result.ResultIsUse = true;
                    olog.logging("RebuildIndex", "4");
                }

                return result;
            }
            catch (Exception ex)
            {
                olog.logging("RebuildIndex", ex.ToString());
                throw ex;
            }
        }

        public CloseWaveViewModel RebuildSearch(CloseWaveViewModel data)
        {
            try
            {
                var result = new CloseWaveViewModel();

                var query = db.sy_LogRebuild.AsQueryable();
                var temp_datetime = DateTime.Now.ToString("yyyyMMdd");
                var Start = temp_datetime.toBetweenDate();
                var End = temp_datetime.toBetweenDate();
                var queryCheck = db.sy_LogRebuild.AsQueryable();

                data.isuse_rebuild = false;
                var isuse_rebuild = new List<CloseWaveViewModel>();

                queryCheck = queryCheck.Where(c => c.Rebuild_Date_Start >= Start.start && c.Rebuild_Date_End <= End.end);

                if (queryCheck.Count() == 1)
                {
                    data.isuse_rebuild = true;
                    //isuse_rebuild = data.isuse_rebuild;
                }

                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Rebuild_By == data.key);
                }
                if (!string.IsNullOrEmpty(data.Rebuild_Date_Start) && !string.IsNullOrEmpty(data.Rebuild_Date_End))
                {
                    var dateStart = data.Rebuild_Date_Start.toBetweenDate();
                    var dateEnd = data.Rebuild_Date_End.toBetweenDate();
                    query = query.Where(c => c.Rebuild_Date_Start >= dateStart.start && c.Rebuild_Date_End <= dateEnd.end);
                }

                var queryResult = query.OrderByDescending(o => o.Rebuild_Date_Start).ToList();

                result.ResultIsUse = data.isuse_rebuild;
                foreach (var item in queryResult)
                {
                    var resultItem = new CloseWaveViewModel();

                    resultItem.Rebuild_Index = item.Rebuild_Index;
                    resultItem.Rebuild_By = item.Rebuild_By;
                    resultItem.Rebuild_Date_Start = item.Rebuild_Date_Start?.ToString("dd/MM/yyyy HH:mm:ss");
                    resultItem.Rebuild_Date_End = item.Rebuild_Date_End?.ToString("dd/MM/yyyy HH:mm:ss");

                    result.models.Add(resultItem);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}