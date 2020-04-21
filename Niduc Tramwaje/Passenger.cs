using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niduc_Tramwaje
{
    class Passenger
    {
        private TramStop targetStop;
        
        public Passenger(TramStop targetStop)
        {
            this.targetStop = targetStop; //próba
        }
        public TramStop GetTargetStop()
        {
            return targetStop;
        }
    }
}
