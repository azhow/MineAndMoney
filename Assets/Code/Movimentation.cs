using UnityEngine;
using System.Collections;

public class Movimentation : MonoBehaviour
{
    public UnityEngine.Tilemaps.Tilemap m_Tilemap;
    public Camera m_Camera;
    public float m_mainSpeed = 10.0f;   // Regular speed
    public float m_camSens = 0.15f;   // Mouse sensitivity

    private float m_totalRun = 1.0f;
    // Boundary values
    private float m_MIN_X;
    private float m_MAX_X;
    private float m_MIN_Y;
    private float m_MAX_Y;

    void Update()
    {
        // Boundary calculation
        float height = 2f * m_Camera.orthographicSize;
        float width = height * m_Camera.aspect;
        // Set boundary values
        m_MIN_X = m_Tilemap.cellBounds.xMin + (width / 2);
        m_MAX_X = m_Tilemap.cellBounds.xMax - (width / 2);
        m_MIN_Y = m_Tilemap.cellBounds.yMin + (height / 2);
        m_MAX_Y = m_Tilemap.cellBounds.yMax - (height / 2);

        // Keyboard commands
        Vector3 p = GetBaseInput();

        m_totalRun = Mathf.Clamp(m_totalRun * 0.5f, 1f, 1000f);
        p *= m_mainSpeed;

        p *= Time.deltaTime;
        Vector3 newPos = transform.position;
        transform.Translate(p);

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, m_MIN_X, m_MAX_X),
            Mathf.Clamp(transform.position.y, m_MIN_Y, m_MAX_Y),
            Mathf.Clamp(transform.position.z, 0, 0)
            );
    }

    // Returns the basic values, if it's 0 than it's not active.
    private Vector3 GetBaseInput()
    {
        Vector3 p_Velocity = new Vector3();
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        // Forwards
        if ( Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y > screenHeight - 30 )
            p_Velocity += new Vector3(0, 1, 0);

        // Backwards
        if ( Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y < 30 )
            p_Velocity += new Vector3(0, -1, 0);

        // Left
        if ( Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x < 30 )
            p_Velocity += new Vector3(-1, 0, 0);

        // Right
        if ( Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x > screenWidth - 30 )
            p_Velocity += new Vector3(1, 0, 0);

        if ( Input.GetAxis("Mouse ScrollWheel") > 0f && m_Camera.orthographicSize >= 2f )
            m_Camera.orthographicSize /= 1.05f;

        if ( Input.GetAxis("Mouse ScrollWheel") < 0f && m_Camera.orthographicSize <= 7f )
            m_Camera.orthographicSize /= 0.95f;

        return p_Velocity;
    }
}
