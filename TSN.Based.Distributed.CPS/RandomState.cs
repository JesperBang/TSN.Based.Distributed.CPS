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
                    period = streams[i].period
                };


                i_state.Route.Add(paths.FindPath(streams[i].source, streams[i].destination, links, devices, new List<string>(), new Route()));

                state.Add(i_state);
            }

            return state;

        }


    }
}