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
	
	// Use this for initialization
	void Start ()
	{
		_onFire = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        int random = Random.Range(0, _sprites.Count);
        _spriteRenderer.sprite = _sprites[random];
    }
	
	// Update is called once per frame
	void Update ()
	{
	}

    /*
	private void OnParticleCollision(GameObject other)
	{
		if (CompareTag("OilSlickFire")) return;
		if (!other.CompareTag("Explosion")) return;
		_onFire = true;
		GetComponent<Renderer>().material = OnFireMaterial;
		gameObject.layer = 13;
		tag = "OilSlickFire";
	}
    */
}
