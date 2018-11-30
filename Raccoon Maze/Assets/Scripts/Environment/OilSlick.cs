using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSlick : MonoBehaviour
{

	public Material OnFireMaterial;
	private Collider2D[] collisions;
	private bool _onFire;
	
	// Use this for initialization
	void Start ()
	{
		_onFire = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	private void OnParticleCollision(GameObject other)
	{
		if (CompareTag("OilSlickFire")) return;
		if (!other.CompareTag("Explosion")) return;
		_onFire = true;
		GetComponent<Renderer>().material = OnFireMaterial;
		gameObject.layer = 13;
		tag = "OilSlickFire";
	}
}
