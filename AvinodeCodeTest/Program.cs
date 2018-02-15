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
            string path = "C:/Users/Chuck/Downloads/Wyvern Menu.txt";
            string xmlString = File.ReadAllText(path);

            //Initialize reader with which to parse xml string
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                while(reader.Read())
                {
                    //Gets menu name
                    reader.ReadToFollowing("displayName");
                    //without nnelow if-statement the program hits a bug once it finishes the xml file
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        //Gets menu name from inside element
                        string menuName = reader.ReadElementContentAsString();
                        //Finds path and gets value of path from attributes list
                        reader.ReadToFollowing("path");
                        reader.MoveToFirstAttribute();
                        string menuPath = reader.Value;
                        Console.WriteLine(menuName + ", " + menuPath);
                    }
                }
            }
            Console.Read();
        }
    }
}
