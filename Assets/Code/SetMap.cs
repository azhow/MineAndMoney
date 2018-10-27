using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SetMap : MonoBehaviour
{
    public List<UnityEngine.SceneManagement.Scene> m_MapList;
    public UnityEngine.UI.Dropdown m_DropdownMenu;
    public int m_SceneIndex = 0;
    private List<string> m_Indexes;

    public void Start()
    {
        m_Indexes = new List<string>();
        Populate();
    }

    public void OnSet( int index )
    {
        m_SceneIndex = index;
    }

    /// <summary>
    /// Loads all maps from the map folder.
    /// </summary>
    private void Populate()
    {
        string dataPath = Application.dataPath + "/Scenes/Maps";
        string[] allMaps = Directory.GetFiles(dataPath, "*.unity", SearchOption.TopDirectoryOnly);
        foreach (string map in allMaps)
        {
            string[] tokens = map.Split(new[] { "/Assets" }, System.StringSplitOptions.RemoveEmptyEntries);
            string[] tokens1 = tokens[1].Split('\\');
            string token = "Assets" + tokens1[0] + '/' + tokens1[1];
            m_Indexes.Add(System.Convert.ToString(UnityEngine.SceneManagement.SceneUtility.GetBuildIndexByScenePath(token)));
        }
        m_DropdownMenu.AddOptions(m_Indexes);
    }
}
