using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour {

    public GameInfo GameInfo;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayButton(int noPlayers)
    {
        if(noPlayers <= 4 && noPlayers >= 2)
        {
            GameInfo.PlayerAmount = noPlayers;
        }
        //SceneManager.LoadScene("PlayMenu");
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
