using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CEntity : MonoBehaviour
{
    // UI Panel
    public RectTransform m_UIPanel;
    // Entity inventory
    Dictionary<CResourceBehaviour.EResources, double> m_EntityInventory;
    // Entity inventory
    Dictionary<CResourceBehaviour.EResources, Text> m_EntityInventoryPanel;
    // Entity's owned resource tiles

    // Amount of credits the entity has
    double m_Credits = 150000;
    Text m_CreditsEntry;
    // Market controller instance
    public CMarketController m_MarketController;
    // Name of the entity
    public string Name = "Player";

    // Start is called before the first frame update
    void Start()
    {
        // Initializes inventory
        m_EntityInventory = new Dictionary<CResourceBehaviour.EResources, double>();
        for (int i = 0; i < (int)(CResourceBehaviour.EResources.TOTAL_NUM_OF_RESOURCES); i++)
        {
            if ((CResourceBehaviour.EResources)i != CResourceBehaviour.EResources.NUM_OF_MINERAL_RESOURCES
                || (CResourceBehaviour.EResources)i != CResourceBehaviour.EResources.TOTAL_NUM_OF_RESOURCES)
            {
                m_EntityInventory.Add((CResourceBehaviour.EResources)i, 0);
            }
        }

        // Assigns each text to its entry in the canvas
        m_EntityInventoryPanel = new Dictionary<CResourceBehaviour.EResources, Text>();
        foreach (Text t in m_UIPanel.GetComponentsInChildren<Text>())
        {
            if (t.name == "UraniumEntry")
            {
                m_EntityInventoryPanel.Add(CResourceBehaviour.EResources.UraniumOre, t);
            }
            else if (t.name == "IronEntry")
            {
                m_EntityInventoryPanel.Add(CResourceBehaviour.EResources.IronOre, t);
            }
            else if (t.name == "CopperEntry")
            {
                m_EntityInventoryPanel.Add(CResourceBehaviour.EResources.CopperOre, t);
            }
            else if (t.name == "RealEstateEntry")
            {
                m_EntityInventoryPanel.Add(CResourceBehaviour.EResources.RealEstate, t);
            }
            else if (t.name == "EnergyEntry")
            {
                m_EntityInventoryPanel.Add(CResourceBehaviour.EResources.Energy, t);
            }
            else if (t.name == "CreditsEntry")
            {
                m_CreditsEntry = t;
            }
        }

    }

    private void Update()
    {
        // Updates text entry in the canvas
        foreach (CResourceBehaviour.EResources resource in m_EntityInventoryPanel.Keys)
        {
            m_EntityInventoryPanel[resource].text = m_EntityInventory[resource].ToString("0");
        }
        m_CreditsEntry.text = m_Credits.ToString("0");
    }

    // Adds amount to inventory
    public void AddToInventory(CResourceBehaviour.EResources resource, double amount)
    {
        m_EntityInventory[resource] += amount;
    }

    // Buy/Sell
    public bool makeOperation(CMarketBehaviour.EMarketOperations operation, CResourceBehaviour.EResources resource, double amount)
    {
        bool returnStatus = false;
        switch (operation)
        {
            case CMarketBehaviour.EMarketOperations.Buy:
                if (m_Credits - m_MarketController.GetMarketBehaviour().TryBuy(resource, amount) >= 0)
                {
                    m_Credits -= m_MarketController.GetMarketBehaviour().Buy(resource, amount);
                    m_EntityInventory[resource] += amount;
                    returnStatus = true;
                }
                break;
            case CMarketBehaviour.EMarketOperations.Sell:
                if (m_EntityInventory[resource] - amount >= 0)
                {
                    m_Credits += m_MarketController.GetMarketBehaviour().Sell(resource, amount);
                    m_EntityInventory[resource] -= amount;
                    returnStatus = true;
                }
                break;
        }

        return returnStatus;
    }
}
