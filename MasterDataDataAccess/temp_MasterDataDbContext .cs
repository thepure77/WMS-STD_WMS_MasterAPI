using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MasterDataDataAccess.Models;

namespace DataAccess
{
    public class temp_MasterDataDbContext : DbContext
    {
        public DbSet<sp_LogGi> sp_LogGi { get; set; }

        public DbSet<sp_LogGr> sp_LogGr { get; set; }

        public DbSet<sp_LogTransfer> sp_LogTransfer { get; set; }

        //LogCancle
        public DbSet<sp_LogCancel> sp_LogCancel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false);

                var configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("temp_MasterDefaultConnection").ToString();

                optionsBuilder.UseSqlServer(connectionString);

                //optionsBuilder.UseSqlServer(@"Server=kascoit.ddns.net,22017;Database=WMSDB_QA;Trusted_Connection=True;Integrated Security=False;user id=sa;password=K@sc0db12345;");
                //optionsBuilder.UseSqlServer(@"Server=192.168.1.11\MSSQL2017;Database=WMSDB_QA;Trusted_Connection=True;Integrated Security=False;user id=sa;password=K@sc0db12345;");
            }
            //optionsBuilder.UseSqlServer(@"Server=10.0.177.33\SQLEXPRESS;Database=WMSDB;Trusted_Connection=True;Integrated Security=False;user id=cfrffmusr;password=ffmusr@cfr;");

        }
    }
}
