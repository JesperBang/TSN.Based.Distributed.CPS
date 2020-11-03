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

        public List<Route> FindMultiplePaths(string src, string dest, int size, List<Link> links, List<Device> devices, int RL, List<String> visited, List<Route> usedLinks)
        {
            if (usedLinks.Count < 3)
            {
                usedLinks.Add(new Route()); ;
                usedLinks.Add(new Route());
                usedLinks.Add(new Route());
            }

            for (int i = 0; i < RL; i++)
            {
                //Init Route
                usedLinks[i].src ??= src;
                usedLinks[i].dest ??= dest;
                usedLinks[i].links ??= new List<Link>();


                if(!usedLinks[i].links.Exists(l => l.destination == dest))
                    {
                    List<Link> availableLinks =
                            links.FindAll(link => link.source == src && (link.destination == dest || devices.FindAll(d => d.type == "Switch").Exists(d => d.name == link.destination)))
                            .FindAll(link => !visited.Contains(link.destination));
                    if (availableLinks.Exists(l => l.destination == dest))
                    {
                        usedLinks[i].links.Add(availableLinks.Find(link => link.destination == dest));
                        return usedLinks;
                    }
                    else if(availableLinks.Count > 0)
                    {
                        //Pick a link witch has not been utilized yet 
                        Random rand = new Random();
                        int r = rand.Next(availableLinks.Count);
                        usedLinks[i].links.Add(availableLinks[r]);
                        visited.Add(availableLinks[r].destination);
                        usedLinks.Concat(FindMultiplePaths(availableLinks[r].destination, dest, size, links, devices, RL, visited, usedLinks));
                    }
                    else
                    {
                        //If no alternative link is available, reuse one. 
                        availableLinks =
                               links.FindAll(link => link.source == src && (link.destination == dest || devices.FindAll(d => d.type == "Switch").Exists(d => d.name == link.destination)));
                        Random rand = new Random();
                        int r = rand.Next(availableLinks.Count);
                        usedLinks[i].links.Add(availableLinks[r]);
                        visited.Add(availableLinks[r].destination);
                        usedLinks.Concat(FindMultiplePaths(availableLinks[r].destination, dest, size, links, devices, RL, visited, usedLinks));
                    }
                }
            }
            return usedLinks;
        }

    }
}
