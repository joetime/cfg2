using cfgdata;
using joetime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cfglib
{
    //public partial class OxfordLineItem //: ILineItem { }
    //public partial class HorizonLineItem //: ILineItem { }

    public partial class Repos
    {
        RawOxfordBatchOps OxfordBatchOps = new RawOxfordBatchOps();

        public int RawOxfordRecordsCount(int year, int month)
        {
            JoeUtils.YearMonthCheck(year, month);

            return DB.RawOxfords
                .Where(x =>
                    !x.Deleted
                    && x.Month == month
                    && x.Year == year)
                .Count();

        }

        public void AddOxfordRecords(List<OxfordLineItem> items, int year, int month,
            int batchSize = 250)
        {
            double time = OxfordBatchOps.AddRecords(items, year, month, 250);

            AddAudit(
                message: String.Format("Add {0} RawHorizons, month: {1} year: {2}... totalSeconds: {3:0.00}", items.Count, month, year, time),
                objectType: "RawHorizon",
                objectKey: null,
                recordCount: items.Count(),
                action: DbActionType.Insert);
        }

        public void DeleteOxfordRecords(int year, int month)
        {
            OxfordBatchOps.DeleteRecords(year, month);

            AddAudit(
                message: String.Format("Delete Horizon records (M/Y): {0}/{1}", month, year),
                objectType: "RawOxford",
                objectKey: null,
                //recordCount: records.Count(),
                action: DbActionType.Delete);

        }
    }
}
