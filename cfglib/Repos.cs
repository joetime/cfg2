using cfgdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace cfglib
{
    public enum DbActionType
    {
        NullAction,
        Insert,
        Delete,
        Modify,
        DeleteForever
    }

    public partial class Repos
    {
        town6668Entities DB = new town6668Entities();
        string UserName;

        public Repos(string userName = "") 
        {
            UserName = userName;
        }

        public void SaveChanges() 
        {
            DB.SaveChanges();
        }

        private void AddAudit(string message, string objectType = "", int? objectKey = null, int recordCount = 0, DbActionType action = DbActionType.NullAction)
        {
            Audit audit = new Audit();
            audit.DateStamp = DateTime.Now;
            audit.Message = message;

            audit.ObjectKey = objectKey;
            audit.ObjectType = objectType;
            audit.UserName = UserName;

            audit.RecordCount = recordCount;
            audit.Action = action.ToString();


            DB.Audits.Add(audit);
        }

        private static void YearMonthCheck(int year, int month)
        {
            if (year < 1990 || year > 2025 || month < 1 || month > 12)
                throw new InvalidOperationException(String.Format("Date seems invalid (M/Y): {0}/{1}", month, year));
        }
    }
}
