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
             * En stream indeholder data på eks. size = 100 Bytes, som kører gennem alle links i sin route.
             * 
             * Dvs. 1.250.000 Bytes kan køre gennem et link, og hvis der findes 2 routes der kører gennem den samme
             * link, begge med en data size på 100 bytes, er der 1.250.000 Bytes - 200 Bytes = 1248950 bytes/s
             * tilrådighed på det givne link.
             * 
             */


            List<Link> links_test = new List<Link>();
            Route route_test = new Route();



        }
    }
}
