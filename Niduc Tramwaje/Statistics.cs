using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Niduc_Tramwaje
{
    static class Statistics
    {
        public static List<int> numberOfPeopleOnStop(String tramName, double timeInterval, double endOfStatisticPeriod)
        {
            TramStop currentStop = null;
            List<int> statisticOfPeopleOnStop = new List<int>();
            for(int i=0; i<= SimulationControl.getMap().getStops().Count();i++)
            {
                if(SimulationControl.getMap().getStops()[i].getTramStopName() == tramName)
                {
                    currentStop = SimulationControl.getMap().getStops()[i];
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
