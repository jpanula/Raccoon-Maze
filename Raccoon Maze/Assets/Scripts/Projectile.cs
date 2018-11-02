using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public ParticleSystem FireballParticle;
    public ParticleSystem Explosion;
    private ParticleSystem _ps;
    public string Owner;
    private float _emissionTimer;
    private string[] _nonCollidingTags = {"Path", "DeepPuddle", "SpikeTrap", "SpikeTrapActive", "OilSlick", "OilSlickFire"};

    private void Start()
    {
        //var em = FireballParticle.emission;
        //em.enabled = false;
        //_emissionTimer = 0;
    }

    public ParticleSystem GetPs()
    {
        return _ps;
    }
    private void Update()
    {
        /*if(_emissionTimer < 0.07f)
        {
            _emissionTimer += Time.deltaTime;
        }
        else
        {
            var em = FireballParticle.emission;
            em.enabled = true;
        }
        */
        if(_ps != null)
        {
            Debug.Log("moi");
            if (!_ps.IsAlive())
            {
                Debug.Log("hei");
                Destroy(_ps.gameObject);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag(Owner) && !_nonCollidingTags.Contains(col.tag))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            var em = FireballParticle.emission;
            em.enabled = false;
            if (_ps == null)
            {
                _ps = Instantiate(Explosion, transform.position, Quaternion.identity);
            }
        }

    }
}
