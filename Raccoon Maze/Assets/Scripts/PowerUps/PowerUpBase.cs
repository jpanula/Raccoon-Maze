using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : MonoBehaviour {
    private bool _pickUp;
    public Player _owner;
    [SerializeField]
    private float _duration;
    [SerializeField]
    private float _timer;
    [SerializeField]
    private int _powerUpNum;
    [SerializeField]
    protected int _powerUpType;
    [SerializeField]
    protected float _cooldown;
    private float _cooldownTimer;
    protected bool _readyToUse;
    private PowerUpSpawner _spawner;

    private void Start()
    {
        _pickUp = false;
        _timer = 0;
    }

    protected virtual void Update()
    {
        if(_pickUp)
        {
            if (_timer < _duration)
            {
                _timer += Time.deltaTime;
            }
            else if(_duration != -1)
            {
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 8 && !_pickUp)
        {
            _pickUp = true;
            PickUp(col.gameObject);
        }
    }

    public virtual void PickUp(GameObject player)
    {
        if (player.GetComponent<Player>().AddPowerUp(this))
        {
            _owner = player.GetComponent<Player>();
            Disappear();
            SetSpawner()
            //Effect(true);
        }
    }

    public void Disappear()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }

    public virtual void Effect()
    {
    }

    public int GetPowerUpNum()
    {
        return _powerUpNum;
    }

    public int GetPowerUpType()
    {
        return _powerUpType;
    }

    public float GetCooldown()
    {
        return _cooldown;
    }

    public void SetSpawner(PowerUpSpawner spawner)
    {
        _spawner = spawner;
    }




}
