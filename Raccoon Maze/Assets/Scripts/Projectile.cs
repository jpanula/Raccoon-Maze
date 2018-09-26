using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public ParticleSystem Explosion;
    private ParticleSystem _ps;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(_ps == null)
        {
            _ps = Instantiate(Explosion, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
