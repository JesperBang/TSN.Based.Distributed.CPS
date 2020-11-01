using System;
using System.Collections.Generic;
using TSN.Based.Distributed.CPS.Models;

namespace TSN.Based.Distributed.CPS
{
    public class LinkUtil
    {       
        public LinkUtil()
        {
            
        }

        /*
         * (1.25 B/us = 10 Mbit/s).
         * Et link fra dets src til dst er eks. 10 Mbit/s = 10.000.000 bit/s. = 1.250.000 Bytes/s
         * Dvs. så meget kan der max gå gennem linket, det er bandwith.
         * En stream indeholder data på eks. size = 100 Bytes, som skal køre gennem alle links i sin route,
         * på en periode på eks. 10.000 us = 0,01 s. 
         * 
         * Den brugte bandwith på en stream er så size/period = 800 bit / 0,01 s = 80.000 bit/s = 0,08 Mbit/s
         * 
         * Man tjekker alle links i en route, hvis der findes flere links i forskellige routes der benytter den samme link, skal de ligges sammen ved 
         * beregningen af den brugte bandtwidth. Hvis den brugte bandwidth ikke overskrider speed eks. 10 Mbit/s, retuneres false.  
         */


        /// <summary>
        /// checks if the bandwidth of 
        /// links are exceeded
        /// and return true if it is.
        /// </summary>
        /// <param name="s">Stream</param>
        /// <param name="r">List of route objects</param>
        public bool isBandwidthExceeded(Stream s, List<Route> r)
        {
            Dictionary<string, Dictionary<double, double>> dict = new Dictionary<string, Dictionary<double, double>>();
            double used_bandwidth_mbits = ((s.size * 8)/ (1000000)) / (s.period/1000000);

            foreach (Route item in r)
            {
                foreach (Link l in item.links)
                {
                    double bandwidth_mbits = ((l.speed* 8 * 1000000) / (1000000));
                    string link_name = l.source + "_" + l.destination;
                    
                    if (dict.ContainsKey(link_name))
                    {
                        var old_val = dict[link_name][bandwidth_mbits];
                        Dictionary<double, double> temp = new Dictionary<double, double>(){ { bandwidth_mbits, old_val + used_bandwidth_mbits}};
                        dict[link_name] = temp;  
                    }
                    else
                    {
                        Dictionary<double, double> temp = new Dictionary<double, double>() { { bandwidth_mbits, used_bandwidth_mbits } };
                        dict[link_name] = temp;
                    }
                }            
            }
            

            foreach (var item in dict.Values)
            {
                foreach (var d in item)
                {
                    if (d.Value > d.Key)
                        return true;
                }
            }
            return false;

        }
    }




}
