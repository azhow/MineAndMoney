using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CResourceBehaviour : MonoBehaviour
{
    enum EResources
    {
        uranium = 0,
        iron = 1,
        copper = 2,
        NUM_OF_RESOURCES = 3
    }

    private const int mc_MaxResource = 1000;
    private const int mc_MinResource = 200;

    public double m_DecayAmountPerSecond = 50;
    public bool m_HasBuilding = false;

    private EResources m_Resource;
    private double m_ResourceAmount;

    // Instantiates the tile
    void Start()
    {
        System.Random randomGen = new System.Random();
        m_Resource = (EResources)randomGen.Next((int)EResources.NUM_OF_RESOURCES);
        m_ResourceAmount = UnityEngine.Random.Range(mc_MinResource, mc_MaxResource);
    }

    // Update is called once per frame
    void Update()
    {
        // Only if it is being explored you should decrease deposit amount
        if ( m_HasBuilding )
        {
            //Updates amount of resource
            double newValue = m_ResourceAmount - m_DecayAmountPerSecond * Time.deltaTime;
            // Check if there's enough resources to explore
            if ( newValue >= 0 )
            {
                m_ResourceAmount -= m_DecayAmountPerSecond * Time.deltaTime;
            }
        }
    }

    public void SetResourceAmount( double newValue )
    {
        m_ResourceAmount = newValue;
    }

    public double GetResourceAmount()
    {
        return m_ResourceAmount;
    }
}
