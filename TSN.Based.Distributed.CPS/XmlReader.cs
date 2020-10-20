using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Xml.Linq;
using TSN.Based.Distributed.CPS.Models;

namespace SystemsOptimization
{
    public class XmlReader
    {

        // file of jobs

        public static (List<Device>, List<Link>, List<Stream>) LoadXml(int xml)
        {
            string xmlfile = "TC0_example.app_network_description";
            //switch (xml)
            //{
            //    case 1:
            //        xmlfile = "small.xml";
            //        break;
            //    case 2:
            //        xmlfile = "medium.xml";
            //        break;
            //    case 3:
            //        xmlfile = "large.xml";
            //        break;
            //}

            string filename = "../../../inputs/" + xmlfile;
            List<Device> devices = new List<Device>();
            List<Link> link = new List<Link>();
            List<Stream> stream = new List<Stream>();

            //Read xml file
            XDocument xdoc = XDocument.Load(filename);

            //get the CPU's
            IEnumerable<XElement> cpus = xdoc.Element("Model").Element("Platform").Descendants("MCP");
            foreach (XElement item1 in cpus)
            {
                foreach (XElement item2 in item1.Descendants())
                {
                    Core core = new Core();
                    core.MCPId = Convert.ToInt32(item1.Attribute("Id").Value);
                    core.CoreId = Convert.ToInt32(item2.Attribute("Id").Value);
                    string corestring = Convert.ToString(item2.Attribute("WCETFactor").Value);
                    core.WCETFactor = double.Parse(corestring, CultureInfo.InvariantCulture);
                    cores.Add(core);
                }
            }

            //get the task
            IEnumerable<XElement> taskss = xdoc.Element("Model").Element("Application").Descendants("Task");
            foreach (XElement item3 in taskss)
            {
                Task task = new Task();
                task.Deadline = Convert.ToInt32(item3.Attribute("Deadline").Value);
                task.Id = Convert.ToInt32(item3.Attribute("Id").Value);
                task.Period = Convert.ToInt32(item3.Attribute("Period").Value);
                task.WCET = Convert.ToInt32(item3.Attribute("WCET").Value);
                tasks.Add(task);
            }

            return (tasks, cores);
        }
    }
}
