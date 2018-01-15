using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

	public Player m_Player;
	public InventoryHandler m_InventoryHandler;
	// Use this for initialization
	void Start () {
		m_InventoryHandler.Init ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
