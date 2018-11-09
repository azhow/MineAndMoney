using System;
using System.Xml;
using System.Xml.Serialization;

[Serializable()]
public class CMapHolder
{
    [XmlElement]
    public CMap[] cmaps;
}