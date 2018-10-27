using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffMusic : MonoBehaviour {

    private bool m_musicStatus = true; //It is playing

    public void 
        changeText(UnityEngine.UI.Text text)
    {
        if (text.text == "On") text.text = "Off";
        else text.text = "On";
    }

    public void
        stopPlayAudio(AudioSource audioSource)
    {
        if (m_musicStatus)
        {
            audioSource.Stop();
            m_musicStatus = false;
        }
        else
        {
            audioSource.Play();
            m_musicStatus = true;
        }
    }
}
