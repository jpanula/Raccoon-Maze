using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneMissile : Projectile {

    [SerializeField]
    private GameObject _targetingPoint;
    public ParticleSystem Explosion;
    private GameObject target;
    private Rigidbody2D _rb;
    [SerializeField]
    private int _speed;
    private GameObject _player;
    //private Vector3 _direction;
    [SerializeField]
    private int _rotateSpeed;
    private bool _targeting;
    private GameObject _target;
    private PolygonCollider2D _targetCollider;
    

    protected void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _targetCollider = _targetingPoint.GetComponent<PolygonCollider2D>();
    }

    protected override void Start()
    {
        //_player = GameObject.Find("Player2");
        _targeting = true;
        //_target = SearchForTarget();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected void FixedUpdate()
    {
        if (_target != null)
        {
            Vector3 direction = (Vector2)_target.transform.position - _rb.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            _rb.angularVelocity = -rotateAmount * _rotateSpeed * Time.timeScale;
        }

        _rb.velocity = transform.up * _speed * Time.timeScale;
    }

    protected override void Hit()
    {
        var em = _ps.emission;
        em.enabled = false;
        
        if (_explosion == null)
        {
            _explosion = Instantiate(Explosion, transform.position, Quaternion.identity);
        }
        
    }
    /*
    public void SearchForTarget(GameObject obj)
    {
        for(int i = 0; i < 2; i++)
        {
            RaycastHit hit;
            Vector3 rayDirection = playerObject.transform.position - transform.position;

            if ((Vector3.Angle(rayDirection, transform.forward) & lt; fieldOfViewRange) 
            { // Detect if player is within the field of view

                if (Physics.Raycast(transform.position, rayDirection, hit))
                {

                    if (hit.transform.tag == "Player")
                    {
                        //Debug.Log("Can see player");
                    }
                    else
                    {
                        //Debug.Log("Can not see player");
                    }
                }
            }
        }
    }
    */
}
