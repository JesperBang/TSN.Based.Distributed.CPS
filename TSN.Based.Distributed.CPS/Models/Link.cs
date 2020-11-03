using System;

namespace TSN.Based.Distributed.CPS.Models
{
    [Serializable]
    public class Link 
    {
        public string source { get; set; }
        public string destination { get; set; }
        public double speed { get; set; }
    }
}
