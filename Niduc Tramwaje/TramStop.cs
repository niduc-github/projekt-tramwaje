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
        private float popularity;
        private static Random r = new Random();
        List<Passenger> passengers;

        public TramStop(String name, Vector2 position, float popularity)
        {
            this.name = name;
            this.position = position;
            passengers = new List<Passenger>();
            this.popularity = popularity;
        }

        public TramStop(String name, Vector2 position) : this(name, position, (float)r.NextDouble()) { }

        public void PassangerGeneration(List<TramStop> tramStops)
        {
            int amountOfPeople = 0;
            if (popularity < 0.3) {
                amountOfPeople = r.Next(0, 4);
            }else if (popularity >= 0.3 && popularity < 0.7) {
                amountOfPeople = r.Next(5, 9);
            }else if (popularity >= 0.7) {
                amountOfPeople = r.Next(10, 14);
            }

            for (int i = 0; i < amountOfPeople; i++)
                passengers.Add(new Passenger(tramStops));
        }

        public float GetCurrentAmountOfPeople()
        {
            return passengers.Count;
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
