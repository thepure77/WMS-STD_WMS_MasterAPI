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
    public class LogGrService
    {
        private MasterDataDbContext db;

        public LogGrService()
        {
            db = new MasterDataDbContext();
        }

        public LogGrService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterLogGr
        public actionResultLogGrViewModel filterLogGr(SearchLogGrViewModel data)
        {
            try
            {
               var Master_DBContext = new MasterDataDbContext();
                var temp_Master_DBContext = new temp_MasterDataDbContext();

                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);

                var PurchaseOrder_No = new SqlParameter("@PurchaseOrder_No", "");

                if (!string.IsNullOrEmpty(data.purchaseOrder_No))
                {
                    PurchaseOrder_No = new SqlParameter("@PurchaseOrder_No", data.purchaseOrder_No);
                }

                var PlanGoodsReceive_No = new SqlParameter("@PlanGoodsReceive_No", "");

                if (!string.IsNullOrEmpty(data.planGoodsReceive_No))
                {
                    PlanGoodsReceive_No = new SqlParameter("@PlanGoodsReceive_No", data.planGoodsReceive_No);
                }

                var GoodsReceive_No = new SqlParameter("@GoodsReceive_No", "");

                if (!string.IsNullOrEmpty(data.goodsReceive_No))
                {
                    GoodsReceive_No = new SqlParameter("@GoodsReceive_No", data.goodsReceive_No);
                }

                var order = new SqlParameter("@order_remark", "");

                if (!string.IsNullOrEmpty(data.order_remark))
                {
                    order = new SqlParameter("@order_remark", data.order_remark);
                }

                var wms_id = new SqlParameter("@WMS_ID", "");

                if (!string.IsNullOrEmpty(data.wms_IDgr))
                {
                    wms_id = new SqlParameter("@WMS_ID", data.wms_IDgr);
                }
                

                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var PlanGoodsReceive_Due_Date = new SqlParameter("@PlanGoodsReceive_Due_Date", "");
                var PlanGoodsReceive_Due_Date_To = new SqlParameter("@PlanGoodsReceive_Due_Date_To", "");
                if (!string.IsNullOrEmpty(data.PlanGoodsReceive_Due_Date_To) || !string.IsNullOrEmpty(data.PlanGoodsReceive_Due_Date))
                {
                    dateStart = data.PlanGoodsReceive_Due_Date.toBetweenDate().start;
                    dateEnd = data.PlanGoodsReceive_Due_Date_To.toBetweenDate().end;
                    PlanGoodsReceive_Due_Date = new SqlParameter("@PlanGoodsReceive_Due_Date", dateStart);
                    PlanGoodsReceive_Due_Date_To = new SqlParameter("@PlanGoodsReceive_Due_Date_To", dateEnd);
                }
                var resultquery = new List<MasterDataDataAccess.Models.sp_LogGr>();

                if (data.room_Name == "01")
                {
                    resultquery = temp_Master_DBContext.sp_LogGr.FromSql("sp_LogGr  @PurchaseOrder_No , @PlanGoodsReceive_No , @GoodsReceive_No , @PlanGoodsReceive_Due_Date , @PlanGoodsReceive_Due_Date_To , @order_remark, @WMS_ID", PlanGoodsReceive_No, GoodsReceive_No, PurchaseOrder_No, PlanGoodsReceive_Due_Date, PlanGoodsReceive_Due_Date_To, order, wms_id).ToList();
                }
                else
                {
                    resultquery = Master_DBContext.sp_LogGr.FromSql("sp_LogGr  @PurchaseOrder_No , @PlanGoodsReceive_No , @GoodsReceive_No , @PlanGoodsReceive_Due_Date , @PlanGoodsReceive_Due_Date_To , @order_remark , @WMS_ID", PlanGoodsReceive_No, GoodsReceive_No, PurchaseOrder_No, PlanGoodsReceive_Due_Date, PlanGoodsReceive_Due_Date_To, order, wms_id).ToList();
                }

                var statusModels = new List<string>();
                var status_SAPModels = new List<string>();
                //var sortModels = new List<SortModel>();

                if (data.statusgr.Count > 0)
                {
                    foreach (var item in data.statusgr)
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

                if (data.status_Gr.Count > 0)
                {
                    foreach (var item in data.status_Gr)
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



                var Item = new List<sp_LogGr>();
                var TotalRow = new List<sp_LogGr>();

                TotalRow = resultquery;

                Item = resultquery.OrderBy(o => o.CreatedDate).ToList();

                var result = new List<SearchLogGrViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchLogGrViewModel();

                    resultItem.rowIndexgr = item.RowIndex;
                    resultItem.wms_IDgr = item.WMS_ID;
                    resultItem.doc_LINKgr = item.DOC_LINK;
                    resultItem.jsongr = item.Json;
                    resultItem.createdDategr = item.CreatedDate != null ? item.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.wms_ID_STATUSgr = item.WMS_ID_STATUS;
                    resultItem.typegr = item.Type;
                    resultItem.mat_Docgr = item.Mat_Doc;
                    resultItem.mESSAGEgr = item.MESSAGE;

                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultLogGrViewModel = new actionResultLogGrViewModel();
                actionResultLogGrViewModel.itemsLoggr = result.ToList();
                actionResultLogGrViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.planGoodsReceive_No };

                return actionResultLogGrViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Exportgr
        public actionResultLoggrExportViewModel Exportgr(LogGrExportViewModel data)
        {
            try
            {
                var Master_DBContext = new MasterDataDbContext();
                Master_DBContext.Database.SetCommandTimeout(360);

                var PurchaseOrder_No = new SqlParameter("@PurchaseOrder_No", "");

                if (!string.IsNullOrEmpty(data.purchaseOrder_No))
                {
                    PurchaseOrder_No = new SqlParameter("@PurchaseOrder_No", data.purchaseOrder_No);
                }

                var PlanGoodsReceive_No = new SqlParameter("@PlanGoodsReceive_No", "");

                if (!string.IsNullOrEmpty(data.planGoodsReceive_No))
                {
                    PlanGoodsReceive_No = new SqlParameter("@PlanGoodsReceive_No", data.planGoodsReceive_No);
                }

                var GoodsReceive_No = new SqlParameter("@goodsReceive_No", "");

                if (!string.IsNullOrEmpty(data.planGoodsReceive_No))
                {
                    GoodsReceive_No = new SqlParameter("@goodsReceive_No", data.goodsReceive_No);
                }

                var order = new SqlParameter("@order_remark", "");

                if (!string.IsNullOrEmpty(data.order_remark))
                {
                    order = new SqlParameter("@order_remark", data.order_remark);
                }

                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var PlanGoodsReceive_Due_Date = new SqlParameter("@PlanGoodsReceive_Due_Date", "");
                var PlanGoodsReceive_Due_Date_To = new SqlParameter("@PlanGoodsReceive_Due_Date_To", "");
                if (!string.IsNullOrEmpty(data.PlanGoodsReceive_Due_Date_To) || !string.IsNullOrEmpty(data.PlanGoodsReceive_Due_Date))
                {
                    dateStart = data.PlanGoodsReceive_Due_Date.toBetweenDate().start;
                    dateEnd = data.PlanGoodsReceive_Due_Date_To.toBetweenDate().end;
                    PlanGoodsReceive_Due_Date = new SqlParameter("@PlanGoodsReceive_Due_Date", dateStart);
                    PlanGoodsReceive_Due_Date_To = new SqlParameter("@PlanGoodsReceive_Due_Date_To", dateEnd);
                }

                var resultquery = Master_DBContext.sp_LogGr.FromSql("sp_LogGr @PlanGoodsReceive_No , @GoodsReceive_No , @PurchaseOrder_No , @PlanGoodsReceive_Due_Date , @PlanGoodsReceive_Due_Date_To , @order_remark", PlanGoodsReceive_No, GoodsReceive_No, PurchaseOrder_No, PlanGoodsReceive_Due_Date, PlanGoodsReceive_Due_Date_To, order).ToList();

                var statusModels = new List<string>();
                var status_SAPModels = new List<string>();
                //var sortModels = new List<SortModel>();

                if (data.statusgr.Count > 0)
                {
                    foreach (var item in data.statusgr)
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

                if (data.status_gr.Count > 0)
                {
                    foreach (var item in data.status_gr)
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

                var Item = new List<sp_LogGr>();
                var TotalRow = new List<sp_LogGr>();

                TotalRow = resultquery;

                Item = resultquery.OrderBy(o => o.CreatedDate).ToList();

                var result = new List<LogGrExportViewModel>();
                foreach (var item in Item)
                {
                    var resultItem = new LogGrExportViewModel();

                    resultItem.rowIndexgr = item.RowIndex;
                    resultItem.wms_IDgr = item.WMS_ID;
                    resultItem.doc_LINKgr = item.DOC_LINK;
                    resultItem.jsongr = item.Json;
                    resultItem.createdDategr = item.CreatedDate != null ? item.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.wms_ID_STATUSgr = item.WMS_ID_STATUS;
                    resultItem.typegr = item.Type;
                    resultItem.mat_Docgr = item.Mat_Doc;
                    resultItem.mESSAGEgr = item.MESSAGE;

                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var LogGrExportViewModel = new actionResultLoggrExportViewModel();
                LogGrExportViewModel.itemsLoggr = result.ToList();

                return LogGrExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        #region Export Excel
        //public ResultLogViewModel ExportGr(LogExportViewModel data)
        //{
        //    try
        //    {
        //        var query = db.sp_LogGr.AsQueryable();

        //        //query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);
        //        if (!string.IsNullOrEmpty(data.planGoodsReceive_No))
        //        {
        //            query = query.Where(c => c.WMS_IDgr.Contains(data.planGoodsReceive_No)
        //                                //|| c.Plant_Name.Contains(data.planGoodsReceive_No)
        //                                );

        //        }

        //        var Item = new List<sp_LogGr>();
        //        var TotalRow = new List<sp_LogGr>();

        //        TotalRow = query.ToList();

        //        Item = query.OrderBy(o => o.WMS_IDgr).ToList();

        //        var result = new List<LogGrExportViewModel>();
        //        //var num = 0;
        //        int num = 0;
        //        foreach (var item in Item)
        //        {
        //            var resultItem = new LogGrExportViewModel();

        //            resultItem.rowIndexgr = item.RowIndexgr;
        //            resultItem.wms_IDgr = item.WMS_IDgr;
        //            resultItem.doc_LINKgr = item.DOC_LINKgr;
        //            resultItem.jsongr = item.Jsongr;
        //            resultItem.mESSAGEgr = item.MESSAGEgr;
        //            resultItem.createdDategr = item.CreatedDategr != null ? item.CreatedDategr.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
        //            resultItem.wms_ID_STATUSgr = item.WMS_ID_STATUSgr;
        //            resultItem.typegr = item.Typegr;
        //            result.Add(resultItem);
        //            num++;
        //        }

        //        var count = TotalRow.Count;

        //        var LogGrExportViewModel = new ResultLogGrViewModel();
        //        LogGrExportViewModel.itemsLoggr = result.ToList();

        //        return LogGrExportViewModel;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion
    }
}
