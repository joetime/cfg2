using cfgdata;
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
        public int RawHorizonRecordsCount(int year, int month)
        {
            YearMonthCheck(year, month);

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
            YearMonthCheck(year, month);
            
            town6668Entities AddContext = new town6668Entities();
            AddContext.Configuration.AutoDetectChangesEnabled = false;
            AddContext.Configuration.ValidateOnSaveEnabled = false;

            string sql_header = "INSERT into cfg.RawHorizon " +
                        "(Created,Month,Year,Filename,GroupNumber,GroupName,Product," +
                        "InsuredPeriod,RenewalDate,EffectiveDate," +
                        "CommissionSchedule,CommissionReceived,PremiumReceived," +
                        "_CoastalCode,_CodeOneHundredd,_TotalPremiumYTD" +
                    ") VALUES ";
            
            int count = 0;
            int total = 0;
            string sql_command = sql_header;
            DateTime start = DateTime.Now;
            DateTime loop = DateTime.Now;

            foreach (HorizonLineItem item in items)
            {
                count++;
                total++;

                sql_command += String.Format(
                    " (getDate(), " +
                        "{0},{1},'{2}','{3}','{4}','{5}'," +
                        "'{6}','{7}','{8}', " +
                        "'{9}',{10},{11}," +
                        "'{12}','{13}','{14}'" +
                    ")",
                    month, year, item.Filename, item.GroupNumber, item.GroupName.Replace("'","''"), item.Product,
                    item.InsuredPeriod, item.RenewalDate, item.EffectiveDate,
                    item.CommissionSchedule, item.CommissionReceived, item.PremiumReceived,
                    item.C_CoastalCode, item.C_CodeOneHundredd, item.C_TotalPremiumYTD);

                if (count % batchSize == 0 || total == items.Count)
                {
                    AddContext.Database.ExecuteSqlCommand(sql_command);

                    Double seconds = DateTime.Now.Subtract(loop).TotalSeconds;

                    Debug.WriteLine(
                            String.Format("{0:00000} - {1:0000} - {2:000.00} - {3:000}",
                                batchSize, total, seconds,
                                (seconds / batchSize) * 4000
                            ));

                    count = 0;
                    sql_command = sql_header;

                    AddContext.Dispose();
                    AddContext = new town6668Entities();
                    AddContext.Configuration.AutoDetectChangesEnabled = false;
                    AddContext.Configuration.ValidateOnSaveEnabled = false;

                    loop = DateTime.Now;
                }
                else
                {
                    sql_command += ",";
                }
            }

            // final execute... 
            if (count > 0)
                AddContext.Database.ExecuteSqlCommand(sql_command);


            double totalSeconds = DateTime.Now.Subtract(start).TotalSeconds;

            AddAudit(
                message: String.Format("Add {0} RawHorizons, month: {1} year: {2}... totalSeconds: {3:0.00}", items.Count, month, year, totalSeconds),
                objectType: "RawHorizon",
                objectKey: null,
                recordCount: items.Count(),
                action: DbActionType.Insert);

            DB.SaveChanges();
        }


        public void DeleteHorizonRecords(int year, int month)
        {
            YearMonthCheck(year, month);

            town6668Entities DeleteContext = new town6668Entities();
            DeleteContext.Configuration.AutoDetectChangesEnabled = false;

            var records = DeleteContext.RawHorizons.Where(x => x.Month == month && x.Year == year && !x.Deleted);

            foreach (RawHorizon record in records)
                record.Deleted = true;

            DeleteContext.SaveChanges();

            AddAudit(
                message: String.Format("Delete Horizon records (M/Y): {0}/{1}", month, year),
                objectType: "RawHorizon",
                objectKey: null,
                recordCount: records.Count(),
                action: DbActionType.Delete);

            DB.SaveChanges();
        }
    }
}
