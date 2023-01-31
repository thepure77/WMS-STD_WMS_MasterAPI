using DataAccess;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MasterDataBusiness
{
    public class ServiceChargeListService
    {
        private MasterDataDbContext db;

        public ServiceChargeListService()
        {
            db = new MasterDataDbContext();
        }

        public ServiceChargeListService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filter
        public actionResultServiceCharge filter(SearchServiceChargeListViewModel data)
        {
            try
            {
                var query = db.ms_ServiceChargeList.AsQueryable();

                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                query = query.Where(c => c.Owner_Index == data.owner_Index);

                var Item = new List<ms_ServiceChargeList>();
                var TotalRow = new List<ms_ServiceChargeList>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }


                Item = query.OrderByDescending(o => o.Create_Date).ToList();

                var result = new List<SearchServiceChargeListViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchServiceChargeListViewModel();

                    resultItem.serviceChargeList_Index = item.ServiceChargeList_Index;
                    resultItem.serviceCharge_Index = item.ServiceCharge_Index;
                    resultItem.serviceCharge_Id = item.ServiceCharge_Id;
                    resultItem.serviceCharge_Name = item.ServiceCharge_Name;
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.dEFAULT_Process_Index = item.DEFAULT_Process_Index;
                    resultItem.processName = db.sy_ProcessStatus.Where(c => c.Process_Index == item.DEFAULT_Process_Index).Select(s => s.ProcessStatus_Name).FirstOrDefault();

                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultServiceChargeListViewModel = new actionResultServiceCharge();
                actionResultServiceChargeListViewModel.items = result.ToList();
                actionResultServiceChargeListViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultServiceChargeListViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region filterServiceChargePopup
        public List<SearchServiceChargeViewModel> filterServiceChargePopup(SearchServiceChargeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var result = new List<SearchServiceChargeViewModel>();

                var query = db.ms_ServiceChargeList.AsQueryable();

                if (!string.IsNullOrEmpty(data.serviceCharge_Id))
                {
                    query = query.Where(c => c.ServiceCharge_Id.Contains(data.serviceCharge_Id));
                }

                query = query.Where(c => c.Owner_Index == data.owner_Index);


                var CharegeList = query.ToList();


                var SelectData = db.ms_ServiceCharge.Where(c => !CharegeList.Select(s => s.ServiceCharge_Index).Contains(c.ServiceCharge_Index)).ToList();

                foreach (var item in SelectData)
                {
                    var resultItem = new SearchServiceChargeViewModel();

                    resultItem.serviceCharge_Index = item.ServiceCharge_Index;
                    resultItem.serviceCharge_Id = item.ServiceCharge_Id;
                    resultItem.serviceCharge_Name = item.ServiceCharge_Name;
                    resultItem.serviceCharge_SecondName = item.ServiceCharge_SecondName;
                    resultItem.default_Process_Index = item.DEFAULT_Process_Index;
                    resultItem.isSkuUse = item.IsSkuUse;
                    resultItem.vatRate = item.VatRate;
                    resultItem.vatType = item.VatType;
                    resultItem.vatCode = item.VatCode;
                    resultItem.vatGroup = item.VatGroup;
                    resultItem.ref_No1 = item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3;
                    resultItem.remark = item.Remark;
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

        #region SaveChanges
        public String SaveChanges(ServiceChargeListViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                Guid Index = new Guid();


                var selectData = db.ms_ServiceCharge.Where(c => data.listServiceCharge.Select(s => s.serviceCharge_Index).Contains(c.ServiceCharge_Index)).ToList();


                foreach (var item in selectData)
                {
                    var resultItem = new ms_ServiceChargeList();

                    resultItem.ServiceChargeList_Index = Index;
                    resultItem.ServiceCharge_Index = item.ServiceCharge_Index;
                    resultItem.ServiceCharge_Id = item.ServiceCharge_Id;
                    resultItem.ServiceCharge_Name = item.ServiceCharge_Name;
                    resultItem.ServiceCharge_SecondName = item.ServiceCharge_SecondName;
                    resultItem.DEFAULT_Process_Index = item.DEFAULT_Process_Index;
                    resultItem.Owner_Index = data.owner_Index;
                    resultItem.Owner_Id = data.owner_Id;
                    resultItem.Owner_Name = data.owner_Name;
                    resultItem.Ref_No1 = item.Ref_No1;
                    resultItem.Ref_No2 = item.Ref_No2;
                    resultItem.Ref_No3 = item.Ref_No3;
                    resultItem.Ref_No4 = item.Ref_No4;
                    resultItem.Ref_No5 = item.Ref_No5;
                    resultItem.IsActive = 1;
                    resultItem.IsDelete = 0;
                    resultItem.Create_By = data.create_By;
                    resultItem.Create_Date = DateTime.Now;

                    db.ms_ServiceChargeList.Add(resultItem);

                }

                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("SaveServiceChargeList", msglog);
                    transactionx.Rollback();

                    throw exy;
                }

                return "Done"; ;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region filterServiceChargeFix
        public List<serviceChargeFixViewModel> filterServiceChargeFix(serviceChargeFixViewModel data)
        {
            try
            {
                var query = db.ms_ServiceChargeFix.AsQueryable();

                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                query = query.Where(c => c.Owner_Index == data.owner_Index && c.ServiceCharge_Index == data.serviceCharge_Index);

                var queryResult = query.ToList();


                var result = new List<serviceChargeFixViewModel>();

                foreach (var item in queryResult)
                {
                    var resultItem = new serviceChargeFixViewModel();

                    resultItem.serviceChargeFix_Index = item.ServiceChargeFix_Index;
                    resultItem.serviceCharge_Index = item.ServiceCharge_Index;
                    resultItem.serviceCharge_Id = item.ServiceCharge_Id;
                    resultItem.serviceCharge_Name = item.ServiceCharge_Name;
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.minimumrate = item.Minimumrate;
                    resultItem.rate = item.Rate;
                    resultItem.ref_No1 = item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3;
                    resultItem.ref_No4 = item.Ref_No4;
                    resultItem.ref_No5 = item.Ref_No5;
                    resultItem.remark = item.Remark;

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

        #region SaveServiceChargeFix
        public String SaveServiceChargeFix(serviceChargeFixViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                Guid Index = new Guid();


                var checkFix = db.ms_ServiceChargeFix.Where(c => c.ServiceCharge_Index == data.serviceCharge_Index
                                                            && c.Owner_Index == data.owner_Index
                                                            && c.IsActive == 1
                                                            && c.IsDelete == 0).FirstOrDefault();

                if (checkFix != null)
                {
                    var FixOld = db.ms_ServiceChargeFix.Find(checkFix.ServiceChargeFix_Index);

                    FixOld.Remark = data.remark;
                    FixOld.Rate = data.rate;
                    FixOld.Minimumrate = data.minimumrate;
                    FixOld.Update_By = data.create_By;
                    FixOld.Update_Date = DateTime.Now;
                }
                else
                {
                    var FixNew = new ms_ServiceChargeFix();

                    FixNew.ServiceChargeFix_Index = new Guid();
                    FixNew.ServiceCharge_Index = data.serviceCharge_Index;
                    FixNew.ServiceCharge_Id = data.serviceCharge_Id;
                    FixNew.ServiceCharge_Name = data.serviceCharge_Name;
                    FixNew.Owner_Index = data.owner_Index;
                    FixNew.Owner_Id = data.owner_Id;
                    FixNew.Owner_Name = data.owner_Name;
                    FixNew.Rate = data.rate;
                    FixNew.Minimumrate = data.minimumrate;
                    FixNew.Remark = data.remark;
                    FixNew.Create_By = data.create_By;
                    FixNew.IsActive = 1;
                    FixNew.IsDelete = 0;
                    FixNew.Create_Date = DateTime.Now;
                    db.ms_ServiceChargeFix.Add(FixNew);
                }



                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("SaveServiceChargeFix", msglog);
                    transactionx.Rollback();

                    throw exy;
                }

                return "Done"; ;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region deleteServiceChargeFix
        public String deleteServiceChargeFix(serviceChargeFixViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                Guid Index = new Guid();


                var checkFix = db.ms_ServiceChargeFix.Where(c => data.listServiceChargeFix.Select(s => s.serviceChargeFix_Index).Contains(c.ServiceChargeFix_Index)).FirstOrDefault();


                if (checkFix != null)
                {
                    var FixOld = db.ms_ServiceChargeFix.Find(checkFix.ServiceChargeFix_Index);

                    FixOld.IsActive = 0;
                    FixOld.IsDelete = 1;
                    FixOld.Minimumrate = data.minimumrate;
                    FixOld.Cancel_By = data.create_By;
                    FixOld.Cancel_Date = DateTime.Now;
                }



                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("deleteServiceChargeFix", msglog);
                    transactionx.Rollback();

                    throw exy;
                }

                return "Done"; ;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region filterView_WRL
        public actionResultWRL filterView_WRL(WRLViewModel data)
        {
            try
            {
                var query = db.View_WRL.AsQueryable();

                query = query.Where(c => c.Warehouse_Index == data.warehouse_Index && c.Room_Index == data.room_Index);

                if (!string.IsNullOrEmpty(data.locationAisle_Index.ToString()))
                {
                    query = query.Where(c => c.LocationAisle_Index == data.locationAisle_Index);
                }

                if (data.listServiceWRL.Count > 0)
                {
                    query = query.Where(c => !data.listServiceWRL.Select(s => s.location_Index).Contains(c.Location_Index));
                }

                var queryResult = query.ToList();


                var result = new List<WRLViewModel>();

                var Item = new List<View_WRL>();
                var TotalRow = new List<View_WRL>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderByDescending(o => o.Location_Id).ToList();

                foreach (var item in Item)
                {
                    var resultItem = new WRLViewModel();

                    resultItem.warehouse_Index = item.Warehouse_Index;
                    resultItem.warehouse_Id = item.Warehouse_Id;
                    resultItem.warehouse_Name = item.Warehouse_Name;

                    resultItem.room_Index = item.Room_Index;
                    resultItem.room_Id = item.Room_Id;
                    resultItem.room_Name = item.Room_Name;

                    resultItem.location_Index = item.Location_Index;
                    resultItem.location_Id = item.Location_Id;
                    resultItem.location_Name = item.Location_Name;

                    resultItem.locationAisle_Index = item.LocationAisle_Index;
                    resultItem.locationType_Index = item.LocationType_Index;

                    result.Add(resultItem);
                }




                var count = TotalRow.Count;

                var actionResultWRL = new actionResultWRL();
                actionResultWRL.items = result.ToList();
                actionResultWRL.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage };

                return actionResultWRL;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveStorageCharge
        public actionResultstorage SaveStorageCharge(storageSaveViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var actionResultstorage = new actionResultstorage();

                var ms_LocationServiceCharge = new ms_LocationServiceCharge();

                var ms_StorageCharge = new ms_StorageCharge();

                #region LocationServiceCharge
                var queryLocation = db.ms_LocationServiceCharge.Where(c => c.ServiceCharge_Index == data.serviceCharge_Index && c.IsActive == 1 && c.IsDelete == 0).ToList();

                if (queryLocation.Count > 0)
                {

                    foreach (var item in data.listServiceWRL)
                    {
                        var query = db.ms_LocationServiceCharge.Find(item.locationServiceCharge_Index);

                        if (query == null)
                        {
                            var resultItem = new ms_LocationServiceCharge();

                            resultItem.LocationServiceCharge_Index = new Guid();
                            resultItem.ServiceCharge_Index = data.serviceCharge_Index.GetValueOrDefault();
                            resultItem.ServiceCharge_Id = data.serviceCharge_Id;
                            resultItem.ServiceCharge_Name = data.serviceCharge_Name;
                            resultItem.Owner_Index = data.owner_Index.GetValueOrDefault();
                            resultItem.Owner_Id = data.owner_Id;
                            resultItem.Owner_Name = data.owner_Name;
                            resultItem.Warehouse_Index = item.warehouse_Index.GetValueOrDefault();
                            resultItem.Warehouse_Id = item.warehouse_Id;
                            resultItem.Warehouse_Name = item.warehouse_Name;
                            resultItem.Room_Index = item.room_Index.GetValueOrDefault();
                            resultItem.Room_Id = item.room_Id;
                            resultItem.Room_Name = item.room_Name;

                            #region Find LocationType
                            var queryLoType = db.MS_LocationType.Find(item.locationType_Index);
                            if (queryLoType != null)
                            {
                                resultItem.LocationType_Index = queryLoType.LocationType_Index;
                                resultItem.LocationType_Id = queryLoType.LocationType_Id;
                                resultItem.LocationType_Name = queryLoType.LocationType_Name;
                            }

                            #endregion

                            resultItem.Location_Index = item.location_Index.GetValueOrDefault(); ;
                            resultItem.Location_Id = item.location_Id;
                            resultItem.Location_Name = item.location_Name;
                            resultItem.IsActive = 1;
                            resultItem.IsDelete = 0;
                            resultItem.Create_Date = DateTime.Now;
                            resultItem.Create_By = data.userName;
                            db.ms_LocationServiceCharge.Add(resultItem);
                        }
                    }
                }

                else
                {
                    foreach (var item in data.listServiceWRL)
                    {
                        var resultItem = new ms_LocationServiceCharge();

                        resultItem.LocationServiceCharge_Index = new Guid();
                        resultItem.ServiceCharge_Index = data.serviceCharge_Index.GetValueOrDefault();
                        resultItem.ServiceCharge_Id = data.serviceCharge_Id;
                        resultItem.ServiceCharge_Name = data.serviceCharge_Name;
                        resultItem.Owner_Index = data.owner_Index.GetValueOrDefault();
                        resultItem.Owner_Id = data.owner_Id;
                        resultItem.Owner_Name = data.owner_Name;
                        resultItem.Warehouse_Index = item.warehouse_Index.GetValueOrDefault();
                        resultItem.Warehouse_Id = item.warehouse_Id;
                        resultItem.Warehouse_Name = item.warehouse_Name;
                        resultItem.Room_Index = item.room_Index.GetValueOrDefault();
                        resultItem.Room_Id = item.room_Id;
                        resultItem.Room_Name = item.room_Name;

                        #region Find LocationType
                        var queryLoType = db.MS_LocationType.Find(item.locationType_Index);
                        if (queryLoType != null)
                        {
                            resultItem.LocationType_Index = queryLoType.LocationType_Index;
                            resultItem.LocationType_Id = queryLoType.LocationType_Id;
                            resultItem.LocationType_Name = queryLoType.LocationType_Name;
                        }

                        #endregion

                        resultItem.Location_Index = item.location_Index.GetValueOrDefault(); ;
                        resultItem.Location_Id = item.location_Id;
                        resultItem.Location_Name = item.location_Name;
                        resultItem.IsActive = 1;
                        resultItem.IsDelete = 0;
                        resultItem.Create_Date = DateTime.Now;
                        resultItem.Create_By = data.userName;
                        db.ms_LocationServiceCharge.Add(resultItem);
                    }

                }

                var deleteItemLoc = db.ms_LocationServiceCharge.Where(c => !data.listServiceWRL.Select(s => s.locationServiceCharge_Index).Contains(c.LocationServiceCharge_Index)
                                     && c.ServiceCharge_Index == data.serviceCharge_Index).ToList();


                foreach (var c in deleteItemLoc)
                {
                    var deleteLocationServiceCharge = db.ms_LocationServiceCharge.Find(c.LocationServiceCharge_Index);
                    deleteLocationServiceCharge.IsActive = 0;
                    deleteLocationServiceCharge.IsDelete = 1;
                    deleteLocationServiceCharge.Cancel_By = data.userName;
                    deleteLocationServiceCharge.Cancel_Date = DateTime.Now;

                }


                #endregion



                #region StorageCharge

                var queryStorageCharge = db.ms_StorageCharge.Where(c => c.ServiceCharge_Index == data.serviceCharge_Index && c.IsActive == 1 && c.IsDelete == 0).ToList();

                var process_Index = new Guid("FBEDC6EF-3871-4CDF-84EE-FE3FF618113D");

                if (queryStorageCharge.Count > 0)
                {
                    foreach (var item in data.listStorage)
                    {
                        var query = db.ms_StorageCharge.Find(item.storageCharge_Index);

                        if (query == null)
                        {
                            var resultItem = new ms_StorageCharge();
                            resultItem.StorageCharge_Index = new Guid();
                            resultItem.ServiceCharge_Index = data.serviceCharge_Index.GetValueOrDefault();
                            resultItem.ServiceCharge_Id = data.serviceCharge_Id;
                            resultItem.ServiceCharge_Name = data.serviceCharge_Name;
                            resultItem.Owner_Index = data.owner_Index.GetValueOrDefault();
                            resultItem.Owner_Id = data.owner_Id;
                            resultItem.Owner_Name = data.owner_Name;
                            resultItem.Currency_Index = item.currency_Index;
                            resultItem.UnitCharge_Name = item.unitCharge_Name;

                            if (item.isMaxQty == 0)
                            {
                                resultItem.QtyStart = item.qtyStart;
                            }
                            else
                            {
                                resultItem.QtyMax = item.qtyStart;
                            }
                            resultItem.QtyEnd = item.qtyEnd;
                            resultItem.IsMaxQty = item.isMaxQty;
                            resultItem.Rate = item.rate;
                            resultItem.StorageDay = item.storageDay;
                            resultItem.FreeDay = item.freeDay;
                            resultItem.Minimumrate = item.minimumrate;
                            resultItem.IsActive = 1;
                            resultItem.IsDelete = 0;
                            resultItem.Create_By = data.userName;
                            resultItem.Create_Date = DateTime.Now;
                            resultItem.DEFAULT_Process_Index = process_Index;
                            db.ms_StorageCharge.Add(resultItem);

                        }
                    }
                }

                else
                {
                    foreach (var item in data.listStorage)
                    {
                        var resultItem = new ms_StorageCharge();

                        resultItem.StorageCharge_Index = new Guid();
                        resultItem.ServiceCharge_Index = data.serviceCharge_Index.GetValueOrDefault();
                        resultItem.ServiceCharge_Id = data.serviceCharge_Id;
                        resultItem.ServiceCharge_Name = data.serviceCharge_Name;
                        resultItem.Owner_Index = data.owner_Index.GetValueOrDefault();
                        resultItem.Owner_Id = data.owner_Id;
                        resultItem.Owner_Name = data.owner_Name;
                        resultItem.Currency_Index = item.currency_Index;
                        resultItem.UnitCharge_Name = item.unitCharge_Name;
                        if (item.isMaxQty == 0)
                        {
                            resultItem.QtyStart = item.qtyStart;
                        }
                        else
                        {
                            resultItem.QtyMax = item.qtyStart;
                        }
                        resultItem.QtyEnd = item.qtyEnd;
                        resultItem.IsMaxQty = item.isMaxQty;
                        resultItem.Rate = item.rate;
                        resultItem.StorageDay = item.storageDay;
                        resultItem.FreeDay = item.freeDay;
                        resultItem.Minimumrate = item.minimumrate;
                        resultItem.IsActive = 1;
                        resultItem.IsDelete = 0;
                        resultItem.Create_By = data.userName;
                        resultItem.Create_Date = DateTime.Now;
                        resultItem.DEFAULT_Process_Index = process_Index;
                        db.ms_StorageCharge.Add(resultItem);

                    }

                }

                var deleteItem = db.ms_StorageCharge.Where(c => !data.listStorage.Select(s => s.storageCharge_Index).Contains(c.StorageCharge_Index)
                           && c.ServiceCharge_Index == data.serviceCharge_Index
                           && c.Owner_Index == data.owner_Index).ToList();

                foreach (var c in deleteItem)
                {
                    var deleteStorageCharge = db.ms_StorageCharge.Find(c.StorageCharge_Index);
                    deleteStorageCharge.IsActive = 0;
                    deleteStorageCharge.IsDelete = 1;
                    deleteStorageCharge.Cancel_By = data.userName;
                    deleteStorageCharge.Cancel_Date = DateTime.Now;

                }
                #endregion



                actionResultstorage.msg = "MSG_Save_error";


                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                    actionResultstorage.msg = "MSG_Save_success";
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("StorageCharge", msglog);
                    transactionx.Rollback();

                    throw exy;
                }


                return actionResultstorage;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region findStorageCharge

        public actionResultstorage findStorageCharge(storageSaveViewModel data)
        {
            try
            {
                var actionResultstorage = new actionResultstorage();

                var queryLocation = db.ms_LocationServiceCharge.Where(c => c.ServiceCharge_Index == data.serviceCharge_Index
                                    &&  c.Owner_Index == data.owner_Index && c.IsActive == 1 && c.IsDelete == 0).ToList();

                var queryStorageCharge = db.ms_StorageCharge.Where(c => c.ServiceCharge_Index == data.serviceCharge_Index
                                         && c.Owner_Index == data.owner_Index && c.IsActive == 1 && c.IsDelete == 0).ToList();


                var Location = new List<locationServiceChargeViewModel>();

                foreach (var item in queryLocation)
                {
                    var resultItem = new locationServiceChargeViewModel();

                    resultItem.locationServiceCharge_Index = item.LocationServiceCharge_Index;
                    resultItem.serviceCharge_Index = data.serviceCharge_Index.GetValueOrDefault();
                    resultItem.serviceCharge_Id = data.serviceCharge_Id;
                    resultItem.serviceCharge_Name = data.serviceCharge_Name;
                    resultItem.owner_Index = data.owner_Index.GetValueOrDefault();
                    resultItem.owner_Id = data.owner_Id;
                    resultItem.owner_Name = data.owner_Name;
                    resultItem.warehouse_Index = item.Warehouse_Index;
                    resultItem.warehouse_Id = item.Warehouse_Id;
                    resultItem.warehouse_Name = item.Warehouse_Name;
                    resultItem.room_Index = item.Room_Index;
                    resultItem.room_Id = item.Room_Id;
                    resultItem.room_Name = item.Room_Name;
                    resultItem.location_Index = item.Location_Index;
                    resultItem.location_Id = item.Location_Id;
                    resultItem.location_Name = item.Location_Name;
                    resultItem.locationType_Index = item.LocationType_Index;
                    Location.Add(resultItem);
                }


                var StorageCharge = new List<storageChargeModel>();

                foreach (var item in queryStorageCharge.OrderBy(o => o.QtyEnd).ThenBy(o => o.QtyMax))
                {
                    var resultItem = new storageChargeModel();

                    resultItem.storageCharge_Index = item.StorageCharge_Index;
                    resultItem.serviceCharge_Index = data.serviceCharge_Index.GetValueOrDefault();
                    resultItem.serviceCharge_Id = data.serviceCharge_Id;
                    resultItem.serviceCharge_Name = data.serviceCharge_Name;
                    resultItem.owner_Index = data.owner_Index.GetValueOrDefault();
                    resultItem.owner_Id = data.owner_Id;
                    resultItem.owner_Name = data.owner_Name;
                    resultItem.currency_Index = item.Currency_Index;
                    resultItem.unitCharge_Name = item.UnitCharge_Name;

                    if (item.IsMaxQty == 1)
                    {
                        resultItem.qtyStart = item.QtyMax;
                    }
                    else
                    {
                        resultItem.qtyStart = item.QtyStart;
                    }
                    resultItem.qtyEnd = item.QtyEnd;
                    resultItem.qtyMax = item.QtyMax;
                    resultItem.isMaxQty = item.IsMaxQty;
                    resultItem.rate = item.Rate;
                    resultItem.storageDay = item.StorageDay;
                    resultItem.freeDay = item.FreeDay;
                    resultItem.minimumrate = item.Minimumrate;
                    resultItem.dEFAULT_Process_Index = item.DEFAULT_Process_Index;
                    StorageCharge.Add(resultItem);
                }

                actionResultstorage.listServiceWRL = Location;
                actionResultstorage.listStorage = StorageCharge;


                return actionResultstorage;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        #endregion


        #region getConfigStorageCharge

        public List<storageChargeModel> getConfigStorageCharge(storageChargeModel data)
        {
            try
            {
                var queryStorageCharge = db.ms_StorageCharge.Where(c => c.Owner_Index == data.owner_Index && c.ServiceCharge_Index == data.serviceCharge_Index && c.IsActive == 1 && c.IsDelete == 0).ToList();

                var StorageCharge = new List<storageChargeModel>();

                foreach (var item in queryStorageCharge.OrderBy(o => o.QtyEnd).ThenBy(o => o.QtyMax))
                {
                    var resultItem = new storageChargeModel();

                    resultItem.storageCharge_Index = item.StorageCharge_Index;
                    resultItem.serviceCharge_Index = data.serviceCharge_Index.GetValueOrDefault();
                    resultItem.serviceCharge_Id = data.serviceCharge_Id;
                    resultItem.serviceCharge_Name = data.serviceCharge_Name;
                    resultItem.owner_Index = data.owner_Index.GetValueOrDefault();
                    resultItem.owner_Id = data.owner_Id;
                    resultItem.owner_Name = data.owner_Name;
                    resultItem.currency_Index = item.Currency_Index;
                    resultItem.unitCharge_Name = item.UnitCharge_Name;

                    if (item.IsMaxQty == 1)
                    {
                        resultItem.qtyStart = item.QtyMax;
                    }
                    else
                    {
                        resultItem.qtyStart = item.QtyStart;
                    }
                    resultItem.qtyEnd = item.QtyEnd;
                    resultItem.qtyMax = item.QtyMax;
                    resultItem.isMaxQty = item.IsMaxQty;
                    resultItem.rate = item.Rate;
                    resultItem.storageDay = item.StorageDay;
                    resultItem.freeDay = item.FreeDay;
                    resultItem.minimumrate = item.Minimumrate;
                    resultItem.dEFAULT_Process_Index = item.DEFAULT_Process_Index;
                    StorageCharge.Add(resultItem);
                }

                return StorageCharge;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region getConfigLocationServiceCharge

        public List<locationServiceChargeViewModel> getConfigLocationServiceCharge(locationServiceChargeViewModel data)
        {
            try
            {

                var queryLocation = db.ms_LocationServiceCharge.Where(c => c.Owner_Index == data.owner_Index && c.ServiceCharge_Index == data.serviceCharge_Index &&c.IsActive == 1 && c.IsDelete == 0).ToList();


                var Location = new List<locationServiceChargeViewModel>();

                foreach (var item in queryLocation)
                {
                    var resultItem = new locationServiceChargeViewModel();

                    resultItem.locationServiceCharge_Index = item.LocationServiceCharge_Index;
                    resultItem.serviceCharge_Index = data.serviceCharge_Index.GetValueOrDefault();
                    resultItem.serviceCharge_Id = data.serviceCharge_Id;
                    resultItem.serviceCharge_Name = data.serviceCharge_Name;
                    resultItem.owner_Index = data.owner_Index.GetValueOrDefault();
                    resultItem.owner_Id = data.owner_Id;
                    resultItem.owner_Name = data.owner_Name;
                    resultItem.warehouse_Index = item.Warehouse_Index;
                    resultItem.warehouse_Id = item.Warehouse_Id;
                    resultItem.warehouse_Name = item.Warehouse_Name;
                    resultItem.room_Index = item.Room_Index;
                    resultItem.room_Id = item.Room_Id;
                    resultItem.room_Name = item.Room_Name;
                    resultItem.location_Index = item.Location_Index;
                    resultItem.location_Id = item.Location_Id;
                    resultItem.location_Name = item.Location_Name;
                    resultItem.locationType_Index = item.LocationType_Index;
                    Location.Add(resultItem);
                }

                return Location;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region dropDownStorageCharge

        public List<storageChargeModel> dropDownStorageCharge(storageChargeModel data)
        {
            try
            {
                var queryStorageCharge = db.ms_StorageCharge.Where(c => c.IsActive == 1 && c.IsDelete == 0)
                    .GroupBy(c => new
                    {
                        c.ServiceCharge_Index,
                        c.ServiceCharge_Id,
                        c.ServiceCharge_Name
                    })
                    .Select(c => new
                    {
                        c.Key.ServiceCharge_Index,
                        c.Key.ServiceCharge_Id,
                        c.Key.ServiceCharge_Name

                    }).ToList();

                   
                var StorageCharge = new List<storageChargeModel>();

                foreach (var item in queryStorageCharge)
                {
                    var resultItem = new storageChargeModel();
                    resultItem.serviceCharge_Index = item.ServiceCharge_Index;
                    resultItem.serviceCharge_Id = item.ServiceCharge_Id;
                    resultItem.serviceCharge_Name = item.ServiceCharge_Name;
                    StorageCharge.Add(resultItem);
                }

                return StorageCharge;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion


        #region filterSelectAll
        public actionResultWRL filterSelectAll(WRLViewModel data)
        {
            try
            {
                var query = db.View_WRL.AsQueryable();

                query = query.Where(c => c.Warehouse_Index == data.warehouse_Index && c.Room_Index == data.room_Index);

                if (!string.IsNullOrEmpty(data.locationAisle_Index.ToString()))
                {
                    query = query.Where(c => c.LocationAisle_Index == data.locationAisle_Index);
                }

                if (data.listServiceWRL.Count > 0)
                {
                    query = query.Where(c => !data.listServiceWRL.Select(s => s.location_Index).Contains(c.Location_Index));
                }

                var queryResult = query.ToList();


                var result = new List<WRLViewModel>();

                var Item = new List<View_WRL>();
                var TotalRow = new List<View_WRL>();

                TotalRow = query.OrderByDescending(o => o.Location_Id).ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderByDescending(o => o.Location_Id).ToList();

                foreach (var item in TotalRow)
                {
                    var resultItem = new WRLViewModel();

                    resultItem.warehouse_Index = item.Warehouse_Index;
                    resultItem.warehouse_Id = item.Warehouse_Id;
                    resultItem.warehouse_Name = item.Warehouse_Name;

                    resultItem.room_Index = item.Room_Index;
                    resultItem.room_Id = item.Room_Id;
                    resultItem.room_Name = item.Room_Name;

                    resultItem.location_Index = item.Location_Index;
                    resultItem.location_Id = item.Location_Id;
                    resultItem.location_Name = item.Location_Name;

                    resultItem.locationAisle_Index = item.LocationAisle_Index;
                    resultItem.locationType_Index = item.LocationType_Index;

                    result.Add(resultItem);
                }




                var count = TotalRow.Count;

                var actionResultWRL = new actionResultWRL();
                actionResultWRL.items = result.ToList();
                //actionResultWRL.total = TotalRow.ToList();
                actionResultWRL.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage };

                return actionResultWRL;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



    }
}
