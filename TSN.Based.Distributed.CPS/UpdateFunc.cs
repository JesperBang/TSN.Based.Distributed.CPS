using System;
using System.Collections.Generic;
using System.Text;
using TSN.Based.Distributed.CPS.Models;

namespace TSN.Based.Distributed.CPS
{
    public class UpdateFunc
    {

        public List<Solution> updateSolution(List<Solution> solutions, List<Link> links, List<Device> devices)
        {
            PathFinder pf = new PathFinder();
            Random rnd = new Random();
            int streamRandom = rnd.Next(0, solutions.Count);
            Solution currStream = solutions[streamRandom];
            int routeCount = currStream.Route.Count;
            int routeRandom = rnd.Next(0, routeCount);
            Route currRoutes = currStream.Route[routeRandom];

            int linkToReplaceFrom = rnd.Next(0, currRoutes.links.Count);
            Route newRoute = new Route();
            newRoute.links = currRoutes.links.GetRange(0, currRoutes.links.Count - (currRoutes.links.Count - linkToReplaceFrom));
            //currRoutes.links.RemoveRange(linkToReplaceFrom, currRoutes.links.Count - linkToReplaceFrom);
           
            string src = currRoutes.links[linkToReplaceFrom].source;
            string dest = solutions[streamRandom].Route[routeRandom].dest;
            
            Route path = pf.FindPath(src, dest, links, devices, new List<string>(), new Route());


            newRoute.links.AddRange(path.links);

            solutions[streamRandom].Route.RemoveAt(routeRandom);
            solutions[streamRandom].Route.Add(newRoute);

            return solutions;
        }











    }
}
