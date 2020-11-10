using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSN.Based.Distributed.CPS.Models;

namespace TSN.Based.Distributed.CPS
{
    public class SimulatedAnnealing
    {
        public SimulatedAnnealing(int xml)
        {
            RandomState random = new RandomState();
            PathFinder path = new PathFinder();
            List<Link> links = new List<Link>();
            List<Device> devices = new List<Device>();
            List<Stream> streams = new List<Stream>();
            (devices, links, streams) = XmlReader.LoadXml();

            List<Solution> s_best = random.generateState(streams, links, devices);

            double temp = 5000000;
            double r = 0.003;
            int nonew = 0;

            CostFunction cf = new CostFunction();
            UpdateFunc uf = new UpdateFunc();
            double c_best = cf.CalcCostFunction(s_best);

            while (temp > 1)
            {
                List<Solution> s_new = uf.updateSolution(s_best, links, devices);



            }



        }
    }
}
