using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private List<int> _activePlayers;
	[SerializeField]
	private int _maxPlayers;
	
	// Use this for initialization
	void Start ()
	{
		// Katotaan mitkä pelaajat on pelissä
		for (int i = 0; i < _maxPlayers; i++)
		{
			if (GameObject.Find("Player" + (i + 1)))
			{
				_activePlayers.Add(i + 1);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		foreach (var playerNumber in _activePlayers)
		{
			GameObject.Find("P" + playerNumber + "UI").GetComponent<Text>().text = BuildString(playerNumber);
		}
	}

	string BuildString(int playerNumber)
	{
		return "Player" + playerNumber + "\nHP: " + GameObject.Find("Player" + playerNumber).GetComponent<Player>().HP;
	}
}
