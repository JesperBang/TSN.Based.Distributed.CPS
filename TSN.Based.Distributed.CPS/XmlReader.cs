using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Xml.Linq;
using TSN.Based.Distributed.CPS.Models;

namespace TSN.Based.Distributed.CPS
{
    public class XmlReader
    {

        // file of jobs

        public static (List<Device>, List<Link>, List<Stream>) LoadXml(int xml)
        {
            string xmlfile = "TC0_example.app_network_description";
            switch (xml)
            {
                case 1:
                    xmlfile = "TC0_example.app_network_description";
                    break;
                case 2:
                    xmlfile = "TC3_medium.app_network_description";
                    break;
                case 3:
                    xmlfile = "TC4_large.app_network_description";
                    break;
            }

            string filename = "../../../inputs/" + xmlfile;
            List<Device> devices = new List<Device>();
            List<Link> link = new List<Link>();
            List<Stream> stream = new List<Stream>();

            //Read xml file
            XDocument xdoc = XDocument.Load(filename);

            // get the devices
            IEnumerable<XElement> devicess = xdoc.Element("NetworkDescription").Descendants("device");
            foreach (XElement item1 in devicess)
            {
                Device device = new Device();
                device.name = Convert.ToString(item1.Attribute("name").Value);
                device.type = Convert.ToString(item1.Attribute("type").Value);
                devices.Add(device);
            }

            IEnumerable<XElement> links = xdoc.Element("NetworkDescription").Descendants("link");
            foreach (XElement item2 in links)
            {
                Link linkss = new Link();
                linkss.source = Convert.ToString(item2.Attribute("src").Value);
                linkss.destination = Convert.ToString(item2.Attribute("dest").Value);
                linkss.speed = double.Parse(Convert.ToString(item2.Attribute("speed").Value), CultureInfo.InvariantCulture);
                link.Add(linkss);

            }

            IEnumerable<XElement> streams = xdoc.Element("NetworkDescription").Descendants("stream");
            foreach (XElement item3 in streams)
            {
                Stream streamss = new Stream();
                streamss.deadline = Convert.ToInt32(item3.Attribute("deadline").Value);
                streamss.streamId = Convert.ToString(item3.Attribute("id").Value);
                streamss.destination = Convert.ToString(item3.Attribute("dest").Value);
                streamss.source = Convert.ToString(item3.Attribute("src").Value);
                streamss.size = Convert.ToInt32(item3.Attribute("size").Value);
                streamss.period = Convert.ToInt32(item3.Attribute("period").Value);
                streamss.rl = Convert.ToInt32(item3.Attribute("rl").Value);
                stream.Add(streamss);
            }

            return (devices, link, stream);
        }
    }
}
