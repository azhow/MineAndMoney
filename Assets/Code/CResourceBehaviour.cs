using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CResourceBehaviour : MonoBehaviour
{
    // Resource sprites
    public Sprite[] m_Sprites;

    // Types of resources
    public enum EResources
    {
        UraniumOre = 0,
        IronOre = 1,
        CopperOre = 2,
        NUM_OF_MINERAL_RESOURCES = 3,
        Energy = 4,
        RealEstate = 5,
        TOTAL_NUM_OF_RESOURCES = 6
    }
    // Decay amount per second
    public double m_DecayAmountPerSecond = 50;
    // Resource owner (null = no owner)
    public CEntity m_Owner = null;

    // Maximum quantity of resources
    private const int mc_MaxResource = 1000;
    // Minimum quantity of resources
    private const int mc_MinResource = 200;
    // Being extracted or not
    private bool m_HasBuilding = false;
    // Type of resource
    private EResources m_ResourceType;
    // Quantity remaining
    private double m_ResourceAmount;

    // Instantiates the tile
    public void
        Start()
    {
        // Uses the object instanceID as seed to avoid getting the same number
        System.Random randomGen = new System.Random(GetInstanceID());
        // Choose resource type
        m_ResourceType = (EResources)randomGen.Next((int)EResources.NUM_OF_MINERAL_RESOURCES);
        // Sets the initial amount
        m_ResourceAmount = UnityEngine.Random.Range(mc_MinResource, mc_MaxResource);

        // Sets the sprite of parent to be the correct one (assumes that the script sprites are in the same order as the enumeration).
        SpriteRenderer parentSpriteRenderer = GetComponentsInParent<SpriteRenderer>()[0];
        parentSpriteRenderer.sprite = m_Sprites[(int)m_ResourceType];
    }

    // Update is called once per frame
    public void
        Update()
    {
        // Only if it is being explored you should decrease deposit amount
        if ( m_HasBuilding )
        {
            //Updates amount of resource
            double newValue = m_ResourceAmount - m_DecayAmountPerSecond * Time.deltaTime;
            // Check if there's enough resources to explore
            if (newValue >= 0)
            {
                m_ResourceAmount = newValue;
                // If it has an owner, updates his inventory
                if (m_Owner != null)
                {
                    m_Owner.AddToInventory(m_ResourceType, newValue);
                }
            }
            // Sets to zero
            else
            {
                m_ResourceAmount = 0;
            }
        }
    }

    /// <summary>
    /// Returns the data about the resource deposit.
    /// </summary>
    /// <param name="resourceType"></param>
    /// <param name="resourceAmount"></param>
    /// <param name="hasBuilding"></param>
    /// <returns></returns>
    public bool
        GetResourceInfo( out EResources resourceType, out double resourceAmount, out bool hasBuilding, out string currOwner )
    {
        resourceType = m_ResourceType;
        resourceAmount = m_ResourceAmount;
        hasBuilding = m_HasBuilding;
        if (m_Owner != null)
        {
            currOwner = m_Owner.Name;
        }
        else
        {
            currOwner = "No One";
        }

        return true;
    }

    /// <summary>
    /// Returns an integer saying the amount of resources available in the deposit (used in the UI).
    /// </summary>
    /// <returns></returns>
    public int
        GetResourceAmount()
    {
        return (int)Math.Round(m_ResourceAmount);
    }

    public void ChangeOwner(CEntity newOwner)
    {
        m_Owner = newOwner;
    }
}