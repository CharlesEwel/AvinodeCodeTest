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
            string active = "/Requests/OpenQuotes.aspx";
            string xmlString = File.ReadAllText(path);

            //Initialize reader with which to parse xml string
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                while (reader.Read())
                {
                    string parentMenu = parseItem(reader, "", active);
                    Console.WriteLine(parentMenu);
                    reader.ReadToNextSibling("subMenu");
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        XmlReader subMenu = reader.ReadSubtree();
                        while(subMenu.Read())
                        {
                            string childMenu = parseItem(subMenu, "    ", active);
                            if(childMenu != null) Console.WriteLine(childMenu);
                        }
                    }
                }
            }
            Console.Read();
        }

        public static string parseItem(XmlReader rdr, string identation, string activePath)
        {
            rdr.ReadToFollowing("item");
            rdr.ReadToDescendant("displayName");
            if (rdr.NodeType == XmlNodeType.Element)
            {
                //Gets menu name from inside element
                string menuName = rdr.ReadElementContentAsString();
                //Finds path and gets value of path from attributes list
                rdr.ReadToFollowing("path");
                rdr.MoveToAttribute("value");
                string menuPath = rdr.Value;
                if (menuPath == activePath)
                {
                    return identation + menuName + ", " + menuPath + " ACTIVE";
                }
                else return identation + menuName + ", " + menuPath;

            }
            else return null;
        }
    }
}
