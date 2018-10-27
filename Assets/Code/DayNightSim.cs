using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightSim : MonoBehaviour
{
    public int m_CycleTimeSec;
    public UnityEngine.Light m_LightSource;

    private float m_LightVariationPerSec;
    private int m_State = 0; // Increasing light or decreasing

    private void Start()
    {
        m_LightVariationPerSec = (2f) / m_CycleTimeSec;
    }

    // Update is called once per frame
    void Update()
    {
        if ( m_State == 0 )
        {
            m_LightSource.intensity += Time.deltaTime * m_LightVariationPerSec;
            if ( m_LightSource.intensity >= 1f )
            {
                m_LightSource.intensity -= Time.deltaTime * m_LightVariationPerSec;
                m_State = 1; //Starts decreasing
            }
        }
        else
        {
            m_LightSource.intensity -= Time.deltaTime * m_LightVariationPerSec;
            if ( m_LightSource.intensity <= 0f )
            {
                m_LightSource.intensity += Time.deltaTime * m_LightVariationPerSec;
                m_State = 0; //Starts increasing
            }
        }
    }
}
