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

namespace MasterDataBusiness.HistoryCloseWave
{
    public class HistoryCloseWaveService
    {
        private MasterDataDbContext db;

        public HistoryCloseWaveService()
        {
            db = new MasterDataDbContext();
        }

        public HistoryCloseWaveService(MasterDataDbContext db)
        {
            this.db = db;
        }

        public HistoryCloseWaveViewModel HistoryCloseWaveSearch(HistoryCloseWaveViewModel data)
        {
            try
            {
                var result = new HistoryCloseWaveViewModel();

                var query = db.View_HistoryCloseWave.AsQueryable();

                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    query = query.Where(c => c.GoodsIssue_No == data.goodsIssue_No);
                }
                if (!string.IsNullOrEmpty(data.waveComplete_Date_Start) && !string.IsNullOrEmpty(data.waveComplete_Date_End))
                {
                    var dateStart = data.waveComplete_Date_Start.toBetweenDate();
                    var dateEnd = data.waveComplete_Date_End.toBetweenDate();
                    query = query.Where(c => c.WaveComplete_Date >= dateStart.start && c.WaveComplete_Date <= dateEnd.end);
                }

                var queryResult = query.OrderByDescending(o => o.WaveComplete_Date).ThenByDescending(d => d.WaveComplete_Date.Value.TimeOfDay).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new HistoryCloseWaveViewModel();

                    resultItem.goodsIssue_Index = item.GoodsIssue_Index;
                    resultItem.goodsIssue_No = item.GoodsIssue_No;
                    resultItem.waveComplete_Status = item.WaveComplete_Status;
                    resultItem.waveComplete_Date = item.WaveComplete_Date?.ToString("dd/MM/yyyy HH:mm:ss");
                    resultItem.wave_Remark = item.Wave_Remark;
                    resultItem.wCS_status = item.WCS_status;
                    resultItem.document_Status = item.Document_Status;

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