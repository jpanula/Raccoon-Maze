using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneMissile : Projectile {

    [SerializeField]
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
    [SerializeField]
    private float _targetRadius;
    [SerializeField]
    private float _targetAngle;


    protected void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();
        //_player = GameObject.Find("Player2");
        _targeting = true;
        
    }

    protected override void Update()
    {
        if (_targeting)
        {
            _targeting = SearchForTarget();
        }
        
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
        base.Hit();

        var em = _ps.emission;
        em.enabled = false;
        
        if (_explosion == null)
        {
            _explosion = Instantiate(Explosion, transform.position, Quaternion.identity);
        }
        
    }
    
    public bool SearchForTarget()
    {
        /*
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
        */
        int layerMask = 1 << 8;
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, _targetRadius, layerMask);
        Vector3 characterToCollider;
        float dot;
        foreach (Collider2D collider in cols)
        {
            
            characterToCollider = (collider.transform.position - transform.position).normalized;
            //Debug.Log(characterToCollider);


            dot = Vector3.Dot(characterToCollider, transform.up);

            //Debug.Log("dot: " + dot + " Cos: " + Mathf.Cos(_targetAngle) + " charToCol: " + characterToCollider + " transform.forward: " + transform.forward);
            if (dot >= Mathf.Cos(_targetAngle))
            {
                //Debug.Log("colldieereja" + collider.gameObject);
                //Debug.Log("colldieereja edessä " + collider.gameObject.GetComponent<Player>().Name + " " + Owner);
                if (collider.gameObject.GetComponent<Player>().Name != Owner)
                {
                    //Debug.Log("Target found! " + collider.gameObject);
                    _target = collider.gameObject;
                    return false;
                }
                
            }
        }
        //Debug.Log("Target not found!");
        return true;
    }
    
}
