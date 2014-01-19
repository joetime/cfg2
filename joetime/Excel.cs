using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace joetime
{
    public class ExcelFile
    {
        public string FileName { get; private set; }

        string connectionString;
        OleDbConnection connection;

        public ExcelFile(string filename) 
        {
            FileName = filename;
            connectionString = ConnectionString(filename);
        }

        public DataTable GetFirstWorksheet()
        {
            string worksheetname = GetWorksheetNames(FileName).First();

            OleDbConnection conn = null;
            
            //open a new connection to the specified excel file
            conn = new OleDbConnection(connectionString);
            conn.Open();

            OleDbCommand select = new OleDbCommand("SELECT * FROM [" + worksheetname + "]", conn);
            
            // Create new OleDbDataAdapter that is used to build a DataSet
            // based on the preceding SQL SELECT statement.
            OleDbDataAdapter oledbAdapter = new OleDbDataAdapter();

            // Pass the Select command to the adapter.
            oledbAdapter.SelectCommand = select;

            // Create new DataSet to hold information from the worksheet.
            DataSet oledbDataset = new DataSet();

            // Fill the DataSet with the information from the worksheet.
            oledbAdapter.Fill(oledbDataset, "XLData");

            return oledbDataset.Tables[0];

        }


        public string[] GetWorksheetNames(string fileName)
        {
            OleDbConnection conn = null;
            DataTable dt = null;

            //open a new connection to the specified excel file
            conn = new OleDbConnection(connectionString);
            conn.Open();
            //create a new DataTable and populate it with the worksheet names within the excel file
            dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            //return a null valued array if the DataTable contains no value
            if (dt == null)
                return null;

            //create a new string array to hold all of the gathered worksheet names
            String[] Worksheets = new String[dt.Rows.Count];
            int i = 0;  //<<<Counter for the foreach loop for the string array index

            //loop through every row in the DataTable and add the worksheet name to the string array
            foreach (DataRow row in dt.Rows)
            {
                Worksheets[i] = row["TABLE_NAME"].ToString();
                i++;    //<<<Increment array index counter
            }
            //return the string array for use in the calling method
            return Worksheets;

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
                if (dt != null)
                    dt.Dispose();
            }

        }


        public static string ConnectionString(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);

            if (fileExtension == ".xls")
                return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath +
                    ";Extended Properties=\"Excel 8.0;IMEX=1;HDR=NO\"";
            else if (fileExtension == ".xlsx")
                return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath +
                    ";Extended Properties=\"Excel 12.0 Xml;HDR=No;IMEX=1\";";
            //else if (fileExtension == ".csv")
            //{
            //    string sourceDirectory = Path.GetDirectoryName(filePath);
            //    return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sourceDirectory +
            //        ";Extended Properties=\"text;HDR=Yes;FMT=Delimited(,)\";";
            //}
            else
                return "";
        }
    }
}
