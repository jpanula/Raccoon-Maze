using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private GameManager _gm;
    public int PlayerNumber;
    public int Speed;
    private int _initialSpeed;
    public float OilSlickTickTime;
    private float _oilSlickTimer;
    private bool _inOilSlickFire;
    public Vector3 Move;
    public int _direction;
    private Vector3 _directionVector;
    public GameObject Mora;
    private float _attackTimer;
    private float _ability1Timer;
    private float _ability1Cooldown;
    private float _baseAbility1Cooldown;
    private float _ability2Timer;
    private float _ability2Cooldown;
    private float _baseAbility2Cooldown;
    private Vector3 _moraPosition;
    private bool _attack;
    private bool _inputLock;
    private Rigidbody2D _rb;
    public BoxCollider2D MoraCollider;
    public int HP;
    private Projectile _spawnedProjectile;
    public GameObject Projectile;
    public GameObject SpawnPoint;
    private List<GameObject> _collidedParticles;
    public string Name;
    private bool _directionLock;
    private GameObject _projectileHit;
    private int _gamepadControl;
    public bool Invulnerable;
    //private List<PowerUpBase> _powerUps;
    private PowerUpBase _powerUp1;
    private PowerUpBase _powerUp2;
    public Vector3 DirectionVector;

    // Use this for initialization



    private void Awake()
    {
        _initialSpeed = Speed;
        _oilSlickTimer = OilSlickTickTime;

        _baseAbility1Cooldown = _ability1Cooldown;
        _baseAbility2Cooldown = _ability2Cooldown;
        _attackTimer = 0;
        _ability1Timer = _ability1Cooldown;
        _ability2Timer = _ability2Cooldown;
        _directionLock = false;
        _attack = false;
        _inputLock = false;
        _gamepadControl = 0;
        Invulnerable = false;
        
    }

    private void Start ()
    {
        DirectionVector = transform.up;
        _direction = Mathf.RoundToInt(transform.rotation.z / 45);
        _moraPosition = Mora.transform.position;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _collidedParticles = new List<GameObject>();
        _gm = FindObjectOfType<GameManager>();
        //_powerUps = new List<PowerUpBase>();
        //InitializeDirectionVector();
    }
	
	// Update is called once per frame
	private void Update ()
    {

        //Debug.Log(_powerUp1);
        _rb.velocity = new Vector3(0, 0, 0);
        if(HP <= 0)
        {
            _gm.KillPlayer(gameObject);
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
        //Debug.Log(Input.GetJoystickNames()[0]);
        //Debug.Log(Input.GetJoystickNames()[1]);
        InputManager.CheckGamepadConnection();
        //Debug.Log(InputManager.GetGamepadControl(PlayerNumber - 1));
        if (InputManager.GetGamepadControl(PlayerNumber - 1) >= 0)
        {
            if (Input.GetJoystickNames()[PlayerNumber - 1] == "Wireless Controller")
            {
                //Debug.Log("PS4 Player " + PlayerNumber);
                _gamepadControl = 1;
            }
            else if(Input.GetJoystickNames()[PlayerNumber - 1] == "Controller (Xbox One For Windows)")
            {
                //Debug.Log("XBox Player " + PlayerNumber);
                _gamepadControl = 2;
            }
        }
        else
        {
            _gamepadControl = 0;
        }
        //Debug.Log("P" + PlayerNumber + (_gamepadControl));
        if (!_inputLock)
        {
            //Player move
            Move = new Vector2(InputManager.GetAxis("P" + PlayerNumber.ToString() + "MoveHorizontal", _gamepadControl, PlayerNumber.ToString()), -InputManager.GetAxis("P" + PlayerNumber.ToString() + "MoveVertical", _gamepadControl, PlayerNumber.ToString()));
            Move.Normalize();

            transform.position += Move * Speed * Time.deltaTime;



            //Debug.Log(Input.GetJoystickNames()[PlayerNumber - 1]);
            //Player rotate
            Vector2 HelpVector = new Vector2(InputManager.GetAxis("P" + PlayerNumber.ToString() + "DirHorizontal", _gamepadControl, PlayerNumber.ToString()), -InputManager.GetAxis("P" + PlayerNumber.ToString() + "DirVertical", _gamepadControl, PlayerNumber.ToString()));
            /*
                        else
                        {
                            HelpVector = new Vector2(Input.GetAxis("Joystick" + PlayerNumber + "Axis5"), -Input.GetAxis("Joystick" + PlayerNumber + "Axis4"));
                        }
                        */


            if (HelpVector != Vector2.zero)
            {
                if(HelpVector.x == 0 && HelpVector.y == -1)
                {
                    transform.eulerAngles = new Vector3(0,0,180);
                }
                else
                {
                    transform.up = HelpVector;
                }
                
                //DirectionVector = HelpVector;
            }
            //DirectionVector.Normalize();
            /*
            float heading = Mathf.Atan2(DirectionVector.x, DirectionVector.y) * Mathf.Rad2Deg;

            Debug.Log(DirectionVector);
            Debug.Log(heading);

            transform.rotation = Quaternion.AngleAxis(-heading, Vector3.forward);
            */
            /*
            if (Input.GetKeyDown("Joystick" + PlayerNumber.ToString() + "Button5"))
            {
                Attack();
            }
            */
            if (InputManager.GetKeyDown("P" + PlayerNumber.ToString() + "Melee", _gamepadControl, PlayerNumber.ToString()))
            {
                Attack();
            }
            if (InputManager.GetKeyDown("P" + PlayerNumber.ToString() + "Ability1", _gamepadControl, PlayerNumber.ToString()) && _ability1Timer >= _ability1Cooldown)
            {
                Ability1();
            }
            if (InputManager.GetKeyDown("P" + PlayerNumber.ToString() + "Ability2", _gamepadControl, PlayerNumber.ToString()) && _ability2Timer >= _ability2Cooldown)
            {
                Ability2();
            }

        }

        //Destroy(_blinkTrail);

        _oilSlickTimer -= Time.deltaTime;
        if (_oilSlickTimer <= 0 && _inOilSlickFire)
        {
            HP--;
            _oilSlickTimer = OilSlickTickTime;
        }
    }

    public bool AddPowerUp(PowerUpBase powerUp)
    {
        if(powerUp.GetPowerUpType() == 1)
        {
            _powerUp1 = powerUp;
            _ability1Cooldown = powerUp.GetCooldown();
            _ability1Timer = _ability1Cooldown;
            return true;
        }
        else if (powerUp.GetPowerUpType() == 2)
        {
            _powerUp2 = powerUp;
            _ability2Cooldown = powerUp.GetCooldown();
            _ability2Timer = _ability2Cooldown;
            return true;
        }
        return false;
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
        
        if (_powerUp1 != null)
        {
            _powerUp1.Effect();
            _ability1Timer = 0;
        }
        
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
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        
        if (_powerUp2 != null)
        {
            _powerUp2.Effect();
            _ability2Timer = 0;
        }
        /*
        Vector3 blinkDirection = Move.normalized;
        if (blinkDirection.x == 0 && blinkDirection.y == 0)
        {
            blinkDirection = DirectionVector;
        }
        CheckWallsOnBlink(blinkDirection);
        */
    } 

    private void CheckWallsOnBlink(Vector3 blinkDirection)
    {
        float blinkDistance = 4f;
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 11;

        RaycastHit2D outerHit = Physics2D.Raycast(transform.position, blinkDirection, blinkDistance, layerMask);

        Vector3 blinkPosition = (transform.position + (blinkDirection * blinkDistance));

        blinkPosition = CheckInnerWallsOnBlink(blinkPosition, blinkDirection, blinkDistance);
        //Debug.Log("Outer: " + outerHit);
        
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
        //Debug.Log("Center: " + innerHit);
        //Debug.Log(blinkPosition);
        
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
            //Debug.Log(col.gameObject);
            //Debug.Log(_spawnedProjectile);
            if (_spawnedProjectile == null)
            {
                //Destroy(gameObject);
                //_rb.AddForce(new Vector2(100, 0));
                transform.position = transform.position + (transform.position - col.transform.position).normalized * 0.5f;
                _projectileHit = col.gameObject;
                HP--;
                //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            }
            else if (col.gameObject != _spawnedProjectile.gameObject)
            {
                //Destroy(gameObject);
                //_rb.AddForce(new Vector2(100, 0));
                transform.position = transform.position + (transform.position - col.transform.position).normalized * 0.5f;
                _projectileHit = col.gameObject;
                HP--;
                //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            }

        }

        if (col.CompareTag("DeepPuddle") || col.CompareTag("OilSlick"))
        {
            Speed = _initialSpeed / 2;
        }

        if (col.CompareTag("Path"))
        {
            Speed = _initialSpeed * 2;
        }

        if (col.CompareTag("SpikeTrap"))
        {
            col.gameObject.GetComponent<SpikeTrap>().TriggerTrap();
        }

        if (col.CompareTag("SpikeTrapActive") || col.CompareTag("Spikes"))
        {
            HP--;
        }

        if (col.CompareTag("OilSlickFire"))
        {
            Speed = _initialSpeed / 2;
            HP--;
            _inOilSlickFire = true;
            _oilSlickTimer = OilSlickTickTime;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("DeepPuddle") || col.CompareTag("Path") || col.CompareTag("OilSlick"))
        {
            Speed = _initialSpeed;
        }

        if (col.CompareTag("OilSlickFire"))
        {
            Speed = _initialSpeed;
            _inOilSlickFire = false;
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

    public void SetSpawnedProjectile(Projectile projectile)
    {
        if (projectile != null)
        {
            _spawnedProjectile = projectile;
        }
    }

    public Projectile GetSpawnedProjectile()
    {
        return _spawnedProjectile;
    }

    

    public void SetAbilityCooldown(float value, int type)
    {
        if (value >= 0)
        {
            if(type == 1)
            {
                _ability1Cooldown = value;
            }
            else
            {
                _ability2Cooldown = value;
            }
            
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
