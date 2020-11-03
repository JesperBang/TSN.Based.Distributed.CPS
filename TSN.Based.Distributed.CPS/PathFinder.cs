using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSN.Based.Distributed.CPS.Models;

namespace TSN.Based.Distributed.CPS
{
    class PathFinder
    {

        public List<Link> FindPath(string src, string dest, int size, List<Link> links, List<Device> devices, List<String> visited, List<Link> usedLinks)
        {

            if (visited.Count > 0) { visited.Add(src); };

            //find all available links from the current source. 
            //Check that the destination of the link is either a new switch or det requested end-destination.
            //Also sorts out already visited switches to prevent looping of the algorithmn. 
            List<Link> availableLinks =
                links.FindAll(link => link.source == src && (link.destination == dest || devices.FindAll(d => d.type == "Switch").Exists(d => d.name == link.destination)))
                .FindAll(link => !visited.Contains(link.destination));
            if (availableLinks.Exists(l => l.destination == dest))
            {
                usedLinks.Add(availableLinks.Find(link => link.destination == dest));
                return usedLinks;
            }
            else
            {
                Random rand = new Random();
                int r = rand.Next(availableLinks.Count);
                usedLinks.Add(availableLinks[r]);
                visited.Add(availableLinks[r].destination);
                usedLinks.Concat(FindPath(availableLinks[r].destination, dest, size, links, devices, visited, usedLinks));
            }
            return usedLinks;
        }

        public void FindMultiplePaths(Device src, Device dest, int size, List<Link> links, List<Device> devices, int RL)
        {

            for (int i = 0; i < RL; i++)
            {


            }


        }
    }
}
