using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cfgdata;
using System.Diagnostics;

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
