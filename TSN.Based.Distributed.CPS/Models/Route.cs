using System;
using System.Collections.Generic;
using System.Text;
using TSN.Based.Distributed.CPS.Models;

namespace TSN.Based.Distributed.CPS
{
    [Serializable]
    public class Route
    {

        public List<Link> links { get; set; }

        public string dest { get; set; }
        public string src { get; set; }

    }
}
