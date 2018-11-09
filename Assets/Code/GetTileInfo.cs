using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;

public class GetTileInfo : MonoBehaviour
{
    // Grid where the tilemaps are positioned
    public Grid m_Grid;
    // Highlight tile
    public UnityEngine.Tilemaps.Tile m_HighlightTile;
    // UI Panel
    public RectTransform m_Panel;

    // Layers
    private UnityEngine.Tilemaps.Tilemap m_Background;
    private UnityEngine.Tilemaps.Tilemap m_Minerals;
    private UnityEngine.Tilemaps.Tilemap m_Buildings;
    private UnityEngine.Tilemaps.Tilemap m_UILayer;
    // Existing objects
    private Dictionary<Vector3Int, Transform> m_MineralDeposits;
    // Coordinate of the last highlighted tile
    private Vector3Int m_LastTileCoordinate;
    // UI entries
    private UnityEngine.UI.Text m_TypeEntry;
    private UnityEngine.UI.Text m_AmountEntry;
    private UnityEngine.UI.Text m_HasBuildingEntry;

    public void
        Start()
    {
        // Set outside of the map
        m_LastTileCoordinate = new Vector3Int(-1, -1, -1);

        // Assigns the correct tilemaps / layers
        foreach ( UnityEngine.Tilemaps.Tilemap tilemap in m_Grid.GetComponentsInChildren<UnityEngine.Tilemaps.Tilemap>() )
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

        // Initializes dictionary
        m_MineralDeposits = new Dictionary<Vector3Int, Transform>();
        // Stores the position and object in dictionary
        foreach ( Transform transform in m_Minerals.GetComponentInChildren<Transform>() )
        {
            Vector3Int objPos = m_Grid.WorldToCell(transform.position);
            m_MineralDeposits.Add(objPos, transform);
        }

        // Assigns the UI elements
        foreach ( UnityEngine.UI.Text text in m_Panel.GetComponentsInChildren<UnityEngine.UI.Text>() )
        {
            if (text.name == "ResourceTypeEntry" )
            {
                m_TypeEntry = text;
            }
            else if (text.name == "ResourceAmountEntry" )
            {
                m_AmountEntry = text;
            }
            else if ( text.name == "ResourceHasBuildingEntry" )
            {
                m_HasBuildingEntry = text;
            }
        }
    }

    // Update is called once per frame
    public void 
        Update()
    {
        // Gets the tile on the mouse position when clicked with the left button
        if ( Input.GetMouseButtonDown(0) )
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int coordinate = m_Grid.WorldToCell(mouseWorldPos);

            HighlightTile(coordinate);
            UpdateUI(coordinate);
        }
        // Right button click
        else if ( Input.GetMouseButtonDown(1) )
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int coordinate = m_Grid.WorldToCell(mouseWorldPos);

        }
    }

    private void 
        UpdateUI( Vector3Int ClickedCoordinate )
    {
        // Detects if the tile clicked has any object
        if ( m_MineralDeposits.ContainsKey(ClickedCoordinate) )
        {
            // Shows UI panel
            m_Panel.gameObject.SetActive(true);

            CResourceBehaviour.EResources depotType;
            double depotAmount;
            bool isExtraction;
            // Gets the resource information
            m_MineralDeposits[ClickedCoordinate].gameObject.GetComponent<CResourceBehaviour>().GetResourceInfo(out depotType, out depotAmount, out isExtraction);
            Debug.Log(depotType);

            // Updates UI
            m_TypeEntry.text = Convert.ToString(depotType);
            m_AmountEntry.text = Convert.ToString(depotAmount);
            m_HasBuildingEntry.text = Convert.ToString(isExtraction);

            // Sets attribute of the clicked object
            //m_Objects[coordinate].gameObject.GetComponent<CResourceBehaviour>().m_HasBuilding = true;
        }
        // If clicked on other than resource, then close the overview
        else
        {
            m_Panel.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Highlights the clicked tile.
    /// </summary>
    /// <param name="ClickedCoordinate"></param>
    private void 
        HighlightTile( Vector3Int ClickedCoordinate )
    {
        // Erases last highlighted tile
        m_UILayer.SetTile(m_LastTileCoordinate, null);
        // Sets new tile
        m_UILayer.SetTile(ClickedCoordinate, m_HighlightTile);
        // Updates last coordinate
        m_LastTileCoordinate = ClickedCoordinate;
    }
}