using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Text;
using System.Xml;

namespace SystemsOptimization
{
    public class XMLWriter
    {
        public static void To_XML(State s, double laxity, int xml)
        {

            string xmlsolution = "solution_small.xml";
            switch (xml)
            {
                case 1:
                    xmlsolution = "solution_small.xml";
                    break;
                case 2:
                    xmlsolution = "solution_medium.xml";
                    break;
                case 3:
                    xmlsolution = "solution_large.xml";
                    break;
            }
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(Path.Combine(Environment.CurrentDirectory, xmlsolution), settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("solution");
            foreach(AssignedTask c in s.Core)
            {
                foreach(Task t in c.tasks)
                {
                writer.WriteStartElement("Task");
                writer.WriteAttributeString("Id", t.Id.ToString());
                writer.WriteAttributeString("MCP", c.MCPId.ToString());
                writer.WriteAttributeString("Core", c.CoreId.ToString());
                writer.WriteAttributeString("WCRT", Math.Round(t.WCET * c.WCETFactor).ToString());
                writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
            writer.WriteComment("Total Laxity: " + Math.Round(laxity).ToString());
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }
    }
}