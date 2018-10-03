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
				if (player)
				{
					UpdatePlayerCooldowns(i);
					EnablePlayerCooldownPies(i, true);
				}
				else
				{
					EnablePlayerCooldownPies(i, false);
				}
				playerUI.text = BuildString(i);
				
			}
			// Muuten jätetään tyhjäksi
			else
			{
				playerUI.text = "";
				EnablePlayerCooldownPies(i, false);
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
			       "\nHP: " + player.GetComponent<Player>().HP/* + 
			       "\nA1: " + cooldowns[0].ToString("0.0") +
			       "\nA2: " + cooldowns[1].ToString("0.0")*/;
		}
		return "Player" + playerNumber + "\nDEAD";
	}
	
	// Etsii pelaajan
	GameObject FindPlayer(int playerNumber)
	{
		return GameObject.Find("Player" + playerNumber);
	}
	
	// Päivittää pelaajan cooldown-elementit
	void UpdatePlayerCooldowns(int playerNumber)
	{
		GameObject player = FindPlayer(playerNumber);
		Image a1Circle;
		Image a2Circle;
		Text a1Timer;
		Text a2Timer;
		
		if (player)
		{
			a1Circle = GameObject.Find("P" + playerNumber + "A1Circle").GetComponent<Image>();
			a2Circle = GameObject.Find("P" + playerNumber + "A2Circle").GetComponent<Image>();
			a1Timer = GameObject.Find("P" + playerNumber + "A1Timer").GetComponent<Text>();
			a2Timer = GameObject.Find("P" + playerNumber + "A2Timer").GetComponent<Text>();

			a1Timer.text = player.GetComponent<Player>().GetAbilityTimers()[0].ToString("0.0");
			a2Timer.text = player.GetComponent<Player>().GetAbilityTimers()[1].ToString("0.0");

			a1Circle.fillAmount = player.GetComponent<Player>().GetAbilityTimers()[2];
			a2Circle.fillAmount = player.GetComponent<Player>().GetAbilityTimers()[3];

		}
		else
		{
			Debug.Log("Player" + playerNumber + " cooldown update requested but player could not be found");
		}
	}
	
	// Piilottaa tai näyttää pelaajan cooldown-pallerot
	void EnablePlayerCooldownPies(int playerNumber, bool isEnabled)
	{
		GameObject.Find("P" + playerNumber + "A1Circle").GetComponent<Image>().enabled = isEnabled;
		GameObject.Find("P" + playerNumber + "A2Circle").GetComponent<Image>().enabled = isEnabled;
		GameObject.Find("P" + playerNumber + "A1Timer").GetComponent<Text>().enabled = isEnabled;
		GameObject.Find("P" + playerNumber + "A2Timer").GetComponent<Text>().enabled = isEnabled;
	}
}
