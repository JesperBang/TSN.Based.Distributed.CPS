using System;
using System.Collections.Generic;
using TSN.Based.Distributed.CPS.Models;

namespace TSN.Based.Distributed.CPS
{
    class PathFinder
    {
        public List<Route> FindAllPaths(string src, string dest, List<Link> links, List<Device> devices, List<Link> used, List<Route> allRoutes, bool validPath)
        {
            List<string> deviceNames = new List<string>();
            List<Device> availDevices = devices.FindAll(d => d.type == "Switch" || d.name == src || d.name == dest);
            foreach (Device dn in availDevices) { deviceNames.Add(dn.name); }

            List<Link> availableLinks =
                links.FindAll(link => link.source == src || link.destination == dest || (deviceNames.Contains(link.source) && deviceNames.Contains(link.destination)));

            Graph g = new Graph(deviceNames, links);

            foreach (Link link in availableLinks)
            {
                g.addEdge(link.source, link.destination);

            }

            Console.WriteLine("Following are all different"
                              + " paths from " + src + " to " + dest);
            List<Route> routes = g.GetAllPaths(src, dest);
            return routes;
        }
    }

    public class Graph
    {

        // Number of switches + src and dest in graph 
        private List<string> devices;
        private Dictionary<string, List<string>> adjList;
        private List<Route> routes;
        private List<Link> links;
        private int solutionNo;
        private string ogSrc;
        private string ogDest;

        // Constructor 
        public Graph(List<string> devices, List<Link> links)
        {

            // initialise vertex count 
            this.devices = devices;
            this.links = links;
            solutionNo = 0;
            routes = new List<Route>();

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

                routes.Add(new Route());
                routes[solutionNo].src = ogSrc;
                routes[solutionNo].dest = ogDest;
                routes[solutionNo].links = new List<Link>();

                for (int i = 0; i < localPathList.Count - 1; i++)
                {
                    routes[solutionNo].links.Add(links.Find(l => l.source == localPathList[i] && l.destination == localPathList[i + 1]));
                }
                solutionNo = solutionNo + 1;
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
        public List<Route> GetAllPaths(string src, string dest)
        {
            Dictionary<string, bool> isVisited = new Dictionary<string, bool>();
            foreach (string device in devices) { isVisited.Add(device, false); }

            List<string> pathList = new List<string>();
            // add source to path[] 
            pathList.Add(src);
            ogSrc = src;
            ogDest = dest;
            // Call recursive utility 
            allPathsUtil(src, dest, isVisited, pathList);

            return this.routes;
        }
    }
}
