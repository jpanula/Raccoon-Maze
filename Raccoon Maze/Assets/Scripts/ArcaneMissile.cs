using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneMissile : Projectile {

    public ParticleSystem ArcaneParticle;
    private Vector3 _nextDirection;
    private Vector3 _currentDirection;
    private Vector3 _startDirection;
    private float _maxCurve;
    private GameObject target;
    private Rigidbody2D _rb;
    [SerializeField]
    private int _speed;
    private GameObject _player;
    //private Vector3 _direction;
    [SerializeField]
    private int _rotateSpeed;

    protected void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        _player = GameObject.Find("Player2");
    }

    protected override void Update()
    {
        base.Update();
    }

    protected void FixedUpdate()
    {
        Vector3 direction = (Vector2)_player.transform.position - _rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        _rb.angularVelocity = -rotateAmount * _rotateSpeed * Time.timeScale;

        _rb.velocity = transform.up * _speed * Time.timeScale;
    }

    protected override void Hit()
    {
        var em = ArcaneParticle.emission;
        em.enabled = false;
        /*
        if (_ps == null)
        {
            _ps = Instantiate(Explosion, transform.position, Quaternion.identity);
        }
        */
    }
}
