using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameInfo GameInfo;
    private AudioManager _audioManager;
    [SerializeField]
    private AudioClip _music1;

    // Use this for initialization
    void Start ()
    {
        GameInfo.Reset();
        _audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        MusicLoop();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }
    private void MusicLoop()
    {
        if (_audioManager.GetSource(0).clip == null)
        {
            _audioManager.PlaySound(_music1, true);
        }
    }


    public void PlayButton()
    {
        SceneManager.LoadScene("PlayMenu");
    }
    public void SettingsButton()
    {
        Debug.Log("Settings");
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
