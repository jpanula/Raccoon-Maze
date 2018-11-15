using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    [SerializeField]
    private Player _player;
    private Player _spawnedPlayer;

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnPlayer()
    {
        Instantiate(_player, transform.position, transform.rotation);
    }


}
