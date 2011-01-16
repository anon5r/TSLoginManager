using System;
using System.Collections.Generic;
using System.Text;
using TricksterTools.Plugins.Config;
using System.Xml.Serialization;

namespace TricksterTools.Plugins.UIEditor
{
    public class PluginConfig : XmlPlugin
    {

        [XmlAttribute(AttributeName = "name")]
        public new string name = "UIEditor";

        [XmlElement(ElementName = "Config")]
        public new ConfigUIEditor Config;
    }


    public class ConfigUIEditor : TricksterTools.Plugins.IPluginConfig
    {
        [XmlElement(ElementName = "ChatUI")]
        public ConfigChatUI chatUI;
    }

    public class ConfigChatUI
    {
        [XmlAttribute(AttributeName = "width")]
        public int Width = 0;

        [XmlAttribute(AttributeName = "height")]
        public int Height = 0;
    }

}
