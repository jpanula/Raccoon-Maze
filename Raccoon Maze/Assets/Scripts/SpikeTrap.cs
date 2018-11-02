using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{

	public float ActivationTime;
	public float HoldTime;
	public Material SpikeTrapMaterial;
	public Material SpikeTrapActiveMaterial;
	public Material SpikeTrapTriggeredMaterial;
	private bool _active;
	private bool _triggered;
	private float _activationTimer;
	private float _holdTimer;
	private Collider2D[] collisions;
	
	
	// Use this for initialization
	void Start ()
	{
		_triggered = false;
		_active = false;
		_activationTimer = ActivationTime;
		_holdTimer = HoldTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (_triggered)
		{
			GetComponent<Renderer>().material = SpikeTrapTriggeredMaterial;
			if (_activationTimer <= 0)
			{
				_active = true;
				_triggered = false;
				_activationTimer = ActivationTime;
				GetComponent<Renderer>().material = SpikeTrapActiveMaterial;
				collisions = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.localScale.x, transform.localScale.y), 0);
				foreach (Collider2D col in collisions)
				{
					if (col.gameObject.GetComponent<Player>())
					{
						col.gameObject.GetComponent<Player>().HP--;
					}
				}
			}
			_activationTimer -= Time.deltaTime;
		}

		if (_active)
		{
			ActivateTrap(true);
			if (_holdTimer <= 0)
			{
				_active = false;
				_holdTimer = HoldTime;
				ActivateTrap(false);
				GetComponent<Renderer>().material = SpikeTrapMaterial;
			}

			_holdTimer -= Time.deltaTime;
		}
	}

	void ActivateTrap(bool activated)
	{
		if (activated)
		{
			gameObject.tag = "SpikeTrapActive";
		}
		else
		{
			gameObject.tag = "SpikeTrap";
		}
	}

	public void TriggerTrap()
	{
		_triggered = true;
	}
}
