using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Niduc_Tramwaje
{
    class TramStop
    {
        private String name;
        private Vector2 position;
        private float amountOfPeople;
        private float popularity;
        private Random r = new Random();
        List<Passenger> passengers;

        public void PassangerGeneration()
        {
            if (popularity < 0.3) 
            {
                amountOfPeople = r.Next(0, 10);
            }
            if (popularity >= 0.3 && popularity < 0.7) 
            {
                amountOfPeople = r.Next(11, 30);
            }
            if (popularity >= 0.7) 
            {
                amountOfPeople = r.Next(31, 60);
            }
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
