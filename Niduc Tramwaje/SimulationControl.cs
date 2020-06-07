using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Niduc_Tramwaje.Properties;

namespace Niduc_Tramwaje
{
    [Serializable]
    static class SimulationControl
    {
        static Map map = new Map();
        static float timeStep = 1f;
        static float minTimeScale = 0f;
        static float maxTimeScale = 10000f;
        static float timeScaleSlider = 1.01f/maxTimeScale;

        static float TimeScale => minTimeScale + (maxTimeScale - minTimeScale) * timeScaleSlider;
        public static float TimeScaleSlider {
            set => timeScaleSlider = Utility.Clamp01(value);
            get => timeScaleSlider;
        }

        private static Bitmap b = new Bitmap(640, 480);
        private static Graphics g = Graphics.FromImage(b);
        private static Pen pen_black = new Pen(Color.Black);
        private static Pen pen_red = new Pen(Color.Red);
        private static Brush brush_white = new SolidBrush(Color.White);
        private static Brush brush_black = new SolidBrush(Color.Black);
        private static Brush brush_green = new SolidBrush(Color.Green);
        private static Brush brush_blue = new SolidBrush(Color.Blue);

        private static Stopwatch stopwatch = new Stopwatch();
        private static double time = 0d;
        private static double totalTime = 0d;

        public static double TotalTime => totalTime;

        static Statistics stats;

        static SimulationControl() 
        {
            stats = new Statistics();
            stopwatch.Start();
            totalTime = 14000;
        }

        public static double HoursToSeconds(double hours) {
            return hours * 60d * 60d;
        }

        public static double SecondsToHours(double seconds) {
            return seconds / 60d / 60d;
        }

        public static float BitmapUnitsToKm(float value) {
            return value / 40f;
        }

        public static void Simulation()
        {
            stopwatch.Stop();
            time += stopwatch.Elapsed.TotalSeconds * TimeScale;
            totalTime += stopwatch.Elapsed.TotalSeconds * TimeScale;
            stopwatch.Restart();

            while(time >= timeStep) 
            {
                time -= timeStep;
                UpdateTrams();
                UpdateTramStops();
                stats.Update(timeStep, map);
            }
        }


        private static void UpdateTrams() 
        {
            foreach (Tram t in map.Trams) 
            {
                t.Update(timeStep);
                t.UpdateArrivalTimes();
            }
        }

        private static void UpdateTramStops() 
        {
            foreach (TramStop stop in map.TramStops)
                stop.GeneratePassengers(stop.AccessibleStops, timeStep);
        }

