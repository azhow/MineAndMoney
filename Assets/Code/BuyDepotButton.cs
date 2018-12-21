using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyDepotButton : MonoBehaviour
{
    public GameObject m_GameController;
    public GameObject m_MktController;

    private CEntity m_Entity;
    private GetTileInfo m_TileInfo;
    private CMarketController m_MarketController;

    // Start is called before the first frame update
    void Start()
    {
        m_TileInfo = m_GameController.GetComponent<GetTileInfo>();
        m_Entity = m_GameController.GetComponent<CEntity>();
        m_MarketController = m_MktController.GetComponent<CMarketController>();
    }

    public void buy()
    {
        m_TileInfo.BuyDepot(m_Entity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
