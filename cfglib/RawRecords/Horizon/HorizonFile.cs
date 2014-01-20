using cfgdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
using System.Data;
using joetime;

namespace cfglib
{
    /// <summary>
    /// The 'go-between' class, abstracted from the data layer.
    /// </summary>
    public class HorizonFile
    {
        public HorizonFile(hzImport import)
        {
            Id = import.Id;
            Filename = import.Filename;
            Month = import.Month;
            Year = import.Year;
            DateAdded = import.DateAdded;
            IsLocked = import.IsLocked;
            DateLocked = import.DateLocked;
            FromDatabase = true;
        }

        public HorizonFile(string path, int month, int year)
        {
            Filename = path;
            FromDatabase = false;
            Month = month;
            Year = year;
        }

        /// <summary>
        /// Opens the text file and loads all of the data into the object.
        /// </summary>
        /// <returns>The number of LineItems read and loaded.</returns>
        


        public ICollection<HorizonLineItem> LineItems { get; private set; }

        public int Id { get; private set; }
        public string Filename { get; private set; }
        public int Month { get; private set; }
        public int Year { get; private set; }
        public System.DateTime DateAdded { get; private set; }
        public bool IsLocked { get; private set; }
        public Nullable<System.DateTime> DateLocked { get; private set; }

        public bool FromDatabase { get; private set; }

        //public void Create()
        //{
        //    FileStream stream = File.Create("../../test2.txt");
        //    StreamWriter writer = new StreamWriter(stream);
        //    writer.Write("Hello World!");
        //    writer.Close();
        //    stream.Close();
        //}

    }

    /// <summary>
    /// Data access for the HorizonFile class.
    /// </summary>
    public partial class Repos
    {
        /// <summary>
        /// Get a file record by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HorizonFile HorizonFile(int id)
        {
            hzImport h = (from import in DB.hzImports
                          where import.Id == id
                          select import).First();

            return new HorizonFile(h);
        }

        /// <summary>
        /// Get the valid file record for the month/year 
        /// (always the last uploaded for that month/year combo, if there's more than one)
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public HorizonFile HorizonFile(int month, int year)
        {
            hzImport h = (from import in DB.hzImports
                          where import.Month == month && import.Year == year
                          orderby import.DateAdded descending
                          select import).First();

            return new HorizonFile(h);
        }

        public IEnumerable<HorizonFile> ActiveFiles(int num = 12, DateTime? start = null)
        {
            List<HorizonFile> list = new List<HorizonFile>();
            DateTime begin = start ?? DateTime.Now;

            for (int i = 0; i <= num; i++)
            {
                DateTime current = begin.AddMonths(-i);
                HorizonFile file = HorizonFile(current.Month, current.Year);
                list.Add(file);
            }

            return list;
        }
    }
}
