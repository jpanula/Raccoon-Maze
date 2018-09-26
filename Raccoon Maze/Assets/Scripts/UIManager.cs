using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

	[SerializeField]
	private bool[] _activePlayers = new bool[4];
	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < _activePlayers.Length; i++)
		{
			string playerName = "Player" + (i + 1);
			_activePlayers[i] = GameObject.Find(playerName);
		}

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
