using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSN.Based.Distributed.CPS.Models;

namespace TSN.Based.Distributed.CPS
{
   public class CoveredLinks
    {




        public int numberOfOneLinksCovered(List<Solution> input, List<Device> devices, List<Link> links)
        {
            int count = 0;
            PathFinder path = new PathFinder();
            foreach (Solution sol in input){
                foreach(Route r in sol.Route)
                {
                    foreach (Link l in r.links) { 
                        string src = l.source;
                        string dest = l.destination;
                        links.Remove(l);
                        Route newRoute = path.FindPath(src, dest, links, devices, new List<string>(), new Route());
                        if(newRoute != null)
                        {
                            count++;
                        }
                        links.Add(l);
                    }
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
                foreach (Route r in sol.Route)
                {
                    for(int i = 0; i < r.links.Count; i++)
                    {
                        if (i != 0)
                        {
                            string src = r.links[i - 1].source;
                            string dest = r.links[i].destination;
                            links.Remove(r.links[i - 1]);
                            links.Remove(r.links[i]);
                            Route newRoute = path.FindPath(src, dest, links, devices, new List<string>(), new Route());
                            if (newRoute != null)
                            {
                                count++;
                            }
                            links.Add(r.links[i - 1]);
                            links.Add(r.links[i]);
                        }

                    }
                   
                }
            }
            return count;
        }





    }
}
