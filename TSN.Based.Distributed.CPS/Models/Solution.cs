using System;
using System.Collections.Generic;
using System.Text;

namespace TSN.Based.Distributed.CPS.Models
{
    public class Solution
    {
        public string StreamId { get; set; }

        public List<Route> Route { get; set; }
        public double Cost { get; set; }
        public double size { get; set;}
        public string src { get; set; }
        public string dest { get; set; }
        public double period { get; set; }
    }
}
