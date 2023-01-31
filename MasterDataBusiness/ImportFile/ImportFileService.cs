using Comone.Utils;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MasterDataBusiness
{
    public class ImportFileService
    {
        private MasterDataDbContext db;

        public ImportFileService()
        {
            db = new MasterDataDbContext();
        }

        public ImportFileService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterWave
        public string filter(ImportFileViewModel data)
        {
            try
            {
                return "";
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
