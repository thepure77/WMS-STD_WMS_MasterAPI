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

namespace MasterDataBusiness.BoxType
{
    public class BoxTypeService
    {
        private MasterDataDbContext db;

        public BoxTypeService()
        {
            db = new MasterDataDbContext();
        }

        public BoxTypeService(MasterDataDbContext db)
        {
            this.db = db;
        }

        public List<BoxTypeViewModel> boxTypedropdown(BoxTypeViewModel data)
        {
            try
            {
                var result = new List<BoxTypeViewModel>();

                var query = db.ms_BoxType.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.BoxType_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new BoxTypeViewModel();

                    resultItem.boxType_Index = item.BoxType_Index;
                    resultItem.boxType_Id = item.BoxType_Id;
                    resultItem.boxType_Name = item.BoxType_Name;
                
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
