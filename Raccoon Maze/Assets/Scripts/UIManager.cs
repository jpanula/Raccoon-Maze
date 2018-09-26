using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private int _maxPlayers;

	private int _startPlayers = 0;
	
	// Use this for initialization
	void Start ()
	{
		for (int i = 1; i <= _maxPlayers; i++)
		{
			if (GameObject.Find("Player" + i))
			{
				_startPlayers++;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 1; i <= _maxPlayers; i++)
		{
			GameObject player = GameObject.Find("Player" + i);
			Text playerUI = GameObject.Find("P" + i + "UI").GetComponent<Text>();
			if (player || i <= _startPlayers)
			{
				playerUI.text = BuildString(i);
			}
			else
			{
				playerUI.text = "";
			}
		}
	}

	string BuildString(int playerNumber)
	{
		if (GameObject.Find("Player" + playerNumber))
		{
			return "Player" + playerNumber + "\nHP: " + GameObject.Find("Player" + playerNumber).GetComponent<Player>().HP;
		}
		return "Player" + playerNumber + "\nDEAD";
	}
}
