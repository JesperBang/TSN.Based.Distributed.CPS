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
            for (int i = 0; i < solutions.Count; i++)
            {
                Solution currStream = solutions[i];
                int routeCount = currStream.routes.Count;
                for (int j = 0; j < routeCount; j++)
                {
                    Route currRoutes = currStream.routes[j];
                    Random rnd = new Random();
                    int linkToReplaceFrom = rnd.Next(0, currRoutes.links.Count);
                    currRoutes.links.RemoveRange(linkToReplaceFrom + 1, currRoutes.links.Count - linkToReplaceFrom);
                    string src = currRoutes.links[linkToReplaceFrom].source;
                    string dest = currRoutes.dest;
                    List<String> strings = new List<string>();
                    List<Link> path = pf.FindPath(src, dest, (int)currStream.size, links, devices, strings);
                    currRoutes.links.AddRange(path);
                }
            }

            return solutions;
        }











    }
}
