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
                Dictionary<string, int> v = new Dictionary<string, int>();
                for (int j = 0; j < i_state.rl; j++)
                    {
                    if (i_state.Route.Count < i_state.rl) { 
                        for (int r = 0; r < i_state.rl; r++)
                        {
                            i_state.Route.Add(new Route());
                        }
                    }
                    bool test = true;
                    //(i_state.Route[j], v) = (paths.FindPath(streams[i].source, streams[i].destination, links, devices, j, v, i_state.Route[j], 1));
                    (i_state.Route, test) = (paths.FindAllPaths(streams[i].source, streams[i].destination, links, devices, new List<Link>(), i_state.Route, true));

                }

                state.Add(i_state);
            }

            return state;

        }
    }
}