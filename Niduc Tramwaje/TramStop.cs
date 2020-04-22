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
        private List<Track> tracks;
        private String name;
        private Vector2 position;
        private float popularity;
        private static Random r = new Random();
        List<Passenger> passengers;
        private float timer = 0f;
        private static float maxGenerationSpeed = 2f;
        private static float generationSlider = 0.5f;
        public static float GenerationSpeedSlider {
            get => generationSlider;
            set => generationSlider = Utility.Clamp01(value);
        }

        private static float GenerationSpeed => maxGenerationSpeed * GenerationSpeedSlider;
        public ReadOnlyCollection<Track> Tracks => tracks.AsReadOnly();

        public TramStop(String name, Vector2 position, float popularity)
        {
            this.name = name;
            this.position = position;
            passengers = new List<Passenger>();
            this.popularity = popularity;
            tracks = new List<Track>();
        }

        public TramStop(String name, Vector2 position) : this(name, position, (float)r.NextDouble()) { }


        public void AssignTrack(Track track) {
            tracks.Add(track);
        }

        public void GeneratePassengers(List<TramStop> tramStops, float time)
        {
            timer += time;
            if (popularity <= 0.01f || GenerationSpeed <= 0.01f)
                return;
            while(timer >= 1 / (popularity * GenerationSpeed)) {
                timer -= 1 / (popularity * GenerationSpeed);
                passengers.Add(new Passenger(tramStops));
            }
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
