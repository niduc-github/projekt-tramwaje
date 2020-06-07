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
    [Serializable]
    public class TramStop : TrackPoint
    {
        private TrackPoint connection1, connection2;
        private HashSet<TramStop> accessibleStops;
        private int oneWayTramLimit = 2;
        private Queue<Tram> slots12;
        private Queue<Tram> slots21;
        private String name;
        private float popularity;
        private static Random r = new Random();
        List<Passenger> passengers;
        private float timer = 0f;
        List<Tuple<Tram, float>> incomingTramsTimes;
        private static List<TramStop> allTramStops = new List<TramStop>();

        private static float maxGenerationSpeed = 1000f;
        private static float generationSlider = 0.5f;
        public static float GenerationSpeedSlider
        {
            get => generationSlider;
            set {
                generationSlider = Utility.Clamp01(value);
                foreach (TramStop stop in allTramStops)
                    stop.timer = 0f;
            }
        }

        public TrackPoint Connection1 => connection1;
        public TrackPoint Connection2 => connection2;

        private static float GenerationSpeed => maxGenerationSpeed * GenerationSpeedSlider;
        public IReadOnlyCollection<TramStop> AccessibleStops => accessibleStops;
        public IReadOnlyCollection<Tuple<Tram, float>> IncomingTramsTimes => incomingTramsTimes;

        public TramStop(String name, Vector2 position, float popularity)
        {
            this.name = name;
            this.position = position;
            incomingTramsTimes = new List<Tuple<Tram, float>>();
            passengers = new List<Passenger>();
            this.popularity = popularity;
            accessibleStops = new HashSet<TramStop>();
            connection1 = null;
            connection2 = null;
            slots12 = new Queue<Tram>();
            slots21 = new Queue<Tram>();
            allTramStops.Add(this);
        }

        ~TramStop() {
            allTramStops.Remove(this);
        }

        public TramStop(String name, Vector2 position) : this(name, position, (float)r.NextDouble()) { }

        public void UpdateArrivalTime(Tram tram) {
            for(int i = incomingTramsTimes.Count - 1; i >= 0; i--) {
                if(incomingTramsTimes[i].Item1 == tram) {
                    incomingTramsTimes.RemoveAt(i);
                    break;
                }
            }
            incomingTramsTimes.Add(Tuple.Create(tram, tram.GetTimeToStop(this)));
            incomingTramsTimes.Sort(Comparer<Tuple<Tram, float>>.Create((x, y) => x.Item2.CompareTo(y.Item2)));
        }

        public bool EnterIfHasSpace(TrackPoint from, Tram tram) {
            Queue<Tram> slots;
            if (from == connection1) {
                if (connection2 == null)
                    slots = slots21;
                else
                    slots = slots12;
            } else if (from == connection2) {
                if (connection1 == null)
                    slots = slots12;
                else
                    slots = slots21;
            } else
                throw new Exception("Nie ma takiego połączenia dla tego punktu!");

            if(slots.Count < oneWayTramLimit) {
                slots.Enqueue(tram);
                return true;
            }
            return false;
        }

        public bool LeaveIfFirst(TrackPoint to, Tram tram) {
            Queue<Tram> slots;
            if (connection1 == to)
                slots = slots21;
            else if (connection2 == to)
                slots = slots12;
            else
                throw new Exception("Nie ma takiego połączenia dla tego punktu!");

            if (slots.Count > 0 && slots.Peek() == tram) {
                slots.Dequeue();
                return true;
            }
            return false;
        }


        public void ExpandAccessibleStops(Track track) 
        {
            accessibleStops.UnionWith(track.Stops);
            accessibleStops.Remove(this);
        }

        public void AddPassengers(IReadOnlyCollection<TramStop> accessibleStops, int amount) {
            for(int i = 0; i < amount; i++)
                passengers.Add(new Passenger(accessibleStops));
        }

        public void GeneratePassengers(IReadOnlyCollection<TramStop> accessibleStops, float time)
        {
            if (accessibleStops == null || accessibleStops.Count == 0)
                return;
            timer += time;
            if (popularity <= 0.01f || GenerationSpeed <= 0.01f)
                return;
            float product = (float)(popularity * GenerationSpeed * PopularityMultiplier(SimulationControl.TotalTime));
            float timePerPassenger = (float)
                SimulationControl.HoursToSeconds(1f / product);
            while (timer >= timePerPassenger) {
                timer -= timePerPassenger;
                passengers.Add(new Passenger(accessibleStops));
            }
        }

        public double PopularityMultiplier(double time)
        {
            if (time >= 86399)
                time -= 86399;
            double currentPopularity = 0;
            if (time <= 14400 || time >= 86400)
                currentPopularity = 0;
            if (time > 14400 && time <= 23384)
                currentPopularity = 0.00001 * Math.Pow(time - 14400,2);
            if (time > 23384 && time <= 27210)
                currentPopularity = 4000000 * (1 / (1000 * Math.Sqrt(2 * Math.PI))) * Math.Pow(Math.E, -(Math.Pow(time - 25200, 2)) / 2000000) + 500;
            if (time > 27210 && time <= 45832)
                currentPopularity = 0.000002 * Math.Pow(time - 43200, 2) + 200;
            if (time > 45832 && time <= 50101)
                currentPopularity = 0.00002 * Math.Pow(time - 45000, 2) + 200;
            if (time > 50101 && time <= 53893)
                currentPopularity = 5000000 * (1 / (1000 * Math.Sqrt(2 * Math.PI))) * Math.Pow(Math.E, -(Math.Pow(time - 52200, 2)) / 2000000) + 500;
            if (time > 53893 && time <= 86400)
                currentPopularity = -0.03*(time - 86400);
            return currentPopularity / 2500d;
        }
        public float GetCurrentAmountOfPeople()
        {
            return passengers.Count;
        }
        public float GetPopularity()
        {
            return popularity;
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

        protected override bool HasFreeConnection() {
            return connection1 == null || connection2 == null;
        }

        protected override bool HasConnection(TrackPoint trackPoint) {
            return trackPoint == connection1 || trackPoint == connection2;
        }

        protected override void AddConnection(TrackPoint trackPoint) {
            if (connection1 == null)
                connection1 = trackPoint;
            else if (connection2 == null)
                connection2 = trackPoint;
            else
                throw new Exception("Element nie ma żadnych wolnych połączeń!");
        }
    }
}
