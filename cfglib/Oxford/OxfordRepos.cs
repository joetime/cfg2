using cfgdata;
using joetime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cfglib
{
    public partial class Repos
    {
        //protected IQueryable<RawOxford> RawOxfordRecords(int year, int month)
        //{

        //    YearMonthCheck(year, month);

        //    return DB.RawOxfords
        //        .Where(x =>
        //            !x.Deleted
        //            && x.Month == month
        //            && x.Year == year);
        //}

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

        public void AddOxfordRecords(List<OxfordLineItem> items, int year, int month)
        {
            JoeUtils.YearMonthCheck(year, month);

            foreach (OxfordLineItem item in items)
            {
                DB.RawOxfords.Add(new RawOxford()
                {
                    Created = item.Created,
                    Month = month,
                    Year = year,
                    FileName = item.FileName,

                    Deleted = false,
                    GroupCode = item.GroupCode,
                    GroupName = item.GroupName,
                    InvoicePeriod = item.InvoicePeriod,
                    AmountBilled = item.AmountBilled,
                    AmountDue = item.AmountDue,
                    CommissionAmount = item.CommissionAmount,

                    PaymentReceived = item.PaymentReceived,
                    PEPM = item.PEPM,
                    PercentOfPremium = item.PercentOfPremium,
                    SubCountPEPM = item.SubCountPEPM
                });
            }

            AddAudit(
                message: String.Format("Add {0} RawOxfords, month: {1} year: {2}", items.Count, month, year),
                objectType: "RawOxford",
                objectKey: null,
                recordCount: items.Count(),
                action: DbActionType.Insert);
        }

        public void DeleteOxfordRecords(IEnumerable<RawOxford> items)
        {
            foreach (RawOxford record in items)
                record.Deleted = true;

            AddAudit(
                message: "Delete Oxford records.",
                objectType: "RawOxford",
                objectKey: null,
                recordCount: items.Count(),
                action: DbActionType.Delete);
        }

        public void DeleteOxfordRecords(int year, int month)
        {
            JoeUtils.YearMonthCheck(year, month);

            var records = DB.RawOxfords.Where(x => x.Month == month && x.Year == year && !x.Deleted);

            foreach (RawOxford record in records)
                record.Deleted = true;

            AddAudit( 
                message: String.Format("Delete Oxford records (M/Y).", month, year), 
                objectType: "RawOxford", 
                objectKey: null, 
                recordCount: records.Count(), 
                action: DbActionType.Delete );
        }
    }
}
