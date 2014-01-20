using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cfglib
{
    //public enum HorizonLineItemStatus
    //{
    //    Ok,
    //    Error,
    //    Unknown
    //}

    public partial class HorizonLineItem
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public Nullable<int> Month { get; set; }
        public string GroupName { get; set; }
        public string GroupNumber { get; set; }
        public string Product { get; set; }
        public string EffectiveDate { get; set; }
        public string RenewalDate { get; set; }
        public string InsuredPeriod { get; set; }
        public string C_CodeOneHundredd { get; set; }
        public string CommissionSchedule { get; set; }
        public string C_TotalPremiumYTD { get; set; }
        public decimal PremiumReceived { get; set; }
        public decimal CommissionReceived { get; set; }
        public string Filename { get; set; }
        public System.DateTime Created { get; set; }
        public bool Deleted { get; set; }
        public string C_CoastalCode { get; set; }
    }
}

