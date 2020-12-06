using System.Collections.Generic;

namespace TSN.Based.Distributed.CPS.Models
{
    public class Solution
    {
        public string StreamId { get; set; }

        public List<Route> Route { get; set; }
        public double Cost { get; set; }
        public double size { get; set;}
        public string source { get; set; }
        public string destination { get; set; }
        public double period { get; set; }
        public int rl { get; set; }
        public double deadline { get; set; }
    }
}
