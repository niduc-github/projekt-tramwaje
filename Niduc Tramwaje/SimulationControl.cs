using System;
using System.Collections.Generic;
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
        static List<Tram> trams = new List<Tram>();
        static Map map = new Map();
        static float timeStep = 0.01f;
        static float timeScale = 1f;

        private static Bitmap b = new Bitmap(640, 480);
        private static Graphics g = Graphics.FromImage(b);
        private static Pen pen_black = new Pen(Color.Black);
        private static Pen pen_red = new Pen(Color.Red);
        private static Brush brush_white = new SolidBrush(Color.White);
        private static Brush brush_black = new SolidBrush(Color.Black);
        private static Brush brush_green = new SolidBrush(Color.Green);

        private static Stopwatch stopwatch = new Stopwatch();
        private static double time = 0d;

        static SimulationControl() {
            stopwatch.Start();
        }

        public static void Simulation()
        {
            stopwatch.Stop();
            time += stopwatch.Elapsed.TotalSeconds * timeScale;
            stopwatch.Restart();

            while(time >= timeStep) {
                time -= timeStep;
                UpdateTrams();
            }
        }

        private static void UpdateTrams() {
            foreach (Tram t in trams) {
                t.Update(timeStep);
            }
        }

        public static void test_fill_map()
        {
            map.getTramStopList().Add(new TramStop("0", new Vector2(10, 10)));
            map.getTramStopList().Add(new TramStop("1", new Vector2(50, 70)));
            map.getTramStopList().Add(new TramStop("2", new Vector2(90, 20)));
            map.getTramStopList().Add(new TramStop("3", new Vector2(140, 87)));
            map.getTramStopList().Add(new TramStop("4", new Vector2(200, 35)));
            map.getTramStopList().Add(new TramStop("5", new Vector2(270, 53)));
            map.getTramStopList().Add(new TramStop("6", new Vector2(340, 60)));
            map.getTramStopList().Add(new TramStop("7", new Vector2(420, 200)));
            map.getTramStopList().Add(new TramStop("8", new Vector2(500, 200)));
            map.getTramStopList().Add(new TramStop("9", new Vector2(560, 90)));

            Track t = new Track();
            foreach (TramStop ts in map.getTramStopList())
            {
                t.getTramStopList().Add(ts);
                ts.PassangerGeneration(map.getTramStopList());
            }       
            map.getTrackList().Add(t);

            Tram tr = new Tram(30, t, t.getTramStopList()[0]);
            trams.Add(tr);
        }

        public static Bitmap Display()
        {
            g.FillRectangle(brush_white, 0, 0, b.Width, b.Height);

            if (map == null) return b;

            List<TramStop> stops = map.getTramStopList();
            List<Track> tracks = map.getTrackList();      
            
            foreach (Track t in tracks)
            {
                List<TramStop> ts = t.getTramStopList();
                for (int i = 0; i < ts.Count - 1; i++)
                {
                    g.DrawLine(pen_red, ts[i].getPosition().X, ts[i].getPosition().Y, ts[i + 1].getPosition().X, ts[i + 1].getPosition().Y);
                }
            }

            foreach (TramStop t in stops)
            {
                g.FillRectangle(brush_black, t.getPosition().X - 4, t.getPosition().Y - 4, 9 - 1, 9 - 1);
                g.DrawString(t.GetCurrentAmountOfPeople().ToString(), new Font("Arial", 10), brush_black, t.getPosition().X, t.getPosition().Y + 5);
            }

            foreach (Tram t in trams)
            {
                int radius = 5;
                g.FillEllipse(brush_green, t.GetCurrentPos().X - radius, t.GetCurrentPos().Y - radius, radius*2, radius*2);
            }

            return b;
        }
    }
}