        public static Map getMap()
        {
            return map;
        }
        public static double getTime()
        {
            return time;
        }
        //TEST
        public static void test_fill_map()
        {

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("przystanki.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<TrackPointSerializable> przystanki = (List<TrackPointSerializable>)formatter.Deserialize(stream);
            stream.Close();

            Stream stream2 = new FileStream("linie.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<TrackSerializable> linie = (List<TrackSerializable>)formatter.Deserialize(stream2);
            stream2.Close();


            foreach (TrackPointSerializable tss in przystanki) {

                if (tss is TramStopSerializable) {
                    TramStopSerializable ts = tss as TramStopSerializable;
                    map.AddTrackPoint(new TramStop(ts.Name, new Vector2(ts.X, ts.Y), ts.popularity));
                } else if (tss is JunctionSerializable) {
                    JunctionSerializable js = tss as JunctionSerializable;
                    map.AddTrackPoint(new Junction(new Vector2(tss.X, tss.Y)));
                }
                
            }
            
            foreach (TrackSerializable ts in linie)
            {

                Track t = new Track(ts.numer);
                //foreach (TramStopSerializable tss in przystanki)
                {
                    foreach(TrackPointSerializable trst in ts.przystanki) {
                        t.AddTrackPoint(map.TrackPoints.ToList().Find(point => point.getPosition().X == trst.X && point.getPosition().Y == trst.Y));
                    }
                }

                map.AddTrack(t);
                map.Trams.Add(new Tram(map, 30, t, t.Stops.ElementAt(0), 0));
                map.Trams.Add(new Tram(map, 30, t, t.Stops.Last(), 0));
            }

            //map.AddTrackPoint(new TramStop("1", new Vector2(50, 170)));
            //map.AddTrackPoint(new TramStop("3", new Vector2(140, 187)));
            //map.AddTrackPoint(new TramStop("4", new Vector2(200, 135)));
            //map.AddTrackPoint(new TramStop("5", new Vector2(270, 153)));

            //map.AddTrackPoint(new Junction(new Vector2(320, 158)));

            //map.AddTrackPoint(new TramStop("6", new Vector2(340, 160)));

            //map.AddTrackPoint(new Junction(new Vector2(380, 240)));

            //map.AddTrackPoint(new TramStop("7", new Vector2(420, 300)));
            //map.AddTrackPoint(new TramStop("8", new Vector2(500, 300)));
            //map.AddTrackPoint(new TramStop("9", new Vector2(560, 190)));

            //map.AddTrackPoint(new TramStop("6", new Vector2(290, 70)));
            //map.AddTrackPoint(new TramStop("7", new Vector2(350, 370)));
            //map.AddTrackPoint(new TramStop("7", new Vector2(270, 400)));

            //List<TrackPoint> track33points = new List<TrackPoint>();
            //for (int i = 0; i < 10; i++)
            //    track33points.Add(map.TrackPoints.ElementAt(i));

            //Track track33 = new Track(33, track33points);
            //map.AddTrack(track33);

            //map.Trams.Add(new Tram(map, 30, track33, track33.Stops.ElementAt(0), 0));
            ////map.Trams.Add(new Tram(map, 15, track33, track33.Stops[0], 4));
            //map.Trams.Add(new Tram(map, 30, track33, track33.Stops.Last(), 0));
            ////map.Trams.Add(new Tram(map, 20, track33, track33.Stops[0], 13));

            //Track track11 = new Track(11, new List<TrackPoint>() {
            //    map.TrackPoints.ElementAt(10),
            //    map.TrackPoints.ElementAt(4),
            //    map.TrackPoints.ElementAt(5),
            //    map.TrackPoints.ElementAt(6),
            //    map.TrackPoints.ElementAt(11),
            //    map.TrackPoints.ElementAt(12)
            //});
            //map.AddTrack(track11);





            foreach (TramStop stop in map.TramStops)
                stop.AddPassengers(stop.AccessibleStops, 50);

        }

        public static Bitmap Display()
        {
            //g.FillRectangle(brush_white, 0, 0, b.Width, b.Height);
            g.DrawImage(Resources.Wrocław, 0, 0);

            if (map == null) return b;

            foreach (var key in map.Traffic.Keys) {
                float x1, y1, x2, y2;
                x1 = key.Item1.getPosition().X;
                y1 = key.Item1.getPosition().Y;
                x2 = key.Item2.getPosition().X;
                y2 = key.Item2.getPosition().Y;
                g.DrawLine(new Pen(brush_black), x1, y1, x2, y2);
            }

            foreach (TrackPoint trackPoint in map.TrackPoints)
            {
                float x1, y1;
                x1 = trackPoint.getPosition().X;
                y1 = trackPoint.getPosition().Y;
                if(trackPoint is TramStop) {
                    int width = 8;
                    g.FillRectangle(brush_blue, trackPoint.getPosition().X - width/2, trackPoint.getPosition().Y - width/2, width, width);
                    TramStop tramStop = trackPoint as TramStop;
                    g.DrawString(tramStop.GetCurrentAmountOfPeople().ToString(), new Font("Arial", 10), brush_black, x1, y1);
                } else if(trackPoint is Junction) {
                    int width = 6;
                    g.FillRectangle(brush_black, x1 - width / 2, y1 - width / 2, width, width);
                } 
                
            } 

            foreach (Tram t in map.Trams)
            {
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                int radius = 10;
                g.FillEllipse(brush_white, t.GetCurrentPos().X - radius, t.GetCurrentPos().Y - radius, radius*2, radius*2);
                g.DrawEllipse(new Pen(Color.Black,2), t.GetCurrentPos().X - radius, t.GetCurrentPos().Y - radius, radius * 2, radius * 2);
                g.DrawString(t.getTrack().Number.ToString(), new Font("Arial", 10, FontStyle.Bold), brush_black, t.GetCurrentPos().X, t.GetCurrentPos().Y, stringFormat);

                g.DrawString(t.getPassengerCount().ToString(), new Font("Arial", 10), brush_black, t.GetCurrentPos().X, t.GetCurrentPos().Y - radius * 2, stringFormat);
            }


            TimeSpan timeSpan = TimeSpan.FromSeconds(totalTime);
            string timeString = timeSpan.ToString(@"hh\:mm\:ss");
            g.DrawString(timeString, new Font("Arial", 16), brush_black, b.Width - 110f, 20f);

            

            return b;
        }
    }
}
