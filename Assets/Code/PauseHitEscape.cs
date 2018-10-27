using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHitEscape : MonoBehaviour
{
    public Transform m_canvas;
    private bool m_status = false;

    // Update is called once per frame
    void
        Update()
    {
        if ( Input.GetKeyDown(KeyCode.Escape) )
        {
            m_canvas.gameObject.SetActive(!m_status);
            m_status = !m_status;
            if ( m_status ) Time.timeScale = 0;
            else Time.timeScale = 1;
        }

    }
}
