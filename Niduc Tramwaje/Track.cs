using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niduc_Tramwaje
{
    struct Track
    {
        private int number;
        private List<TramStop> stops;
        public int getTrackNumber()
        {
            return number;
        }
        public List<TramStop> getTramStopList()
        {
            return stops;
        }
    }
}
