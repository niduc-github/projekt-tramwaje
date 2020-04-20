using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niduc_Tramwaje
{
    class Map
    {
        private List<TramStop> stops;
        private List<Track> tracks;
        public List<TramStop> getTramStopList()
        {
            return stops;
        }
        public List<Track> getTrackList()
        {
            return tracks;
        }
    }
}
