using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {
    public UnityEngine.UI.Dropdown m_DropMenu;

    public void 
        loadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void
        LoadRuntimePlayPanel()
    {
        SetMap mapSetter = m_DropMenu.GetComponent<SetMap>();
        SceneManager.LoadScene(mapSetter.m_BuildIDSelected);
    }
}