//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace cfgdata
{
    using System;
    using System.Collections.Generic;
    
    public partial class hzCommissionRate
    {
        public int Id { get; set; }
        public int BrokerId { get; set; }
        public string Schedule { get; set; }
        public double Rate { get; set; }
    
        public virtual hzBroker hzBroker { get; set; }
    }
}
