using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private float spawnDelay = 0f;

        private static Dictionary<KeyValuePair<TramStop,TramStop>,List<Tram>> traffic;

        public bool OnTramStop => loading || unloading;
        public Tram(float speed, Track track, TramStop currentTramStop, float spawnDelay = 0f)
        {
            this.speed = speed;
            this.track = track;
            this.currentTramStop = currentTramStop;
            this.spawnDelay = spawnDelay;

            for (int i = 0; i < this.track.Stops.Count; i++)
                if (this.track.Stops[i] == this.currentTramStop)
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
            if(stopPassengers.Find(x => track.Stops.Contains(x.GetTargetStop())) == null) {
                loadTimer = 0;
                loading = false;
                return;
            }

            loadTimer += time;
            for (int i = stopPassengers.Count - 1; i >= 0; i--) {
                if (track.Stops.Contains(stopPassengers[i].GetTargetStop())) {
                    if (loadTimer > 1 / loadSpeed) {
                        loadTimer -= 1 / loadSpeed;
                        passengers.Add(stopPassengers[i]);
                        stopPassengers.RemoveAt(i);
                    } else
                        return; 
                }
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
            ReadOnlyCollection<TramStop> stops = track.Stops;
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
            if(spawnDelay > 0) {
                spawnDelay -= time;
                return;
            }

            if (OnTramStop) {
                Load(time);
                Unload(time);
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