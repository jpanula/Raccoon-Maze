using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject _powerUp;
    private GameObject _spawnedPowerUp;
    [SerializeField]
    private float _spawnCooldown;
    private float _spawnTimer;

    // Use this for initialization
    void Start()
    {
        _spawnedPowerUp = Instantiate(_powerUp, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if(_spawnTimer < _spawnCooldown)
        {
            _spawnTimer += Time.deltaTime;
        }
        else if(_spawnedPowerUp == null)
        {
            _spawnedPowerUp = Instantiate(_powerUp, transform.position, transform.rotation);
            _spawnTimer = 0;
        }
    }

    public GameObject SpawnPowerUp()
    {
        return _spawnedPowerUp = Instantiate(_powerUp, transform.position, transform.rotation);
    }


}

