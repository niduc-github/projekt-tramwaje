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
                UpdateTramStops();
            }
        }

        private static void UpdateTrams() {
            foreach (Tram t in trams) {
                t.Update(timeStep);
            }
        }

        private static void UpdateTramStops() {
            foreach (TramStop stop in map.getTramStopList())
                stop.GeneratePassengers(stop.Tracks.SelectMany(x => x.Stops).ToList(), timeStep);
        }

        //TEST
        public static void test_fill_map()
        {
            map.getTramStopList().Add(new TramStop("1", new Vector2(50, 170)));
            map.getTramStopList().Add(new TramStop("3", new Vector2(140, 187)));
            map.getTramStopList().Add(new TramStop("4", new Vector2(200, 135)));
            map.getTramStopList().Add(new TramStop("5", new Vector2(270, 153)));
            map.getTramStopList().Add(new TramStop("6", new Vector2(340, 160)));
            map.getTramStopList().Add(new TramStop("7", new Vector2(420, 300)));
            map.getTramStopList().Add(new TramStop("8", new Vector2(500, 300)));
            map.getTramStopList().Add(new TramStop("9", new Vector2(560, 190)));

            map.getTramStopList().Add(new TramStop("6", new Vector2(340, 70)));
            map.getTramStopList().Add(new TramStop("7", new Vector2(350, 370)));
            map.getTramStopList().Add(new TramStop("7", new Vector2(270, 400)));

            List<TramStop> line33stops = new List<TramStop>();
            for (int i = 0; i < 8; i++)
                line33stops.Add(map.getTramStopList()[i]);
            Track track33 = new Track(33, line33stops);
            map.getTrackList().Add(track33);

            Track track11 = new Track(11, new List<TramStop>() {
                map.getTramStopList()[8],
                map.getTramStopList()[4],
                map.getTramStopList()[5],
                map.getTramStopList()[9],
                map.getTramStopList()[10],
            });
            map.getTrackList().Add(track11);
          
            trams.Add(new Tram(20, track33, track33.Stops[0], 0));
            trams.Add(new Tram(15, track33, track33.Stops[0], 4));
            trams.Add(new Tram(35, track33, track33.Stops[0], 9));
            trams.Add(new Tram(20, track33, track33.Stops[0], 13));

            trams.Add(new Tram(20, track11, track11.Stops[0], 0));
            trams.Add(new Tram(15, track11, track11.Stops[0], 4));
            trams.Add(new Tram(35, track11, track11.Stops[0], 9));
            trams.Add(new Tram(20, track11, track11.Stops[0], 13));
        }

        public static Bitmap Display()
        {
            g.FillRectangle(brush_white, 0, 0, b.Width, b.Height);

            if (map == null) return b;

            List<TramStop> stops = map.getTramStopList();
            List<Track> tracks = map.getTrackList();

            foreach (Track t in tracks)
            {
                Color trackColor = Color.FromArgb((t.Number * 17) % 256, (t.Number * 39) % 256, (t.Number * 6) % 256);
                ReadOnlyCollection<TramStop> ts = t.Stops;
                for (int i = 0; i < ts.Count - 1; i++)
                {
                    g.DrawLine(new Pen(trackColor), ts[i].getPosition().X, ts[i].getPosition().Y, ts[i + 1].getPosition().X, ts[i + 1].getPosition().Y);
                }
            }

            foreach (TramStop t in stops)
            {
                g.FillRectangle(brush_black, t.getPosition().X - 4, t.getPosition().Y - 4, 9 - 1, 9 - 1);
                g.DrawString(t.GetCurrentAmountOfPeople().ToString(), new Font("Arial", 10), brush_black, t.getPosition().X, t.getPosition().Y + 5);
            }

            foreach (Tram t in trams)
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
