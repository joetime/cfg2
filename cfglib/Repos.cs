using cfgdata;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            Debug.WriteLine("[AUDIT]   " + message);
            DB.Audits.Add(audit);
        }
    }
}
