using Comone.Utils;
using DataAccess;
using MasterDataAPI.Controllers;
using MasterDataBusiness.ViewModels;
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
    public class LogCancelService
    {
        private MasterDataDbContext db;

        public LogCancelService()
        {
            db = new MasterDataDbContext();
        }

        public LogCancelService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterLogCancel
        public actionResultLogViewModelCancel filterLogCancel(SearchLogCancelViewModel data)
        {
            try
            {
                var Master_DBContext = new MasterDataDbContext();
                var temp_Master_DBContext = new temp_MasterDataDbContext();

                temp_Master_DBContext.Database.SetCommandTimeout(360);
                Master_DBContext.Database.SetCommandTimeout(360);


                var DOC_LINK_No = new SqlParameter("@DOC_LINK_No", "");

                if (!string.IsNullOrEmpty(data.key))
                {
                    DOC_LINK_No = new SqlParameter("@DOC_LINK_No", data.key);
                }

                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var create_Date = new SqlParameter("@DOC_LINK_Due_Date", "");
                var create_Date_To = new SqlParameter("@DOC_LINK_Due_Date_To", "");
                if (!string.IsNullOrEmpty(data.create_Date_To) || !string.IsNullOrEmpty(data.create_Date))
                {
                    dateStart = data.create_Date.toBetweenDate().start;
                    dateEnd = data.create_Date_To.toBetweenDate().end;
                    create_Date = new SqlParameter("@DOC_LINK_Due_Date", dateStart);
                    create_Date_To = new SqlParameter("@DOC_LINK_Due_Date_To", dateEnd);
                }
                var resultquery = new List<MasterDataDataAccess.Models.sp_LogCancel>();

                if (data.room_Name == "01")
                {
                    resultquery = temp_Master_DBContext.sp_LogCancel.FromSql("sp_LogCancel @DOC_LINK_No , @DOC_LINK_Due_Date , @DOC_LINK_Due_Date_To", DOC_LINK_No, create_Date, create_Date_To).ToList();
                }
                else
                {
                    resultquery = Master_DBContext.sp_LogCancel.FromSql("sp_LogCancel @DOC_LINK_No , @DOC_LINK_Due_Date , @DOC_LINK_Due_Date_To", DOC_LINK_No, create_Date, create_Date_To).ToList();
                }
               

                var statusModels = new List<string>();
                var status_SAPModels = new List<string>();
                //var sortModels = new List<SortModel>();

                if (data.status.Count > 0)
                {
                    foreach (var item in data.status)
                    {
                        if (item.value == "ส่ง")
                        {
                            statusModels.Add("ส่ง");
                        }
                        if (item.value == "ตอบกลับ")
                        {
                            statusModels.Add("ตอบกลับ");
                        }
                       
                    }
                    resultquery = resultquery.Where(c => statusModels.Contains(c.Type)).ToList();
                }

                if (data.status_SAP.Count > 0)
                {
                    foreach (var item in data.status_SAP)
                    {
                        if (item.value == "1")
                        {
                            status_SAPModels.Add("1");
                        }
                        if (item.value == "-1")
                        {
                            status_SAPModels.Add("-1");
                        }
                        if (item.value == "E")
                        {
                            status_SAPModels.Add("E");
                        }
                        if (item.value == "C")
                        {
                            status_SAPModels.Add("C");
                        }
                        if (item.value == "P")
                        {
                            status_SAPModels.Add("P");
                        }
                        if (item.value == "EV")
                        {
                            status_SAPModels.Add("EV");
                        }

                    }
                    resultquery = resultquery.Where(c => status_SAPModels.Contains(c.WMS_ID_STATUS)).ToList();
                }



                var Item = new List<sp_LogCancel>();
                var TotalRow = new List<sp_LogCancel>();

                TotalRow = resultquery;

                Item = resultquery.OrderBy(o => o.WMS_ID).ToList();

                var result = new List<SearchLogCancelViewModel>();
                int num = 0;
                foreach (var item in Item)
                { 
                    var resultItem = new SearchLogCancelViewModel();

                    resultItem.rownum = num + 1 ;
                    resultItem.rowIndex = item.RowIndex;
                    resultItem.wms_ID = item.WMS_ID;
                    resultItem.doc_LINK = item.DOC_LINK;
                    resultItem.json = item.Json;
                    resultItem.createDate = item.CreatedDate != null ? item.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.wms_ID_STATUS = item.WMS_ID_STATUS;
                    resultItem.type = item.Type;
                    resultItem.mat_Doc = item.Mat_Doc;
                    resultItem.mESSAGE = item.MESSAGE;

                    result.Add(resultItem);
                    num++;
                }

                var count = TotalRow.Count;

                var actionResultLogViewModelCancel = new actionResultLogViewModelCancel();
                actionResultLogViewModelCancel.itemsLogCancel = result.ToList();
                actionResultLogViewModelCancel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultLogViewModelCancel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        #region ExportCancel
        public actionResultLogExportViewModelCancel ExportCancel(LogCancelExportViewModel data)
        {
            try
            {
                var Master_DBContext = new MasterDataDbContext();

                var DOC_LINK_No = new SqlParameter("@DOC_LINK_No", "");

                if (!string.IsNullOrEmpty(data.key))
                {
                    DOC_LINK_No = new SqlParameter("@DOC_LINK_No", data.key);
                }

                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var DOC_LINK_Due_Date = new SqlParameter("@DOC_LINK_Due_Date", "");
                var DOC_LINK_Due_Date_To = new SqlParameter("@DOC_LINK_Due_Date_To", "");
                if (!string.IsNullOrEmpty(data.DOC_LINK_Due_Date_To) || !string.IsNullOrEmpty(data.DOC_LINK_Due_Date))
                {
                    dateStart = data.DOC_LINK_Due_Date.toBetweenDate().start;
                    dateEnd = data.DOC_LINK_Due_Date_To.toBetweenDate().end;
                    DOC_LINK_Due_Date = new SqlParameter("@DOC_LINK_Due_Date", dateStart);
                    DOC_LINK_Due_Date_To = new SqlParameter("@DOC_LINK_Due_Date_To", dateEnd);
                }

                var resultquery = Master_DBContext.sp_LogCancel.FromSql("sp_LogCancel @DOC_LINK_No , @DOC_LINK_Due_Date , @DOC_LINK_Due_Date_To", DOC_LINK_No, DOC_LINK_Due_Date, DOC_LINK_Due_Date_To).ToList();

                var statusModels = new List<string>();
                var status_SAPModels = new List<string>();
                //var sortModels = new List<SortModel>();

                if (data.status.Count > 0)
                {
                 
                    foreach (var item in data.status)
                    {
                        if (item.value == "ส่ง")
                        {
                            statusModels.Add("ส่ง");
                        }
                        if (item.value == "ตอบกลับ")
                        {
                            statusModels.Add("ตอบกลับ");
                        }

                    }
                    resultquery = resultquery.Where(c => statusModels.Contains(c.Type)).ToList();
                }

                if (data.status_.Count > 0)
                {
                    foreach (var item in data.status_)
                    {
                        if (item.value == "1")
                        {
                            status_SAPModels.Add("1");
                        }
                        if (item.value == "-1")
                        {
                            status_SAPModels.Add("-1");
                        }
                        if (item.value == "E")
                        {
                            status_SAPModels.Add("E");
                        }
                        if (item.value == "C")
                        {
                            status_SAPModels.Add("C");
                        }
                        if (item.value == "P")
                        {
                            status_SAPModels.Add("P");
                        }
                        if (item.value == "EV")
                        {
                            status_SAPModels.Add("EV");
                        }

                    }
                    resultquery = resultquery.Where(c => status_SAPModels.Contains(c.WMS_ID_STATUS)).ToList();
                }

                var Item = new List<sp_LogCancel>();
                var TotalRow = new List<sp_LogCancel>();

                TotalRow = resultquery;

                Item = resultquery.OrderBy(o => o.WMS_ID).ToList();

                var result = new List<LogCancelExportViewModel>();
                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new LogCancelExportViewModel();
                    resultItem.rownum = num + 1;
                    resultItem.rowIndex = item.RowIndex;
                    resultItem.wms_ID = item.WMS_ID;
                    resultItem.doc_LINK = item.DOC_LINK;
                    resultItem.json = item.Json;
                    resultItem.createDate = item.CreatedDate != null ? item.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.wms_ID_STATUS = item.WMS_ID_STATUS;
                    resultItem.type = item.Type;
                    resultItem.mat_Doc = item.Mat_Doc;
                    resultItem.mESSAGE = item.MESSAGE;

                    result.Add(resultItem);
                    num++;
                }

                var count = TotalRow.Count;

                var LogCancelExportViewModel = new actionResultLogExportViewModelCancel();
                LogCancelExportViewModel.itemsLogCancel = result.ToList();

                return LogCancelExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
