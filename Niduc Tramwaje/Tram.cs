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
        private Map map;
        private static float minDstBetweenTrams = 10f;
        private float loadSpeed = 10f;
        private float loadTimer = 0f;
        private float unloadTimer = 0f;
        private float spawnDelay = 0f;

        public bool OnTramStop => loading || unloading;
        public Tram(Map map, float speed, Track track, TramStop currentTramStop, float spawnDelay = 0f)
        {
            this.map = map;
            this.speed = speed;
            this.track = track;
            this.currentTramStop = currentTramStop;
            this.spawnDelay = spawnDelay;

            for (int i = 0; i < this.track.Stops.Count; i++)
                if (this.track.Stops[i] == this.currentTramStop)
                    stopIndex = i;

            nextTramStop = GetNextTramStop();
            map.Traffic[Tuple.Create(currentTramStop, nextTramStop)].Enqueue(this);
            forward = true;
            loading = true;
            unloading = true;
            passengers = new List<Passenger>();
            progress = 0;       
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
            return stops[Utility.PingPong(stopIndex + (forward ? 1 : -1), 0, stops.Count - 1)];
        }
        

        private void GoToNextStop()
        {
            map.Traffic[Tuple.Create(currentTramStop, nextTramStop)].Dequeue();
            currentTramStop = nextTramStop;
            if (forward) stopIndex++;
            else stopIndex--;

            if (currentTramStop == track.Stops.Last() || currentTramStop == track.Stops.First())
                forward = !forward;
            nextTramStop = GetNextTramStop();
            map.Traffic[Tuple.Create(currentTramStop, nextTramStop)].Enqueue(this);
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
                UpdateProgress(time);
                if (progress >= 1) {
                    progress -= 1f;
                    GoToNextStop();
                    loading = true;
                    unloading = true;
                }
            }  
        }

        //Poprawie te funckje, bo troche balagan jest xd. I beznadziejny algorytm xd.
        private void UpdateProgress(float time) {
            Tram nextTram = FindNextTramOnSegment();

            float firstSegMaxProgress = 1f;
            float secondSegMaxProgress = 0f;
            if(nextTram != null) {
                float maxDist = nextTram.progress * nextTram.CurrentSegmentDst() - minDstBetweenTrams;
                firstSegMaxProgress = maxDist / CurrentSegmentDst();
            } else if (NextSegmentTraffic().Count > 0) {
                nextTram = NextSegmentTraffic().Last();
                float maxDist = CurrentSegmentDst() + nextTram.progress * nextTram.CurrentSegmentDst() - minDstBetweenTrams;
                float currDist = progress * CurrentSegmentDst();
                float firstStep = Math.Min(CurrentSegmentDst() - currDist, maxDist - currDist);
                firstSegMaxProgress = (currDist + firstStep) / CurrentSegmentDst();
                float secondStep = Math.Max(0, maxDist - CurrentSegmentDst());
                secondSegMaxProgress = secondStep / nextTram.CurrentSegmentDst();
            }

            progress += speed * time / CurrentSegmentDst();
            if(progress > 1f) {
                TramStop nextNextStop = track.Stops[Utility.PingPong(stopIndex + 2, 0, track.Stops.Count - 1)];
                progress = 1f + ((progress - 1f) * CurrentSegmentDst()) / ((nextTramStop.getPosition() - nextNextStop.getPosition()).Length());
            }

            progress = Math.Min(firstSegMaxProgress + secondSegMaxProgress, progress);
        }

        private Queue<Tram> NextSegmentTraffic() {
            return map.Traffic[Tuple.Create(nextTramStop, track.Stops[Utility.PingPong(stopIndex + (forward ? 2 : -2), 0, track.Stops.Count -1)])];
        }

        private float CurrentSegmentDst() {
            return (nextTramStop.getPosition() - currentTramStop.getPosition()).Length();
        }

        private Tram FindNextTramOnSegment() {
            var itr = map.Traffic[Tuple.Create(currentTramStop, nextTramStop)].GetEnumerator();
            Tram previous = null;
            while (itr.MoveNext()) {
                if (itr.Current == this)
                    return previous;
                previous = itr.Current;     
            }
            return null;
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