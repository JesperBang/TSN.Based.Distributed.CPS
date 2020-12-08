using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

            // auto
            Dictionary<string, double> master = new Dictionary<string, double>();
            master.Add("small", 0.002);
            master.Add("medium", 0.0003);
            master.Add("large", 0.00015);

            int runs = 10;

            // Create CSV
            var csv = new StringBuilder();
            var header = "runs,temp,ratio,cost,time";

            csv.AppendLine(header);

            foreach(var size in master)
            {
                for (int i = 0; i < runs; i++)
                {
                    double temp = 5000000;
                    double stemp = temp;

                    double r = size.Value;
                    int count = 0;

                    DateTime startTime, endTime;
                    startTime = DateTime.Now;

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

                    endTime = DateTime.Now;
                    Double elapsedMillisecs = ((TimeSpan)(endTime - startTime)).TotalSeconds;

                    csvmodel csvmodel = new csvmodel
                    {
                        cost = c_best.ToString().Replace(",", "."),
                        ratio = r.ToString().Replace(",", "."),
                        runs = count,
                        temp = (int)stemp,
                        time = elapsedMillisecs.ToString().Replace(",", ".")
                    };

                    var newLine = string.Join(
                        ",",
                        typeof(csvmodel).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select
                        (
                            prop => prop.GetValue(csvmodel)
                        )
                    );

                    csv.AppendLine(newLine);

                }
            }
            // Save CSV to path
            File.WriteAllText(@"results.csv", csv.ToString());
            // auto







            //double temp = 5000000;
            //double r = 0.00015;
            //int count = 0;

            //double c_best = cf.CalcCostFunction(s_best);

            //while (temp > 1)
            //{
            //    count++;

            //    List<Solution> s_new = new UpdateFunc().updateSolution(s_best, links, devices);

            //    double s_new_cost = cf.CalcCostFunction(s_new);
            //    double acceptance = Acceptance.Acceptance_Function(c_best, s_new_cost, temp);

            //    if (acceptance.Equals(1.0))
            //    {
            //        s_best = s_new;
            //        c_best = s_new_cost;
            //    }
            //    else
            //    {
            //        if (new Random().Next(100) < acceptance * 100)
            //        {
            //            s_best = s_new;
            //            c_best = s_new_cost;
            //        }
            //    }

            //    temp = temp * (1 - r);
            //}
            //Console.WriteLine(count);
            //XMLWriter.To_XML(s_best, c_best, xml);
        }
    }
}
