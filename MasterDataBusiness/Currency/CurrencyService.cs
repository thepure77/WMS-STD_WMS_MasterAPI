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

namespace MasterDataBusiness.Currency
{
    public class CurrencyService
    {
        private MasterDataDbContext db;

        public CurrencyService()
        {
            db = new MasterDataDbContext();
        }

        public CurrencyService(MasterDataDbContext db)
        {
            this.db = db;
        }

        public List<CurrencyViewModel> currencydropdown(CurrencyViewModel data)
        {
            try
            {
                var result = new List<CurrencyViewModel>();

                var query = db.ms_Currency.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.Currency_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new CurrencyViewModel();

                    resultItem.currency_Index = item.Currency_Index;
                    resultItem.currency_Id = item.Currency_Id;
                    resultItem.currency_Name = item.Currency_Name;
                
                    result.Add(resultItem);
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
