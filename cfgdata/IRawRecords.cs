using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cfgdata
{
    public partial class RawHorizon : IRawRecords 
    {     
    }

    public interface IRawRecords
    {
        bool Deleted { get; set; }
        int Month { get; set; }
        int Year { get; set; }
    }
}