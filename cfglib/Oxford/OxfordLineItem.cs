using cfgdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cfglib
{
    public class OxfordLineItem
    {
        RawOxford rawOxford;

        public OxfordLineItem() { }

        public OxfordLineItem(RawOxford raw)
        {
            rawOxford = raw;
        }

        public int Id { get; set; }
        //[Range(1,12)]
        public int Month { get; set; }
        //[Range(1990,2025)]
        public int Year { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string InvoicePeriod { get; set; }
        public decimal AmountBilled { get; set; }
        public Nullable<decimal> PaymentReceived { get; set; }
        public Nullable<double> PercentOfPremium { get; set; }
        public int PEPM { get; set; }
        public Nullable<int> SubCountPEPM { get; set; }
        public Nullable<decimal> CommissionAmount { get; set; }
        public decimal AmountDue { get; set; }
        public string FileName { get; set; }
        public System.DateTime Created { get; set; }
        public bool Deleted { get; set; }
    }
}
