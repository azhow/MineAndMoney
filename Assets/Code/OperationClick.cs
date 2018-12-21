using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationClick : MonoBehaviour
{
    // Entity controller
    public GameObject m_EntityController;

    private CEntity m_Entity;
    private GetTileInfo m_TileInfo;

    public CResourceBehaviour.EResources m_Resource;

    public GameObject m_Image;

    private double m_TimeImage = 2;

    private void Start()
    {
        m_TileInfo = m_EntityController.GetComponent<GetTileInfo>();
        m_Entity = m_EntityController.GetComponent<CEntity>();
    }

    public void Buy()
    {
        if (!m_Image.activeSelf)
        {
            m_Image.SetActive(!m_Entity.makeOperation(CMarketBehaviour.EMarketOperations.Buy, m_Resource, 10));
        }
        else
        {
            m_Entity.makeOperation(CMarketBehaviour.EMarketOperations.Buy, m_Resource, 10);
        }
    }

    public void Sell()
    {
        if (!m_Image.activeSelf)
        {
            m_Image.SetActive(!m_Entity.makeOperation(CMarketBehaviour.EMarketOperations.Sell, m_Resource, 10));
        }
        else
        {
            m_Entity.makeOperation(CMarketBehaviour.EMarketOperations.Sell, m_Resource, 10);
        }
    }

    public void BuyDepot()
    {
        if (!m_Image.activeSelf)
        {
            m_Image.SetActive(!m_Entity.makeOperation(CMarketBehaviour.EMarketOperations.Buy, m_Resource, 1));

            m_TileInfo.BuyDepot(m_Entity);
        }
        else
        {
            m_Entity.makeOperation(CMarketBehaviour.EMarketOperations.Buy, m_Resource, 1);
        }
    }

    private void Update()
    {
        if (m_Image.activeSelf)
        {
            m_TimeImage -= Time.deltaTime;
            if (m_TimeImage <= 0)
            {
                m_Image.SetActive(false);
                m_TimeImage = 2;
            }
        }
    }
}
