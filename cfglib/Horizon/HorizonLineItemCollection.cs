using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cfglib
{
    public class HorizonLineItemCollection
    {
        public List<HorizonLineItem> LineItems { get; private set; }

        // CONSTRUCTOR
        public HorizonLineItemCollection(ICollection<HorizonLineItem> collection)
        {
            LineItems = collection.ToList();
        }

        
        // SAVE TO DB
        public void SaveToDatabase(int year, int month, bool overwrite = false)
        {
            // check for valid date
            if (year < 1990 || month < 1 || month > 12)
                throw new InvalidOperationException(String.Format("Date seems invalid (M/Y): {0}/{1}", month, year));

            /// check for lineitems to save
            if (LineItems.Count == 0)
                throw new InvalidOperationException("Nothing to save.");

            Repos repos = new Repos();


            int checkDb = repos.RawHorizonRecordsCount(year, month);


            if (checkDb > 0 && !overwrite)
                throw new InvalidOperationException(
                    String.Format("{0} Horizon records exist with year/month: {1}/{2}, and overwrite flag was not set.",
                        checkDb, year, month));

            else if (checkDb > 0 && overwrite)
            {
                /// Force to overwrite records
                repos.DeleteHorizonRecords(year, month);
                repos.SaveChanges();                    // SAVE 
            }

            // Add records (SaveChanges included)
            repos.AddHorizonRecords(LineItems, year, month);

            //repos.SaveChanges();

        }

        // TOTAL FIELDS
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
