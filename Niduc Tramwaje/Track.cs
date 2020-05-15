using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niduc_Tramwaje
{
    class Track
    {
        private int number;
        private List<TramStop> stops;
        public Track(int number)
        {
            stops = new List<TramStop>();
            this.number = number;
        }

        public Track(int number, List<TramStop> stops) 
        {
            this.number = number;
            this.stops = stops;
            foreach (TramStop stop in stops)
                stop.ExpandAccessibleStops(this);
        }

        public ReadOnlyCollection<TramStop> Stops => stops.AsReadOnly();

        public int Number => number;

        public TramStop this[int index] => stops[index];

        public void AddTramStop(TramStop tramStop) {
            stops.Add(tramStop);
            tramStop.ExpandAccessibleStops(this);
        }
    }
}
