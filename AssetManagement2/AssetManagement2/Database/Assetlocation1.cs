//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AssetManagement2.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Assetlocation1
    {
        public int Id { get; set; }
        public Nullable<int> LocationId { get; set; }
        public Nullable<int> AssetId { get; set; }
        public Nullable<System.DateTime> LastSeen { get; set; }
    
        public virtual AssetLocation AssetLocation { get; set; }
        public virtual Asset Asset { get; set; }
    }
}
