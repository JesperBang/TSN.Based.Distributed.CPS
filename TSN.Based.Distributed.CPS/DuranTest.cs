using System;
using System.Collections.Generic;
using TSN.Based.Distributed.CPS.Models;

namespace TSN.Based.Distributed.CPS
{
    public class DuranTest
    {

        Link link1, link2, link3, link4, link5, link6, link7, link8;
        List<Stream> streams;
        List<Route> routes;
        Stream stream0, stream1;
        Route route1, route2, route3;
        
        public DuranTest()
        {
            streams = new List<Stream>();
            link1 = new Link();
            link2 = new Link();
            link3 = new Link();
            link4 = new Link();
            link5 = new Link();
            link6 = new Link();
            link7 = new Link();
            link8 = new Link();

            stream0 = new Stream();
            stream1 = new Stream();
            streams.Add(stream0);
            streams.Add(stream1);
            route1 = new Route();
            route2 = new Route();
            route3 = new Route();

            link1.source = "ES1";
            link1.destination = "SW0";
            link1.speed = 0.25;

            link2.source = "ES2";
            link2.destination = "SW0";
            link2.speed = 1.25;

            link3.source = "ES1";
            link3.destination = "SW1";
            link3.speed = 1.25;

            link4.source = "ES2";
            link4.destination = "SW1";
            link4.speed = 1.25;

            link5.source = "SW0";
            link5.destination = "ES3";
            link5.speed = 1.25;

            link6.source = "SW0";
            link6.destination = "ES4";
            link6.speed = 1.25;

            link7.source = "SW1";
            link7.destination = "ES3";
            link7.speed = 1.25;

            link8.source = "SW1";
            link8.destination = "ES4";
            link8.speed = 1.25;

            route1.links = new List<Link>();
            route1.links.Add(link1);
            route1.links.Add(link5);
            route1.src = "ES1";
            route1.dest = "ES3";

            route2.links = new List<Link>();
            route2.links.Add(link4);
            route2.links.Add(link8);
            route2.src = "ES2";
            route2.dest = "ES4";

            route3.links = new List<Link>();
            route3.links.Add(link2);
            route3.links.Add(link6);
            route3.src = "ES2";
            route3.dest = "ES4";

            routes = new List<Route>();
            routes.Add(route1);
            routes.Add(route2);
            routes.Add(route3);

            stream0.streamId = "Stream0";
            stream0.source = "ES1";
            stream0.destination = "ES3";
            stream0.size = 100;
            stream0.period = 1000;
            stream0.deadline = 10000;
            stream0.rl = 1;

            stream1.streamId = "Stream1";
            stream1.source = "ES2";
            stream1.destination = "ES4";
            stream1.size = 100;
            stream1.period = 1000;
            stream1.deadline = 10000;
            stream1.rl = 2;



            var test = isBandwidthExceeded(stream0, routes);

            Console.WriteLine("returned " + test);
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
