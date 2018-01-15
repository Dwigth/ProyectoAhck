using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class PooledObject {
	
	public GameObject[] m_PooledObjects;
	public GameObject m_ObjectToPool;
	public Transform m_Container;
	public int m_AmountToPool;
	public int m_AmountCount;

	public void Init(){
		
		m_PooledObjects = new GameObject[m_AmountToPool];
		for (int i = 0; i < m_PooledObjects.Length; i++) {
			m_PooledObjects [i] = GameObject.Instantiate (m_ObjectToPool);
			m_PooledObjects [i].transform.parent = m_Container;
			m_PooledObjects [i].SetActive (false);
		}
		m_AmountCount = m_PooledObjects.Length;
	}

	public GameObject GetPooledObject(){

		for (int i = 0; i < m_PooledObjects.Length; i++) {
			if (!m_PooledObjects[i].activeInHierarchy) {
				return m_PooledObjects [i];
			}
		}
		return null;
	}

	public void OnUpdate(){
		if (m_AmountCount < 0) {
			m_AmountCount = m_AmountToPool;
		}
	}
}

