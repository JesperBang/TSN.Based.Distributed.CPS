using System;

namespace TSN.Based.Distributed.CPS.Models
{
    [Serializable]
    public class Stream
    {
        public string streamId { get; set; }
        public string source { get; set; }
        public string destination { get; set; }
        public int size { get; set; }
        public int period { get; set; }
        public int deadline { get; set; }
        public int rl { get; set; }
    }
}
