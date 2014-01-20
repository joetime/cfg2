using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using joetime;

namespace cfglib
{
    public partial class Repos
    {
        public object ImportFile(string path, int month, int year, string provider)
        {
            JoeUtils.YearMonthCheck(year, month);

            if (provider.ToLower() == "oxford")
            {
                List<OxfordLineItem> items =  OxfordUtils.ReadFile(path);
                OxfordLineItemCollection coll = new OxfordLineItemCollection(items);
                coll.SaveToDatabase(year, month, true);

                return new
                {
                    LineItems = items.Count
                };
            }
            else if (provider.ToLower() == "horizon")
            {
                List<HorizonLineItem> items = HorizonUtils.ReadFile(path);
                HorizonLineItemCollection coll = new HorizonLineItemCollection(items);
                coll.SaveToDatabase(year, month, true);

                return new
                {
                    LineItems = items.Count
                };
            }
            else {
                throw new InvalidOperationException(String.Format("'{0}' is not a valid provider name.", provider));
            }
        }
    }
}
