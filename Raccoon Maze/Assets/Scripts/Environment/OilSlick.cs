using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSlick : MonoBehaviour
{

	public Material OnFireMaterial;
	private Collider2D[] collisions;
	private bool _onFire;
    [SerializeField]
    private List<Sprite> _sprites;
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private GameObject _flames;

    private AudioClip _flameSound;
    private bool _soundBool;
    [SerializeField]
    private SoundLibrary SoundLibrary;
    private AudioManager _am;

    // Use this for initialization
    void Start ()
	{
        _flames.SetActive(false);
        _onFire = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        int random = Random.Range(0, _sprites.Count);
        _spriteRenderer.sprite = _sprites[random];
        _soundBool = false;
        _flameSound = SoundLibrary.FireTrap;
        _am = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	}

    
	private void OnParticleCollision(GameObject other)
	{
		//if (CompareTag("OilSlickFire")) return;
		if (!other.CompareTag("Explosion")) return;
        Debug.Log("tuli");
        GetComponent<BoxCollider2D>().enabled = false;
		_onFire = true;
		gameObject.layer = 13;
        _flames.SetActive(true);
        tag = "OilSlickFire";
        if (!_soundBool)
        {
            _soundBool = true;
            _am.PlaySound(_flameSound, false);
        }
    }
    
}
