using System;
using System.Collections.Generic;
using System.Data;
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

            Link link1 = new Link
            {
                source = "ES1",
                destination = "SW0",
                speed = 1.25
            };
            Link link2 = new Link
            {
                source = "SW0",
                destination = "ES3",
                speed = 1.25
            };
            Route route = new Route
            {
                links = new List<Link> { link1, link2 },
                src = "ES1",
                dest = "ES3"
            };
            Stream stream = new Stream
            {
                streamId = "Stream0",
                source = "ES1",
                destination = "ES3",
                size = 100,
                period = 1000,
                deadline = 10000,
                rl = 1,
                Route = new List<Route> { route }
            };


            CostFunction cf = new CostFunction();

            cf.CalcCostFunction(stream);

            //Route l = path.FindPath(streams[0].source, streams[0].destination, links, devices, new List<string>(), new Route());
            //List<Route> ml = path.FindMultiplePaths(streams[0].source, streams[0].destination, links, devices, 3, new List<string>(), new List<Route>());

            Console.Read();
            

        }
    }
}
