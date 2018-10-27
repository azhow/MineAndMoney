using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAudioVolume : MonoBehaviour {

    public AudioSource m_audioSource;
    public UnityEngine.UI.Slider m_slider;

    public void
        OnValueChanged()
    {
        m_audioSource.volume = m_slider.value;
    }
}