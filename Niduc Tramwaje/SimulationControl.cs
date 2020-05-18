using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;
using System.Diagnostics;

namespace Niduc_Tramwaje
{
    static class SimulationControl
    {
        static Map map = new Map();
        static float timeStep = 0.01f;
        static float minTimeScale = 0.1f;
        static float maxTimeScale = 10f;
        static float timeScaleSlider = 0.1f;

        static float TimeScale => minTimeScale + (maxTimeScale - minTimeScale) * timeScaleSlider;
        public static float TimeScaleSlider { set => timeScaleSlider = Utility.Clamp01(value); }

        private static Bitmap b = new Bitmap(640, 480);
        private static Graphics g = Graphics.FromImage(b);
        private static Pen pen_black = new Pen(Color.Black);
        private static Pen pen_red = new Pen(Color.Red);
        private static Brush brush_white = new SolidBrush(Color.White);
        private static Brush brush_black = new SolidBrush(Color.Black);
        private static Brush brush_green = new SolidBrush(Color.Green);

        private static Stopwatch stopwatch = new Stopwatch();
        private static double time = 0d;

        static SimulationControl() 
        {
            stopwatch.Start();
        }

        public static void Simulation()
        {
            stopwatch.Stop();
            time += stopwatch.Elapsed.TotalSeconds * TimeScale;
            stopwatch.Restart();

            while(time >= timeStep) 
            {
                time -= timeStep;
                UpdateTrams();
                UpdateTramStops();
            }
        }

        private static void UpdateTrams() 
        {
            foreach (Tram t in map.Trams) 
            {
                t.Update(timeStep);
            }
        }

        private static void UpdateTramStops() 
        {
            foreach (TramStop stop in map.Stops)
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
            map.AddStop(new TramStop("1", new Vector2(50, 170)));
            map.AddStop(new TramStop("3", new Vector2(140, 187)));
            map.AddStop(new TramStop("4", new Vector2(200, 135)));
            map.AddStop(new TramStop("5", new Vector2(270, 153)));
            map.AddStop(new TramStop("6", new Vector2(340, 160)));
            map.AddStop(new TramStop("7", new Vector2(420, 300)));
            map.AddStop(new TramStop("8", new Vector2(500, 300)));
            map.AddStop(new TramStop("9", new Vector2(560, 190)));

            map.AddStop(new TramStop("6", new Vector2(340, 70)));
            map.AddStop(new TramStop("7", new Vector2(350, 370)));
            map.AddStop(new TramStop("7", new Vector2(270, 400)));

            List<TramStop> line33stops = new List<TramStop>();
            for (int i = 0; i < 8; i++)
                line33stops.Add(map.Stops.ElementAt(i));
            Track track33 = new Track(33, line33stops);
            map.AddTrack(track33);

            Track track11 = new Track(11, new List<TramStop>() {
                map.Stops.ElementAt(8),
                map.Stops.ElementAt(4),
                map.Stops.ElementAt(5),
                map.Stops.ElementAt(9),
                map.Stops.ElementAt(10),
            });
            map.AddTrack(track11);
          
            //map.Trams.Add(new Tram(map, 20, track33, track33.Stops[0], 0));
            map.Trams.Add(new Tram(map, 15, track33, track33.Stops[0], 4));
            map.Trams.Add(new Tram(map,35, track33, track33.Stops[0], 9));
            map.Trams.Add(new Tram(map, 20, track33, track33.Stops[0], 13));

            //map.Trams.Add(new Tram(map, 20, track11, track11.Stops[0], 0));
            map.Trams.Add(new Tram(map, 15, track11, track11.Stops[0], 4));
            map.Trams.Add(new Tram(map,35, track11, track11.Stops[0], 9));
            map.Trams.Add(new Tram(map, 20, track11, track11.Stops[0], 13));
        }

        public static Bitmap Display()
        {
            g.FillRectangle(brush_white, 0, 0, b.Width, b.Height);

            if (map == null) return b;

            foreach (Track t in map.Tracks)
            {
                Color trackColor = Color.FromArgb((t.Number * 17) % 256, (t.Number * 39) % 256, (t.Number * 6) % 256);
                ReadOnlyCollection<TramStop> ts = t.Stops;
                for (int i = 0; i < ts.Count - 1; i++)
                {
                    g.DrawLine(new Pen(trackColor), ts[i].getPosition().X, ts[i].getPosition().Y, ts[i + 1].getPosition().X, ts[i + 1].getPosition().Y);
                }
            }

            foreach (TramStop t in map.Stops)
            {
                g.FillRectangle(brush_black, t.getPosition().X - 4, t.getPosition().Y - 4, 9 - 1, 9 - 1);
                g.DrawString(t.GetCurrentAmountOfPeople().ToString(), new Font("Arial", 10), brush_black, t.getPosition().X, t.getPosition().Y + 5);
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

            return b;
        }
    }
}
