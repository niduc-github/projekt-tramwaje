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

        public Passenger(IReadOnlyCollection<TramStop> possibleStops) 
        {
            targetStop = possibleStops.ElementAt(random.Next(possibleStops.Count));
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
