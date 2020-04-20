using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public TramStop(String name, Vector2 position)
        {
            this.name = name;
            this.position = position;
        }

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
        public Vector2 getPosition()
        {
            return position;
        }
        public void setPosition(Vector2 position)
        {
            this.position = position;
        }
        public String getTramStopName()
        {
            return name;
        }
        public void setTramStopName(String name)
        {
            this.name = name;
        }
        public List<Passenger> getPassangerList()
        {
            return passengers;
        }
    }
}
