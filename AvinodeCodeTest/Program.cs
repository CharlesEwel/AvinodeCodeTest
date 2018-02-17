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
            //Get XML Data from file and put it into strig
            string path = "C:/Users/Chuck/Downloads/SchedAero Menu.txt";
            string xmlString = File.ReadAllText(path);

            //Initialize reader with which to parse xml string
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                while (reader.Read())
                {
                    reader.ReadToFollowing("item");
                    reader.ReadToDescendant("displayName");
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        //Gets menu name from inside element
                        string menuName = reader.ReadElementContentAsString();
                        //Finds path and gets value of path from attributes list
                        reader.ReadToFollowing("path");
                        reader.MoveToAttribute("value");
                        string menuPath = reader.Value;
                        Console.WriteLine(menuName + ", " + menuPath);
                    }

                }
            }
            Console.Read();
        }
    }
}
