using System;
using System.Collections.Generic;
using TSN.Based.Distributed.CPS.Models;


namespace TSN.Based.Distributed.CPS
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Link> links = new List<Link>();
            List<Device> devices = new List<Device>();
            List<Stream> streams = new List<Stream>();

            (devices, links, streams) = XmlReader.LoadXml();


            Console.Read();




            //Duran tester lige her efter kommentar linjen.
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


            List<Link> links_test = new List<Link>();
            Route route_test = new Route();



        }
    }
}
