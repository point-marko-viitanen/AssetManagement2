﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement2.Models
{
    public class LocatedAssetsViewModel
    {
        public int Id { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public string LastSeen { get; set; }

    }
}
