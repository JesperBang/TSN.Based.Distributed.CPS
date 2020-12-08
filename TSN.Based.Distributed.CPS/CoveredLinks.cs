using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSN.Based.Distributed.CPS.Models;

namespace TSN.Based.Distributed.CPS
{
    public class CoveredLinks
    {
        public int numberOfOneLinksCovered(List<Solution> input, List<Device> devices, List<Link> links)
        {
            int count = 0;

            foreach (var sol in input)
            {
                PathFinder path = new PathFinder();
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

            foreach (var sol in input)
            {
                PathFinder path = new PathFinder();
                List<Link> allLinks = links.FindAll(l => sol.Route.Exists(r => r.links.Contains(l)));
                List<(Link, Link)> parlist = new List<(Link, Link)>();

                for (int j = 0; j < sol.Route.Count(); j++)
                {
                    for (int k = 0; k < sol.Route[j].links.Count() - 1; k++) parlist.Add((sol.Route[j].links[k], sol.Route[j].links[k + 1]));
                }

                parlist.Distinct();

                for (int i = 0; i < parlist.Count; i++)
                {
                    var link1 = new Link();
                    var link2 = new Link();

                    (link1, link2) = parlist[i];

                    allLinks.Remove(link1);
                    allLinks.Remove(link2);

                    List<Route> allRoutes = path.FindAllPaths(sol.source, sol.destination, allLinks, devices);

                    allLinks.Add(link1);
                    allLinks.Add(link2);

                    if (allRoutes.Count < 1) count++;
                }
            }
            return count;
        }
    }
}
