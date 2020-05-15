using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Niduc_Tramwaje
{
    class TramStop
    {
        private HashSet<TramStop> accessibleStops;
        private String name;
        private Vector2 position;
        private float popularity;
        private static Random r = new Random();
        List<Passenger> passengers;
        private float timer = 0f;
        private static float maxGenerationSpeed = 2f;
        private static float generationSlider = 0.5f;
        public static float GenerationSpeedSlider 
        {
            get => generationSlider;
            set => generationSlider = Utility.Clamp01(value);
        }

        private static float GenerationSpeed => maxGenerationSpeed * GenerationSpeedSlider;
        public IReadOnlyCollection<TramStop> AccessibleStops => accessibleStops;

        public TramStop(String name, Vector2 position, float popularity)
        {
            this.name = name;
            this.position = position;
            passengers = new List<Passenger>();
            this.popularity = popularity;
            accessibleStops = new HashSet<TramStop>();
        }

        public TramStop(String name, Vector2 position) : this(name, position, (float)r.NextDouble()) { }


        public void ExpandAccessibleStops(Track track) 
        {
            accessibleStops.UnionWith(track.Stops);
        }

        public void GeneratePassengers(IReadOnlyCollection<TramStop> accessibleStops, float time)
        {
            timer += time;
            if (popularity <= 0.01f || GenerationSpeed <= 0.01f)
                return;
            while(timer >= 1 / (popularity * GenerationSpeed)) 
            {
                timer -= 1 / (popularity * GenerationSpeed);
                passengers.Add(new Passenger(accessibleStops));
            }
        }

        public double PopularityMultiplier(int time)
        {
            double currentPopularity = 0;
            if (time <= 14400 || time >= 86400)
                return currentPopularity = 0;
            if (time > 14400 && time <= 23384)
                return currentPopularity = 0.00001 * Math.Pow(time - 14400,2);
            if (time > 23384 && time <= 27210)
                return currentPopularity = 4000000 * (1 / (1000 * Math.Sqrt(2 * Math.PI))) * Math.Pow(Math.E, -(Math.Pow(time - 25200, 2)) / 2000000) + 500;
            if (time > 27210 && time <= 45832)
                return currentPopularity = 0.000002 * Math.Pow(time - 43200, 2) + 200;
            if (time > 45832 && time <= 50101)
                return currentPopularity = 0.00002 * Math.Pow(time - 45000, 2) + 200;
            if (time > 50101 && time <= 53893)
                return currentPopularity = 5000000 * (1 / (1000 * Math.Sqrt(2 * Math.PI))) * Math.Pow(Math.E, -(Math.Pow(time - 52200, 2)) / 2000000) + 500;
            if (time > 53893 && time <= 86400)
                return currentPopularity = -0.03*(time - 86400);
            return currentPopularity;
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
