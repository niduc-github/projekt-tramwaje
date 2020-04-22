using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Niduc_Tramwaje
{
    class Tram
    {
        private float speed;
        private Track track;
        private bool forward;
        private List<Passenger> passengers;
        private TramStop currentTramStop;
        private TramStop nextTramStop;
        private float progress;
        private bool loading;
        private bool unloading;
        private int stopIndex;

        private float loadSpeed = 10f;
        private float loadTimer = 0f;
        private float unloadTimer = 0f;

        private static Dictionary<KeyValuePair<TramStop,TramStop>,List<Tram>> traffic;

        public Tram(float speed, Track track, TramStop currentTramStop)
        {
            this.speed = speed;
            this.track = track;
            this.currentTramStop = currentTramStop;

            for (int i = 0; i < this.track.getTramStopList().Count; i++)
                if (this.track.getTramStopList()[i] == this.currentTramStop)
                    stopIndex = i;

            nextTramStop = GetNextTramStop();
            forward = true;
            loading = true;
            unloading = true;
            passengers = new List<Passenger>();
            progress = 0;
            traffic = new Dictionary<KeyValuePair<TramStop, TramStop>, List<Tram>>();

            
        }

        public void Load(float time)
        {     
            List<Passenger> stopPassengers = currentTramStop.getPassangerList();
            if(stopPassengers.Find(x => track.getTramStopList().Find(y => x.GetTargetStop() == y) != null) == null) {
                loadTimer = 0;
                loading = false;
                return;
            }

            loadTimer += time;
            for (int i = stopPassengers.Count - 1; i >= 0; i--) {
                if (track.getTramStopList().Contains(stopPassengers[i].GetTargetStop()) && loadTimer > 1/loadSpeed) {
                    loadTimer -= 1/loadSpeed;
                    passengers.Add(stopPassengers[i]);
                    stopPassengers.RemoveAt(i);
                } else
                    return;
            }
        }

        public void Unload(float time)
        {
            if (passengers.Find(x => x.GetTargetStop() == currentTramStop) == null) {
                unloadTimer = 0f;
                unloading = false;
                return;
            }

            unloadTimer += time;
            for(int i = passengers.Count - 1; i >= 0; i--) {
                if (unloadTimer >= 1 / loadSpeed) {
                    if (passengers[i].GetTargetStop() == currentTramStop) {
                        unloadTimer -= 1 / loadSpeed;
                        passengers.RemoveAt(i);
                    }
                } else
                    return;
            }
        }

        private TramStop GetNextTramStop() {
            List<TramStop> stops = track.getTramStopList();
            if (forward) {
                if (stopIndex + 1 == stops.Count) {
                    forward = false;
                    stopIndex--;
                } else
                    stopIndex++;
            } else {
                if (stopIndex == 0) {
                    forward = true;
                    stopIndex++;
                } else
                    stopIndex--;
            }
            return stops[stopIndex];
        }

        private void GoToNextStop()
        {
            currentTramStop = nextTramStop;
            nextTramStop = GetNextTramStop();
        }

        public void Update(float time) {
            if (loading || unloading) {
                Load(time);
                Unload(time);
                if (loading == false && unloading == false)
                    Form1.WriteToConsole("Aktualna liczba pasażerów: " + passengers.Count);
            } else {
                progress += speed * time / (nextTramStop.getPosition() - currentTramStop.getPosition()).Length();
                while (progress >= 1) {
                    progress -= 1f;
                    GoToNextStop();
                    loading = true;
                    unloading = true;
                }
            }
           
        }

        public Vector2 GetCurrentPos() {
            return Vector2.Lerp(currentTramStop.getPosition(), nextTramStop.getPosition(), progress);
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