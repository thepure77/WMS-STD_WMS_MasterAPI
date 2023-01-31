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
    public class OwnerVendorService
    {
        private MasterDataDbContext db;

        public OwnerVendorService()
        {
            db = new MasterDataDbContext();
        }

        public OwnerVendorService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterOwnerVendor
        public actionResultOwnerVendorViewModel filter(SearchOwnerVendorViewModel data)
        {
            try
            {
                var query = db.View_OwnerVendor.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.OwnerVendor_Id.Contains(data.key)
                                        || c.Owner_Name.Contains(data.key)
                                        || c.Vendor_Name.Contains(data.key));
                                      
                }

                var Item = new List<View_OwnerVendor>();
                var TotalRow = new List<View_OwnerVendor>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.OwnerVendor_Id).ToList();

                var result = new List<SearchOwnerVendorViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchOwnerVendorViewModel();

                    resultItem.ownerVendor_Index = item.OwnerVendor_Index;
                    resultItem.ownerVendor_Id = item.OwnerVendor_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.vendor_Id = item.Vendor_Id;
                    resultItem.vendor_Name = item.Vendor_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultOwnerVendorViewModel = new actionResultOwnerVendorViewModel();
                actionResultOwnerVendorViewModel.itemsOwnerVendor = result.ToList();
                actionResultOwnerVendorViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultOwnerVendorViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region SaveChanges
        public String SaveChanges(OwnerVendorViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var OwnerVendorOld = db.MS_OwnerVendor.Find(data.ownerVendor_Index);

                if (OwnerVendorOld == null)
                {
                    if (!string.IsNullOrEmpty(data.ownerVendor_Id))
                    {
                        var query = db.MS_OwnerVendor.FirstOrDefault(c => c.OwnerVendor_Id == data.ownerVendor_Id && c.IsActive == 1);
                        if (query != null)
                        {
                            return "Fail";
                        }
                    }
                    if (string.IsNullOrEmpty(data.ownerVendor_Id))
                    {
                        data.ownerVendor_Id = "OwnerVendor_Id".genAutonumber();
                        int i = 1;
                        while (i > 0)
                        {
                            var query = db.MS_OwnerVendor.FirstOrDefault(c => c.OwnerVendor_Id == data.ownerVendor_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                if (query.OwnerVendor_Id == data.ownerVendor_Id)
                                {
                                    data.ownerVendor_Id = "OwnerVendor_Id".genAutonumber();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    //MS_OwnerVendor Model = new MS_OwnerVendor();

                    //Model.OwnerVendor_Index = Guid.NewGuid();
                    //Model.OwnerVendor_Id = data.ownerVendor_Id;
                    //Model.Owner_Index = data.owner_Index;
                    //Model.Vendor_Index = data.vendor_Index;
                    //Model.IsActive = Convert.ToInt32(data.isActive);
                    //Model.IsDelete = 0;
                    //Model.IsSystem = 0;
                    //Model.Status_Id = 0;
                    //Model.Create_By = data.create_By;
                    //Model.Create_Date = DateTime.Now;

                    //db.MS_OwnerVendor.Add(Model);


                    foreach (var item in data.listOwnerVendorItemViewModel)
                    {
                        MS_OwnerVendor resultItem = new MS_OwnerVendor();

                        resultItem.OwnerVendor_Index = Guid.NewGuid();
                        resultItem.Owner_Index = data.owner_Index;
                        resultItem.Vendor_Index = item.vendor_Index;
                        resultItem.OwnerVendor_Id = data.ownerVendor_Id;
                        resultItem.IsActive = 1;
                        resultItem.IsDelete = 0;
                        resultItem.IsSystem = 0;
                        resultItem.Status_Id = 0;
                        resultItem.Create_By = data.create_By;
                        resultItem.Create_Date = DateTime.Now;
                        db.MS_OwnerVendor.Add(resultItem);

                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(data.ownerVendor_Id))
                    {
                        if (OwnerVendorOld.OwnerVendor_Id != "")
                        {
                            data.ownerVendor_Id = OwnerVendorOld.OwnerVendor_Id;
                        }
                    }
                    else
                    {
                        if (OwnerVendorOld.OwnerVendor_Id != data.ownerVendor_Id)
                        {
                            var query = db.MS_OwnerVendor.FirstOrDefault(c => c.OwnerVendor_Id == data.ownerVendor_Id && c.IsActive == 1);
                            if (query != null)
                            {
                                return "Fail";
                            }
                        }
                        else
                        {
                            data.ownerVendor_Id = OwnerVendorOld.OwnerVendor_Id;
                        }
                    }

                    OwnerVendorOld.OwnerVendor_Id = data.ownerVendor_Id;
                    OwnerVendorOld.Owner_Index = data.owner_Index;
                    OwnerVendorOld.Vendor_Index = data.vendor_Index;
                    OwnerVendorOld.IsActive = Convert.ToInt32(data.isActive);
                    OwnerVendorOld.Update_By = data.create_By;
                    OwnerVendorOld.Update_Date = DateTime.Now;
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
                    olog.logging("SaveOwnerVendor", msglog);
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

        #region find
        public OwnerVendorViewModel find(Guid id)
        {
            try
            {

                var queryResult = db.View_OwnerVendor.Where(c => c.OwnerVendor_Index == id).FirstOrDefault();

                var result = new OwnerVendorViewModel();


                result.ownerVendor_Index = queryResult.OwnerVendor_Index;
                result.ownerVendor_Id = queryResult.OwnerVendor_Id;
                result.owner_Index = queryResult.Owner_Index;
                result.owner_Name = queryResult.Owner_Name;
                result.vendor_Index = queryResult.Vendor_Index;
                result.vendor_Name = queryResult.Vendor_Name;
                result.key = queryResult.Owner_Id + " - " + queryResult.Owner_Name;
                result.key2 = queryResult.Vendor_Id + " - " + queryResult.Vendor_Name;
                result.isActive = queryResult.IsActive;

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region findOwnerVendor
        public OwnerVendorViewModel findOwnerVendor(Guid id)
        {
            try
            {

                var queryResult = db.View_OwnerVendor.Where(c => c.Owner_Index == id && c.IsActive == 1).ToList();


                var result = new OwnerVendorViewModel();

                result.listOwnerVendorItemViewModel = new List<OwnerVendorItemViewModel>();

                foreach (var item in queryResult)
                {
                    var resultItem = new OwnerVendorItemViewModel();
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.ownerVendor_Index = item.OwnerVendor_Index;
                    resultItem.vendor_Index = item.Vendor_Index;
                    resultItem.vendor_Id = item.Vendor_Id;
                    resultItem.vendor_Name = item.Vendor_Name;


                    result.listOwnerVendorItemViewModel.Add(resultItem);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getDelete
        public Boolean getDelete(OwnerVendorViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var Owner = db.MS_OwnerVendor.Find(data.ownerVendor_Index);

                if (Owner != null)
                {
                    Owner.IsActive = 0;
                    Owner.IsDelete = 1;


                    var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                    try
                    {
                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }

                    catch (Exception exy)
                    {
                        msglog = State + " ex Rollback " + exy.Message.ToString();
                        olog.logging("DeleteOwnerVendor", msglog);
                        transaction.Rollback();
                        throw exy;
                    }

                }


                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoOwnerVendor
        public List<ItemListViewModel> autoOwnerVendor(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_OwnerVendor.Where(c => c.OwnerVendor_Id.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.OwnerVendor_Id,
                        key = s.OwnerVendor_Id
                    }).Distinct();

                    var query2 = db.View_OwnerVendor.Where(c => c.Owner_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Owner_Name,
                        key = s.Owner_Name
                    }).Distinct();

                    var query3 = db.View_OwnerVendor.Where(c => c.Vendor_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Vendor_Name,
                        key = s.Vendor_Name

                    }).Distinct();

                    var query = query1.Union(query2).Union(query2).Union(query3);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }
        #endregion

    }
}
