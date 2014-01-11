using cfgdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cfglib
{
    public class HorizonFile {

        public HorizonFile(hzImport import) {
            Id = import.Id;
        }

        public int Id { get; private set; }
        
    }

    public class Repos
    {
        town6668Entities DB = new town6668Entities();

        public HorizonFile HorizonFile (int id) {

            hzImport h = (from import in DB.hzImports
                    where import.Id == id
                    select import).First();

            return new HorizonFile(h);
                       
        }
        
    }
}
