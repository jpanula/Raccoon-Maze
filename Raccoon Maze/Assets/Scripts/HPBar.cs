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
		parent = this.transform.parent.gameObject;
		_initialHP = GetParentHP();
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.GetChild(0).transform.localScale = new Vector3((float) GetParentHP() / _initialHP, 1, 1);
	}

	private void LateUpdate()
	{
		transform.rotation = Quaternion.identity;
	}

	private int GetParentHP()
	{
		if (parent.GetComponent<Player>())
		{
			return parent.GetComponent<Player>().HP;
		}

		return 0;
	}
}
