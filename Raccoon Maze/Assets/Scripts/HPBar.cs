using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HPBar : MonoBehaviour
{

	private int _initialHP;
	private GameObject parent;
	
	// Use this for initialization
	void Start ()
	{
		// Haetaan maksimi HP
		parent = this.transform.parent.gameObject;
		_initialHP = GetParentHP();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Muutetaan vihreän palkin pituutta nykyisen hp-tason mukaan verrattuna maksimiin
		transform.GetChild(0).transform.localScale = new Vector3((float) GetParentHP() / _initialHP, 1, 1);
	}

	private void LateUpdate()
	{
		// Estetään palkin kääntyminen pelaajan kääntyessä
		transform.rotation = Quaternion.identity;
	}

	// Hakee parentin HP:n
	private int GetParentHP()
	{
		if (parent.GetComponent<Player>())
		{
			return parent.GetComponent<Player>().HP;
		}

		return 0;
	}
}
