using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TricksterTools
{
    namespace Plugins
    {
        namespace Config
        {

            [XmlRoot("Trickster")]
            public class XmlTricksterRoot
            {
                [XmlElement(ElementName = "Tools")]
                public XmlTools Tools;
            }

            public class XmlTools
            {
                [XmlAttribute(AttributeName = "name")]
                public string name = "TSLoginManager";

                [XmlElement(ElementName = "Plugins")]
                public XmlPlugin[] Plugin;

            }

            public abstract class XmlPlugin
            {
                [XmlAttribute(AttributeName = "name")]
                public string name = "";

                [XmlElement(ElementName = "Config")]
                public TricksterTools.Plugins.IPluginConfig Config;
            }
        }
    }
}
