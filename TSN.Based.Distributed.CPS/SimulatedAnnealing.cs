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
            List<Models.Stream> streams;

            (devices, links, streams) = XmlReader.LoadXml(xml);

            List<Solution> s_best = random.generateState(streams, links, devices);

            double temp = 5000000;
            double r = 0.00002;
            int count = 0;

            double c_best = cf.CalcCostFunction(s_best, devices, links);

            while (temp > 1)
            {
                count++;

                List<Solution> s_new = new UpdateFunc().updateSolution(s_best, links, devices);

                double s_new_cost = cf.CalcCostFunction(s_new, devices, links);

                double acceptance = Acceptance.Acceptance_Function(c_best, s_new_cost, temp);

                if (acceptance.Equals(1.0))
                {
                    s_best = s_new;
                    c_best = s_new_cost;
                }
                else
                {
                    if (new Random().Next(100) < acceptance * 100)
                    {
                        s_best = s_new;
                        c_best = s_new_cost;
                    }
                }

                temp = temp * (1 - r);
            }
            XMLWriter.To_XML(s_best, c_best, xml);
        }
    }
}