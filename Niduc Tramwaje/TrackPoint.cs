﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Niduc_Tramwaje
{
    [Serializable]
    public abstract class TrackPoint
    {
        protected Vector2 position;

        public Vector2 getPosition()
        {
            return position;
        }


        protected abstract bool HasFreeConnection();
        protected abstract void AddConnection(TrackPoint trackPoint);
        protected abstract bool HasConnection(TrackPoint trackPoint);

        public void Connect(TrackPoint trackPoint) {
            if (this.HasFreeConnection() && trackPoint.HasFreeConnection()) {
                this.AddConnection(trackPoint);
                trackPoint.AddConnection(this);
            } else
                throw new Exception("Jeden z elementów nie ma wolnych połączeń! " + ((this is TramStop) ? (this as TramStop).getTramStopName() : ""));
        }

        public bool IsConnectedWith(TrackPoint trackPoint) {
            return this.HasConnection(trackPoint) && trackPoint.HasConnection(this);
        }
    }
}
