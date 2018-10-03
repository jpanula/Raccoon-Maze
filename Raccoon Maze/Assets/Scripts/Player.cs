using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int PlayerNumber;
    public int Speed;
    public Vector3 Move;
    public int _direction;
    private Vector3 _directionVector;
    public GameObject Mora;
    private float _attackTimer;
    private float _ability1Timer;
    [SerializeField]
    private float _ability1Cooldown;
    private float _ability2Timer;
    [SerializeField]
    private float _ability2Cooldown;
    private Vector3 _moraPosition;
    private bool _attack;
    private bool _inputLock;
    private Rigidbody2D _rb;
    public BoxCollider2D MoraCollider;
    public int HP;
    private GameObject _spawnedProjectile;
    public GameObject Projectile;
    public GameObject SpawnPoint;
    private List<GameObject> _collidedParticles;
    private float _intercardinalDirTimer;
    private Vector3 _intercardinalDir;
    public string Name;

    // Use this for initialization
    void Start ()
    {
        _direction = 0;
        _attackTimer = 0;
        _intercardinalDirTimer = 0;
        _ability1Timer = _ability1Cooldown;
        _ability2Timer = _ability2Cooldown;
        _moraPosition = Mora.transform.position;
        _attack = false;
        _inputLock = false;
        _intercardinalDir = new Vector3(0,0,0);
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _collidedParticles = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        _rb.velocity = new Vector3(0, 0, 0);
        if(HP <= 0)
        {
            Destroy(gameObject);
        }
        if(_attackTimer < 0.2f)
        {
            _attackTimer += Time.deltaTime;
        }
        else
        {
            if(_attack)
            {
                Mora.transform.localPosition = new Vector3(0, 0.3f, 0);
                Mora.GetComponent<BoxCollider2D>().enabled = false;
                _attack = false;
                _inputLock = false;
            }
        }
        if (_intercardinalDirTimer < 0.1f)
        {
            _intercardinalDirTimer += Time.deltaTime;
        }
        else
        {
            _intercardinalDir = new Vector3(0,0,0);
        }

        if (_ability1Timer < _ability1Cooldown)
        {
            _ability1Timer += Time.deltaTime;
        }

        if (_ability2Timer < _ability2Cooldown)
        {
            _ability2Timer += Time.deltaTime;
        }

        if (!_inputLock)
        {
            if (Input.GetAxisRaw("P" + PlayerNumber + "H") < 0)
            {
                Move = new Vector3(-1f, Move.y, 0);
            }
            else if (Input.GetAxisRaw("P" + PlayerNumber + "H") > 0)
            {
                Move = new Vector3(1f, Move.y, 0);
            }
            else
            {
                Move = new Vector3(0, Move.y, 0);
            }

            if (Input.GetAxisRaw("P" + PlayerNumber + "V") < 0)
            {
                Move = new Vector3(Move.x, -1f, 0);
            }
            else if (Input.GetAxisRaw("P" + PlayerNumber + "V") > 0)
            {
                Move = new Vector3(Move.x, 1f, 0);
            }
            else
            {
                Move = new Vector3(Move.x, 0, 0);
            }

            if(Move.x != 0 && Move.y != 0)
            {
                Move = new Vector3(Move.x * 0.75f, Move.y * 0.75f, 0);
                _intercardinalDirTimer = 0;
                _intercardinalDir = Move;
                //Debug.Log(_intercardinalDir.x + " " + _intercardinalDir.y);
            }
            /*
            else if(Move.x == 0 && Move.y == 0)
            {
                Debug.Log(Move);
                CheckIntercardinalDirections();
            }
            */
            UpdateDirection();
            transform.position += Move * Speed * Time.deltaTime;
            if (Input.GetButton("P" + PlayerNumber + "Backward") && !_inputLock && _ability2Timer >= _ability2Cooldown)
            {
                //Debug.Log("taakke");
                if(Move != Vector3.zero)
                {
                    _directionVector = _directionVector * -1f;
                }
                Move = Move * -1f;
                UpdateDirection();
            }
        }

        if(Input.GetButtonDown("P" + PlayerNumber + "Attack") && !_inputLock)
        {
            Attack();
        }

        if (Input.GetButtonDown("P" + PlayerNumber + "Ability1") && !_inputLock && _ability1Timer >= _ability1Cooldown)
        {
            Ability1();
        }
        if (Input.GetButtonDown("P" + PlayerNumber + "Ability2") && !_inputLock && _ability2Timer >= _ability2Cooldown)
        {
            Ability2();
        }
        

        //Destroy(_blinkTrail);

    }
    public void UpdateDirection()
    {
        if (Move.x > 0)
        {
            if(Move.y > 0)
            {
                _direction = 7;
                _directionVector = Move;
            }
            else if (Move.y < 0)
            {
                _direction = 5;
                _directionVector = Move;
            }
            else
            {
                _direction = 6;
                _directionVector = Move;
            }
        }
        else if(Move.x < 0)
        {
            if (Move.y > 0)
            {
                _direction = 1;
                _directionVector = Move;
            }
            else if (Move.y < 0)
            {
                _direction = 3;
                _directionVector = Move;
            }
            else
            {
                _direction = 2;
                _directionVector = Move;
            }
        }
        else if (Move.y > 0)
        {
            _direction = 0;
            _directionVector = Move;
        }
        else if (Move.y < 0)
        {
            _direction = 4;
            _directionVector = Move;
        }

        transform.eulerAngles = new Vector3(0, 0, 45 * _direction);
    }

    /*public void CheckIntercardinalDirections()
    {
        if (_intercardinalDir.x != 0 && _intercardinalDir.y != 0)
        {
            if (_intercardinalDir.x > 0)
            {
                if (_intercardinalDir.y > 0)
                {
                    _direction = 7;
                    _directionVector = _intercardinalDir;
                }
                else if (_intercardinalDir.y < 0)
                {
                    _direction = 5;
                    _directionVector = _intercardinalDir;
                }
            }
            else if (_intercardinalDir.x < 0)
            {
                if (_intercardinalDir.y > 0)
                {
                    _direction = 1;
                    _directionVector = _intercardinalDir;
                }
                else if (_intercardinalDir.y < 0)
                {
                    _direction = 3;
                    _directionVector = _intercardinalDir;
                }
            }
        }
    }
    */



    public void Attack()
    {
        Mora.GetComponent<BoxCollider2D>().enabled = true;
        Mora.transform.localPosition = new Vector3(0, 1, 0);
        _attackTimer = 0;
        _attack = true;
        _inputLock = true;
    }

    public void Ability1()
    {
        _spawnedProjectile = Instantiate(Projectile, SpawnPoint.transform.position, Quaternion.identity);
        _spawnedProjectile.GetComponent<Projectile>().Owner = Name;
        _spawnedProjectile.GetComponent<Rigidbody2D>().AddForce(_directionVector * 20, ForceMode2D.Impulse);
        _ability1Timer = 0;
    }
    public void Ability2()
    {
        Debug.Log("Ability2");
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 11;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _directionVector, 5f, layerMask);
        if (hit.collider != null)
        {
            transform.position += _directionVector * (hit.distance * 0.9f);
        }
        else
        {
            transform.position += _directionVector * 5;
        }
        _ability2Timer = 0;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Weapon"))
        {
            if(col.gameObject != Mora)
            {
                //Destroy(gameObject);
                //_rb.AddForce(new Vector2(100, 0));
                transform.position =transform.position + (transform.position - col.transform.position).normalized * 0.7f;
                HP = 0;
                //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            }
        }
        if (col.CompareTag("Projectile"))
        {
            Debug.Log("Tiili");
            if (col.gameObject != _spawnedProjectile)
            {
                Debug.Log("Osuma");
                //Destroy(gameObject);
                //_rb.AddForce(new Vector2(100, 0));
                transform.position = transform.position + (transform.position - col.transform.position).normalized * 0.5f;
                //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            }
        }
    }

    void OnParticleCollision(GameObject other)
    {
        bool sameExplosion = false;
        for(int i = 0; i < _collidedParticles.Count; i++)
        {
            if (_collidedParticles[i].gameObject == other.gameObject)
            {
                sameExplosion = true;
                Debug.Log(other + " " + _collidedParticles[i]);
            }
        }
        Debug.Log("räjähdys");
        if (other.gameObject.CompareTag("Explosion") && !sameExplosion)
        {
            HP--;
            _collidedParticles.Add(other.gameObject);
        }
    }

    public float[] GetAbilityTimers()
    {
        float ability1TimeLeft = _ability1Cooldown - _ability1Timer;
        float ability2TimeLeft = _ability2Cooldown - _ability2Timer;
        float ability1TimeLeftPercentage = _ability1Timer / _ability1Cooldown;
        float ability2TimeLeftPercentage = _ability2Timer / _ability2Cooldown;
        return new float[] {ability1TimeLeft, ability2TimeLeft, ability1TimeLeftPercentage, ability2TimeLeftPercentage};
    }
}
