using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Numerics;
using Niduc_Tramwaje.Properties;

namespace Niduc_Tramwaje
{
    [Serializable]
    class Statistics
    {
        Graphics g;

        double intervalHours = 0.33f;
        static int id = 1;

        Color lowTrafficColor = Color.LightGreen;
        Color highTrafficColor = Color.Red;

        int upperTramStopLimit = 25;
        int tramPerSegmentLimit = 4;

        double timeSeconds = 0;

        public void Update(float time, Map map) {
            timeSeconds += time;
            if(timeSeconds > SimulationControl.HoursToSeconds(intervalHours)) {
                timeSeconds -= SimulationControl.HoursToSeconds(intervalHours);
                ProbeTraffic(map);
            }
        }

        public void ProbeTraffic(Map map) {
            Bitmap bitmap = Resources.Wrocław;
            g = Graphics.FromImage(bitmap);
 
            foreach (var segmentTraffic in map.Traffic) {
                Color blend = Utility.Blend(lowTrafficColor, highTrafficColor, segmentTraffic.Value.Count / (float)tramPerSegmentLimit);
                Vector2 pos1 = segmentTraffic.Key.Item1.getPosition(), pos2 = segmentTraffic.Key.Item2.getPosition();
                int width = 5;
                g.DrawLine(new Pen(blend, width), pos1.X, pos1.Y, pos2.X, pos2.Y);
            }

            foreach (TramStop stop in map.TramStops) {
                int radius = 16;
                Color blend = Utility.Blend(lowTrafficColor, highTrafficColor, stop.getPassangerList().Count / (float)upperTramStopLimit);
                g.DrawEllipse(new Pen(Color.Black), stop.getPosition().X - radius / 2, stop.getPosition().Y - radius / 2, radius, radius);
                g.FillEllipse(new SolidBrush(blend), stop.getPosition().X - radius / 2, stop.getPosition().Y - radius / 2, radius, radius);
            }

            TimeSpan timeSpan = TimeSpan.FromSeconds(SimulationControl.TotalTime);
            string timeString = timeSpan.ToString(@"hh\:mm\:ss");
            g.DrawString(timeString, new Font("Arial", 16), new SolidBrush(Color.Black), bitmap.Width - 110f, 20f);

            bitmap.Save("Stats" + id.ToString() + ".bmp");
            id++;
        }
    }
}
