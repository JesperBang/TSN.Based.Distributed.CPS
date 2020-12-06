using System;
using System.Collections.Generic;
using TSN.Based.Distributed.CPS.Models;

namespace TSN.Based.Distributed.CPS
{
    public class SimulatedAnnealing
    {
        public SimulatedAnnealing(int xml)
        {
            RandomState random = new RandomState();
            CostFunction cf = new CostFunction();

            List<Link> links;
            List<Device> devices;
            List<Stream> streams;
            (devices, links, streams) = XmlReader.LoadXml(xml);

            List<Solution> s_best = random.generateState(streams, links, devices);

            double temp = 5000000;
            double r = 0.003;
            int nonew = 0;

            double c_best = cf.CalcCostFunction(s_best);

            while (temp > 1)
            {
                List<Solution> s_new;
                if (nonew > 100) { s_new = random.generateState(streams, links, devices); }
                else { s_new = new UpdateFunc().updateSolution(s_best, links, devices); }

                var onelin = new CoveredLinks().numberOfOneLinksCovered(s_new, devices, links);
                var twolin = new CoveredLinks().numberOfTwoLinksCovered(s_new, devices, links);

                double s_new_cost = cf.CalcCostFunction(s_new);
                double acceptance = Acceptance.Acceptance_Function(c_best, s_new_cost, temp);

                if (acceptance.Equals(1.0))
                {
                    s_best = s_new;
                    c_best = s_new_cost;
                    nonew = 0;
                }
                else
                {
                    if (new Random().Next(100) < acceptance * 100)
                    {
                        s_best = s_new;
                        c_best = s_new_cost;
                        nonew = 0;
                    }
                    else
                    {
                        nonew += 1;
                    }
                }

                temp = temp * (1 - r);
            }
            XMLWriter.To_XML(s_best, c_best, xml);
        }
    }
}
