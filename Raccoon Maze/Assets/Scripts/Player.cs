using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameManager Gm;
    public int PlayerNumber;
    public int Speed;
    private int _initialSpeed;
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
    public string Name;
    private bool _directionLock;
    private GameObject _projectileHit;
    private bool _gamepadControl;
    public bool Invulnerable;

    // Use this for initialization

    private void Awake()
    {
        _initialSpeed = Speed;
        _attackTimer = 0;
        _ability1Timer = _ability1Cooldown;
        _ability2Timer = _ability2Cooldown;
        _directionLock = false;
        _attack = false;
        _inputLock = false;
        _gamepadControl = false;
        Invulnerable = false;
    }

    private void Start ()
    {
        _direction = Mathf.RoundToInt(transform.rotation.z / 45);
        _moraPosition = Mora.transform.position;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _collidedParticles = new List<GameObject>();
        InitializeDirectionVector();
    }
	
	// Update is called once per frame
	private void Update ()
    {
        _rb.velocity = new Vector3(0, 0, 0);
        if(HP <= 0)
        {
            Gm.KillPlayer(gameObject);
            gameObject.SetActive(false);
            //Destroy(gameObject);
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

        if (_ability1Timer < _ability1Cooldown)
        {
            _ability1Timer += Time.deltaTime;
        }

        if (_ability2Timer < _ability2Cooldown)
        {
            _ability2Timer += Time.deltaTime;
        }
        InputManager.CheckGamepadConnection();
        if (InputManager.GetGamepadControl(PlayerNumber) >= 0)
        {
            _gamepadControl = true;
        }
        else
        {
            _gamepadControl = false;
        }
        //Debug.Log("P" + PlayerNumber + (_gamepadControl));
        if (!_inputLock)
        {
            /*
            if (InputManager.GetKey("P" + PlayerNumber + "H") < 0)
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
            */

            //Input.GetJoystickNames().Length >= PlayerNumber
            if (InputManager.GetKey("P" + PlayerNumber + "Left", _gamepadControl, PlayerNumber.ToString()))
            {
                Move = new Vector3(-1f, Move.y, 0);
            }
            else if (InputManager.GetKey("P" + PlayerNumber + "Right", _gamepadControl, PlayerNumber.ToString()))
            {
                Move = new Vector3(1f, Move.y, 0);
            }
            else
            {
                    Move = new Vector3(0, Move.y, 0);
            }
            if (InputManager.GetKey("P" + PlayerNumber + "Up", _gamepadControl, PlayerNumber.ToString()))
            {
                Move = new Vector3(Move.x, 1f, 0);
            }
            else if (InputManager.GetKey("P" + PlayerNumber + "Down", _gamepadControl, PlayerNumber.ToString()))
            {
                Move = new Vector3(Move.x, -1f, 0);
            }
            else
            {
                Move = new Vector3(Move.x, 0, 0);
            }

            if (Move.x != 0 && Move.y != 0)
            {
                Move = new Vector3(Move.x * 0.75f, Move.y * 0.75f, 0);
                //Debug.Log(_intercardinalDir.x + " " + _intercardinalDir.y);
            }
            /*
            else if(Move.x == 0 && Move.y == 0)
            {
                Debug.Log(Move);
                CheckIntercardinalDirections();
            }
            */
            if (!_directionLock)
            {
                UpdateDirection();
            }
            
            transform.position += Move * Speed * Time.deltaTime;
            if (InputManager.GetKeyDown("P" + PlayerNumber + "DirLock", _gamepadControl, PlayerNumber.ToString()))
            {
                //Debug.Log("taakke");

                _directionLock = true;
            }
            if (InputManager.GetKeyUp("P" + PlayerNumber + "DirLock", _gamepadControl, PlayerNumber.ToString()))
            {
                //Debug.Log("taakke");

                _directionLock = false;
            }
        }
        if (!_inputLock)
        {
            if (InputManager.GetKeyDown("P" + PlayerNumber + "Melee", _gamepadControl, PlayerNumber.ToString()))
            {
                Attack();
            }
            if (InputManager.GetKeyDown("P" + PlayerNumber + "Ability1", _gamepadControl, PlayerNumber.ToString()) && _ability1Timer >= _ability1Cooldown)
            {
                Ability1();
            }
            if (InputManager.GetKeyDown("P" + PlayerNumber + "Ability2", _gamepadControl, PlayerNumber.ToString()) && _ability2Timer >= _ability2Cooldown)
            {
                Ability2();
            }
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

    private void InitializeDirectionVector()
    {
        if(_direction == 0)
        {
            _directionVector = new Vector3(0, 1);
        }
        else if (_direction == 1)
        {
            _directionVector = new Vector3(-1, 1);
        }
        else if (_direction == 2)
        {
            _directionVector = new Vector3(-1, 0);
        }
        else if (_direction == 3)
        {
            _directionVector = new Vector3(-1, -1);
        }
        else if (_direction == 4)
        {
            _directionVector = new Vector3(0, -1);
        }
        else if (_direction == 5)
        {
            _directionVector = new Vector3(1, -1);
        }
        else if (_direction == 6)
        {
            _directionVector = new Vector3(1, 0);
        }
        else if (_direction == 7)
        {
            _directionVector = new Vector3(1, 1);
        }
    }

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
        _spawnedProjectile = Instantiate(Projectile, SpawnPoint.transform.position, Quaternion.Euler(new Vector3(0, 0, 45 * _direction)));
        _spawnedProjectile.GetComponent<Projectile>().Owner = Name;
        _spawnedProjectile.GetComponent<Rigidbody2D>().AddForce(_directionVector * 15, ForceMode2D.Impulse);
        _ability1Timer = 0;
    }
    /*
    public void Ability1()
    {
        GameObject spinHitBox = new GameObject();
        spinHitBox.name = "SpinHitbox";
        spinHitBox.transform.SetParent(transform);
        spinHitBox.transform.localPosition = new Vector3(0, 0, 0);
        spinHitBox.AddComponent<CircleCollider2D>().radius = 0.75f;
        spinHitBox.GetComponent<CircleCollider2D>().isTrigger = true;
        spinHitBox.tag = "Weapon";
        Destroy(spinHitBox, 0.2f);
    }
    */
    public void Ability2()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        Vector3 blinkDirection = Move;
        int layerMask = 1 << 11;
        
        if (blinkDirection.x == 0 && blinkDirection.y == 0)
        {
            blinkDirection = _directionVector;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, blinkDirection, 5f, layerMask);
        if (hit.collider != null)
        {
            transform.position += blinkDirection * (hit.distance * 0.8f);
        }
        else
        {
            transform.position += blinkDirection * 5;
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
                HP--;
                //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            }
        }
        if (col.CompareTag("Projectile"))
        {
            if (col.gameObject != _spawnedProjectile)
            {
                //Destroy(gameObject);
                //_rb.AddForce(new Vector2(100, 0));
                transform.position = transform.position + (transform.position - col.transform.position).normalized * 0.5f;
                _projectileHit = col.gameObject;
                HP--;
                //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            }
        }

        if (col.CompareTag("DeepPuddle"))
        {
            Speed = _initialSpeed / 2;
        }

        if (col.CompareTag("Path"))
        {
            Speed = _initialSpeed * 2;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("DeepPuddle") || col.CompareTag("Path"))
        {
            Speed = _initialSpeed;
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
            }
        }

        if (other.gameObject.CompareTag("Explosion") && !sameExplosion)
        {
            if(_projectileHit != null)
            {
                if(other.gameObject != _projectileHit.GetComponent<Projectile>().GetPs().gameObject)
                {
                    if(!Invulnerable)
                    {
                        HP--;
                    }
                    _collidedParticles.Add(other.gameObject);
                }
            }
            else
            {
                if (!Invulnerable)
                {
                    HP--;
                }
                _collidedParticles.Add(other.gameObject);
            }
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
