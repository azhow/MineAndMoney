using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEntityController : MonoBehaviour
{
    // Entity
    private CEntity m_Entity;

    // Start is called before the first frame update
    void Start()
    {
        m_Entity = GetComponentInParent<CEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public CEntity GetEntity()
    {
        return m_Entity;
    }
}
