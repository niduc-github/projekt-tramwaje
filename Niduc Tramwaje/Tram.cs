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
        private int passangerLimit = 100;
        private TrackPoint currentTrackPoint;
        private TrackPoint nextTrackPoint;
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
        private bool onTramStop = false;

        private TramStop CurrentTramStop => currentTrackPoint as TramStop;
        public Tram(Map map, float speed, Track track, TrackPoint currentTrackPoint, float spawnDelay = 0f)
        {
            this.map = map;
            this.speed = speed;
            this.track = track;
            this.currentTrackPoint = currentTrackPoint;
            this.spawnDelay = spawnDelay;

            for (int i = 0; i < this.track.Stops.Count; i++)
                if (this.track.Stops.ElementAt(i) == this.currentTrackPoint)
                    stopIndex = i;

            nextTrackPoint = GetNextTrackPoint();
            map.Traffic[Tuple.Create(currentTrackPoint, nextTrackPoint)].Enqueue(this);
            forward = true;
            loading = true;
            unloading = true;
            passengers = new List<Passenger>();
            progress = 0;       
        }

        public void ExchangePassangers(float time) {
            if (!(currentTrackPoint is TramStop))
                throw new Exception("Ten punkt nie jest przystankiem!");

            loadTimer += time;
            unloadTimer += time;

            TramStop currentTramStop = currentTrackPoint as TramStop;

            if (passengers.Count == 0)
                unloading = false;
            for (int i = passengers.Count - 1; i >= 0; i--) {
                if (unloadTimer > 1 / loadSpeed) {
                    if (currentTramStop == passengers[i].GetTargetStop()) {
                        passengers.RemoveAt(i);
                        unloadTimer -= 1 / loadSpeed;
                    }
                } else {
                    break;
                }
                if (i == 0)
                    unloading = false;
            }

            List<Passenger> passList = currentTramStop.getPassangerList();
            if (passList.Count == 0)
                loading = false;
            for (int i = passList.Count - 1; i >= 0; i--) {
                if (loadTimer > 1 / loadSpeed) {
                    if(passengers.Count >= passangerLimit) {
                        loading = false;
                        break;
                    }
                    if (track.Stops.Contains(passList[i].GetTargetStop())) {
                        passengers.Add(passList[i]);
                        passList.RemoveAt(i);
                        loadTimer -= 1 / loadSpeed;
                    }
                } else {
                    break;
                }
                if (i == 0)
                    loading = false;
            }
        }


        private TrackPoint GetNextTrackPoint() {
            IReadOnlyCollection<TrackPoint> trackPoints = track.TrackPoints;
            return trackPoints.ElementAt(Utility.PingPong(stopIndex + (forward ? 1 : -1), 0, trackPoints.Count - 1));
        }
        

        private void GoToNextStop()
        {
            map.Traffic[Tuple.Create(currentTrackPoint, nextTrackPoint)].Dequeue();
            currentTrackPoint = nextTrackPoint;
            if (forward) stopIndex++;
            else stopIndex--;

            if (currentTrackPoint == track.TrackPoints.Last() || currentTrackPoint == track.TrackPoints.First())
                forward = !forward;
            nextTrackPoint = GetNextTrackPoint();
            map.Traffic[Tuple.Create(currentTrackPoint, nextTrackPoint)].Enqueue(this);
        }

        public void Update(float time) {
            if(spawnDelay > 0) {
                spawnDelay -= time;
                return;
            }
            
            if (onTramStop) {
                if (loading || unloading) {
                    ExchangePassangers(time);
                } else if (CurrentTramStop.LeaveIfFirst(nextTrackPoint, this)) {
                    onTramStop = false;
                    loadTimer = 0f;
                    unloadTimer = 0f;
                }
                    
            } else {
                UpdateProgress(time);
                if (progress == 1f) {
                    if (nextTrackPoint is TramStop) {
                        if ((nextTrackPoint as TramStop).EnterIfHasSpace(CurrentTramStop, this)) {
                            GoToNextStop();
                            progress = 0f;
                            onTramStop = true;
                            loading = true;
                            unloading = true;
                        }
                    } else {
                        GoToNextStop();
                        progress = 0f;
                    }
                              
                }
            }  
        }


        private void UpdateProgress(float time) {
            Tram nextTram = FindNextTramOnSegment();

            float maxProgress = 1f;
            if (nextTram != null) {
                float maxDist = nextTram.progress * nextTram.CurrentSegmentDst() - minDstBetweenTrams;
                maxProgress = maxDist / CurrentSegmentDst();
            }
            progress += speed * time / CurrentSegmentDst();
            if (progress > maxProgress)
                progress = Math.Max(0f, maxProgress);
        }

        private Queue<Tram> NextSegmentTraffic() {
            return map.Traffic[Tuple.Create(nextTrackPoint, track.TrackPoints.ElementAt(Utility.PingPong(stopIndex + (forward ? 2 : -2), 0, track.Stops.Count - 1)))];
        }

        private float CurrentSegmentDst() {
            return (nextTrackPoint.getPosition() - currentTrackPoint.getPosition()).Length();
        }

        private Tram FindNextTramOnSegment() {
            var itr = map.Traffic[Tuple.Create(currentTrackPoint, nextTrackPoint)].GetEnumerator();
            Tram previous = null;
            while (itr.MoveNext()) {
                if (itr.Current == this)
                    return previous;
                previous = itr.Current;     
            }
            return null;
        }

        public Vector2 GetCurrentPos() {
            return Vector2.Lerp(currentTrackPoint.getPosition(), nextTrackPoint.getPosition(), progress);
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

        public TrackPoint getCurrentTrackPoint()
        {
            return currentTrackPoint;
        }
    }
}