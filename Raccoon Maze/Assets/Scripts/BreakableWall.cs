using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{

	[SerializeField]
	private int _health;
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
		if (_health <= 0)
		{
			Destroy(gameObject);
		}
	}

	// Ottaa osuman
	private void OnTriggerEnter2D(Collider2D other)
	{
		// Ottaa osuman räjähdyksestä
		if (other.CompareTag("Explosion"))
		{
			_health--;
		}
	}
}
