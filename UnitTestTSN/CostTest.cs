using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TSN.Based.Distributed.CPS;
using TSN.Based.Distributed.CPS.Models;

namespace UnitTestTSN
{
    [TestClass]
    public class CostTest
    {
        static Stream stream;
        static readonly CostFunction cf = new CostFunction();

        [ClassInitialize]
        public static void BeforeClass(TestContext tc)
        {
            stream = new Stream();
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



        }


    }
}
