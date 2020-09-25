using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml;

namespace ConvertAzJsonConfig
{
    class Program
    {
        private class ConfigEntry
        {
            /// <summary>
            /// Key name
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// Value name
            /// </summary>
            public string value { get; set; }
        }

        static void Main(string[] args)
        {
            var contents = File.ReadAllText("test.json");
            var config = JsonSerializer.Deserialize<List<ConfigEntry>>(contents);

            var doc = new XmlDocument( );

            //(1) the xml declaration is recommended, but not mandatory

            var parentNode = doc.CreateElement("appSettings");
            doc.AppendChild(parentNode);

            foreach (var configEntry in config)
            {
                var addElement = doc.CreateElement( string.Empty, "add", string.Empty );
                addElement.SetAttribute("key", configEntry.name);
                addElement.SetAttribute("value", configEntry.value);
                parentNode.AppendChild(addElement);
            }

            doc.Save("config.xml");

        }
    }
}
