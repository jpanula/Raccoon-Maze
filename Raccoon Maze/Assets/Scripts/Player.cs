using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int PlayerNumber;
    public int Speed;
    public Vector3 Move;
    public int _direction;
    public GameObject Mora;
    private float _attackTimer;
    private Vector3 _moraPosition;
    private bool _attack;
    private bool _inputLock;
    private Rigidbody2D _rb;
    public BoxCollider2D MoraCollider;
    public int HP;
    private GameObject _spawnedProjectile;
    public GameObject Projectile;
    public GameObject SpawnPoint;

    // Use this for initialization
    void Start ()
    {
        _direction = 0;
        _attackTimer = 0;
        _moraPosition = Mora.transform.position;
        _attack = false;
        _inputLock = false;
        _rb = gameObject.GetComponent<Rigidbody2D>();
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
            }
            _inputLock = false;
        }
        if (!_inputLock)
        {
            if (Input.GetAxisRaw("P" + PlayerNumber + "H") < 0)
            {
                Move = new Vector3(-1, Move.y, 0);
            }
            else if (Input.GetAxisRaw("P" + PlayerNumber + "H") > 0)
            {
                Move = new Vector3(1, Move.y, 0);
            }
            else
            {
                Move = new Vector3(0, Move.y, 0);
            }

            if (Input.GetAxisRaw("P" + PlayerNumber + "V") < 0)
            {
                Move = new Vector3(Move.x, -1, 0);
            }
            else if (Input.GetAxisRaw("P" + PlayerNumber + "V") > 0)
            {
                Move = new Vector3(Move.x, 1, 0);
            }
            else
            {
                Move = new Vector3(Move.x, 0, 0);
            }

            UpdateDirection();
            transform.position += Move * Speed * Time.deltaTime;
        }

        if(Input.GetButtonDown("P" + PlayerNumber + "Attack") && !_inputLock)
        {
            Attack();
        }

        if (Input.GetButtonDown("P" + PlayerNumber + "Ability") && !_inputLock)
        {
            Ability();
        }

    }
    public void UpdateDirection()
    {
        if(Move.x == 1)
        {
            if(Move.y == 1)
            {
                _direction = 7;
            }
            else if (Move.y == -1)
            {
                _direction = 5;
            }
            else
            {
                _direction = 6;
            }
        }
        else if(Move.x == -1)
        {
            if (Move.y == 1)
            {
                _direction = 1;
            }
            else if (Move.y == -1)
            {
                _direction = 3;
            }
            else
            {
                _direction = 2;
            }
        }
        else if (Move.y == 1)
        {
            _direction = 0;
        }
        else if (Move.y == -1)
        {
            _direction = 4;
        }

        transform.eulerAngles = new Vector3(0, 0, 45 * _direction);
    }

    public void Attack()
    {
        Mora.GetComponent<BoxCollider2D>().enabled = true;
        Mora.transform.localPosition = new Vector3(0, 1, 0);
        _attackTimer = 0;
        _attack = true;
        _inputLock = true;
    }

    public void Ability()
    {
        _spawnedProjectile = Instantiate(Projectile, SpawnPoint.transform.position, Quaternion.identity);
        AddForceToProjectile(_spawnedProjectile);
        _attackTimer = 0;
        //_attack = true;
        _inputLock = true;
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
                Destroy(col.gameObject);
                //Destroy(gameObject);
                //_rb.AddForce(new Vector2(100, 0));
                transform.position = transform.position + (transform.position - col.transform.position).normalized * 0.5f;
                HP -= 1;
                //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            }
        }
    }

    public void AddForceToProjectile(GameObject obj)
    {
        if(_direction == 0)
        {
            obj.GetComponent<Rigidbody2D>().AddForce(_spawnedProjectile.transform.up * 20, ForceMode2D.Impulse);
        }
        else if (_direction == 1)
        {
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(_spawnedProjectile.transform.right.x * -20, _spawnedProjectile.transform.up.y * 20), ForceMode2D.Impulse);
        }
        else if (_direction == 2)
        {
            obj.GetComponent<Rigidbody2D>().AddForce(_spawnedProjectile.transform.right * -20, ForceMode2D.Impulse);
        }
        else if (_direction == 3)
        {
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(_spawnedProjectile.transform.right.x * -20, _spawnedProjectile.transform.up.y * -20), ForceMode2D.Impulse);
        }
        else if (_direction == 4)
        {
            obj.GetComponent<Rigidbody2D>().AddForce(_spawnedProjectile.transform.up * -20, ForceMode2D.Impulse);
        }
        else if (_direction == 5)
        {
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(_spawnedProjectile.transform.right.x * 20, _spawnedProjectile.transform.up.y * -20), ForceMode2D.Impulse);
        }
        else if (_direction == 6)
        {
            obj.GetComponent<Rigidbody2D>().AddForce(_spawnedProjectile.transform.right * 20, ForceMode2D.Impulse);
        }
        else if (_direction == 7)
        {
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(_spawnedProjectile.transform.right.x * 20, _spawnedProjectile.transform.up.y * 20), ForceMode2D.Impulse);
        }
    }
}
