using System;
using System.Collections.Generic;
using TSN.Based.Distributed.CPS.Models;


namespace TSN.Based.Distributed.CPS
{
    class Program
    {
        static void Main(string[] args)
        {
            RandomState random = new RandomState();
            PathFinder path = new PathFinder();
            List<Link> links = new List<Link>();
            List<Device> devices = new List<Device>();
            List<Stream> streams = new List<Stream>();
            (devices, links, streams) = XmlReader.LoadXml();

            List<Solution> s_best = random.generateState(streams, links, devices);
            UpdateFunc up = new UpdateFunc();
            s_best = up.updateSolution(s_best, links, devices);
        


            //Route l = path.FindPath(streams[0].source, streams[0].destination, links, devices, new List<string>(), new Route());
            //List<Route> ml = path.FindMultiplePaths(streams[0].source, streams[0].destination, links, devices, 3, new List<string>(), new List<Route>());

            Console.Read();
            

        }
    }
}
