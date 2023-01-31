using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MasterDataBusiness.T3PL
{
    public class T3PLService
    {
        public List<T3PLViewModel> Filter()
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var queryResult = context.MS_3PL.FromSql("sp_Get3PL").Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();

                    var result = new List<T3PLViewModel>();
                    foreach (var item in queryResult)
                    {
                        var resultItem = new T3PLViewModel();
                        resultItem.C3PLIndex = item.C3PL_Index;
                        resultItem.C3PLId = item.C3PL_Id;
                        resultItem.C3PLName = item.C3PL_Name;
                        resultItem.IsActive = item.IsActive;
                        resultItem.IsDelete = item.IsDelete;
                        resultItem.IsSystem = item.IsSystem;
                        resultItem.StatusId = item.Status_Id;
                        resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
                        resultItem.CreateBy = item.Create_By;
                        resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
                        resultItem.UpdateBy = item.Update_By;
                        resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
                        resultItem.CancelBy = item.Cancel_By;
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

        public List<T3PLViewModel> search(T3PLViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    string pwhereFilter = "";
                    string pwhereLike = "";
                    var result = new List<T3PLViewModel>();
                    if (data.C3PLId != "" && data.C3PLId != null)
                    {
                        pwhereFilter = " And [3PL_Id] like N'%" + data.C3PLId + "%'";
                    }
                    else
                    {
                        pwhereFilter = "";
                    }

                    if (data.C3PLName != "" && data.C3PLName != null)
                    {
                        pwhereFilter += " And [3PL_Name] like N'%" + data.C3PLName + "%'";
                    }
                    else
                    {
                        pwhereFilter += "";
                    }

                    
                        pwhereFilter += " And isActive = '" + 1 + "'";
                        pwhereFilter += " And isDelete = '" + 0 + "'";
                        var strwhere = new SqlParameter("@strwhere", pwhereFilter);
                        var query = context.MS_3PL.FromSql("sp_Get3PL @strwhere ", strwhere).ToList();
                        foreach (var item in query)
                        {
                            var resultItem = new T3PLViewModel();

                            resultItem.C3PLIndex = item.C3PL_Index;
                            resultItem.C3PLId = item.C3PL_Id;
                            resultItem.C3PLName = item.C3PL_Name;
                            resultItem.IsActive = item.IsActive;
                            resultItem.IsDelete = item.IsDelete;
                            resultItem.IsSystem = item.IsSystem;
                            resultItem.StatusId = item.Status_Id;
                            resultItem.CreateDate = item.Create_Date.GetValueOrDefault();
                            resultItem.CreateBy = item.Create_By;
                            resultItem.UpdateDate = item.Update_Date.GetValueOrDefault();
                            resultItem.UpdateBy = item.Update_By;
                            resultItem.CancelDate = item.Cancel_Date.GetValueOrDefault();
                            resultItem.CancelBy = item.Cancel_By;

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
    }
}
