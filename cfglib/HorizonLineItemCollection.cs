using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cfglib
{
    public class HorizonLineItemCollection
    {
        public HorizonLineItemCollection(ICollection<HorizonFileLineItem> collection)
        {
            LineItems = collection;
        }

        public ICollection<HorizonFileLineItem> LineItems { get; private set; }

        public decimal CommissionReceived
        {
            get
            {
                return LineItems.Select(x => x.CommissionReceived).Sum();
            }
        }

        public decimal PremiumReceived
        {
            get
            {
                return LineItems.Select(x => x.PremiumReceived).Sum();
            }
        }

        public decimal CommissionPercentage
        {
            get
            {
                return CommissionReceived / PremiumReceived;
            }
        }
    }
}
