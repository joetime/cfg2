using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cfglib
{
    public class Lookup
    {
        public string Name { get; set; }
        public int Key { get; set; }
        public object Alt { get; set; }
    }

    public partial class Repos
    {
        public IQueryable<Lookup> Brokers()
        {
            return from cfgdata.hzBroker b in DB.hzBrokers
                    where !b.Deleted
                    orderby b.Name
                    select new Lookup()
                    {
                        Name = b.Name,
                        Key = b.Id,
                        Alt = b.Number
                    };
        }

        public IEnumerable<object> Files()
        {
            return from HorizonFile file in this.ActiveFiles()
                   select file; 
        }
    }
}
