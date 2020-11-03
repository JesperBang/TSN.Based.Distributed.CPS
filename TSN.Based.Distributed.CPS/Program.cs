using System;
using System.Collections.Generic;
using TSN.Based.Distributed.CPS.Models;


namespace TSN.Based.Distributed.CPS
{
    class Program
    {
        static void Main(string[] args)
        {
            PathFinder path = new PathFinder();
            List<Link> links = new List<Link>();
            List<Device> devices = new List<Device>();
            List<Stream> streams = new List<Stream>();

            (devices, links, streams) = XmlReader.LoadXml();

            List<Link> l = path.FindPath(streams[0].source, streams[0].destination, 100, links, devices, new List<string>(), new List<Link>());
            List<Route> ml = path.FindMultiplePaths(streams[0].source, streams[0].destination, 100, links, devices, 3, new List<string>(), new List<Route>());

            Console.Read();
            

        }
    }
}
