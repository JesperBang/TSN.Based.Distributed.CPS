using System;
using System.Collections.Generic;
using System.Linq;
using TSN.Based.Distributed.CPS.Models;

namespace TSN.Based.Distributed.CPS
{
    class PathFinder
    {

        //public Route FindPath(string src, string dest, List<Link> links, List<Device> devices, List<string> visited, Route usedLinks)
        //{
        //    usedLinks.links ??= new List<Link>();
        //    //find all available links from the current source. 
        //    //Check that the destination of the link is either a new switch or det requested end-destination.
        //    //Also sorts out already visited switches to prevent looping of the algorithmn. 
        //    List<Link> availableLinks =
        //        links.FindAll(link => link.source == src && (link.destination == dest || devices.FindAll(d => d.type == "Switch").Exists(d => d.name == link.destination)))
        //        .FindAll(link => !visited.Contains(link.destination));
        //    if (availableLinks.Exists(l => l.destination == dest))
        //    {
        //        usedLinks.links.Add(availableLinks.Find(link => link.destination == dest));
        //        usedLinks.src = src;
        //        usedLinks.dest = dest;
        //        return usedLinks;
        //    }
        //    else
        //    {
        //        Random rand = new Random();
        //        int r = rand.Next(availableLinks.Count);
        //        usedLinks.links.Add(availableLinks[r]);
        //        visited.Add(availableLinks[r].destination);
        //        usedLinks.links.Concat(FindPath(availableLinks[r].destination, dest, links, devices, visited, usedLinks).links);
        //        usedLinks.src = src;
        //        usedLinks.dest = dest;
        //    }
        //    return usedLinks;
        //}

        public Tuple<Route, Dictionary<string, int>> FindPath(string src, string dest, List<Link> links, List<Device> devices, int RL, Dictionary<string, int> visited, Route usedLinks, int minSrc)
        {

            //Init Route
            usedLinks.links ??= new List<Link>();
            if (!usedLinks.links.Exists(l => l.destination == dest))
            {
                bool hasLinkToDest = links.Exists(link => link.source == src && link.destination == dest);

                List<Link> availableLinks =
                        links.FindAll(link => link.source == src && (link.destination == dest || devices.FindAll(d => d.type == "Switch").Exists(d => d.name == link.destination)))
                        .FindAll(link => !visited.ContainsKey(link.destination));

                if (hasLinkToDest)
                {
                    Link linkToDest = links.FirstOrDefault(link => link.source == src && link.destination == dest);
                    usedLinks.links.Add(linkToDest);
                    return Tuple.Create(usedLinks, visited);
                }

                else if (availableLinks.Count > 0)
                {
                    //Pick a link witch has not been utilized yet 
                    Random rand = new Random();
                    int r = rand.Next(availableLinks.Count);
                    usedLinks.links.Add(availableLinks[r]);

                    if (!visited.ContainsKey(availableLinks[r].destination))
                    {
                        visited.Add(availableLinks[r].destination, 1);
                    }

                    usedLinks.links.Concat(FindPath(availableLinks[r].destination, dest, links, devices, RL, visited, usedLinks, minSrc).Item1.links);
                }
                else if (usedLinks.links.Count > minSrc)
                {
                    usedLinks.links.RemoveAt(usedLinks.links.Count - 1);
                    usedLinks.links.Concat(FindPath(usedLinks.links[(usedLinks.links.Count - 1)].destination, dest, links, devices, RL, visited, usedLinks, minSrc).Item1.links);
                }
                else
                {
                    if (usedLinks.links.Count == minSrc)
                    {
                        minSrc += minSrc;
                    }
                    //If no alternative link is available, reuse one. 
                    List<Link> availableLinks1 =
                           links.FindAll(link => link.source == src && visited.ContainsKey(link.destination));

                    if (availableLinks1.Count > 0)
                    {
                        Link minVisited = availableLinks1.Aggregate((l, r) =>
                            visited.GetValueOrDefault(l.source) < visited.GetValueOrDefault(r.source) ? l : r);

                        usedLinks.links.Add(minVisited);
                        if (!visited.ContainsKey(minVisited.destination))
                        {
                            visited.Add(minVisited.destination, 1);
                        }
                        else
                        {
                            visited[minVisited.destination] = visited.GetValueOrDefault(minVisited.destination) + 1;
                        }

                        usedLinks.links.Concat(FindPath(minVisited.destination, dest, links, devices, RL, visited, usedLinks, minSrc).Item1.links);

                    }

                }
            }
            return Tuple.Create(usedLinks, visited);
        }


