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
    public partial class Repos
    {
        town6668Entities DB = new town6668Entities();
        string UserName;

        public Repos(string userName) 
        {
            UserName = userName;
        }

        public void SaveChanges() 
        {
            DB.SaveChanges();
        }

    }
}
