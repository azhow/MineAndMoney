using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

    public EventSystem m_eventSystem;
    public GameObject m_selectedGameObject;

    private bool m_buttonSelected;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void 
        Update ()
    {
		if(Input.GetAxisRaw("Vertical") != 0 && !m_buttonSelected)
        {
            m_eventSystem.SetSelectedGameObject(m_selectedGameObject);
            m_buttonSelected = true;
        }
	}

    private void 
        OnDisable()
    {
        m_buttonSelected = false;        
    }
}
