using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{

	[SerializeField]
	private int _health;

    private List<GameObject> _collidedParticles;

    [SerializeField]

    private Sprite _crackedWall;
    private AudioClip _crackSound;
    private bool _crackBool;
    private AudioClip _destroySound;
    [SerializeField]
    private SoundLibrary SoundLibrary;
    private AudioManager _am;


    // Use this for initialization
    void Start ()
	{
        _collidedParticles = new List<GameObject>();
        _am = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        _crackSound = SoundLibrary.WallCrackles;
        _destroySound = SoundLibrary.WallWeaponHit;
        _crackBool = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (_health <= 0)
		{
            _am.PlaySound(_destroySound, false);
            Destroy(gameObject);
		}
        else if(_health == 1)
        {
            GetComponent<SpriteRenderer>().sprite = _crackedWall;
            if(!_crackBool)
            {
                _am.PlaySound(_crackSound, false);
                _crackBool = true;
            }
        }
	}

    /*
	// Ottaa osuman
	private void OnTriggerEnter2D(Collider2D other)
	{
		// Ottaa osuman räjähdyksestä
		if (other.CompareTag("Explosion"))
		{
            if(_collidedOnce == null)
            {
                Debug.Log(other);
                _health--;
                _collidedOnce = other.gameObject;
            }
            else
            {
                _collidedOnce = null;
            }
        }
	}
    */

    void OnParticleCollision(GameObject other)
    {
        bool sameExplosion = false;
        for (int i = 0; i < _collidedParticles.Count; i++)
        {
            if (_collidedParticles[i].gameObject == other.gameObject)
            {
                sameExplosion = true;
                //Debug.Log(other + " " + _collidedParticles[i]);
            }
        }
        if (other.gameObject.CompareTag("Explosion") && !sameExplosion)
        {
            _health--;
        }
        _collidedParticles.Add(other.gameObject);
    }
}
