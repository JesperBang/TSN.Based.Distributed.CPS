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
            List<Route> routes = new List<Route>();
            Route r1 = new Route();
            r1.src = "ES1";
            r1.dest = "ES3";
            List<Link> l1 = new List<Link>();
            Link ln1 = new Link();
            ln1.source = "ES1";
            ln1.destination = "SW0";
            ln1.speed = 1.25;
            l1.Add(ln1);
            Link ln2 = new Link();
            ln2.source = "SW0";
            ln2.destination = "ES3";
            ln2.speed = 1.25;
            l1.Add(ln2);
            r1.links = l1;

            Route r2 = new Route();
            r2.src = "ES1";
            r2.dest = "ES3";
            List<Link> l2 = new List<Link>();
            Link ln3 = new Link();
            ln3.source = "ES1";
            ln3.destination = "SW1";
            ln3.speed = 1.25;
            l2.Add(ln3);
            Link ln4 = new Link();
            ln4.source = "SW1";
            ln4.destination = "ES3";
            ln4.speed = 1.25;
            l2.Add(ln4);
            r2.links = l2;


            Solution sol1 = new Solution();
            sol1.size = 100;
            sol1.StreamId = "Stream0";
            sol1.routes = routes;
            

            Console.Read();
            

        }
    }
}
