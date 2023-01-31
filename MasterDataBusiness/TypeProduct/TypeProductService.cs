using DataAccess;
using MasterDataAPI.Controllers;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MasterDataBusiness.ViewModels.SearchTypeProductViewModel;

namespace MasterDataBusiness
{
  
    public class TypeProductService
    {
        private MasterDataDbContext db;

        public TypeProductService()
        {
            db = new MasterDataDbContext();
        }
        public TypeProductService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterTempCondition
        public actionResultTypeProductViewModel filter(SearchTypeProductViewModel data)
        {
            try
            {
                var query = db.ms_Type_Production.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Type_Production_Id.Contains(data.key)
                                         || c.Type_Production_Name.Contains(data.key));
                }

                var Item = new List<ms_Type_Production>();
                var TotalRow = new List<ms_Type_Production>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.Type_Production_Id).ToList();

                var result = new List<SearchTypeProductViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchTypeProductViewModel();

                    resultItem.type_Production_Index = item.Type_Production_Index;
                    resultItem.type_Production_Id = item.Type_Production_Id;
                    resultItem.type_Production_Name = item.Type_Production_Name;
                    resultItem.ref_No1 = item.Ref_No1;
                    resultItem.ref_No2 = item.Ref_No2;
                    resultItem.ref_No3 = item.Ref_No3;
                    resultItem.ref_No4 = item.Ref_No4;
                    resultItem.ref_No5 = item.Ref_No5;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.udf_5 = item.UDF_5;
                    resultItem.remark = item.Remark;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultTypeProductViewModel = new actionResultTypeProductViewModel();
                actionResultTypeProductViewModel.itemsTypeProduct = result.ToList();
                actionResultTypeProductViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResultTypeProductViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }



}
