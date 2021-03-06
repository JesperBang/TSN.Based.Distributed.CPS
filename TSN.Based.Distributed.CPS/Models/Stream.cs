﻿using System;
using System.Collections.Generic;

namespace TSN.Based.Distributed.CPS.Models
{
    [Serializable]
    public class Stream
    {
        public string streamId { get; set; }
        public string source { get; set; }
        public string destination { get; set; }
        public double size { get; set; }
        public double period { get; set; }
        public double deadline { get; set; }
        public int rl { get; set; }
        public List<Route> Route { get; set; }
        public double Cost { get; set; }
    }
}
