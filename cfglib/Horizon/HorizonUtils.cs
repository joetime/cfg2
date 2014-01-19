using joetime;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cfglib
{
    public static class HorizonUtils
    {
        static string CfgCode = "COASTAL BENEFITS";
        static string[] delimiters = new string[] { "\",\"" };
        static char quoteChar = '"';


        public static List<HorizonLineItem> ReadFile(string path)
        {
            List<HorizonLineItem> records = new List<HorizonLineItem>();

            if (path.ToLower().Contains(".xls"))
            {
                return LoadFromExcelFile(path);
            }
            else
            {
                return LoadFromTextFile(path);
            }
        }


        // **** All Private Members

        private static List<HorizonLineItem> LoadFromExcelFile(string path)
        {
            List<HorizonLineItem> records = new List<HorizonLineItem>();
            ExcelFile excel = new ExcelFile(path);
            DataTable dataTable = excel.GetFirstWorksheet();

            DateTime timeStamp = DateTime.Now;

            foreach (DataRow row in dataTable.Rows)
            {
                HorizonLineItem lineItem = ProcessRowExcel(row);

                if (lineItem != null)
                {
                    lineItem.Filename = path;
                    lineItem.Created = timeStamp;
                    records.Add(lineItem);
                }
            }

            return records;
        }

        private static List<HorizonLineItem> LoadFromTextFile(string path)
        {
            List<HorizonLineItem> items = new List<HorizonLineItem>();
            FileStream stream = OpenTextFile(path);
            StreamReader reader = new StreamReader(stream);

            DateTime timeStamp = DateTime.Now;

            while (!reader.EndOfStream)
            {
                HorizonLineItem item = ProcessRowText(reader.ReadLine());
                if (item != null)
                {
                    item.Created = timeStamp;
                    item.Filename = path;
                    items.Add(item);
                }
            }

            return items;
        }

        private static HorizonLineItem ProcessRowExcel(DataRow row)
        {
            /// only use rows with the code 
            /// (other rows are not data)
            if (row[0].ToString().Trim() != CfgCode) return null;

            HorizonLineItem item = new HorizonLineItem();

            // 0 - "COASTAL FINANCIAL GROUP"
            // 1 - "005766"
            item.GroupName = row.StringAt(2);
            item.GroupNumber = row.StringAt(3);
            item.Product = row.StringAt(4);
            item.EffectiveDate = row.StringAt(5);
            item.RenewalDate = row.StringAt(6);
            item.InsuredPeriod = row.StringAt(7);
            item.C_CodeOneHundredd = row.StringAt(8); // always "100"
            item.C_CoastalCode = row.StringAt(9);     // always "005766"
            item.CommissionSchedule = row.StringAt(10);
            item.C_TotalPremiumYTD = row.StringAt(11);
            item.PremiumReceived = row.DecimalAt(12);
            item.CommissionReceived = row.DecimalAt(13);

            return item;
        }

        // public so I can test it
        public static HorizonLineItem ProcessRowText(string row)
        {
            HorizonLineItem item = new HorizonLineItem();

            string[] parsed;

            parsed = row.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            if (parsed.Count() != 14 ||
                parsed[0].First() != quoteChar ||
                parsed[13].Last() != quoteChar)
            {
                throw new InvalidDataException("Error in file format.");
            }
            else
            {
                parsed[0] = parsed.First().TrimStart(quoteChar);
                parsed[13] = parsed.Last().TrimEnd(quoteChar);

                // 0 - "COASTAL FINANCIAL GROUP"
                // 1 - "005766"
                item.GroupName = parsed[2];
                item.GroupNumber = parsed[3];
                item.Product = parsed[4];
                item.EffectiveDate = parsed[5];
                item.RenewalDate = parsed[6];
                item.InsuredPeriod = parsed[7];
                item.C_CodeOneHundredd = parsed[8]; // always "100"
                item.C_CoastalCode = parsed[9];     // always "005766"
                item.CommissionSchedule = parsed[10];
                item.C_TotalPremiumYTD = parsed[11];
                item.PremiumReceived = Decimal.Parse(parsed[12]);
                item.CommissionReceived = Decimal.Parse(parsed[13]);
            }

            return item;
        }

        private static FileStream OpenTextFile(string path)
        {
            if (!File.Exists(path))
                throw new InvalidOperationException("File could not be found: " + path);

            try
            {
                return File.OpenRead(path);
            }
            catch
            {
                throw new InvalidOperationException("File could not be opened: " + path);
            }
        }
    }
}



/*
public int Load()
        {
            LineItems = new List<HorizonLineItem>();

            if (Filename.ToLower().Contains(".txt"))
            {
                LoadTextFile();
            }
            else if (Filename.ToLower().Contains(".xls"))
            {
                string path = "../../TestFiles/" + Filename;
                ExcelFile file = new ExcelFile(path);
                DataTable data = file.GetFirstWorksheet();

                foreach (DataRow row in data.Rows)
                {
                    var x = row.ItemArray[0];

                    int i = 5;
                }
            }

            return LineItems.Count();
        }

        private void LoadTextFile()
        {
            FileStream stream = OpenTextFile();
            StreamReader reader = new StreamReader(stream);

            // For text files:
            while (!reader.EndOfStream)
            {
                HorizonLineItem item = new HorizonLineItem(reader.ReadLine());
                if (item.status == HorizonLineItemStatus.Ok)
                    LineItems.Add(item);
            }
        }

        /// <summary>
        /// Open the text file
        /// </summary>
        /// <returns>A stream to read.</returns>
        private FileStream OpenTextFile()
        {
            string path = "../../TestFiles/" + Filename;

            if (!File.Exists(path))
                throw new InvalidOperationException("File could not be found: " + path);

            try
            {
                return File.OpenRead(path);
            }
            catch
            {
                throw new InvalidOperationException("File could not be opened: " + path);
            }
        }
*/