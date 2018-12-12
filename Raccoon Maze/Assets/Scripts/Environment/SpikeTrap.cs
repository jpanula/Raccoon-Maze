using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{

	public float ActivationTime;
	public float HoldTime;
	public Sprite SpikeTrapMaterial;
	public Sprite SpikeTrapActiveMaterial;
	public Sprite SpikeTrapTriggeredMaterial;
	private bool _active;
	private bool _triggered;
	private float _activationTimer;
	private float _holdTimer;
	private Collider2D[] collisions;
    private AudioClip _activationSound;
    private AudioClip _triggerSound;
    private bool _activationBool;
    private bool _triggerBool;
    [SerializeField]
    private SoundLibrary SoundLibrary;
    private AudioManager _am;

    // Use this for initialization
    void Start ()
	{
		_triggered = false;
		_active = false;
		_activationTimer = ActivationTime;
		_holdTimer = HoldTime;
        _am = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        _activationSound = SoundLibrary.SpikeTrapUp;
        _triggerSound = SoundLibrary.SpikeTrapActivation;
        _activationBool = false;
        _triggerBool = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (_triggered)
		{
			GetComponent<SpriteRenderer>().sprite = SpikeTrapTriggeredMaterial;
            if(!_triggerBool)
            {
                _triggerBool = true;
                _am.PlaySound(_triggerSound, false);
            }
            if (_activationTimer <= 0)
			{
                if (!_activationBool)
                {
                    _activationBool = true;
                    _am.PlaySound(_activationSound, false);
                }
                
                _active = true;
				_triggered = false;
				_activationTimer = ActivationTime;
                GetComponent<SpriteRenderer>().sprite = SpikeTrapActiveMaterial;
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
                GetComponent<SpriteRenderer>().sprite = SpikeTrapMaterial;
                _activationBool = false;
                _triggerBool = false;
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
