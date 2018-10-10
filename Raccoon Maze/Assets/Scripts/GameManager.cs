using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public List<GameObject> Players;
    public GameObject WinLine;
    public GameObject[] PlayerPoints;
    public GameInfo GameInfo;
    private bool _winner;

    // Use this for initialization

    private void Awake()
    {
        _winner = false;
    }
    private void Start ()
    {
        for(int i = 0; i < Players.Count; i++)
        {
            PlayerPoints[Players[i].GetComponent<Player>().PlayerNumber - 1].GetComponent<Text>().text = "Points: " + GameInfo.Wins[Players[i].GetComponent<Player>().PlayerNumber - 1];
        }
    }

    // Update is called once per frame
    private void Update ()
    {
        if (Players.Count <= 1)
        {
            IEnumerator coroutine = WinCheck(1.0f);
            StartCoroutine(coroutine);
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
        SceneManager.LoadScene("TestailuScene");
    }

    private IEnumerator WinCheck(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (Players.Count == 1)
        {
            int PNum = Players[0].GetComponent<Player>().PlayerNumber - 1;
            if (GameInfo.Wins[PNum] < GameInfo.WinGoal)
            {
                if (!_winner)
                {
                    GameInfo.Wins[PNum]++;
                    PlayerPoints[PNum].GetComponent<Text>().text = "Points: " + GameInfo.Wins[PNum];
                    _winner = true;
                    //Players[0].GetComponent<Player>().Invulnerable = true;

                    if (GameInfo.Wins[PNum] < GameInfo.WinGoal)
                    {
                        IEnumerator coroutine = NextRound(2.0f);
                        StartCoroutine(coroutine);
                    }

                }
            }
            else
            {
                Players[0].GetComponent<Player>().Invulnerable = true;
                WinLine.SetActive(true);
                WinLine.GetComponent<Text>().text = "Player " + (PNum + 1) + " wins!";
            }
        }
        else if(Players.Count < 1)
        {
            IEnumerator coroutine = NextRound(2.0f);
            StartCoroutine(coroutine);
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
