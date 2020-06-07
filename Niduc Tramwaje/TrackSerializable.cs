using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niduc_Tramwaje
{
    [Serializable]
    public class TrackSerializable
    {
        public List<TrackPointSerializable> przystanki;
        public int numer;
    }
}
