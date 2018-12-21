using System.Collections.Generic;
using UnityEngine;

public class CMarketBehaviour : MonoBehaviour
{
    // Operations available in the market
    public enum EMarketOperations
    {
        Buy = 0,
        Sell = 1,
        NUM_OF_OPTIONS = 2
    };

    // Number of resources
    private int m_NumOfResources = (int)CResourceBehaviour.EResources.TOTAL_NUM_OF_RESOURCES;
    // Dictionary with the price of each resource
    Dictionary<CResourceBehaviour.EResources, double> m_ResourcePrices;

    public void
        Start()
    {
        // Should be somewhat random
        double startingPrice = 500;

        // Starts the market
        m_ResourcePrices = new Dictionary<CResourceBehaviour.EResources, double>();
        for ( int i = 0; i < m_NumOfResources; i++ )
        {
            if ( (CResourceBehaviour.EResources)i != CResourceBehaviour.EResources.NUM_OF_MINERAL_RESOURCES )
            {
                m_ResourcePrices.Add((CResourceBehaviour.EResources)i, startingPrice);
            }
        }
    }

    /// <summary>
    /// Buy operation.
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="amount"></param>
    /// <returns>Cost of the operation</returns>
    public double
        Buy( CResourceBehaviour.EResources resource, double amount )
    {
        double costValue = m_ResourcePrices[resource] * amount;
        UpdatePrice(resource, EMarketOperations.Buy, amount);

        return costValue;
    }

    public double
        TryBuy(CResourceBehaviour.EResources resource, double amount)
    {
        double costValue = m_ResourcePrices[resource] * amount;

        return costValue;
    }

    /// <summary>
    /// Sell operation.
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="amount"></param>
    /// <returns>Amount earned with the sell</returns>
    public double
        Sell( CResourceBehaviour.EResources resource, double amount )
    {
        double sellValue = m_ResourcePrices[resource] * amount;
        UpdatePrice(resource, EMarketOperations.Sell, amount);

        return sellValue;
    }

    public double
        TrySell(CResourceBehaviour.EResources resource, double amount)
    {
        double sellValue = m_ResourcePrices[resource] * amount;

        return sellValue;
    }

    /// <summary>
    /// Updates price based on the operation and amount.
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="operation"></param>
    /// <param name="amount"></param>
    private void
        UpdatePrice( CResourceBehaviour.EResources resource, EMarketOperations operation, double amount )
    {
        // If demand goes up the price goes up
        if ( operation == EMarketOperations.Buy )
        {
            m_ResourcePrices[resource] += 1.1 * amount;
        }
        // If supply increases the price decreases
        else if ( operation == EMarketOperations.Sell )
        {
            m_ResourcePrices[resource] -= 0.9 * amount;
        }
    }

    /// <summary>
    /// Get market prices.
    /// </summary>
    /// <returns>Market price dictionary</returns>
    public Dictionary<CResourceBehaviour.EResources, double>
        GetPricesDictionary()
    {
        return m_ResourcePrices;
    }
}