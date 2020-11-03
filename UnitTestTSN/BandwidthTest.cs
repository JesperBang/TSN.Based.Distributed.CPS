using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TSN.Based.Distributed.CPS;
using TSN.Based.Distributed.CPS.Models;

namespace UnitTestTSN
{
    [TestClass]
    public class BandwidthTest
    {
        static List<Route> routes;
        static List<Stream> streams;
        static readonly LinkUtil linkUtil = new LinkUtil();

        [ClassInitialize]
        public static void BeforeClass(TestContext tc)
        {
            routes = new List<Route>();
            streams = new List<Stream>();
        }

        [TestInitialize]
        public void BeforeTest()
        {
            routes.Clear();
            streams.Clear();
        }

        [TestCleanup]
        public void AfterTest() 
        {
            
        }

        [TestMethod]
        public void IsBandwidthExceeded_ReturnsFalse()
        {
            Stream stream = new Stream
            {
                streamId = "Stream0",
                source = "ES1",
                destination = "ES3",
                size = 100,
                period = 1000,
                deadline = 10000,
                rl = 1,
            };
            Link link1 = new Link
            {
                source = "ES1",
                destination = "SW0",
                speed = 1.25,
            };
            Link link2 = new Link
            {
                source = "SW0",
                destination = "ES3",
                speed = 1.25,
            };
            Route route = new Route
            {
                links = new List<Link> { link1, link2 },
                src = "ES1",
                dest = "ES3",
            };

            routes.Add(route);
            var result = linkUtil.IsBandwidthExceeded(stream, routes);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsBandwidthExceeded_ReturnsTrue()
        {
            Stream stream = new Stream
            {
                streamId = "Stream1",
                source = "ES2",
                destination = "ES4",
                size = 1000,
                period = 1000,
                deadline = 10000,
                rl = 2,
            };
            Link link1 = new Link
            {
                source = "ES2",
                destination = "SW1",
                speed = 1.25,
            };
            Link link2 = new Link
            {
                source = "SW1",
                destination = "ES4",
                speed = 1.25,
            };
            Route route1 = new Route
            {
                links = new List<Link> { link1, link2 },
                src = "ES2",
                dest = "ES4",
            };
            Link link3 = new Link
            {
                source = "ES2",
                destination = "SW1",
                speed = 1.25,
            };
            Link link4 = new Link
            {
                source = "SW1",
                destination = "SW0",
                speed = 1.25,
            };
            Link link5 = new Link
            {
                source = "SW0",
                destination = "ES4",
                speed = 1.25,
            };
            Route route2 = new Route
            {
                links = new List<Link> { link3, link4, link5 },
                src = "ES2",
                dest = "ES4",
            };
            routes.Add(route1);
            routes.Add(route2);
            var result = linkUtil.IsBandwidthExceeded(stream, routes);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsBandwidthExceeded_AllStreams_ReturnsTrue()
        {
            Stream stream1 = new Stream
            {
                streamId = "Stream1",
                source = "ES2",
                destination = "ES4",
                size = 100,
                period = 1000,
                deadline = 10000,
                rl = 2,
            };

            var result = linkUtil.IsBandwidthExceeded(stream1, routes);
            Assert.IsTrue(result);
        }
    }
}
