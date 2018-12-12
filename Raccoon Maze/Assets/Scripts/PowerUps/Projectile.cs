using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    protected ParticleSystem _ps;

    private float _particleTimer;

    [SerializeField]
    private float _particleDestructionTime;

    protected ParticleSystem _explosion;
    public string Owner;
    private string[] _nonCollidingTags = {"Path", "DeepPuddle", "SpikeTrap", "SpikeTrapActive", "OilSlick", "OilSlickFire"};

    protected AudioClip _explosionSound;
    [SerializeField]
    protected SoundLibrary SoundLibrary;
    protected AudioManager _am;

    protected virtual void Start()
    {
        _particleTimer = -1f;
        _am = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        //var em = FireballParticle.emission;
        //em.enabled = false;
        //_emissionTimer = 0;
    }

    public ParticleSystem GetPs()
    {
        return _ps;
    }

    public ParticleSystem GetExplosion()
    {
        return _explosion;
    }

    protected virtual void Update()
    {
        //Debug.Log("update");
        if(_ps != null)
        {
            if (_particleTimer > _particleDestructionTime)
            {
                //Debug.Log("hei");
                Destroy(_explosion.gameObject);
                Destroy(gameObject);
            }
            else if(_particleTimer >= 0)
            {
                _particleTimer += Time.deltaTime;
            }
            
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.CompareTag(Owner) + " " + Owner + " " + col.tag + " " + col);
        if (!col.CompareTag(Owner) && !_nonCollidingTags.Contains(col.tag) && !col.CompareTag("Weapon"))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

            Hit();
        }

    }

    protected virtual void Hit()
    {
        _particleTimer = 0;
        if (_explosion == null)
        {
            _am.PlaySound(_explosionSound, false);
        }
    }
}
