using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CBuildingBehaviour : MonoBehaviour
{
    // Building sprites
    public Sprite[] m_Sprites;

    // Type of resource
    private CResourceBehaviour.EResources m_ResourceType;

    // Creates the building
    public void
        CreateBuiding(CResourceBehaviour.EResources resourceType)
    {
        // Choose resource type
        m_ResourceType = resourceType;

        // Sets the sprite of parent to be the correct one.
        SpriteRenderer parentSpriteRenderer = GetComponentsInParent<SpriteRenderer>()[0];
        parentSpriteRenderer.sprite = m_Sprites[(int)m_ResourceType];
    }

    // Update is called once per frame
    public void
        Update()
    {
    }

    /// <summary>
    /// Returns the data about the building.
    /// </summary>
    /// <param name="resourceType"></param>
    /// <param name="resourceAmount"></param>
    /// <param name="hasBuilding"></param>
    /// <returns></returns>
    public bool
        GetResourceInfo( out CResourceBehaviour.EResources resourceType )
    {
        resourceType = m_ResourceType;

        return true;
    }
}