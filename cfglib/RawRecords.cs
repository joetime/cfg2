using cfgdata;
using joetime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cfglib
{
    public class RawHorizonBatchOps : RawRecordsBatchOps<RawHorizon>
    {
        protected override string TableName { get { return "RawHorizon"; } }

        protected override string sql_InsertHeader { get { return 
            "INSERT into cfg.{0} " +
                        "(Created,Month,Year,Filename,GroupNumber,GroupName,Product," +
                        "InsuredPeriod,RenewalDate,EffectiveDate," +
                        "CommissionSchedule,CommissionReceived,PremiumReceived," +
                        "_CoastalCode,_CodeOneHundred,_TotalPremiumYTD" +
                    ") VALUES ";
            }}

        protected string sql_InsertValues { get { return 
            " (getDate(), " +
                        "{0},{1},'{2}','{3}','{4}','{5}'," +
                        "'{6}','{7}','{8}', " +
                        "'{9}',{10},{11}," +
                        "'{12}','{13}','{14}'" +
                    ")";
            }}

        public override string FormatInsertValuesString(HorizonLineItem item, int year, int month)
        {
            return String.Format(
                sql_InsertValues,
                month, year, item.Filename, item.GroupNumber, item.GroupName.Replace("'", "''"), item.Product,
                item.InsuredPeriod, item.RenewalDate, item.EffectiveDate,
                item.CommissionSchedule, item.CommissionReceived, item.PremiumReceived,
                item.C_CoastalCode, item.C_CodeOneHundredd, item.C_TotalPremiumYTD);
        }
    }

    public abstract class RawRecordsBatchOps<T> where T : IRawRecords
    {
        town6668Entities DB;
        
        IQueryable<IRawRecords> records;


        protected abstract string TableName { get; }
        protected abstract string sql_InsertHeader { get; }

        protected string sql_UpdateCommand =
            "UPDATE cfg.{0} " +
                        "SET Deleted = 1 " +
                        "WHERE Month = {1} AND Year = {2} AND Deleted = 0";

        public abstract string FormatInsertValuesString(HorizonLineItem item, int year, int month);


        public RawRecordsBatchOps()
        {
            DB = new town6668Entities();
            
            records = from rec in DB.RawHorizons select (IRawRecords)rec;
        }

        public void DeleteRecords(int year, int month)
        {
            JoeUtils.YearMonthCheck(year, month);

            string sql_command = String.Format(
                sql_UpdateCommand, TableName, month, year);

            DB.Database.ExecuteSqlCommand(sql_command);
        }
        
        public double AddHorizonRecords(List<HorizonLineItem> items, int year, int month,
            int batchSize = 250)
        {
            JoeUtils.YearMonthCheck(year, month);

            DB.Configuration.AutoDetectChangesEnabled = false;
            DB.Configuration.ValidateOnSaveEnabled = false;

            int count = 0;
            int total = 0;
            string sql_command = String.Format(sql_InsertHeader, TableName);
            DateTime start = DateTime.Now;
            DateTime loop = DateTime.Now;

            foreach (HorizonLineItem item in items)
            {
                count++;
                total++;

                sql_command += FormatInsertValuesString(item, year, month);

                if (count % batchSize == 0 || total == items.Count)
                {
                    DB.Database.ExecuteSqlCommand(sql_command);

                    Double seconds = DateTime.Now.Subtract(loop).TotalSeconds;

                    Debug.WriteLine(
                            String.Format("{0:00000} - {1:0000} - {2:000.00} - {3:000}",
                                batchSize, total, seconds,
                                (seconds / batchSize) * 4000
                            ));

                    count = 0;
                    sql_command = String.Format(sql_InsertHeader, TableName);

                    //DB.Dispose();
                    //DB = new town6668Entities();
                    //DB.Configuration.AutoDetectChangesEnabled = false;
                    //DB.Configuration.ValidateOnSaveEnabled = false;

                    loop = DateTime.Now;
                }
                else
                {
                    sql_command += ",";
                }
            }

            // final execute... 
            //if (count > 0)
            //    DB.Database.ExecuteSqlCommand(sql_command);
            //DB.SaveChanges();

            double totalSeconds = DateTime.Now.Subtract(start).TotalSeconds;
            return totalSeconds;
        }
    }


}
