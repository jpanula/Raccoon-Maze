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
		// Ota ylös aloittavien pelaajien määrä
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
		
		// Pelaajien UI
		for (int i = 1; i <= _maxPlayers; i++)
		{
			// Haetaan pelaaja ja sen UI
			GameObject player = GameObject.Find("Player" + i);
			Text playerUI = GameObject.Find("P" + i + "UI").GetComponent<Text>();
			
			// Jos pelaaja on elossa, tai jos pelaaja on ollut pelin alussa olemassa, kirjoitetaan teksti
			if (player || i <= _startPlayers)
			{
				playerUI.text = BuildString(i);
			}
			// Muuten jätetään tyhjäksi
			else
			{
				playerUI.text = "";
			}
		}
	}

	// Rakentaa pelaajan UI:n tekstin
	string BuildString(int playerNumber)
	{
		if (GameObject.Find("Player" + playerNumber))
		{
			return "Player" + playerNumber + "\nHP: " + GameObject.Find("Player" + playerNumber).GetComponent<Player>().HP;
		}
		return "Player" + playerNumber + "\nDEAD";
	}
}
