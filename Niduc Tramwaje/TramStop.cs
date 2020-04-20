using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niduc_Tramwaje
{
    class TramStop
    {
        private String name;
        private Vector2 positiion;
        private float amountOfPeople;
        private float popularity;
        List<Passenger> passengers;

        public void PassangerGeneration()
        {
            if (popularity < 0.3) { }
            if (popularity >= 0.3 && popularity < 0.7) { }
            if (popularity >= 0.7) { }
        }

        public float GetCurrentAmountOfPeople()
        {
            return amountOfPeople;
        }
        public float GetPopularity()
        {
            return popularity;
        }
    }
}
