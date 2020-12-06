using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using TSN.Based.Distributed.CPS.Models;

namespace TSN.Based.Distributed.CPS
{
    public class CostFunction
    {
        /// <summary>
        /// Calculates cost of List<Solution>
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Cost as a double</returns>
        public double CalcCostFunction(List<Solution> input) 
        {
            LinkUtil lu = new LinkUtil();
            double totalCost = 0;

            foreach (Solution sol in input)
            {
                double LenTerm = 0;
                Dictionary<string, int> linkmap = new Dictionary<string, int>();

                // Use LinkUtil to test bandwidth
                int BandTerm = new LinkUtil().IsBandwidthExceeded(sol) ? 1 : 0;
                int ScheduTerm = new LinkUtil().IsScheduable(stream(sol)) ? 1 : 0;

                // Overlapping links
                foreach (Route route in sol.Route)
                {
                    foreach (Link link in route.links)
                    {
                        if (linkmap.ContainsKey(link.source + link.destination)) linkmap[link.source + link.destination] += 1;
                        if (!linkmap.ContainsKey(link.source + link.destination)) linkmap.Add(link.source + link.destination, 1);
                    }
                    LenTerm += route.links.Count;
                }

                int OverlapTerm = linkmap.Where(links => links.Value > 1).Count();

                // Average length of routes
                LenTerm = LenTerm / sol.Route.Count();

                // Total cost calc
                totalCost += CostCalc(BandTerm, OverlapTerm, LenTerm, ScheduTerm);
                sol.Cost = CostCalc(BandTerm, OverlapTerm, LenTerm, ScheduTerm);
            }

            return totalCost;
        }

        /// <summary>
        /// Takes term inputs and return cost based on variables in App.config
        /// </summary>
        /// <param name="BandTerm"></param>
        /// <param name="OverlapTerm"></param>
        /// <param name="LenTerm"></param>
        /// <returns>Returns cost for item as double</returns>
        public double CostCalc(double BandTerm, int OverlapTerm, double LenTerm, double ScheduTerm)
        {
            return double.Parse(ConfigurationManager.AppSettings.Get("w1")) * BandTerm + 
                double.Parse(ConfigurationManager.AppSettings.Get("w2")) * OverlapTerm + 
                double.Parse(ConfigurationManager.AppSettings.Get("w3")) * LenTerm +
                double.Parse(ConfigurationManager.AppSettings.Get("w4")) * ScheduTerm;
        }


        /// <summary>
        /// Converter from solution to stream - used to satisfy LinkUtil input
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Stream stream(Solution s)
        {
            Stream st = new Stream();
            st.Cost = s.Cost;
            st.deadline = s.deadline;
            st.destination = s.destination;
            st.period = s.period;
            st.rl = s.rl;
            st.Route = s.Route;
            st.size = s.size;
            st.source = s.source;
            st.streamId = s.StreamId;
            return st;
        }
    }
}
