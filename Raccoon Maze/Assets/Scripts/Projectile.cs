using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public ParticleSystem FireballParticle;
    public ParticleSystem Explosion;
    private ParticleSystem _ps;

    private void Update()
    {
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
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        var em = FireballParticle.emission;
        em.enabled = false;
        if (_ps == null)
        {
            _ps = Instantiate(Explosion, transform.position, Quaternion.identity);
        }
    }
}
