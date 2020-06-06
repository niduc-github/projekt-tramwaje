using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niduc_Tramwaje
{
    [Serializable]
    public class Map
    {
        private List<TrackPoint> trackPoints = new List<TrackPoint>();
        private List<Track> tracks = new List<Track>();
        private static Dictionary<Tuple<TrackPoint, TrackPoint>, Queue<Tram>> traffic = new Dictionary<Tuple<TrackPoint, TrackPoint>, Queue<Tram>>();
        public List<Tram> Trams { get; } = new List<Tram>();

        public IReadOnlyCollection<TrackPoint> TrackPoints => trackPoints;
        public IReadOnlyCollection<TramStop> TramStops => trackPoints.OfType<TramStop>().ToList();
        public IReadOnlyCollection<Track> Tracks => tracks;
        public IReadOnlyDictionary<Tuple<TrackPoint, TrackPoint>, Queue<Tram>> Traffic => traffic;

        public void AddTram(Tram newTram)
        {
            Trams.Add(newTram);
        }

        public void AddTrackPoint(TrackPoint trackPoint)
        {
            trackPoints.Add(trackPoint);
        }

        public void AddTrack(Track newTrack)
        {
            tracks.Add(newTrack);
            for (int i = 0; i < newTrack.TrackPoints.Count - 1; i++)
            {
                if (!traffic.ContainsKey(Tuple.Create(newTrack.TrackPoints.ElementAt(i), newTrack.TrackPoints.ElementAt(i + 1))))
                    traffic.Add(Tuple.Create(newTrack.TrackPoints.ElementAt(i), newTrack.TrackPoints.ElementAt(i + 1)), new Queue<Tram>());
                if (!traffic.ContainsKey(Tuple.Create(newTrack.TrackPoints.ElementAt(i + 1), newTrack.TrackPoints.ElementAt(i))))
                    traffic.Add(Tuple.Create(newTrack.TrackPoints.ElementAt(i + 1), newTrack.TrackPoints.ElementAt(i)), new Queue<Tram>());
            }
        }

    }
}
