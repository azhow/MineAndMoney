using UnityEngine;
using System.Collections;

public class Movimentation : MonoBehaviour
{
    // Background tilemap is used to set boundaries for the camera movement
    public UnityEngine.Tilemaps.Tilemap m_BackgroundTilemap;
    public float m_CameraSpeed = 10.0f;   // Camera speed

    private float m_totalRun = 1.0f;
    // Boundary values
    private float m_MIN_X;
    private float m_MAX_X;
    private float m_MIN_Y;
    private float m_MAX_Y;

    public void 
        Update()
    {
        // Boundary calculation
        float height = 2f * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        // Set boundary values (considers the tilemap to be a rectangle)
        m_MIN_X = m_BackgroundTilemap.cellBounds.xMin + (width / 2);
        m_MAX_X = m_BackgroundTilemap.cellBounds.xMax - (width / 2);
        m_MIN_Y = m_BackgroundTilemap.cellBounds.yMin + (height / 2);
        m_MAX_Y = m_BackgroundTilemap.cellBounds.yMax - (height / 2);

        // Keyboard commands
        Vector3 p = GetBaseInput();

        // Calculates new position and camera size
        m_totalRun = Mathf.Clamp(m_totalRun * 0.5f, 1f, 1000f);
        p *= m_CameraSpeed;

        p *= Time.deltaTime;
        Vector3 newPos = transform.position;
        transform.Translate(p);

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, m_MIN_X, m_MAX_X),
            Mathf.Clamp(transform.position.y, m_MIN_Y, m_MAX_Y),
            Mathf.Clamp(transform.position.z, 0, 0));
    }

    // Returns the basic values, if it's 0 than it's not active.
    private Vector3
        GetBaseInput()
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

        // Zoom in with mouse scroll or keypad +
        if ( (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKeyDown(KeyCode.KeypadPlus)) && Camera.main.orthographicSize >= 2f )
            Camera.main.orthographicSize /= 1.05f;
        
        // Zoom out with mouse scroll or keypad -
        if ( (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetKeyDown(KeyCode.KeypadMinus)) && Camera.main.orthographicSize <= 7f )
            Camera.main.orthographicSize /= 0.95f;

        return p_Velocity;
    }
}
