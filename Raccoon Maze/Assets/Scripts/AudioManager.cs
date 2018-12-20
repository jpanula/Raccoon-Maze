using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField]
    private List<AudioSource> _sources;

    // Use this for initialization

    private void Awake()
    {

        
    }
    void Start () {
		if(GameObject.FindGameObjectsWithTag("AudioManager").Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        for (int i = 0; i < _sources.Count; i++)
        {
            if (!_sources[i].isPlaying)
            {
                _sources[i].clip = null;
            }
        }
        
    }

    public int PlaySound(AudioClip clip, bool loop)
    {
        int help = -1;
        for (int i = 0; i < _sources.Count; i++)
        {
            if(_sources[i].clip == null)
            {
                _sources[i].clip = clip;
                _sources[i].Play();
                _sources[i].loop = loop;
                help = i;
                i = _sources.Count;
                
            }
        }
        return help;
    }

    public AudioSource GetSource(int index)
    {
        return _sources[index];
    }

    public int GetSourcesLength()
    {
        return _sources.Count;
    }

    public void RemoveSound(int index)
    {
        //Debug.Log(_sources[index].clip);
        _sources[index].Stop();
        _sources[index].clip = null;
        //Debug.Log(_sources[index].clip);
        
    }
}
