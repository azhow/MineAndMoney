using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTileInfo : MonoBehaviour
{
    public UnityEngine.UI.Text m_Text;
    public Grid m_Grid;
    public UnityEngine.Tilemaps.Tile m_Tile;

    private UnityEngine.Tilemaps.Tilemap m_Background;
    private UnityEngine.Tilemaps.Tilemap m_Minerals;
    private UnityEngine.Tilemaps.Tilemap m_Buildings;
    private UnityEngine.Tilemaps.Tilemap m_UILayer;
    private Dictionary<Vector3Int, Transform> m_Objects;
    private Vector3Int m_LastTileCoordinate;

    void Start()
    {
        // Set outside of the map
        m_LastTileCoordinate = new Vector3Int(-1, -1, -1);
        // Assigns the correct tilemaps
        UnityEngine.Tilemaps.Tilemap[] tilemaps = m_Grid.GetComponentsInChildren<UnityEngine.Tilemaps.Tilemap>();
        foreach ( UnityEngine.Tilemaps.Tilemap tilemap in tilemaps )
        {
            if ( tilemap.name == "Background" )
                m_Background = tilemap;
            else if ( tilemap.name == "Minerals" )
                m_Minerals = tilemap;
            else if ( tilemap.name == "Buildings" )
                m_Buildings = tilemap;
            else if ( tilemap.name == "UILayer" )
                m_UILayer = tilemap;
        }

        m_Objects = new Dictionary<Vector3Int, Transform>();
        // Stores the position and object in dictionary
        foreach ( Transform t in m_Minerals.GetComponentInChildren<Transform>() )
        {
            Vector3Int objPos = m_Grid.WorldToCell(t.position);
            m_Objects.Add(objPos, t);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Gets the tile on the mouse position when clicked
        if ( Input.GetMouseButtonDown(0) )
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int coordinate = m_Grid.WorldToCell(mouseWorldPos);
            // Erases last highlighted tile
            m_UILayer.SetTile(m_LastTileCoordinate, null);
            // Sets new tile
            m_UILayer.SetTile(coordinate, m_Tile);
            // Updates last coordinate
            m_LastTileCoordinate = coordinate;
        }
        else if ( Input.GetMouseButtonDown(1) )
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int coordinate = m_Grid.WorldToCell(mouseWorldPos);

            // Detects if the tile clicked has any object
            if ( m_Objects.ContainsKey(coordinate) )
            {
                Debug.Log(m_Objects[coordinate].gameObject.GetComponent<CResourceBehaviour>().GetResourceAmount());
                // Sets attribute of the clicked object
                m_Objects[coordinate].gameObject.GetComponent<CResourceBehaviour>().m_HasBuilding = true;
            }
        }
    }
}