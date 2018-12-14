using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameInfo GameInfo;
    private AudioManager _audioManager;
    private AudioClip _music1;
    private AudioClip _hoverSound;
    private AudioClip _clickSound;
    [SerializeField]
    private SoundLibrary _soundLibrary;
    

    // Use this for initialization
    void Start ()
    {
        GameInfo.Reset();
        _audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        _music1 = _soundLibrary.MenuMusic;
        _hoverSound = _soundLibrary.MenuScroll;
        _clickSound = _soundLibrary.MenuClick;
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

    public void HoverSound()
    {
        _audioManager.PlaySound(_hoverSound, false);
    }

    public void ClickSound()
    {
        _audioManager.PlaySound(_clickSound, false);
    }
}
