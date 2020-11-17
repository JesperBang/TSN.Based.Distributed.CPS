using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TSN.Based.Distributed.CPS;
using TSN.Based.Distributed.CPS.Models;

namespace UnitTestTSN
{
    [TestClass]
    public class BandwidthTest
    {
        static readonly LinkUtil linkUtil = new LinkUtil();

        [ClassInitialize]
        public static void BeforeClass(TestContext tc)
        {
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
                Route = new List<Route>
                {
                    new Route
                    {
                        links = new List<Link> {
                            new Link
                            {
                                source = "ES1",
                                destination = "SW0",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW0",
                                destination = "ES3",
                                speed = 1.25,
                            }
                        },
                        src = "ES1",
                        dest = "ES3",
                    }
                }
            };

            var result = linkUtil.IsBandwidthExceeded(stream);
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
                size = 100000,
                period = 1000,
                deadline = 10000,
                rl = 2,
                Route = new List<Route>
                {
                    new Route
                    {
                        links = new List<Link> {
                            new Link
                            {
                                source = "ES1",
                                destination = "SW0",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW0",
                                destination = "ES3",
                                speed = 1.25,
                            }
                        },
                        src = "ES1",
                        dest = "ES3",
                    }
                }
            };
            var result = linkUtil.IsBandwidthExceeded(stream);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsBandwidthExceeded_AllStreams_ReturnsFalse()
        {
            List<Stream> streams = new List<Stream> {
                new Stream
                {
                streamId = "Stream0",
                source = "ES1",
                destination = "ES3",
                size = 100,
                period = 1000,
                deadline = 10000,
                rl = 1,
                Route = new List<Route>
                {
                    new Route
                    {
                        links = new List<Link> {
                            new Link
                            {
                                source = "ES1",
                                destination = "SW0",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW0",
                                destination = "ES3",
                                speed = 1.25,
                            }
                        },
                        src = "ES1",
                        dest = "ES3",
                    },
                    new Route
                    {
                        links = new List<Link> {
                            new Link
                            {
                                source = "ES2",
                                destination = "SW1",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW1",
                                destination = "ES4",
                                speed = 1.25,
                            }
                        },
                        src = "ES2",
                        dest = "ES4",
                    }
                }},
                new Stream
                {
                streamId = "Stream1",
                source = "ES2",
                destination = "ES4",
                size = 100,
                period = 1000,
                deadline = 10000,
                rl = 2,
                Route = new List<Route>
                {
                    new Route
                    {
                        links = new List<Link> {
                            new Link
                            {
                                source = "ES1",
                                destination = "SW0",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW0",
                                destination = "ES3",
                                speed = 1.25,
                            }
                        },
                        src = "ES1",
                        dest = "ES3",
                    },
                    new Route
                    {
                        links = new List<Link> {
                            new Link
                            {
                                source = "ES2",
                                destination = "SW1",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW1",
                                destination = "ES4",
                                speed = 1.25,
                            }
                        },
                        src = "ES2",
                        dest = "ES4",
                    }
                }
            }
        };
            var result = linkUtil.IsBandwidthExceeded(streams);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsBandwidthExceeded_AllStreams_ReturnsTrue()
        {
            List<Stream> streams = new List<Stream> {
                new Stream
                {
                streamId = "Stream0",
                source = "ES1",
                destination = "ES3",
                size = 10000,
                period = 1000,
                deadline = 10000,
                rl = 1,
                Route = new List<Route>
                {
                    new Route
                    {
                        links = new List<Link> {
                            new Link
                            {
                                source = "ES1",
                                destination = "SW0",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW0",
                                destination = "ES3",
                                speed = 1.25,
                            }
                        },
                        src = "ES1",
                        dest = "ES3",
                    },
                    new Route
                    {
                        links = new List<Link> {
                            new Link
                            {
                                source = "ES2",
                                destination = "SW1",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW1",
                                destination = "ES4",
                                speed = 1.25,
                            }
                        },
                        src = "ES2",
                        dest = "ES4",
                    }
                },
                },
                new Stream
                {
                streamId = "Stream1",
                source = "ES2",
                destination = "ES4",
                size = 1000,
                period = 1000,
                deadline = 10000,
                rl = 2,
                Route = new List<Route>
                {
                    new Route
                    {
                        links = new List<Link> {
                            new Link
                            {
                                source = "ES1",
                                destination = "SW0",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW0",
                                destination = "ES3",
                                speed = 1.25,
                            }
                        },
                        src = "ES1",
                        dest = "ES3",
                    },
                    new Route
                    {
                        links = new List<Link> {
                            new Link
                            {
                                source = "ES2",
                                destination = "SW1",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW1",
                                destination = "ES4",
                                speed = 1.25,
                            }
                        },
                        src = "ES2",
                        dest = "ES4",
                    }
                }
                }
        };
            var result = linkUtil.IsBandwidthExceeded(streams);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FindHops_ReturnsTrue()
        {
            Route route = new Route
            {
                links = new List<Link> {
                            new Link
                            {
                                source = "ES2",
                                destination = "SW1",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW1",
                                destination = "ES4",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "ES4",
                                destination = "SW2",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW2",
                                destination = "ES7",
                                speed = 1.25,
                            }

                        },
                src = "ES2",
                dest = "ES7",
            };

            var result = linkUtil.FindHops(route);
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void FindHops_ReturnsFalse()
        {
            Route route = new Route
            {
                links = new List<Link> {
                            new Link
                            {
                                source = "ES2",
                                destination = "SW1",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW1",
                                destination = "ES4",
                                speed = 1.25,
                            }
                        },
                src = "ES2",
                dest = "ES4",
            };

            var result = linkUtil.FindHops(route);
            Assert.AreNotEqual(3, result);
        }

        [TestMethod]
        public void IsScheduable_ReturnsTrue()
        {
            List<Stream> streams = new List<Stream> {
                new Stream
                {
                streamId = "Stream0",
                source = "ES1",
                destination = "ES3",
                size = 100,
                period = 100000,
                deadline = 10000,
                rl = 1,
                Route = new List<Route>
                {
                    new Route
                    {
                        links = new List<Link> {
                            new Link
                            {
                                source = "ES1",
                                destination = "SW0",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW0",
                                destination = "ES3",
                                speed = 1.25,
                            }
                        },
                        src = "ES1",
                        dest = "ES3",
                    },
                    new Route
                    {
                        links = new List<Link> {
                            new Link
                            {
                                source = "ES2",
                                destination = "SW1",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW1",
                                destination = "ES4",
                                speed = 1.25,
                            }
                        },
                        src = "ES2",
                        dest = "ES4",
                    }
                },
                },
                new Stream
                {
                streamId = "Stream1",
                source = "ES2",
                destination = "ES4",
                size = 1000,
                period = 10000,
                deadline = 10000,
                rl = 2,
                Route =                 new List<Route>
                {
                    new Route
                    {
                        links = new List<Link> {
                            new Link
                            {
                                source = "ES1",
                                destination = "SW0",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW0",
                                destination = "ES3",
                                speed = 1.25,
                            }
                        },
                        src = "ES1",
                        dest = "ES3",
                    },
                    new Route
                    {
                        links = new List<Link> {
                            new Link
                            {
                                source = "ES2",
                                destination = "SW1",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW1",
                                destination = "ES4",
                                speed = 1.25,
                            }
                        },
                        src = "ES2",
                        dest = "ES4",
                    }
                }
                }
        };
            var result = linkUtil.IsScheduable(streams);
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void IsScheduable_ReturnsFalse()
        {
            List<Stream> streams = new List<Stream> {
                new Stream
                {
                streamId = "Stream0",
                source = "ES1",
                destination = "ES3",
                size = 100000,
                period = 1000,
                deadline = 10000,
                rl = 1,
                Route = new List<Route>
                {
                    new Route
                    {
                        links = new List<Link> {
                            new Link
                            {
                                source = "ES1",
                                destination = "SW0",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW0",
                                destination = "ES3",
                                speed = 1.25,
                            }
                        },
                        src = "ES1",
                        dest = "ES3",
                    },
                    new Route
                    {
                        links = new List<Link> {
                            new Link
                            {
                                source = "ES2",
                                destination = "SW1",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW1",
                                destination = "ES4",
                                speed = 1.25,
                            }
                        },
                        src = "ES2",
                        dest = "ES4",
                    }
                },
                },
                new Stream
                {
                streamId = "Stream1",
                source = "ES2",
                destination = "ES4",
                size = 1000,
                period = 1000,
                deadline = 10000,
                rl = 2,
                Route = new List<Route>
                {
                    new Route
                    {
                        links = new List<Link> {
                            new Link
                            {
                                source = "ES1",
                                destination = "SW0",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW0",
                                destination = "ES3",
                                speed = 1.25,
                            }
                        },
                        src = "ES1",
                        dest = "ES3",
                    },
                    new Route
                    {
                        links = new List<Link> {
                            new Link
                            {
                                source = "ES2",
                                destination = "SW1",
                                speed = 1.25,
                            },
                            new Link
                            {
                                source = "SW1",
                                destination = "ES4",
                                speed = 1.25,
                            }
                        },
                        src = "ES2",
                        dest = "ES4",
                    }
                }
                }
        };
            var result = linkUtil.IsScheduable(streams);
            Assert.IsFalse(result);
        }
    }
}