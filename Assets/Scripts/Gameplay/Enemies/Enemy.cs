using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float m_EnemySpeed;
	public float m_EnemyRadioVision;
	public Transform m_Player; 
	public int m_CurrentLife;
	public Transform Caminar; 	
	public int Estados = 1;


	private Vector3 [] Posiciones = new Vector3[5];
	private Rigidbody2D m_EnemyRB;
	private int m_Life = 100;
	private BoxCollider2D golpe;
	private CircleCollider2D vigia;
	private Vector2 m_InitialPos; 
	//Posiciones[1]=posicion del jugador
	//Posiciones[2]=Ojetivo para el modo IDLE
	//Posiciones[3]=Posicion en la que entro
	//Posiciones[4]=posicion en la que salio



	void Start () 
	{
		
		m_EnemyRB = GetComponent<Rigidbody2D> (); //cuerpo del enemigo
		golpe = GetComponent<BoxCollider2D> ();//Collider para golpes
		vigia = GetComponent<CircleCollider2D> ();//Campo de vision
		m_InitialPos = transform.position;//Posicion incial
		m_CurrentLife = m_Life;//Vida del jugador
		Posiciones [0] = transform.position;
		Posiciones [1] = Caminar.position;
		Estados = 1;


	}
	

	void Update () 
	{
		NumEstados ();
		Die ();

	}

	void NumEstados () //Accion segun el estado
	{
		if (Estados == 1) 
		{
			Vigilia ();
		}

		if (Estados == 2) 
		{
			Persecucion ();
		}

		if (Estados == 3) 
		{
			Busqueda ();
		}
			

	}
		

	void Vigilia() //Estado idle
	{
		Estados = 1;
		Caminar.parent = null;
		m_EnemySpeed = .3f;		
			transform.position = Vector3.MoveTowards(transform.position, Caminar.position, m_EnemySpeed);
			if (transform.position == Caminar.position)
			{
			Caminar.position = (Caminar.position == Posiciones[0]) ? Posiciones[1] : Posiciones[0];
				transform.position = Vector3.MoveTowards(transform.position, Caminar.position, m_EnemySpeed);
			}		
	}

	void Persecucion () //Persecucion
	{
		Estados = 2;
		vigia.radius = 50f;
		Debug.Log ("jUGADOR DETECTADO");
		m_EnemySpeed = .5f;
		transform.position = Vector3.MoveTowards (transform.position, Posiciones[2], m_EnemySpeed);
		//Perseguira al jugador mientras este dentro del radio de vision
			
	}

		void Busqueda () //Busqueda
	{
		Estados = 3;
		vigia.radius = 65f;
		m_EnemySpeed = .4f;
		transform.position = Vector3.MoveTowards (transform.position, Posiciones [4], m_EnemySpeed);
		//Se movera a la ultima posicion del jugador para buscarlo
		if (transform.position == Posiciones [4]) 
		{ 	//Si el enemigo no encunetra de nuevo al jugdor, buscara en las ultimas posiciones y al final regresara al IDLE
			m_EnemySpeed= .2f;
			for (int contador = 0; contador < 11; contador += Time.deltaTime) 
			{
				Debug.Log(contador);
				if (contador = 10)
				{
					transform.position = Vector3.MoveTowards (transform.position, Posiciones [3], m_EnemySpeed);
				}
			}

		
		}
	}
		
	void OnTriggerEnter (Collider2D Entrada)
	{
		Posiciones [3] = Entrada.transform.position;
	}

	void OnTriggerStay2D(Collider2D other)
	{
		//Cambia el estado y guarda la posicion mientras este dentro del campo de vision
		Estados = 2;
		Posiciones [2] = other.transform.position;

	}


	void OnTriggerExit2D (Collider2D Buscar)
	{
		//Cambia de estado y garda la ultima posicion del jugador
		Estados = 3;
		Posiciones [4] = Buscar.transform.position;
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
		Gizmos.DrawWireSphere (transform.position, vigia.radius);
	}

	void Die(){
		if (m_CurrentLife <= 0) {
			transform.gameObject.SetActive (false);
		}
	}

	
	
}
