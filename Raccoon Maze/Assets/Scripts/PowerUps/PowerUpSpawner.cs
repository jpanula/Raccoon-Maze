using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject _powerUp;
    [SerializeField]
    private GameObject _spawnedPowerUp;
    [SerializeField]
    private float _spawnCooldown;
    private float _spawnTimer;

    // Use this for initialization
    void Start()
    {
        SpawnPowerUp();
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
            SpawnPowerUp();
            _spawnTimer = 0;
        }
    }

    public GameObject SpawnPowerUp()
    {
        _spawnedPowerUp = Instantiate(_powerUp, transform.position, transform.rotation);
        //Debug.Log(_spawnedPowerUp.GetComponent<PowerUpBase>().GetSpawner());
        _spawnedPowerUp.GetComponent<PowerUpBase>().SetSpawner(this);
        //Debug.Log(_spawnedPowerUp.GetComponent<PowerUpBase>().GetSpawner());
        return _spawnedPowerUp;
    }

    public void SetSpawnedPowerUp(GameObject powerUp)
    {
        _spawnedPowerUp = powerUp;
    }
}

