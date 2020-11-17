using System.Collections.Generic;
using System.Linq;
using TSN.Based.Distributed.CPS.Models;

namespace TSN.Based.Distributed.CPS
{
    public class LinkUtil
    {       
        public LinkUtil()
        {
            
        }

        /// <summary>
        /// Checks if stream is scheduable according
        /// to cyclic queuing and forwarding.
        /// Used formulas:
        /// WCD = (h + 1) * C
        /// C = size / link_speed
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="link">Link</param>
        /// <param name="hops">Hops</param>
        /// <returns></returns>
        public bool IsScheduable(Stream stream)
        {
            Dictionary<string, double> dict = new Dictionary<string, double>();
            double size_bit = stream.size * 8;
            double deadline_s = stream.deadline / 1000000;
            double period_s = stream.period / 1000000;
            double smallest_period = period_s;
            double cycle_time_max = 0.0;

            //build up cycle_time and and save the smallest_period of streams
            foreach (Route route in stream.Route)
            {
                if (route.src == stream.source && route.dest == stream.destination)
                {
                    foreach (Link link in route.links)
                    {
                        string link_name = link.source + "_" + link.destination;
                        double linkSpeed_bit_per_s = link.speed * 8000000;
                        double cycle_time = size_bit / linkSpeed_bit_per_s;

                        if (dict.ContainsKey(link_name))
                        {
                            dict[link_name] += cycle_time;
                        }
                        else
                        {
                            dict[link_name] = cycle_time;
                        }
                    }
                }
            }
            cycle_time_max = dict.Values.Max();

            /*check each stream if their wcd is greater than deadline.
             * if so, return false.
             */
            foreach (Route route in stream.Route)
            {
                if (route.src == stream.source && route.dest == stream.destination)
                {
                    int hops = FindHops(route);
                    double wcd = (hops + 1) * cycle_time_max;
                    if (wcd > deadline_s)
                        return false;
                }
            }

            /*also check if cycle time is greater
             * than the smallest period among all streams.
             * if so, return false.
             */
            if (cycle_time_max <= smallest_period)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Checks if stream is scheduable according
        /// to cyclic queuing and forwarding.
        /// Used formulas:
        /// WCD = (h + 1) * C
        /// C = size / link_speed
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="link">Link</param>
        /// <param name="hops">Hops</param>
        /// <returns></returns>
        public bool IsScheduable(List<Stream> streams)
        {
            Dictionary<string, double> dict = new Dictionary<string, double>();
            double smallest_period = 0.0;
            double cycle_time_max = 0.0;

            //build up cycle_time and and save the smallest_period of streams
            foreach (Stream stream in streams)
            {
                double size_bit = stream.size * 8;
                double deadline_s = stream.deadline / 1000000;
                double period_s = stream.period / 1000000;

                if (smallest_period == 0.0)
                    smallest_period = period_s;
                if (smallest_period > period_s)
                    smallest_period = period_s;


                foreach (Route route in stream.Route)
                {
                    if (route.src == stream.source && route.dest == stream.destination)
                    {
                        foreach (Link link in route.links)
                        {
                            string link_name = link.source + "_" + link.destination;
                            double linkSpeed_bit_per_s = link.speed * 8000000;
                            double cycle_time = size_bit / linkSpeed_bit_per_s;

                            if (dict.ContainsKey(link_name))
                            {
                                dict[link_name] += cycle_time;
                            }
                            else
                            {
                                dict[link_name] = cycle_time;
                            }
                        }
                    }
                }        
            }
            cycle_time_max = dict.Values.Max();

            /*check each stream if their wcd is greater than deadline.
             * if so, return false.
             */
            foreach (Stream stream in streams)
            {
                double deadline_s = stream.deadline / 1000000;

                foreach (Route route in stream.Route)
                {
                    if (route.src == stream.source && route.dest == stream.destination)
                    {
                        int hops = FindHops(route);
                        double wcd = (hops + 1) * cycle_time_max;
                        if (wcd > deadline_s)
                            return false;
                    }
                }
            }

            /*also check if cycle time is greater
             * than the smallest period among all streams.
             * if so, return false.
             */
            if (cycle_time_max <= smallest_period)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Find amount of hops 
        /// given a route.
        /// </summary>
        /// <param name="route">Route</param>
        /// <returns>hop counts</returns>
        public int FindHops(Route route)
        {
            return route.links.Count;
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
        /// Checks if the bandwidth of 
        /// links are exceeded
        /// and return true if it is.
        /// This method takes a Stream
        /// and a list of routes.
        /// </summary>
        /// <param name="s">Stream</param>
        /// <param name="r">List of route objects</param>
        public bool IsBandwidthExceeded(Stream s, List<Route> r)
        {
            Dictionary<string, Dictionary<double, double>> dict = new Dictionary<string, Dictionary<double, double>>();
            double used_bandwidth_mbits = ((s.size * 8)/ (1000000)) / (s.period/1000000);

            foreach (Route item in r)
            {
                foreach (Link l in item.links)
                {
                    double bandwidth_mbits = l.speed* 8;
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

        /// <summary>
        /// Checks if the bandwidth of 
        /// links are exceeded
        /// and return true if it is.
        /// This method takes a List of Streams
        /// and a list of lists of routes, since
        /// each list of route belongs to a Stream.
        /// </summary>
        /// <param name="streams">List of Stream</param>
        /// <param name="routes">List of lists of routes</param>
        /// <returns></returns>
        public bool IsBandwidthExceeded(List<Stream> streams, List<List<Route>> routes)
        {
            Dictionary<string, Dictionary<double, double>> dict = new Dictionary<string, Dictionary<double, double>>();

            foreach (Stream s in streams)
            {
                double used_bandwidth_mbits = ((s.size * 8) / (1000000)) / (s.period / 1000000);

                foreach (List<Route> items in routes)
                {
                    foreach (Route item in items)
                    {
                        if (item.src == s.source && item.dest == s.destination)
                        {
                            foreach (Link l in item.links)
                            {
                                double bandwidth_mbits = l.speed * 8;
                                string link_name = l.source + "_" + l.destination;

                                if (dict.ContainsKey(link_name))
                                {
                                    var old_val = dict[link_name][bandwidth_mbits];
                                    Dictionary<double, double> temp = new Dictionary<double, double>() { { bandwidth_mbits, old_val + used_bandwidth_mbits } };
                                    dict[link_name] = temp;
                                }
                                else
                                {
                                    Dictionary<double, double> temp = new Dictionary<double, double>() { { bandwidth_mbits, used_bandwidth_mbits } };
                                    dict[link_name] = temp;
                                }
                            }
                        }
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

        public bool IsBandwidthExceeded(Solution s, List<Route> r)
        {
            Dictionary<string, Dictionary<double, double>> dict = new Dictionary<string, Dictionary<double, double>>();
            double used_bandwidth_mbits = ((s.size * 8) / (1000000)) / (s.period / 1000000);

            foreach (Route item in r)
            {
                foreach (Link l in item.links)
                {
                    double bandwidth_mbits = l.speed * 8;
                    string link_name = l.source + "_" + l.destination;

                    if (dict.ContainsKey(link_name))
                    {
                        var old_val = dict[link_name][bandwidth_mbits];
                        Dictionary<double, double> temp = new Dictionary<double, double>() { { bandwidth_mbits, old_val + used_bandwidth_mbits } };
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