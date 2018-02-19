using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace AvinodeCodeTest
{
    class Program
    {
        //This is our program which takes the xml and makes it into a menu. It accepts two arguments corresponding to the location of the xml file and the path which we would like to see marked as active
        static void Main(string[] args)
        {
            //Get XML Data from file and put it into strig
            string path = args[0];
            string active = args[1];
            string xmlString = File.ReadAllText(path);

            //Initialize reader with which to parse xml string
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                while (reader.Read())
                {
                    //This method call goes to work on the first item, turning it into a string to be written into our menu
                    string parentMenu = parseItem(reader, active);
                    //This variable will be switched to true if one of the child menus has a path which matches our active path
                    bool activeParent = false;
                    //Next we look to see if there is a submenu, and prepare to access it if so
                    reader.ReadToNextSibling("subMenu");
                    List<string> childMenus = new List<string>();
                    Dictionary<string, List<string>> childSubMenus = new Dictionary<string, List<string>>{ };
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        //This reader will read through the items in the submenu and finish when the submenu ends
                        XmlReader subMenu = reader.ReadSubtree();
                        while(subMenu.Read())
                        {
                            string childMenu = parseItem(subMenu, active);
                            
                            if (childMenu != null)
                            {
                                bool activeChild = false;
                                List<string> subChildMenus = new List<string>();
                                //Next we look to see if there is a submenu within the current submenu, and prepare to access it if so
                                subMenu.ReadToNextSibling("subMenu");
                                if (reader.NodeType == XmlNodeType.Element)
                                {
                                    XmlReader subSubMenu = subMenu.ReadSubtree();
                                    while (subSubMenu.Read())
                                    {
                                        string subChildMenu = parseItem(subSubMenu, active);
                                        if (subChildMenu != null)
                                        {
                                            //tells the program to put 'ACTIVE' next to the parent menu if this given child menu is a match for our active parameter
                                            if (subChildMenu.Substring(Math.Max(0, subChildMenu.Length - 6)) == "ACTIVE") activeChild = true;
                                            //pushes child menu displayName and path to our array
                                            subChildMenus.Add(subChildMenu);
                                        }

                                    }
                                }
                                if (childMenu != null && childMenu.Substring(Math.Max(0, childMenu.Length - 6)) != "ACTIVE" && activeChild) childMenu = childMenu + " ACTIVE";
                                childSubMenus.Add(childMenu, subChildMenus);
                                //tells the program to put 'ACTIVE' next to the parent menu if this given child menu is a match for our active parameter
                                if (childMenu.Substring(Math.Max(0, childMenu.Length - 6)) == "ACTIVE") activeParent = true;
                                //pushes child menu displayName and path to our array
                                childMenus.Add(childMenu);
                            }
                                
                        }
                    }
                    //Checks to see if the activeParent boolean was ever switched to true, indicating that the parent menu needs to me marked as active
                    //There is also a check to see if it was already marked as active when performing parseItem on the original parent item, this prevents it being marked as active twice in cases where the child path is the same as the parent path
                    if (parentMenu!=null && parentMenu.Substring(Math.Max(0, parentMenu.Length - 6)) != "ACTIVE" && activeParent) parentMenu = parentMenu + " ACTIVE";
                    Console.WriteLine(parentMenu);
                    foreach (string childMenu in childMenus)
                    {
                        //prints the child menu with identation
                        Console.WriteLine("        "+childMenu);
                        foreach (string subChildMenu in childSubMenus[childMenu])
                        {
                            //prints the child menu with identation
                            Console.WriteLine("        " + "        " + subChildMenu);
                        }
                    }
                }
            }
            //Added this to keep the menu on screen once the program finishes running
            Console.Read();
        }

        //This method parses a given xml item element, extracts the display name and path value, and formats those into a string
        public static string parseItem(XmlReader rdr, string activePath)
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
                    return menuName + ", " + menuPath + " ACTIVE";
                }
                else return menuName + ", " + menuPath;

            }
            else return null;
        }
    }
}
