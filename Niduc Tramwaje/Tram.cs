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

        Tram(float speed, Track track, TramStop currentTramStop)
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

        void GoTo(TramStop tramStop)
        {
            //TODO
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
    }
}