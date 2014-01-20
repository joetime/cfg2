using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cfgdata;
using System.IO;
using joetime;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace cfglib
{   
    public class OxfordLineItemCollection
    {
        List<OxfordLineItem> lineItems;

        public OxfordLineItemCollection(List<OxfordLineItem> list)
        {
            lineItems = list;
        }

        public void SaveToDatabase(int year, int month, bool overwrite = false)
        {            
            // check for valid date
            if (year < 1990 || month < 1 || month > 12)
                throw new InvalidOperationException(String.Format("Date seems invalid (M/Y): {0}/{1}", month, year));

            /// check for lineitems to save
            if (lineItems.Count == 0)
                throw new InvalidOperationException("Nothing to save.");

            Repos repos = new Repos("?");


            int checkDb = repos.RawOxfordRecordsCount(year, month);
            

            if (checkDb > 0 && !overwrite)
                throw new InvalidOperationException(
                    String.Format("{0} Oxford records exist with year/month: {1}/{2}, and overwrite flag was not set.",
                        checkDb, year, month));

            else if (checkDb > 0 && overwrite)
            {
                /// Force to overwrite records
                repos.DeleteOxfordRecords(year, month);
                repos.SaveChanges();                    // SAVE 
            }

            // Add records
            repos.AddOxfordRecords(lineItems, year, month);

            repos.SaveChanges();

        }
    }
}
