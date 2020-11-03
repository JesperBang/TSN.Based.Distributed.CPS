using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TSN.Based.Distributed.CPS;
using TSN.Based.Distributed.CPS.Models;

namespace UnitTestTSN
{
    [TestClass]
    public class BandwidthTest
    {
        static Link link1, link2, link3, link4, link5, link6, link7, link8;
        static List<Route> routes;
        static Stream stream0, stream1, stream2;
        static Route route1, route2, route3;
        static readonly LinkUtil linkUtil = new LinkUtil();

        [ClassInitialize]
        public static void BeforeClass()
        {
            stream0 = new Stream
            {
                streamId = "Stream0",
                source = "ES1",
                destination = "ES3",
                size = 100,
                period = 1000,
                deadline = 10000,
                rl = 1,
            };

            stream1 = new Stream {
                streamId = "Stream1",
                source = "ES2",
                destination = "ES4",
                size = 100,
                period = 1000,
                deadline = 10000,
                rl = 2,
            };
            

            stream2 = new Stream {
                streamId = "Stream1",
                source = "ES2",
                destination = "ES4",
                size = 10000,
                period = 1000,
                deadline = 10000,
                rl = 2,
            };


            link1 = new Link {
            source = "ES1",
            destination = "SW0",
            speed = 1.25,
        };

            link2 = new Link {
                source = "ES2",
                destination = "SW0",
                speed = 1.25,
        };
            

            link3 = new Link {
                source = "ES1",
                destination = "SW1",
                speed = 1.25,
        };
            
            link4 = new Link {
                source = "ES2",
                destination = "SW1",
                speed = 1.25,
        };
            

            link5 = new Link {
                source = "SW0",
                destination = "ES3",
                speed = 1.25,
        };
            

            link6 = new Link {
                source = "SW0",
                destination = "ES4",
                speed = 1.25,
        };
            

            link7 = new Link {
                source = "SW1",
                destination = "ES3",
                speed = 1.25,
        };
        
            link8 = new Link {
                source = "SW1",
                destination = "ES4",
                speed = 1.25,
        };
            

            route1 = new Route {
                links = new List<Link> { link1, link5 },
                src = "ES1",
                dest = "ES3",
        };
            

            route2 = new Route {
                links = new List<Link> { link4, link8 },
                src = "ES2",
                dest = "ES4",
        };
            

            route3 = new Route {
                links = new List<Link> { link2, link6 },
                src = "ES2",
                dest = "ES4",
        };
            

            routes = new List<Route>();      
        }

        [TestInitialize]
        public void BeforeTest()
        {
            routes.Clear();
        }

        [TestCleanup]
        public void AfterTest() 
        {
            
        }


        [TestMethod]
        public void IsBandwidthExceeded_ReturnsFalse()
        {
            routes.Add(route1);
            var result = linkUtil.IsBandwidthExceeded(stream0, routes);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsBandwidthExceeded_ReturnsTrue()
        {
            routes.Add(route2);
            routes.Add(route3);
            var result = linkUtil.IsBandwidthExceeded(stream2, routes);
            Assert.IsTrue(result);
        }
    }
}
