using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class CameraFollow {
	public Transform m_Target;
	public Transform m_MainCamera;

	private Vector3 m_offset;
	public float m_Distance;
	public float m_Smothness;

	public void Init(){
		m_offset = m_Target.position - m_MainCamera.position;
	}

	public void OnUpdate(){
		Vector3 camerapos = m_Target.position - m_offset * m_Distance;
		m_MainCamera.position = Vector3.Lerp (m_MainCamera.position , camerapos, m_Smothness * Time.deltaTime);
	}
}

