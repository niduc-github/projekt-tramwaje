using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

namespace Niduc_Tramwaje
{
    static class SimulationControl
    {
        static List<Tram> trams;
        static Map map;

        public static void Simulation()
        {
            map = new Map();
            test_fill_map();
        }

        static void test_fill_map()
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
            }
            

            map.getTrackList().Add(t);
        }

        public static Bitmap Display()
        {
            Bitmap b = new Bitmap(640, 480);
            Graphics g = Graphics.FromImage(b);
            Pen pen_black = new Pen(Color.Black);
            Pen pen_red = new Pen(Color.Red);
            Brush brush_white = new SolidBrush(Color.White);
            Brush brush_black = new SolidBrush(Color.Black);

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
            }

            foreach (Tram t in trams)
            {
                t.
            }

            return b;
        }

        /*private class TramsOnTrack
        {
            private Dictionary<KeyValuePair<TramStop, TramStop>, List<KeyValuePair<Tram, float>>> tramsOnTrack;

            public List<KeyValuePair<Tram,float>> GetTramsOnTrack(KeyValuePair<TramStop, TramStop> origindDestPair) {
                return tramsOnTrack[origindDestPair];
            }
        }*/
    }
}
