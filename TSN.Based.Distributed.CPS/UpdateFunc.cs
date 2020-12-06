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
            bool newState = false;
            int noOfRetries = 0;
            // find random stream
            while (!newState && noOfRetries < solutions.Count)
            {
                int streamRandom = rnd.Next(0, solutions.Count);
                Solution currStream = solutions[streamRandom];
                // find random route for the specific stream

                List<Route> allRoutes = pf.FindAllPaths(currStream.source, currStream.destination, links, devices);

                if (currStream.rl < allRoutes.Count)
                {

                    int routeCount = currStream.Route.Count;
                    int routeRandom = rnd.Next(0, currStream.Route.Count);

                    //Find all possible new routes 
                    List<Route> temp = allRoutes.FindAll(r => !currStream.Route.Exists(cr => cr.id == r.id));
                    int tempRandom = rnd.Next(0, temp.Count);

                    //Remove random old route and add random new from the list of possible new.
                    currStream.Route.RemoveAt(routeRandom);
                    currStream.Route.Add(temp[tempRandom]);

                    // call path method with the source of the random link and the destination of the stream
                    newState = true;
                }
                noOfRetries++;
            }
            return solutions;
        }
    }
}
