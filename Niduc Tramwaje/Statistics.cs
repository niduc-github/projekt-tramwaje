using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Niduc_Tramwaje
{
    [Serializable]
    static class Statistics
    {
        public static List<int> numberOfPeopleOnStop(String tramName, double timeInterval, double endOfStatisticPeriod)
        {
            TramStop currentStop = null;
            List<int> statisticOfPeopleOnStop = new List<int>();
            for(int i=0; i<= SimulationControl.getMap().TramStops.Count();i++)
            {
                if(SimulationControl.getMap().TramStops.ElementAt(i).getTramStopName() == tramName)
                {
                    currentStop = SimulationControl.getMap().TramStops.ElementAt(i);
                    break;
                }
            }
            double currentTime = timeInterval;
            while(currentTime <= endOfStatisticPeriod)
            {
                if(SimulationControl.getTime() == currentTime)
                {
                    statisticOfPeopleOnStop.Add(currentStop.getPassangerList().Count());
                }
                currentTime += timeInterval;
            }
            return statisticOfPeopleOnStop;
        }
    }
}
