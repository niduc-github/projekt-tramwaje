using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niduc_Tramwaje
{
    class Tram
    {
        private float speed;
        private Track track;
        private bool forward;
        private List<Passenger> passengers;
        private TramStop currentTramStop;

        public Tram(float speed, Track track, TramStop currentTramStop)
        {
            this.speed = speed;
            this.track = track;
            this.currentTramStop = currentTramStop;
        }

        public void Load()
        {

        }

        public void Unload()
        {

        }

        public void GoTo(TramStop tramStop)
        {
            //TODO
            if (speed == 0) speed = 27;
            else
            {
                speed = 0;
                TramStop next = getNextTramStop();
                if (next != null) currentTramStop = next;
            }
        }

        public TramStop getNextTramStop()
        {
            int i = 0;
            for (; i < track.getTramStopList().Count; i++)
            {
                if (currentTramStop.Equals(track.getTramStopList()[i])) break;
            }

            if (track.getTramStopList().Count > i + 1) return track.getTramStopList()[i + 1];
            else return null;
        }

        public float getSpeed()
        {
            return speed;
        }

        public Track getTrack()
        {
            return track;
        }

        public bool getForward()
        {
            return forward;
        }

        public int getPassengerCount()
        {
            return passengers.Count();
        }

        public TramStop getCurrentTramStop()
        {
            return currentTramStop;
        }

        public void setCurrentTramStop(TramStop ts)
        {
            this.currentTramStop = ts;
        }
    }
}