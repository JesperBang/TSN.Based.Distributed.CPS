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

        //public Dictionary<string, string> FindPath2(string src, string dest, List<Link> links, List<Device> devices)
        //{
        //    bool destReached = false;
        //    Dictionary<string, int> dist = new Dictionary<string, int >();
        //    Dictionary<string, string> prev = new Dictionary<string, string>();



        //    Queue<string> remaining = new Queue<string>();

        //    Dictionary<string, List<string>> nodes = ConverToAdjacencyMatrix(src, dest, links, devices);

        //    foreach (var node in nodes.Keys)
        //    {
        //        //We assume the lenght between all node is equal
        //        dist.Add(node, 1);
        //        remaining.Enqueue(node);
        //    }

        //    dist.Add(src, 0);

        //    while (remaining.Count > 0 || destReached)
        //    {
        //        string n = remaining.Dequeue();
        //        foreach (var neighbour in nodes.First(k => k.Key == n).Value)
        //        {
        //            int new_pathLength = dist.First(k => k.Key == n).Value + 1;
        //            int old_pathLength = dist.First(k => k.Key == n).Value;

        //            dist.Add(neighbour, new_pathLength);
        //            prev.Add(neighbour, n);
        //        }
        //    }

        //    return prev;
        //}


        //private Dictionary<string, List<string>> ConverToAdjacencyMatrix(string src, string dest, List<Link> links, List<Device> devices)
        //{
        //    List<Device> nodes = devices.FindAll(d => d.type == "Switch");
        //    Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();

        //    foreach (var node in nodes)
        //    {
        //        List<Link> nodeLinks = links.FindAll(n => n.source == node.name || n.source == src);
        //        List<Device> availDevice = devices.FindAll(d => nodeLinks.Exists(nl => nl.destination == d.name));

        //        graph[node.name] = availDevice.Select(d => d.name).ToList();

        //    }

        //    return graph;

        //}

        //private void BFS(List<string>[] adj, string src, string[] dist, string[] paths, int n)
        //{
        //    bool[] visited = new bool[n];
        //    for (int i = 0; i < n; i++)
        //        visited[i] = false;
        //    dist.First = 0;
        //    paths[src] = 1;

        //    List<int> q = new List<int>();
        //    q.Add(src);
        //    visited[src] = true;
        //    while (q.Count != 0)
        //    {
        //        int curr = q[0];
        //        q.RemoveAt(0);

        //        // For all neighbors of current vertex do: 
        //        foreach (int x in adj[curr])
        //        {

        //            // if the current vertex is not yet 
        //            // visited, then push it to the queue. 
        //            if (visited[x] == false)
        //            {
        //                q.Add(x);
        //                visited[x] = true;
        //            }

        //            // check if there is a better path. 
        //            if (dist[x] > dist[curr] + 1)
        //            {
        //                dist[x] = dist[curr] + 1;
        //                paths[x] = paths[curr];
        //            }

        //            // additional shortest paths found 
        //            else if (dist[x] == dist[curr] + 1)
        //                paths[x] += paths[curr];
        //        }
        //    }
        //}

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
    }

}
