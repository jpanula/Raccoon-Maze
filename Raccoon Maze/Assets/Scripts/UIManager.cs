using System;
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
			if (FindPlayer(i))
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
			GameObject player = FindPlayer(i);
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
		GameObject player = FindPlayer(playerNumber);
		if (player)
		{
			float[] cooldowns = player.GetComponent<Player>().GetAbilityTimers();
			
			return "Player" + playerNumber +
			       "\nHP: " + player.GetComponent<Player>().HP + 
			       "\nA1: " + cooldowns[0].ToString("0.0") +
			       "\nA2: " + cooldowns[1].ToString("0.0");
		}
		return "Player" + playerNumber + "\nDEAD";
	}
	
	// Etsii pelaajan
	GameObject FindPlayer(int playerNumber)
	{
		return GameObject.Find("Player" + playerNumber);
	}
}
