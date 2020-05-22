using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niduc_Tramwaje
{
    class Track
    {
        private int number;
        private List<TrackPoint> trackPoints;
        public Track(int number)
        {
            trackPoints = new List<TrackPoint>();
            this.number = number;
        }

        public Track(int number, List<TrackPoint> trackPoints) : this(number)
        {
            foreach (TrackPoint trackPoint in trackPoints)
                AddTrackPoint(trackPoint);
        }

        public IReadOnlyCollection<TramStop> Stops => trackPoints.OfType<TramStop>().ToList();
        public IReadOnlyCollection<TrackPoint> TrackPoints => trackPoints;

        public int Number => number;

        public TramStop this[int index] => Stops.ElementAt(index);

        public void AddTrackPoint(TrackPoint trackPoint) {
            if(trackPoints.Count > 0 && !trackPoints.Last().IsConnectedWith(trackPoint))
                trackPoints.Last().Connect(trackPoint);
            trackPoints.Add(trackPoint);
            
            if(trackPoint is TramStop)
                foreach(TramStop ts in trackPoints.OfType<TramStop>())
                    ts.ExpandAccessibleStops(this);
        }
    }
}
