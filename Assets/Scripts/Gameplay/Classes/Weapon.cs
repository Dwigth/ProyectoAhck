using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Weapon {
	public Sprite m_SpriteRenderer;
	public string m_WeaponName;
	public int m_WeaponType;
	public Bullet m_Bullet;
	public AudioClip m_WeaponSound;
}
