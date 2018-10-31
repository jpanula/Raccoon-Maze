using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameManager Gm;
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
    private float _baseAbility1Cooldown;
    private float _ability2Timer;
    [SerializeField]
    private float _ability2Cooldown;
    private float _baseAbility2Cooldown;
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
    private List<PowerUpBase> _powerUps;
    public Vector3 DirectionVector;

    // Use this for initialization



    private void Awake()
    {

        _baseAbility1Cooldown = _ability1Cooldown;
        _baseAbility2Cooldown = _ability2Cooldown;
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
        DirectionVector = new Vector3(0,0,0);
        _direction = Mathf.RoundToInt(transform.rotation.z / 45);
        _moraPosition = Mora.transform.position;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _collidedParticles = new List<GameObject>();
        _powerUps = new List<PowerUpBase>();
        //InitializeDirectionVector();
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
            //Player move
            Move = new Vector3(Input.GetAxis("Joystick" + PlayerNumber + "Axis1"), -Input.GetAxis("Joystick" + PlayerNumber + "Axis2"), 0);
            Move.Normalize();

            transform.position += Move * Speed * Time.deltaTime;


            //Player rotate
            DirectionVector = new Vector3(Input.GetAxis("Joystick" + PlayerNumber + "Axis3"), -Input.GetAxis("Joystick" + PlayerNumber + "Axis6"), 0);
            //DirectionVector.Normalize();
            transform.up = DirectionVector;

            /*
            float heading = Mathf.Atan2(DirectionVector.x, DirectionVector.y) * Mathf.Rad2Deg;

            Debug.Log(DirectionVector);
            Debug.Log(heading);

            transform.rotation = Quaternion.AngleAxis(-heading, Vector3.forward);
            */
            

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


        //Destroy(_blinkTrail);

    }

    public bool AddPowerUp(PowerUpBase powerUp)
    {
        for (int i = 0; i < _powerUps.Count; i++)
        {
            if (_powerUps[i].GetPowerUpNum() == powerUp.GetPowerUpNum())
            {
                return false;
            }
        }
        _powerUps.Add(powerUp);
        return true;
    }
    
    public void UpdateDirection()
    {
        /*
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
        */
    }

    /*
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
        Vector3 blinkDirection = Move;
        if (blinkDirection.x == 0 && blinkDirection.y == 0)
        {
            blinkDirection = _directionVector;
        }
        CheckWallsOnBlink(blinkDirection);
        _ability2Timer = 0;
    } 

    private void CheckWallsOnBlink(Vector3 blinkDirection)
    {
        float blinkDistance = 4f;
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 11;

        RaycastHit2D outerHit = Physics2D.Raycast(transform.position, blinkDirection, blinkDistance, layerMask);

        Vector3 blinkPosition = (transform.position + (blinkDirection * blinkDistance));

        blinkPosition = CheckInnerWallsOnBlink(blinkPosition, blinkDirection, blinkDistance);
        Debug.Log("Outer: " + outerHit);
        
        if (outerHit.collider != null)
        {
            blinkPosition = transform.position;
            blinkPosition += blinkDirection * (outerHit.distance * 0.8f);
            blinkPosition = CheckInnerWallsOnBlink(blinkPosition, blinkDirection, blinkDistance);
        }
        
        transform.position = blinkPosition;
    }

    private Vector3 CheckInnerWallsOnBlink(Vector3 blinkPosition, Vector3 blinkDirection, float blinkDistance)
    {
        float blinkOffset = 0.51f;
        int layerMask = (1 << 11) | (1 << 12);

        Vector3 result = blinkPosition;

        Collider2D innerHit = Physics2D.OverlapPoint(blinkPosition, layerMask);
        /*
        innerHit = Physics2D.OverlapBox(, new Vector2(transform.localScale.x, transform.localScale.y) *1.1f,);
        bool isEmpty = innerHit == null;
       */
        Debug.Log("Center: " + innerHit);
        Debug.Log(blinkPosition);
        
        if (innerHit != null)
        {
            innerHit = Physics2D.OverlapPoint(GetSideVector(blinkPosition, blinkDirection, true, blinkOffset), layerMask);
            //Debug.Log("Second: " + innerHit);
            //Debug.Log(GetSideVector(blinkPosition, blinkDirection, true, blinkOffset));
            if (innerHit != null)
            {
                innerHit = Physics2D.OverlapPoint(GetSideVector(blinkPosition, blinkDirection, false, blinkOffset), layerMask);
                //Debug.Log("Third: " + innerHit);
                //Debug.Log(GetSideVector(blinkPosition, blinkDirection, false, blinkOffset));
                if (innerHit != null)
                {
                    blinkOffset = 1f;
                    innerHit = Physics2D.OverlapPoint(new Vector3(blinkPosition.x - (blinkOffset * blinkDirection.x), blinkPosition.y - (blinkOffset * blinkDirection.y), layerMask));
                    if (innerHit == null)
                    {
                        result = new Vector3(blinkPosition.x - (blinkOffset * blinkDirection.x), blinkPosition.y - (blinkOffset * blinkDirection.y));
                    }
                    else
                    {
                        result = transform.position;
                        //result = new Vector3(blinkPosition.x + (blinkOffset * blinkDirection.x), blinkPosition.y + (blinkOffset * blinkDirection.y));
                    }
                }
                else
                {
                    result = GetSideVector(blinkPosition, blinkDirection, false, blinkOffset);
                    
                }
            }
            else
            {
                result = GetSideVector(blinkPosition, blinkDirection, true, blinkOffset);
            }

        }
        return result;
    }

    private Vector3 GetSideVector(Vector3 position, Vector3 direction, bool right, float offset)
    {
        Vector3 result;

        if (right)
        {
            result = new Vector3(position.x + (offset * direction.y), position.y + (offset * direction.x), 0);
        }
        else
        {
            result = new Vector3(position.x - (offset * direction.y), position.y - (offset * direction.x), 0);
        }

        return result;
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

    public void SetAbility2Cooldown(float value)
    {
        if (value >= 0)
        {
            _ability2Cooldown = value;
        }
    }

    public void SetAbility1Cooldown(float value)
    {
        if (value >= 0)
        {
            _ability1Cooldown = value;
        }
    }

    public void ResetAbility2Cooldown()
    {
        if (_ability2Timer >= _ability2Cooldown)
        {
            _ability2Cooldown = _baseAbility2Cooldown;
            _ability2Timer = _ability2Cooldown;
        }
        else
        {
            _ability2Cooldown = _baseAbility2Cooldown;
        }

    }
    public void ResetAbility1Cooldown()
    {
        if (_ability1Timer >= _ability1Cooldown)
        {
            _ability1Cooldown = _baseAbility1Cooldown;
            _ability1Timer = _ability1Cooldown;
        }
        else
        {
            _ability1Cooldown = _baseAbility1Cooldown;
        }

    }

}
