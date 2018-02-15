using System;
using System.IO;
using System.Text;
using System.Xml;

namespace AvinodeCodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:/Users/Chuck/Downloads/Wyvern Menu.txt";
            string xmlString = File.ReadAllText(path);

            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                while(reader.Read())
                {
                    reader.ReadToFollowing("displayName");
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        Console.WriteLine(reader.ReadElementContentAsString());
                    }
                }
            }
            Console.Read();
        }
    }
}
