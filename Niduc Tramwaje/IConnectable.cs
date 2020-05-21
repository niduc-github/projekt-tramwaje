﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niduc_Tramwaje
{
    interface IConnectable
    {
        bool HasFreeConnection();
        bool AddConnection(TrackPoint element);
        void Connect(IConnectable element);  
    }
}
