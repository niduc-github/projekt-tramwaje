using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niduc_Tramwaje
{
    class Passenger
    {
        private static Random random = new Random();

        private TramStop targetStop;

        public Passenger(List<TramStop> tramStops) {
            targetStop = tramStops[random.Next(tramStops.Count)];
        }

        public Passenger(TramStop targetStop)
        {
            this.targetStop = targetStop;
        }

        public TramStop GetTargetStop()
        {
            return targetStop;
        }
    }
}
