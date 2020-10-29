using System;
using System.Collections.Generic;
using TSN.Based.Distributed.CPS.Models;

namespace TSN.Based.Distributed.CPS
{
    public class DuranTest
    {

        Link link1, link2, link3, link4, link5, link6, link7, link8;
        List<Stream> streams;
        Stream stream0, stream1;
        Route route1, route2, route3;
        

        public DuranTest()
        {
            List<Link> links = new List<Link>();
            link1 = new Link();
            link2 = new Link();
            link3 = new Link();
            link4 = new Link();
            link5 = new Link();
            link6 = new Link();
            link7 = new Link();
            link8 = new Link();
            List<Stream> streams = new List<Stream>();
            stream0 = new Stream();
            stream1 = new Stream();
            streams.Add(stream0);
            streams.Add(stream1);
            route1 = new Route();
            route2 = new Route();
            route3 = new Route();

            link1.source = "ES1";
            link1.destination = "SW0";
            link1.speed = 1.25;

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

            links.Add(link1);
            links.Add(link2);
            links.Add(link3);
            links.Add(link4);
            links.Add(link5);
            links.Add(link6);
            links.Add(link7);
            links.Add(link8);


            route1.links.Add(link1);
            route1.links.Add(link5);
            route1.src = "ES1";
            route1.dest = "ES3";

            route2.links.Add(link4);
            route2.links.Add(link8);
            route2.src = "ES2";
            route2.dest = "ES4";

            route3.links.Add(link2);
            route3.links.Add(link6);
            route3.src = "ES2";
            route3.dest = "ES4";

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

            

            
        }


       
            /*
             * (1.25 B/us = 10 Mbit/s).
             * Et link fra dets src til dst er eks. 10 Mbit/s = 10.000.000 bit/s. = 1.250.000 Bytes/s
             * Dvs. så meget kan der max gå gennem linket, det er bandwith.
             * En stream indeholder data på eks. size = 100 Bytes, som skal køre gennem alle links i sin route,
             * på en deadline på eks. 10.000 us = 0,01 s. 
             * 
             * Den brugte bandwith på en stream er så size/deadline = 800 bit / 0,01 s = 80.000 bit/s = 0,08 Mbit/s
             * 
             * Man tjekker alle streams' dst eks. ES4, hvis to streams har samme dst skal de ligges sammen ved 
             * beregningen af brugt bandtwidth. Hvis det ikke overskrider speed eks. 10 Mbit/s er det en feasible 
             * stream.
             * 
             * 
             */


            public void start()
        {
            float used_bandwith = stream0.size / stream0.deadline;
            Dictionary<string, float> dict;
            foreach (Stream item in streams)
            {
                
            }
        }
            
            






    }
}
