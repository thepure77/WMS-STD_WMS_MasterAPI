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

namespace MasterDataBusiness.BoxSize
{
    public class BoxSizeService
    {
        private MasterDataDbContext db;

        public BoxSizeService()
        {
            db = new MasterDataDbContext();
        }

        public BoxSizeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        public List<BoxSizeViewModel> boxSizedropdown(BoxSizeViewModel data)
        {
            try
            {
                var result = new List<BoxSizeViewModel>();

                var query = db.ms_BoxSize.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.BoxSize_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new BoxSizeViewModel();

                    resultItem.boxSize_Index = item.BoxSize_Index;
                    resultItem.boxSize_Id = item.BoxSize_Id;
                    resultItem.boxSize_Name = item.BoxSize_Name;
                
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
