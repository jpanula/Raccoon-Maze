using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour {

    
    protected ParticleSystem _ps;
    public string Owner;
    private string[] _nonCollidingTags = {"Path", "DeepPuddle", "SpikeTrap", "SpikeTrapActive", "OilSlick", "OilSlickFire"};

    protected virtual void Start()
    {
        //var em = FireballParticle.emission;
        //em.enabled = false;
        //_emissionTimer = 0;
    }

    public ParticleSystem GetPs()
    {
        return _ps;
    }

    protected virtual void Update()
    {
        if(_ps != null)
        {
            //Debug.Log("moi");
            if (!_ps.IsAlive())
            {
                //Debug.Log("hei");
                Destroy(_ps.gameObject);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.CompareTag(Owner) + " " + Owner + " " + col.tag);
        if (!col.CompareTag(Owner) && !_nonCollidingTags.Contains(col.tag) && !col.CompareTag("Weapon"))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

            Hit();
        }

    }

    protected virtual void Hit()
    {

    }
}
