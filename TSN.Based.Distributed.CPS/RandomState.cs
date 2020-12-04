using System;
using System.Collections.Generic;
using TSN.Based.Distributed.CPS.Models;

namespace TSN.Based.Distributed.CPS
{
    class RandomState
    {

        public List<Solution> generateState(List<Stream> streams, List<Link> links, List<Device> devices)
        {
            PathFinder paths = new PathFinder();
            int num_stream = streams.Count;
            int num_links = links.Count;
            int num_devices = devices.Count;
            List<Solution> state = new List<Solution>();

            //create a List<Stream> random_stream
            //add routes to the streams
            for (int i = 0; i < num_stream; i++)
            {
                Solution i_state = new Solution
                {
                    StreamId = streams[i].streamId,
                    Route = new List<Route>(),
                    Cost = 0,
                    size = streams[i].size,
                    src = streams[i].source,
                    dest = streams[i].destination,
                    period = streams[i].period,
                    rl = streams[i].rl
                };

                    //Utillize the FindAllPath function to get all possible routes between to endsystems.
                    List<Route> allPaths = paths.FindAllPaths(streams[i].source, streams[i].destination, links, devices, new List<Link>(), i_state.Route, true);
                    
                    //Sort paths based on number of links 
                    allPaths.Sort((a,b) => a.links.Count - b.links.Count);

                    //Assign unique routes according to the rl value. If rl is bigger than the number of paths, the paths are used again. 
                    if (i_state.Route.Count < i_state.rl) { 
                        for (int r = 0; r < i_state.rl; r++)
                        {
                            i_state.Route.Add(allPaths[r % (allPaths.Count + 1)]);
                        }

                    }

                state.Add(i_state);
            }

            return state;

        }
    }
}