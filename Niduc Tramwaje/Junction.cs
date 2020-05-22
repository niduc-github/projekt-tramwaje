using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Niduc_Tramwaje {
    class Junction : TrackPoint{
        List<TrackPoint> connections;

        public Junction(Vector2 position) {
            connections = new List<TrackPoint>();
            this.position = position;
        }

        protected override void AddConnection(TrackPoint trackPoint) {
            connections.Add(trackPoint);
        }

        protected override bool HasFreeConnection() {
            return true;
        }

        protected override bool HasConnection(TrackPoint trackPoint) {
            return connections.Contains(trackPoint);
        }
    }
}
