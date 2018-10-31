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
    private int PowerUpNum;

    private void Start()
    {
        _pickUp = false;
        _timer = 0;
    }

    private void Update()
    {
        if(_pickUp)
        {
            if (_timer < _duration)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                Effect(false);
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
            Effect(true);
        }
    }

    public void Disappear()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    public virtual void Effect(bool toggle)
    {

    }

    public int GetPowerUpNum()
    {
        return PowerUpNum;
    }

    


}
