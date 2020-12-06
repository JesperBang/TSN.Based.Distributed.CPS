using System.Collections.Generic;
using System.Linq;
using TSN.Based.Distributed.CPS.Models;

namespace TSN.Based.Distributed.CPS
{
    public class CoveredLinks
    {
        public int numberOfOneLinksCovered(List<Solution> input, List<Device> devices, List<Link> links)
        {
            int count = 0;
            PathFinder path = new PathFinder();
            foreach (Solution sol in input)
            {
                List<Link> allLinks = links.FindAll(l => sol.Route.Exists(r => r.links.Contains(l)));
                foreach (Link l in allLinks.ToList())
                {
                    allLinks.Remove(l);
                    List<Route> allRoutes = path.FindAllPaths(sol.source, sol.destination, allLinks, devices);
                    if (allRoutes.Count < 1) count++;

                    allLinks.Add(l);
                }
            }
            return count;
        }
        public int numberOfTwoLinksCovered(List<Solution> input, List<Device> devices, List<Link> links)
        {
            int count = 0;

            PathFinder path = new PathFinder();
            foreach (Solution sol in input)
            {
                List<Link> allLinks = links.FindAll(l => sol.Route.Exists(r => r.links.Contains(l)));

                for (int i = 0; i < allLinks.Count - 1; i++)
                {
                    List<Link> temp = allLinks.FindAll(l => (allLinks[i].source != l.source && allLinks[i].destination != l.destination) ||
                                     (allLinks[i + 1].source != l.source && allLinks[i + 1].destination != l.destination));

                    // dunno
                    for (int counter = 0; counter < temp.Count(); counter++)
                    {
                        allLinks.Remove(temp[counter]);
                    }

                    List<Route> allRoutes = path.FindAllPaths(sol.source, sol.destination, temp, devices);

                    foreach (Link link in temp)
                    {
                        allLinks.Add(link);
                    }

                    if (allRoutes.Count < 1) count++;
                }
            }
            return count;
        }
    }
}
