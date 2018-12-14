using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private List<PlayerSpawner> _playerSpawners;
    public List<GameObject> PlayerPrefabs;
    private List<GameObject> Players;
    public GameObject WinLine;
    public GameObject[] PlayerPoints;
    public GameInfo GameInfo;
    private bool _winner;
    private AudioManager _audioManager;
    [SerializeField]
    private AudioClip _startMusic;
    [SerializeField]
    private AudioClip _fourPlayerMusic;
    [SerializeField]
    private AudioClip _threePlayerMusic;
    [SerializeField]
    private AudioClip _twoPlayerMusic;

    // Use this for initialization

    private void Awake()
    {
        _winner = false;
        Players = new List<GameObject>();
        
    }
    private void Start ()
    {
        InitializePlayers();

        for(int i = 0; i < Players.Count; i++)
        {
            PlayerPoints[Players[i].GetComponent<Player>().PlayerNumber - 1].GetComponent<Text>().text = "P" + (i + 1) + " Points: " + GameInfo.Wins[Players[i].GetComponent<Player>().PlayerNumber - 1];
        }
        _audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        for (int i = 1; i < _audioManager.GetSourcesLength(); i++)
        {
            _audioManager.RemoveSound(i);
        }
    }

    // Update is called once per frame
    private void Update ()
    {
        MusicLoop();
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

    private void MusicLoop()
    {
        int closer = 0;
        if (!GameInfo.InGameMusic)
        {
            _audioManager.RemoveSound(0);
            _audioManager.PlaySound(_startMusic, false);
            GameInfo.InGameMusic = true;
        }
        else if(!_audioManager.GetSource(0).isPlaying)
        {
            _audioManager.RemoveSound(0);
            for(int i = 0; i < GameInfo.Wins.Count; i++)
            {
                if (GameInfo.Wins[i] >= GameInfo.WinGoal - 1)
                {
                    _audioManager.PlaySound(_twoPlayerMusic, false);
                    i = GameInfo.Wins.Count;
                    closer = 2;
                }
                else if (GameInfo.Wins[i] >= GameInfo.WinGoal - 3)
                {
                    closer = 1;
                }
                
            }
            if (closer == 1)
            {
                _audioManager.PlaySound(_threePlayerMusic, false);
            }
            else if(closer == 0) 
            {
                _audioManager.PlaySound(_fourPlayerMusic, false);
            }
        }
    }

    public void NextLevel()
    {
        bool rotation = false;
        for (int i = 0; i < GameInfo.LevelRotation.Length; i++)
        {
            if (!GameInfo.LevelRotation[i])
            {
                rotation = true;
            }
        }
        Debug.Log(rotation);
        if (!rotation)
        {
            GameInfo.LevelRotation = new bool[] { false, false, false };
        }
        bool help = true;
        while (help)
        {
            int random = Random.Range(0, GameInfo.LevelRotation.Length);
            if (!GameInfo.LevelRotation[random])
            {
                GameInfo.LevelRotation[random] = false;
                help = false;
                SceneManager.LoadScene("Level" + (random + 2));
            }
        }
    }

    private IEnumerator NextRound(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NextLevel();
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

    private void InitializePlayers()
    {
        for(int i = 0; i < GameInfo.PlayerAmount; i++)
        {
            Players.Add(_playerSpawners[i].SpawnPlayer().gameObject);
        }
    }

    public void KillPlayer(GameObject player)
    {
        Players.Remove(player);
    }

    public void ReStart()
    {
        for (int i = 0; i < GameInfo.Wins.Count; i++)
        {
            GameInfo.Wins[i] = 0;
        }
        NextLevel();
    }
    public AudioManager GetAudioManager()
    {
        return _audioManager;
    }
}
