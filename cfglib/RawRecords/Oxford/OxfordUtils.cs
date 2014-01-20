using joetime;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cfglib
{
    public static class OxfordUtils
    {
        public static string CfgCode = "BC5672";

        public static List<OxfordLineItem> ReadFile(string path)
        {
            List<OxfordLineItem> records = new List<OxfordLineItem>();

            ExcelFile excel = new ExcelFile(path);
            DataTable dataTable = excel.GetFirstWorksheet();

            DateTime timeStamp = DateTime.Now;

            foreach (DataRow row in dataTable.Rows)
            {
                OxfordLineItem lineItem = ProcessRow(row);

                if (lineItem != null)
                {
                    lineItem.FileName = path;
                    lineItem.Created = timeStamp;

                    records.Add(lineItem);
                }
            }

            return records;
        }

        static OxfordLineItem ProcessRow(DataRow row)
        {
            /// only use rows with the code 
            /// (other rows are not data)
            if (row[0].ToString() != CfgCode) return null;

            OxfordLineItem lineItem = new OxfordLineItem();

            // columns in order from 6 - 15
            lineItem.GroupCode = row[6].ToString();
            lineItem.GroupName = row[7].ToString();
            lineItem.InvoicePeriod = row[8].ToString();
            lineItem.AmountBilled = row.DecimalAt(9);
            lineItem.PaymentReceived = row.DecimalOrNullAt(10);
            lineItem.PercentOfPremium = row.DoubleOrNullAt(11);
            lineItem.PEPM = row.IntAt(12);
            lineItem.SubCountPEPM = row.IntOrNullAt(13, "N/A");
            lineItem.CommissionAmount = row.DecimalOrNullAt(14);
            lineItem.AmountDue = row.DecimalAt(15);

            return lineItem;
        }
    }
}
