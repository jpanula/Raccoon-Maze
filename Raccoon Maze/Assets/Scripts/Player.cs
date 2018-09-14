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

        if(HP <= 0)
        {
            Destroy(gameObject);
        }
        if(_attackTimer < 0.2f)
        {
            _attackTimer += Time.deltaTime;
        }
        else if(_attack)
        {
            _attack = false;
            _inputLock = false;
            Mora.transform.localPosition = new Vector3(0, 0.3f, 0);
            Mora.GetComponent<BoxCollider2D>().enabled = false;
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Weapon"))
        {
            if(col.gameObject != Mora)
            {
                //Destroy(gameObject);
                //_rb.AddForce(new Vector2(100, 0));
                transform.position =transform.position + (transform.position - col.transform.position).normalized * 0.7f;
                HP -= 1;
                //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            }
        }
    }
}
