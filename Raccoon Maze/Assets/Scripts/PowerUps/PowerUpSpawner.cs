using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> _powerUp;
    [SerializeField]
    private GameObject _spawnedPowerUp;
    [SerializeField]
    private float _spawnCooldown;
    private float _spawnTimer;

    // Use this for initialization
    private void Awake()
    {
        SpawnPowerUp();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Random.Range(0,3));
        if(_spawnedPowerUp == null)
        {
            if (_spawnTimer < _spawnCooldown)
            {
                _spawnTimer += Time.deltaTime;
            }
            else
            {
                SpawnPowerUp();
                _spawnTimer = 0;
            }
        }
    }

    public GameObject SpawnPowerUp()
    {
        int random = Random.Range(0, _powerUp.Count);
        _spawnedPowerUp = Instantiate(_powerUp[random], transform.position, transform.rotation);
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

