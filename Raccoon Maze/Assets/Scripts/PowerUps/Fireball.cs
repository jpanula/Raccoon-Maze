using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile {

    public ParticleSystem Explosion;

    protected override void Start()
    {
        base.Start();
        _explosionSound = SoundLibrary.FireballHit;
    }

    protected override void Hit()
    {
        base.Hit();

        var em = _ps.emission;
        em.enabled = false;

        if (_explosion == null)
        {
            _explosion = Instantiate(Explosion, transform.position, Quaternion.identity);
        }
    
    }
}
