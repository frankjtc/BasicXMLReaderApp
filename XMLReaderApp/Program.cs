using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLReaderApp
{
    class XMLReaderApp :IBooksParser
    {
        static void Main(string[] args)
        {
            XmlTextReader reader = new XmlTextReader("C:\\Books.xml");
            StringBuilder xmlString = new StringBuilder();

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        xmlString.Append("<" + reader.Name);
                        xmlString.Append(">");
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        xmlString.Append(reader.Value);
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        xmlString.Append("</" + reader.Name);
                        xmlString.Append(">");
                        break;
                }
            }


                XMLReaderApp app = new XMLReaderApp();

                app.ParseFile(xmlString.ToString());
                Console.ReadKey();
            
        }            

        public void ParseFile(string fileContent) {

            StringBuilder output = new StringBuilder();

            // Create an XmlReader
            using (XmlReader reader = XmlReader.Create(new StringReader(fileContent)))
            {
                reader.ReadToFollowing("book");
                reader.MoveToFirstAttribute();
                string genre = reader.Value;
                output.AppendLine("The genre: " + genre);

                reader.ReadToFollowing("title");
                output.AppendLine("Content of the title element: " + reader.ReadElementContentAsString());
            }

            Console.WriteLine(output.ToString());
        }
    }
}
