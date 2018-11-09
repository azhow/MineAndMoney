using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable()]
public class CMap
{
    [XmlElement]
    public string Filepath;
    [XmlElement]
    public string MapName;
    [XmlElement]
    public int buildID;
    [XmlElement]
    public int XSize;
    [XmlElement]
    public int YSize;
}