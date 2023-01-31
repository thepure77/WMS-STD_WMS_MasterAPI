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
    public class LogtfService
    {
        private MasterDataDbContext db;

        public LogtfService()
        {
            db = new MasterDataDbContext();
        }

        public LogtfService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterLogtf
        public actionResultLogtfViewModel filterLogtf(SearchLogTfViewModel data)
        {
            try
            {
                var Master_DBContext = new MasterDataDbContext();
                var temp_Master_DBContext = new temp_MasterDataDbContext();

                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);

                var Goodstransfer_No = new SqlParameter("@Goodstransfer_No", "");

                if (!string.IsNullOrEmpty(data.key))
                {
                   Goodstransfer_No = new SqlParameter("@Goodstransfer_No", data.key);
                }

                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var Goodstransfer_Due_Date = new SqlParameter("@Goodstransfer_Due_Date", "");
                var Goodstransfer_Due_Date_To = new SqlParameter("@Goodstransfer_Due_Date_To", "");
                if (!string.IsNullOrEmpty(data.Goodstransfer_Due_Date_To) || !string.IsNullOrEmpty(data.Goodstransfer_Due_Date))
                {
                    dateStart = data.Goodstransfer_Due_Date.toBetweenDate().start;
                    dateEnd = data.Goodstransfer_Due_Date_To.toBetweenDate().end;
                    Goodstransfer_Due_Date = new SqlParameter("@Goodstransfer_Due_Date", dateStart);
                    Goodstransfer_Due_Date_To = new SqlParameter("@Goodstransfer_Due_Date_To", dateEnd);
                }
                var resultquery = new List<MasterDataDataAccess.Models.sp_LogTransfer>();

                if (data.room_Name == "01")
                {
                    resultquery = temp_Master_DBContext.sp_LogTransfer.FromSql("sp_LogTransfer @Goodstransfer_No , @Goodstransfer_Due_Date , @Goodstransfer_Due_Date_To", Goodstransfer_No, Goodstransfer_Due_Date, Goodstransfer_Due_Date_To).ToList();
                }
                else
                {
                    resultquery = Master_DBContext.sp_LogTransfer.FromSql("sp_LogTransfer @Goodstransfer_No , @Goodstransfer_Due_Date , @Goodstransfer_Due_Date_To", Goodstransfer_No, Goodstransfer_Due_Date, Goodstransfer_Due_Date_To).ToList();
                }
                

                var statusModels = new List<string>();
                var status_SAPModels = new List<string>();
                //var sortModels = new List<SortModel>();

                if (data.statustf.Count > 0)
                {
                    foreach (var item in data.statustf)
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

                if (data.status_tf.Count > 0)
                {
                    foreach (var item in data.status_tf)
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



                var Item = new List<sp_LogTransfer>();
                var TotalRow = new List<sp_LogTransfer>();

                TotalRow = resultquery;

                Item = resultquery.OrderBy(o => o.CreatedDate).ToList();

                var result = new List<SearchLogTfViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchLogTfViewModel();

                    resultItem.rowIndextf = item.RowIndex;
                    resultItem.wms_IDtf = item.WMS_ID;
                    resultItem.doc_LINKtf = item.DOC_LINK;
                    resultItem.jsontf = item.Json;
                    resultItem.createdDatetf = item.CreatedDate != null ? item.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.wms_ID_STATUStf = item.WMS_ID_STATUS;
                    resultItem.typetf = item.Type;
                    resultItem.mat_Doctf = item.Mat_Doc;
                    resultItem.messagEtf = item.MESSAGE;

                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultLogtfViewModel = new actionResultLogtfViewModel();
                actionResultLogtfViewModel.itemsLogtf = result.ToList();
                actionResultLogtfViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultLogtfViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        #region Exporttf
        public actionResultLogtfExportViewModel Exporttf(LogTfExportViewModel data)
        {
            try
            {
                var Master_DBContext = new MasterDataDbContext();
                Master_DBContext.Database.SetCommandTimeout(360);

                var Goodstransfer_No = new SqlParameter("@Goodstransfer_No", "");

                if (!string.IsNullOrEmpty(data.key))
                {
                    Goodstransfer_No = new SqlParameter("@Goodstransfer_No", data.key);
                }

                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var Goodstransfer_Due_Date = new SqlParameter("@Goodstransfer_Due_Date", "");
                var Goodstransfer_Due_Date_To = new SqlParameter("@Goodstransfer_Due_Date_To", "");
                if (!string.IsNullOrEmpty(data.Goodstransfer_Due_Date_To) || !string.IsNullOrEmpty(data.Goodstransfer_Due_Date))
                {
                    dateStart = data.Goodstransfer_Due_Date.toBetweenDate().start;
                    dateEnd = data.Goodstransfer_Due_Date_To.toBetweenDate().end;
                    Goodstransfer_Due_Date = new SqlParameter("@Goodstransfer_Due_Date", dateStart);
                    Goodstransfer_Due_Date_To = new SqlParameter("@Goodstransfer_Due_Date_To", dateEnd);
                }

                var resultquery = Master_DBContext.sp_LogTransfer.FromSql("sp_LogTransfer @Goodstransfer_No , @Goodstransfer_Due_Date , @Goodstransfer_Due_Date_To", Goodstransfer_No, Goodstransfer_Due_Date, Goodstransfer_Due_Date_To).ToList();

                var statusModels = new List<string>();
                var status_SAPModels = new List<string>();
                //var sortModels = new List<SortModel>();

                if (data.statustf.Count > 0)
                {
                    foreach (var item in data.statustf)
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

                if (data.status_tf.Count > 0)
                {
                    foreach (var item in data.status_tf)
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

                var Item = new List<sp_LogTransfer>();
                var TotalRow = new List<sp_LogTransfer>();

                TotalRow = resultquery;

                Item = resultquery.OrderBy(o => o.CreatedDate).ToList();

                var result = new List<LogTfExportViewModel>();
                foreach (var item in Item)
                {
                    var resultItem = new LogTfExportViewModel();

                    resultItem.rowIndextf = item.RowIndex;
                    resultItem.wms_IDtf = item.WMS_ID;
                    resultItem.doc_LINKtf = item.DOC_LINK;
                    resultItem.jsontf = item.Json;
                    resultItem.createdDatetf = item.CreatedDate != null ? item.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.wms_ID_STATUStf = item.WMS_ID_STATUS;
                    resultItem.typetf = item.Type;
                    resultItem.mat_Doctf = item.Mat_Doc;
                    resultItem.mESSAGEtf = item.MESSAGE;

                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var LogTfExportViewModel = new actionResultLogtfExportViewModel();
                LogTfExportViewModel.itemsLogtf = result.ToList();

                return LogTfExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
