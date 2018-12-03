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
        GameInfo.Wins.Clear();
        if (noPlayers <= 4 && noPlayers >= 2)
        {
            GameInfo.PlayerAmount = noPlayers;
            for(int i = 0; i < noPlayers; i++)
            {
                GameInfo.Wins.Add(0);
            }
        }
        int random = Random.Range(0, GameInfo.LevelRotation.Length);
        if (!GameInfo.LevelRotation[random])
        {
            GameInfo.LevelRotation[random] = false;
            SceneManager.LoadScene("Level" + (random + 2));
        }
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
