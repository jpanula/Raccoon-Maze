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
    [SerializeField]
    private PowerUpSpawner _spawner;
    [SerializeField]
    private ParticleSystem _ps;

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
            _spawner.SetSpawnedPowerUp(null);
            SetSpawner(null);
            //Effect(true);
        }
    }

    public void Disappear()
    {
        if(gameObject.GetComponent<MeshRenderer>() != null)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        if (gameObject.GetComponent<SpriteRenderer>() != null)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        _ps.Stop();
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
        Debug.Log("moi");
        _spawner = spawner;
    }

    public PowerUpSpawner GetSpawner()
    {
        return _spawner;
    }




}
