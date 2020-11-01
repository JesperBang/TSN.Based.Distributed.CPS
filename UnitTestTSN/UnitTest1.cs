using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TSN.Based.Distributed.CPS;
using TSN.Based.Distributed.CPS.Models;

namespace UnitTestTSN
{
    [TestClass]
    public class UnitTest1
    {
        static Link link1, link2, link3, link4, link5, link6, link7, link8;
        static List<Route> routes;
        static Stream stream0, stream1;
        static Route route1, route2, route3;
        static LinkUtil linkUtil = new LinkUtil();

        [ClassInitialize]
        public static void BeforeClass(TestContext tc)
        {
            link1 = new Link();
            link2 = new Link();
            link3 = new Link();
            link4 = new Link();
            link5 = new Link();
            link6 = new Link();
            link7 = new Link();
            link8 = new Link();

            stream0 = new Stream();
            stream0.streamId = "Stream0";
            stream0.source = "ES1";
            stream0.destination = "ES3";
            stream0.size = 100;
            stream0.period = 1000;
            stream0.deadline = 10000;
            stream0.rl = 1;

            stream1 = new Stream();
            stream1.streamId = "Stream1";
            stream1.source = "ES2";
            stream1.destination = "ES4";
            stream1.size = 100;
            stream1.period = 1000;
            stream1.deadline = 10000;
            stream1.rl = 2;





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


            route1 = new Route();
            route1.links = new List<Link>();
            route1.links.Add(link1);
            route1.links.Add(link5);
            route1.src = "ES1";
            route1.dest = "ES3";

            route2 = new Route();
            route2.links = new List<Link>();
            route2.links.Add(link4);
            route2.links.Add(link8);
            route2.src = "ES2";
            route2.dest = "ES4";

            route3 = new Route();
            route3.links = new List<Link>();
            route3.links.Add(link2);
            route3.links.Add(link6);
            route3.src = "ES2";
            route3.dest = "ES4";

            routes = new List<Route>();
            routes.Add(route1);
            routes.Add(route2);
            routes.Add(route3);



            
        }

        [TestInitialize]
        public void BeforeTest()
        {
            
        }

        [TestCleanup]
        public void AfterTest() 
        {
            
        }


        [TestMethod]
        public void isBandwithExceeded_ReturnsFalse()
        {
            var result = linkUtil.isBandwidthExceeded(stream1, routes);
            Assert.IsFalse(result);
        }
    }
}
