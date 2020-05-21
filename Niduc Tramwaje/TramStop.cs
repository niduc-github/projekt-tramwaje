﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Niduc_Tramwaje
{
    class TramStop : TrackPoint
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
        private static float maxGenerationSpeed = 2f;
        private static float generationSlider = 0.5f;
        public static float GenerationSpeedSlider 
        {
            get => generationSlider;
            set => generationSlider = Utility.Clamp01(value);
        }

        public TrackPoint Connection1 => connection1;
        public TrackPoint Connection2 => connection2;

        private static float GenerationSpeed => maxGenerationSpeed * GenerationSpeedSlider;
        public IReadOnlyCollection<TramStop> AccessibleStops => accessibleStops;

        public TramStop(String name, Vector2 position, float popularity)
        {
            this.name = name;
            this.position = position;
            passengers = new List<Passenger>();
            this.popularity = popularity;
            accessibleStops = new HashSet<TramStop>();
            connection1 = this;
            connection2 = this;
            slots12 = new Queue<Tram>();
            slots21 = new Queue<Tram>();
        }

        public TramStop(String name, Vector2 position) : this(name, position, (float)r.NextDouble()) { }

        public bool EnterIfHasSpace(TrackPoint from, Tram tram) {
            Queue<Tram> slots;
            if (from == connection1) {
                if (connection2 == this)
                    slots = slots21;
                else
                    slots = slots12;
            } else if (from == connection2) {
                if (connection1 == this)
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
        }

        public void Connect(TramStop tramStop)
        {
            if (connection2 != null && connection2 != this|| tramStop.connection1 != null && tramStop.connection1 != tramStop)
                throw new Exception("Conajmniej jedna ze stron jest już połączona!");
            this.connection2 = tramStop;
            tramStop.connection1 = this;         
        }

        public void GeneratePassengers(IReadOnlyCollection<TramStop> accessibleStops, float time)
        {
            if (accessibleStops == null || accessibleStops.Count == 0)
                return;
            timer += time;
            if (popularity <= 0.01f || GenerationSpeed <= 0.01f)
                return;
            while(timer >= 1 / (popularity * GenerationSpeed)) 
            {
                timer -= 1 / (popularity * GenerationSpeed);
                passengers.Add(new Passenger(accessibleStops));
            }
        }

        public double PopularityMultiplier(double time)
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
