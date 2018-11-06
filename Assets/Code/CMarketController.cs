using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the market and interacts with the UI.
/// Logic control is implemented in this layer.
/// </summary>
class CMarketController : MonoBehaviour
{
    // UI Panel
    public RectTransform m_UIPanel;

    // Market behaviour script
    private CMarketBehaviour m_MarketBehaviour;
    private Dictionary<CResourceBehaviour.EResources, Text> m_Prices;

    public void
        Start()
    {
        // Gets the market behaviour instance from the game object
        m_MarketBehaviour = GetComponentInParent<CMarketBehaviour>();

        // Assigns each text to its entry in the canvas
        m_Prices = new Dictionary<CResourceBehaviour.EResources, Text>();
        foreach ( Text t in m_UIPanel.GetComponentsInChildren<Text>() )
        {
            if ( t.name == "UraniumEntry" )
            {
                m_Prices.Add(CResourceBehaviour.EResources.UraniumOre, t);
            }
            else if ( t.name == "IronEntry" )
            {
                m_Prices.Add(CResourceBehaviour.EResources.IronOre, t);
            }
            else if ( t.name == "CopperEntry" )
            {
                m_Prices.Add(CResourceBehaviour.EResources.CopperOre, t);
            }
            else if ( t.name == "RealEstateEntry" )
            {
                m_Prices.Add(CResourceBehaviour.EResources.RealEstate, t);
            }
            else if ( t.name == "EnergyEntry" )
            {
                m_Prices.Add(CResourceBehaviour.EResources.Energy, t);
            }
        }
    }

    public void
        Update()
    {
        // Price dictionary
        Dictionary<CResourceBehaviour.EResources, double> prices = m_MarketBehaviour.GetPricesDictionary();

        // Updates text entry in the canvas
        foreach (CResourceBehaviour.EResources resource in prices.Keys)
        {
            m_Prices[resource].text = Convert.ToString(prices[resource]);
        }
    }
}