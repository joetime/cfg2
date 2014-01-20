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
        protected override IQueryable<IRawRecords> records
        {
            get
            {
                return from rec in DB.RawHorizons select (IRawRecords)rec;
            }
        }

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

        public override string FormatInsertValuesString(object lineitem, int year, int month)
        {
            HorizonLineItem item = (HorizonLineItem)lineitem;

            return String.Format(
                sql_InsertValues,
                month, year, item.Filename, item.GroupNumber, 
                // check for '
                item.GroupName.ToStringSql(), 
                item.Product,
                item.InsuredPeriod, item.RenewalDate, item.EffectiveDate,
                item.CommissionSchedule, item.CommissionReceived, item.PremiumReceived,
                item.C_CoastalCode, item.C_CodeOneHundredd, item.C_TotalPremiumYTD);
        }
    }

    public class RawOxfordBatchOps : RawRecordsBatchOps<RawOxford>
    {
        protected override IQueryable<IRawRecords> records
        {
            get
            {
                return from rec in DB.RawOxfords select (IRawRecords)rec;
            }
        }

        protected override string TableName { get { return "RawOxford"; } }

        protected override string sql_InsertHeader
        {
            get
            {
                return
                    "INSERT into cfg.{0} ( " +
                    "Month," + 
                    "Year," + 
                    "GroupCode," + 
                    "GroupName," +
                    "InvoicePeriod," +
                    "AmountBilled," +
                    "PaymentReceived," +
                    "PercentOfPremium," +
                    "PEPM," +
                    "SubCountPEPM," +
                    "CommissionAmount," +
                    "AmountDue," +
                    "FileName," +
                    "Created) VALUES ";
            }
        }

        protected string sql_InsertValues
        {
            get
            {
                return
                    " (" +
                    // month year
                    "{0},{1}," +            
                    // GroupCode, GroupName, invoicePeriod
                    "'{2}','{3}','{4}'," +
                    // AmountBilled, PaymentReceived, PercentOfPremium
                    "{5},{6},{7}," +
                    // PEPM, SubcountPEPM
                    "{8},{9}," +
                    // CommissionAmount, AmountDue
                    "{10},{11}," +
                    // Filename, Created
                    "'{12}',getDate())";
            }
        }

        public override string FormatInsertValuesString(object lineitem, int year, int month)
        {
            OxfordLineItem item = (OxfordLineItem)lineitem;

            return String.Format(
                sql_InsertValues,
                month, year,
                item.GroupCode, 
                // check for '
                item.GroupName.ToStringSql(), 
                item.InvoicePeriod,
                item.AmountBilled, 
                // check for NULL
                item.PaymentReceived.ToStringSqlNullable(),
                // check for NULL
                item.PercentOfPremium.ToStringSqlNullable(),
                item.PEPM,
                // check for NULL
                item.SubCountPEPM.ToStringSqlNullable(),
                // check for NULL
                item.CommissionAmount.ToStringSqlNullable(), 
                item.AmountDue,
                item.FileName);
        }
    }

    public abstract class RawRecordsBatchOps<T> where T : IRawRecords
    {
        protected town6668Entities DB;
        
        protected abstract IQueryable<IRawRecords> records { get; }
        protected abstract string TableName { get; }
        protected abstract string sql_InsertHeader { get; }

        // the delete command is the same for both
        protected string sql_UpdateCommand =
            "UPDATE cfg.{0} " +
            "SET Deleted = 1 " +
            "WHERE Month = {1} AND Year = {2} AND Deleted = 0";

        public abstract string FormatInsertValuesString(object lineitem, int year, int month);


        public RawRecordsBatchOps()
        {
            DB = new town6668Entities();
        }

        public void DeleteRecords(int year, int month)
        {
            JoeUtils.YearMonthCheck(year, month);

            string sql_command = String.Format(
                sql_UpdateCommand, TableName, month, year);

            DB.Database.ExecuteSqlCommand(sql_command);
        }
        
        public double AddRecords<T2>(
            List<T2> lineitems, 
            int year, int month,
            int batchSize = 250, 
            bool resetContext = false)
        {
            JoeUtils.YearMonthCheck(year, month);
            
            // supposed to speed up execution
            DB.Configuration.AutoDetectChangesEnabled = false;
            DB.Configuration.ValidateOnSaveEnabled = false;

            int count = 0;  // count number in current batch
            int total = 0;  // count total number of entries added so far

            // the "INSERT..." part of the command
            string sql_command = String.Format(sql_InsertHeader, TableName); 
            
            DateTime start = DateTime.Now;  // will record total time
            DateTime loop = start;   // record time for each execution

            foreach (object item in lineitems)
            {
                count++;
                total++;

                // add current set of values to command string (val1, val2,...)
                sql_command += FormatInsertValuesString(item, year, month);

                
                if (count % batchSize == 0 || total == lineitems.Count)
                    
                    // execute on batch size or end of array
                    ExecuteInsert(batchSize, resetContext, ref count, total, ref sql_command, ref loop);
                
                else
                
                    // add a comma between VALUES sets (...) , (...)
                    sql_command += ",";
                
            }

            double totalSeconds = DateTime.Now.Subtract(start).TotalSeconds;
            return totalSeconds;
        }

        private void ExecuteInsert(int batchSize, bool resetContext, ref int count, int total, ref string sql_command, ref DateTime loop)
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

            if (resetContext)
            {
                DB.Dispose();
                DB = new town6668Entities();
                DB.Configuration.AutoDetectChangesEnabled = false;
                DB.Configuration.ValidateOnSaveEnabled = false;
            }

            loop = DateTime.Now;
        }
    }


}
