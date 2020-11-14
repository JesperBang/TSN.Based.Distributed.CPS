using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using TSN.Based.Distributed.CPS.Models;

namespace TSN.Based.Distributed.CPS
{
    public class XMLWriter
    {
        public static void To_XML(List<Solution> sl, double cost, int xml)
        {

            string xmlsolution = "TC0_small.solution";
            switch (xml)
            {
                case 1:
                    xmlsolution = "TC0_small.solution";
                    break;
                case 2:
                    xmlsolution = "TC0_medium.solution";
                    break;
            }
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            XmlWriter writer = XmlWriter.Create(Path.Combine(Environment.CurrentDirectory, xmlsolution), settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("solution");
            writer.WriteAttributeString("tc_name", xmlsolution.Split(".")[0]);
            foreach (Solution s in sl)
            {
                writer.WriteStartElement("stream");
                writer.WriteAttributeString("id", s.StreamId);

                foreach (Route r in s.Route)
                {
                    writer.WriteStartElement("route");
                    foreach (Link l in r.links)
                    {
                        writer.WriteStartElement("link");
                        writer.WriteAttributeString("src", l.source);
                        writer.WriteAttributeString("dest", l.destination);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteComment("Total cost: " + cost.ToString());
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }
    }
}