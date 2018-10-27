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
        LoadRuntime()
    {
        SceneManager.LoadScene(System.Convert.ToInt32(m_DropMenu.captionText.text));
    }
}