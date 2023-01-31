using Business.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.Rebuild
{
    public class CloseWaveViewModel : Result
    {
        public CloseWaveViewModel()
        {
            models = new List<CloseWaveViewModel>();
        }
        public Guid? Rebuild_Index { get; set; }

        public string Rebuild_By { get; set; }

        public string Rebuild_Date_Start { get; set; }

        public string Rebuild_Date_End { get; set; }

        public bool isuse_rebuild { get; set; }

        public string key { get; set; }

        public List<CloseWaveViewModel> models { get; set; }

    }
}
