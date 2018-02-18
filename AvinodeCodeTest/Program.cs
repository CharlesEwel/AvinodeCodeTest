using System;
using System.Collections.Generic;
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
                    reader.ReadToNextSibling("subMenu");
                    bool activeParent = false;
                    List<string> childMenus = new List<string>();
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        XmlReader subMenu = reader.ReadSubtree();
                        while(subMenu.Read())
                        {
                            string childMenu = parseItem(subMenu, "    ", active);
                            
                            if (childMenu != null)
                            {
                                //tells the program to put 'ACTIVE' next to the parent menu if this given child menu is a match for our active parameter
                                if (childMenu.Substring(Math.Max(0, childMenu.Length - 6)) == "ACTIVE") activeParent = true;
                                //pushes child menu displayName and path to our array
                                childMenus.Add(childMenu);
                            }
                                
                        }
                    }
                    if (parentMenu!=null && parentMenu.Substring(Math.Max(0, parentMenu.Length - 6)) != "ACTIVE" && activeParent) parentMenu = parentMenu + " ACTIVE";
                    Console.WriteLine(parentMenu);
                    foreach (string childMenu in childMenus)
                    {
                        Console.WriteLine(childMenu);
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
