using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneMissile : Projectile {

    public ParticleSystem ArcaneParticle;
    private Vector3 _nextDirection;
    private Vector3 _currentDirection;
    private Vector3 _startDirection;
    private float _maxCurve;

    protected override void Start()
    {
        
    }

    protected override void Update()
    {
        base.Update();
        
        if(true)
        {

        }
    }

    protected override void Hit()
    {/*
        var em = FireballParticle.emission;
        em.enabled = false;
        if (_ps == null)
        {
            _ps = Instantiate(Explosion, transform.position, Quaternion.identity);
        }
        */
    }
}
