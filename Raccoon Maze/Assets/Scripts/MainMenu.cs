using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameInfo GameInfo;

	// Use this for initialization
	void Start ()
    {
        GameInfo.Reset();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartButton()
    {
        
        SceneManager.LoadScene("TestailuScene");
    }
}
