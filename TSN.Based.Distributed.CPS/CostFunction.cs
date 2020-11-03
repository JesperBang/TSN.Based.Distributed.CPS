﻿using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using TSN.Based.Distributed.CPS.Models;

namespace TSN.Based.Distributed.CPS
{
    public class CostFunction
    {
        public Stream CalcCostFunction(Stream input) 
        {
            LinkUtil lu = new LinkUtil();
            int BandTerm;
            int OverlapTerm;
            double LenTerm = 0;

            Dictionary<string, int> linkmap = new Dictionary<string, int>();

            // bandwidth ok?
            BandTerm = lu.isBandwidthExceeded(input, input.Route) ? 1 : 0;

            // Overlapping links
            foreach (Route route in input.Route)
            {
                foreach (Link link in route.links)
                {
                    if (linkmap.ContainsKey(link.source + link.destination)) linkmap[link.source + link.destination] += 1;
                    if (!linkmap.ContainsKey(link.source + link.destination)) linkmap.Add(link.source + link.destination, 1);    
                }
                LenTerm = route.links.Count;
            }

            OverlapTerm = linkmap.Where(links => links.Value > 1).Count();

            // Average length of routes
            LenTerm = LenTerm / input.Route.Count();

            input.Cost = CostCalc(BandTerm, OverlapTerm, LenTerm);
            return input;
        }

        public double CostCalc(double BandTerm, int OverlapTerm, double LenTerm)
        {
            return double.Parse(ConfigurationManager.AppSettings.Get("w1")) * BandTerm + 
                double.Parse(ConfigurationManager.AppSettings.Get("w2")) * OverlapTerm + 
                double.Parse(ConfigurationManager.AppSettings.Get("w3")) * LenTerm;
        }
    }
}