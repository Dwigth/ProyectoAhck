using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


	public float m_EnemySpeed;
	public float m_EnemyRadioVision;
	public Transform m_Player; 
	public int m_CurrentLife;
	public Transform Caminar; 	//Direccion a donde que se dirige en modo vigilia
	Vector2 target;//Objetivo cambainte
	

	private Rigidbody2D m_EnemyRB;
	private int m_Life = 100;
	private BoxCollider2D daño;
	private Vector2 m_InitialPos; 
	private Vector3 start, end;

	void Start () 
	{
		
		m_EnemyRB = GetComponent<Rigidbody2D> ();
		m_InitialPos = transform.position;
		m_CurrentLife = m_Life;
		start = transform.position;
		end = Caminar.position; 
		Debug.Log(start);
		

	}
	

	void Update () 
	{
		Vigilia();
		Die ();
	}
		

	void Vigilia()
	{
		Caminar.parent = null;
		m_EnemySpeed = .1f;		
			transform.position = Vector3.MoveTowards(transform.position, Caminar.position, m_EnemySpeed);
			if (transform.position == Caminar.position)
			{
				Caminar.position = (Caminar.position == start) ? end : start;
				transform.position = Vector3.MoveTowards(transform.position, Caminar.position, m_EnemySpeed);
			}		
	}

	void Persecucion (Collider2D Jugador)
	{
		if (Jugador.tag == "Player" )
		{

		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		Persecucion (other);
	}

	

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			Debug.Log("Player Detected");
		}
	}


	private void Daño()
	{

	}

	private void Puntaje ()
	{
		Debug.Log (m_Life);
	}

	void OnDrawGizmos () 
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere (transform.position, m_EnemyRadioVision);
	}

	void Die(){
		if (m_CurrentLife <= 0) {
			transform.gameObject.SetActive (false);
		}
	}

	
	
}