        public Tuple<List<Route>, bool> FindAllPaths(string src, string dest, List<Link> links, List<Device> devices, List<Link> used, List<Route> allRoutes, bool validPath)
        {

            List<Link> availableLinks =
                links.FindAll(link => link.source == src && (link.destination == dest || devices.FindAll(d => d.type == "Switch").Exists(d => d.name == link.destination)));

            bool hasLinkToDest = links.Exists(link => link.source == src && link.destination == dest);
            if (hasLinkToDest)
            {
                Route r = new Route();
                r.links = new List<Link>();
                r.links.Add(availableLinks.FirstOrDefault(link => link.source == src && link.destination == dest));
                allRoutes.Add(r);
                return Tuple.Create(allRoutes, true);

            }
            else if (availableLinks.Count == 0)
            {
                return Tuple.Create(new List<Route>(), false);
            }
            else if (validPath)
            {
                foreach (Link link in availableLinks)
                {
                    used.Add(link);
                    (allRoutes, validPath) = FindAllPaths(link.destination, dest, links, devices, used, allRoutes, validPath);
                    return Tuple.Create(allRoutes, validPath);

                }
            }
            return Tuple.Create(allRoutes, validPath);

        }

        // A recursive function to print 
        // all paths from 'u' to 'd'. 
        // isVisited[] keeps track of 
        // vertices in current path. 
        // localPathList<> stores actual 
        // vertices in the current path 

 
        public bool compute(string src, string dest, List<Link> links, List<Device> devices, List<Link> used, List<Route> allRoutes, bool validPath)
        {
            List<string> deviceNames = new List<string>();
            List<Device> availDevices = devices.FindAll(d => d.type == "Switch" || d.name == src || d.name == dest);
            foreach (Device dn in availDevices) { deviceNames.Add(dn.name); }

            List<Link> availableLinks =
                links.FindAll(link => link.source == src || link.destination == dest || (deviceNames.Contains(link.source) && deviceNames.Contains(link.destination)));

            Graph g = new Graph(deviceNames);
            
            foreach (Link link in availableLinks)
            {
                g.addEdge(link.source, link.destination);

            }

            Console.WriteLine("Following are all different"
                              + " paths from " + src + " to " + dest);
            g.GetAllPaths(src, dest);
            return true;
        }
    }
}
public class Graph
{

    // Number of switches + src and dest in graph 
    private List<string> devices;

    // adjacency list 
    private Dictionary<string, List<string>> adjList;

    // Constructor 
    public Graph(List<string> devices)
    {

        // initialise vertex count 
        this.devices = devices;

        // initialise adjacency list 
        initAdjList();
    }

    // utility method to initialise 
    // adjacency list 
    private void initAdjList()
    {
        adjList = new Dictionary<string, List<string>>();

        foreach (string device in devices)
        {
            adjList.Add(device, new List<string>());
        }
    }

    // add edge from u to v 
    public void addEdge(string src, string dest)
    {
        // Add link to list. 
        adjList[src].Add(dest);
    }

    private void allPathsUtil(string src, string dest,
                                          Dictionary<string, bool> isVisited,
                                          List<string> localPathList)
    {

        if (src.Equals(dest))
        {
            Console.WriteLine(string.Join(" ", localPathList));
            // if match found then no need 
            // to traverse more till depth 
            return;
        }

        // Mark the current node 
        isVisited[src] = true;

        // Recur for all the vertices 
        // adjacent to current vertex 
        foreach (string i in adjList[src])
        {
            if (!isVisited[i])
            {
                // store current node 
                // in path[] 
                localPathList.Add(i);
                allPathsUtil(i, dest, isVisited,
                                  localPathList);

                // remove current node 
                // in path[] 
                localPathList.Remove(i);
            }
        }
        // Mark the current node 
        isVisited[src] = false;
    }
    public void GetAllPaths(string src, string dest)
    {
        Dictionary<string, bool> isVisited = new Dictionary<string, bool>();
        foreach (string device in devices) { isVisited.Add(device, false); }

        List<string> pathList = new List<string>();

        // add source to path[] 
        pathList.Add(src);

        // Call recursive utility 
        allPathsUtil(src, dest, isVisited, pathList);
    }
}

