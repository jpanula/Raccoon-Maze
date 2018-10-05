using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public List<GameObject> Players;
    public GameObject WinLine;
    public GameInfo GameInfo;
    private bool _winner;

    // Use this for initialization
    void Start ()
    {
        _winner = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Players.Count <= 1)
        {
            if (Players[0] != null)
            {
                if (GameInfo.Wins[Players[0].GetComponent<Player>().PlayerNumber - 1] < GameInfo.WinGoal)
                {
                    if(!_winner)
                    {
                        GameInfo.Wins[Players[0].GetComponent<Player>().PlayerNumber - 1]++;
                        _winner = true;
                        Players[0].GetComponent<Player>().Invulnerable = true;
                        IEnumerator coroutine = NextRound(2.0f);
                        StartCoroutine(coroutine);
                    }
                }
                else
                {
                    WinLine.SetActive(true);
                    WinLine.GetComponent<Text>().text = "Player " + Players[0].GetComponent<Player>().PlayerNumber + " wins!";
                }
            }
        }
        if(Input.GetKeyDown("r"))
        {
            ReStart();
            GameInfo.Reset();
        }
    }

    private IEnumerator NextRound(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if(GameInfo.Wins[Players[0].GetComponent<Player>().PlayerNumber - 1] < GameInfo.WinGoal)
        {
            SceneManager.LoadScene("TestailuScene");
        }
    }

    public void KillPlayer(GameObject player)
    {
        Players.Remove(player);
    }

    public void ReStart()
    {
        for (int i = 0; i < GameInfo.Wins.Length; i++)
        {
            GameInfo.Wins[i] = 0;
        }
        SceneManager.LoadScene("TestailuScene");
    }
}
