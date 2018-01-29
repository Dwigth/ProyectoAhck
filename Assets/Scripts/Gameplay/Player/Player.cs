using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Player : MonoBehaviour  {
	
	[Header("Player settings")]
	public float m_Life = 100f;
	public int m_ArmatureType;
	public int m_CurrentArmature;
	public int m_WeaponType;
	public int m_CurrentWeapon;
	public int m_CanonType;
	public int m_CurrentCanon;
	public int m_DamagePoints;
	public int m_Attack;
	public Attack m_AhckAttack;
	public Weapon m_AhckWeapon;
	public Transform m_ShootsOutPos;
	public Transform m_AttacksOutPos;
	public GameObject m_Ahck;
	public LayerMask m_WhatIsGround;
	public Transform m_GroundCheck;
	public Transform m_HandWeapon;
	public Transform m_Canon;
	public PooledObject m_Bullets;
	public PooledObject m_Attacks;

	private float m_ShootForce = 5000f;
	private int m_AreaAffectedRange;
	private float m_Stamina;
	private int m_MovementVelocity = 50;
	private float m_JumpForce = 2000f;
	private Animator m_AnimatorController;
	private Rigidbody2D m_RigidBody2D;
	private bool m_FacingRight = true;
	private bool m_Grounded = false;
	private float m_GroundRadius = 0.3f;
	// Use this for initialization
	void Start () {
		Init ();
	}
	
	// Update is called once per frame
	void Update () {
		OnUpdate ();
	}

	private void Init(){
		m_AnimatorController = m_Ahck.GetComponent<Animator> ();
		m_RigidBody2D = GetComponent<Rigidbody2D> ();
		m_AhckWeapon.m_WeaponType = m_WeaponType;
		m_Bullets.Init ();

		//m_Attacks.Init ();

	}

	private void OnUpdate(){
		bool jump = Input.GetKeyDown (KeyCode.Space);
		bool attack = Input.GetKeyDown (KeyCode.Mouse0);
		bool aim = Input.GetKey (KeyCode.Q);
		bool shoot = Input.GetKeyDown (KeyCode.Mouse1);
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		m_Bullets.OnUpdate ();
		m_Attacks.OnUpdate ();
		float move = Input.GetAxis ("Horizontal");

		Move (move,jump);
		Aim (aim);
		PrimaryAttack (attack);
		if (aim) {
			RotateCanon (mousePos);
			Shoot (shoot,mousePos);
		}
	}

	private void Move(float move, bool jump){
		
		Jump (jump);
		m_RigidBody2D.velocity = new Vector2 (move * m_MovementVelocity, m_RigidBody2D.velocity.y);
		m_AnimatorController.SetFloat ("Move", Mathf.Abs(move));

		if (move > 0 && !m_FacingRight) {
			Flip ();
		}else if (move < 0 && m_FacingRight) {
			Flip ();
		}


	}

	private void Jump(bool jump){
		//se reestablece la escala de la gravedad cada que salta.
		m_RigidBody2D.gravityScale = 10;
		//Se obtiene el estado de su posición si está en el suelo o no.
		m_Grounded = Physics2D.OverlapCircle (m_GroundCheck.position, m_GroundRadius, m_WhatIsGround);
		//Se mandan los resultados al animator
		m_AnimatorController.SetBool ("Grounded", m_Grounded);
		m_AnimatorController.SetFloat ("vSpeed", m_RigidBody2D.velocity.y);

		//Si no está en el suelo 
		if (!m_Grounded) {
			/**
			Si su velocidad en el eje "y" es mayor a cero que se incremente la escala de la gravedad
			**/
			if (m_RigidBody2D.velocity.y < 0) {
				m_RigidBody2D.gravityScale += (m_RigidBody2D.velocity.y * -1) / 3;

				if (m_RigidBody2D.gravityScale > 215) {
					m_RigidBody2D.gravityScale = 215;
				}
			}
		}


		if (m_Grounded && jump) {
			m_AnimatorController.SetBool ("Grounded", false);
			m_RigidBody2D.AddForce (new Vector2 (0, m_JumpForce));

		}

	}



	private void PrimaryAttack(bool attack){
		
			m_AnimatorController.SetBool ("Attack", attack);
			
		float random = UnityEngine.Random.Range (1, 4);
		if (attack) {
			m_AnimatorController.SetFloat("RandomAttack",Mathf.Abs(random));

			GameObject AttackPrefabs = m_Attacks.GetPooledObject ();

			if (AttackPrefabs != null) {
				m_Attacks.m_AmountCount -= 1;
				AttackPrefabs.transform.position = m_AttacksOutPos.position;
				AttackPrefabs.SetActive (true);
				if (!m_FacingRight) {
					AttackPrefabs.GetComponent<Rigidbody2D> ().AddForce (-m_AttacksOutPos.right * m_ShootForce);
					AttackPrefabs.transform.localScale *= -1;
				} else {
					AttackPrefabs.GetComponent<Rigidbody2D> ().AddForce (m_AttacksOutPos.right * m_ShootForce);
				}
				//AttackPrefabs.transform.rotation = Quaternion.LookRotation (Vector3.forward, mousePos - AttackPrefabs.transform.position);
			}
			if (m_Attacks.m_AmountCount <= 0) {
				for (int i = 0; i < m_Attacks.m_AmountToPool; i++) {
					m_Attacks.m_PooledObjects [i].SetActive (false);
				}
			}
		}


	}

	private void Die(){
		
	}

	private void Shoot(bool shoot, Vector3 mousePos){
		if (shoot) {
			GameObject bullet = m_Bullets.GetPooledObject ();

			if (bullet != null) {
				m_Bullets.m_AmountCount -= 1;
				bullet.transform.position = m_ShootsOutPos.position;
				bullet.SetActive (true);

				if (!m_FacingRight) {
					bullet.GetComponent<Rigidbody2D> ().AddForce (-m_ShootsOutPos.right * m_ShootForce);
					bullet.transform.localScale *= -1;
				} else {
					bullet.GetComponent<Rigidbody2D> ().AddForce (m_ShootsOutPos.right * m_ShootForce);
				}
				bullet.transform.rotation = Quaternion.LookRotation (Vector3.forward, mousePos - bullet.transform.position);

			}

			if (m_Bullets.m_AmountCount <= 0) {
				for (int i = 0; i < m_Bullets.m_AmountToPool; i++) {
					m_Bullets.m_PooledObjects [i].SetActive (false);
				}
			}
		}
	}



	private void Aim(bool aim){
		m_AnimatorController.SetBool ("Aim", aim);

	}

	private void RotateCanon(Vector3 mousePos){		
		m_Canon.rotation = Quaternion.LookRotation (Vector3.forward, mousePos - m_Canon.position);
		//Debug.Log (m_Canon.rotation);
	}

	private void Flip(){
		m_FacingRight = !m_FacingRight;

		Vector3 Scale = transform.localScale;

		Scale.x *= -1;

		transform.localScale = Scale;
	}

	void OnTriggerEnter2D(){
	}
}