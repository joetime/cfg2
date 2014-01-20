using cfgdata;
using joetime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace cfglib
{
    public partial class Repos
    {
        RawHorizonBatchOps HorizonBatchOps = new RawHorizonBatchOps();

        public int RawHorizonRecordsCount(int year, int month)
        {
            JoeUtils.YearMonthCheck(year, month);

            return DB.RawHorizons
                .Where(x =>
                    !x.Deleted
                    && x.Month == month
                    && x.Year == year)
                .Count();
        
        }

        public void AddHorizonRecords(List<HorizonLineItem> items, int year, int month,
            int batchSize = 250)
        {
            double time = HorizonBatchOps.AddRecords(items, year, month, 250);

            AddAudit(
                message: String.Format("Add {0} RawHorizons, month: {1} year: {2}... totalSeconds: {3:0.00}", items.Count, month, year, time),
                objectType: "RawHorizon",
                objectKey: null,
                recordCount: items.Count(),
                action: DbActionType.Insert);

        }

        public void DeleteHorizonRecords(int year, int month) 
        {
            HorizonBatchOps.DeleteRecords(year, month);

            AddAudit(
                message: String.Format("Delete Horizon records (M/Y): {0}/{1}", month, year),
                objectType: "RawHorizon",
                objectKey: null,
                //recordCount: records.Count(),
                action: DbActionType.Delete);

        }

    }
}
