using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MasterDataAPI.Controllers
{
    public class Pagination
    {
        public int CurrentPage { get; set; }

        public int NumPerPage { get; set; }

        public int PerPage { get; set; }

        public int TotalRow { get; set; }

        public string Key { get; set; }

        public bool AdvanceSearch { get; set; }
    }
}
