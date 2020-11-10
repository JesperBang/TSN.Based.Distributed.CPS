using System;
using System.Collections.Generic;
using TSN.Based.Distributed.CPS.Models;

namespace TSN.Based.Distributed.CPS
{
    public class UpdateFunc
    {

        public List<Solution> updateSolution(List<Solution> solutions, List<Link> links, List<Device> devices)
        {
            PathFinder pf = new PathFinder();
            Random rnd = new Random();
            // find random stream
            int streamRandom = rnd.Next(0, solutions.Count);
            Solution currStream = solutions[1];
            // find random route for the specific stream
            int routeCount = currStream.Route.Count;
            int routeRandom = rnd.Next(0, routeCount);
            Route currRoutes = currStream.Route[routeRandom];
            // find random link for the specific route
            int linkToReplaceFrom = rnd.Next(0, currRoutes.links.Count);

            // make new route with the remnants from the old route that should still be used
            Route newRoute = new Route();
            newRoute.links = currRoutes.links.GetRange(0, linkToReplaceFrom);
            
            // call path method with the source of the random link and the destination of the stream
            string src = currRoutes.links[linkToReplaceFrom].source;
            string dest = currStream.dest;

            if (currStream.rl > 1) {
                List<string> visited = new List<string>();
                currStream.Route.RemoveAt(routeRandom);
                foreach(Route r in currStream.Route)
                {
                    foreach (Link l in r.links)
                    {
                        if (l.source != currStream.dest)
                        {
                            visited.Add(l.source);
                        }
                    }
                }
                List<Route> path = pf.FindMultiplePaths(src, dest, links, devices, 1, visited, new List<Route>());
                currStream.Route.Add(path[0]);
                solutions[streamRandom].Route = currStream.Route;
            }
            else
            {               
                Route path = pf.FindPath(src, dest, links, devices, new List<string>(), new Route());

                // add the newly found path to the route
                newRoute.links.AddRange(path.links);

                // remove old route and add new to solution
                solutions[streamRandom].Route[routeRandom] = newRoute;

            }


            return solutions;
        }

    }
}
