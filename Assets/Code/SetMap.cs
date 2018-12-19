using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System;

public class SetMap : MonoBehaviour
{
    // Dropdown menu instance
    public UnityEngine.UI.Dropdown m_DropdownMenu;
    // Default points to menu itself
    public int m_BuildIDSelected = 0;

    // Used to populate the dropmenu
    private List<string> m_Indexes;
    // Map objects used to get BuildID
    private List<CMap> m_Maps;

    public void Start()
    {
        m_Maps = new List<CMap>();
        m_Indexes = new List<string>();
        Populate();
    }

    public void OnSet()
    {
        // Updates the buildID of the selected item
        m_BuildIDSelected = m_Maps.Find(x => string.Equals(x.MapName, m_DropdownMenu.captionText.text)).buildID;
    }

    /// <summary>
    /// Loads all maps from the map folder.
    /// </summary>
    private void Populate()
    {
        // Maps folder
        string dataPath = Application.dataPath + "/Maps";
        // XML Read
        XmlSerializer serializer = new XmlSerializer(typeof(CMapHolder));
        FileStream mapConfigStream = new FileStream(dataPath + "/maps_config.xml", FileMode.Open);
        CMapHolder cmapHolder = (CMapHolder)serializer.Deserialize(mapConfigStream);

        foreach(CMap map in cmapHolder.cmaps)
        {
            m_Maps.Add(map);
            m_Indexes.Add(map.MapName);
        }

        m_DropdownMenu.AddOptions(m_Indexes);
        /*
        //XML WRITE
        XmlSerializer serializer = new XmlSerializer(typeof(CMapHolder));
        CMapHolder mapHolder = new CMapHolder();
        CMap map1 = new CMap();
        map1.Filepath = "bbbb";
        map1.MapName = "New Map";
        map1.buildID = 3;
        map1.XSize = 15;
        map1.YSize = 20;

        CMap map2 = new CMap();
        map2.Filepath = "aaaa";
        map2.MapName = "New Map 1";
        map2.buildID = 2;
        map2.XSize = 13;
        map2.YSize = 21;

        CMap[] a = { map1, map2 };
        mapHolder.cmaps = a;
        
        System.IO.StreamWriter writer = new System.IO.StreamWriter("C:\\Games\\test.xml");

        serializer.Serialize(writer, mapHolder);
        */
    }
}
