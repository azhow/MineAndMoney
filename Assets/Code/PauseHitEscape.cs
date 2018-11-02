using UnityEngine;

public class PauseHitEscape : MonoBehaviour
{
    // Pause canvas
    public Transform m_Canvas;
    // Active status
    private bool m_ActiveStatus = false;

    // Update is called once per frame
    void
        Update()
    {
        // If hit Esc then pauses the game
        if ( Input.GetKeyDown(KeyCode.Escape) )
        {
            // Set new status
            m_Canvas.gameObject.SetActive(!m_ActiveStatus);
            // Updates status
            m_ActiveStatus = !m_ActiveStatus;
            // Pauses the time passage
            if ( m_ActiveStatus )
            {
                Time.timeScale = 0;
            }
            // Unpauses the time passage
            else
            {
                Time.timeScale = 1;
            }
        }

    }
}
