using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_ : MonoBehaviour {

	public CameraFollow m_CameraFollow;
	// Use this for initialization
	void Start () {
		m_CameraFollow.Init ();
	}
	
	// Update is called once per frame
	void Update () {
		m_CameraFollow.OnUpdate ();
	}

}
