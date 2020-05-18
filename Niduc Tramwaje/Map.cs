using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niduc_Tramwaje
{
    class Map
    {
        private List<TramStop> stops = new List<TramStop>();
        private List<Track> tracks = new List<Track>();
        private static Dictionary<Tuple<TramStop, TramStop>, Queue<Tram>> traffic = new Dictionary<Tuple<TramStop, TramStop>, Queue<Tram>>();
        public List<Tram> Trams { get; } = new List<Tram>();

        public IReadOnlyCollection<TramStop> Stops => stops;
        public IReadOnlyCollection<Track> Tracks => tracks;
        public IReadOnlyDictionary<Tuple<TramStop, TramStop>, Queue<Tram>> Traffic => traffic;
        
        public List<TramStop> getStops()
        {
            return stops;
        }
        public void AddTram(Tram newTram) {
            Trams.Add(newTram);
        }

        public void AddStop(TramStop newStop) {               
            stops.Add(newStop);
        }

        public void AddTrack(Track newTrack) {
            tracks.Add(newTrack);
            for(int i = 0; i < newTrack.Stops.Count - 1; i++) {
                if (!traffic.ContainsKey(Tuple.Create(newTrack.Stops[i], newTrack.Stops[i + 1])))
                    traffic.Add(Tuple.Create(newTrack.Stops[i], newTrack.Stops[i + 1]), new Queue<Tram>());
                if (!traffic.ContainsKey(Tuple.Create(newTrack.Stops[i + 1], newTrack.Stops[i])))
                    traffic.Add(Tuple.Create(newTrack.Stops[i + 1], newTrack.Stops[i]), new Queue<Tram>());
            }
        }

    }
}
