using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;


[RequireComponent(typeof (Animator))]
[Serializable]
public class AhckAnimatorController : MonoBehaviour {
	
	public SpriteRenderer m_SpriteRenderer;
	public AnimationClip[] m_Animations;

	private int s_Min = 0;
	private int m_Max;
	private int m_Count;
	private Animator m_AhckAnimator;
	
	void Start(){
		Init ();
	}

	void Update(){
		ChangeAnimation ();
	}

	void ChangeAnimation(){
		bool left = Input.GetKeyDown (KeyCode.D);
		bool rigth = Input.GetKeyDown (KeyCode.A);

		NextAnimation(left,rigth);

		if (m_Count > m_Max) {
			m_Count = s_Min;
		}
		if(m_Count < s_Min){
			m_Count = m_Max;
		}

		PlayAnimation ();
		 
	}

	void Init(){
		m_Max = m_Animations.Length - 1;
		m_AhckAnimator = GetComponent<Animator> ();
	}

	void NextAnimation(bool l, bool r){
		if (l) {
			m_Count--;
		}
		if (r) {
			m_Count++;
		}

	}

	void PlayAnimation(){
		m_AhckAnimator.Play (m_Animations[m_Count].name);

	}
}