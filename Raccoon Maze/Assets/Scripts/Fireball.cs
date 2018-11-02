using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile {

    public ParticleSystem FireballParticle;
    public ParticleSystem Explosion;

    protected override void Hit()
    {
        var em = FireballParticle.emission;
        em.enabled = false;
        if (_ps == null)
        {
            _ps = Instantiate(Explosion, transform.position, Quaternion.identity);
        }
    
    }
}
