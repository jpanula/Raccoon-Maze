using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : PowerUpBase {

    private bool _jump;
    private float _t;


    private void Awake()
    {
        _jump = false;
        _t = 0f;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();

        if (_jump)
        {
            if(_t < 0.2f)
            {
                _t += Time.deltaTime;
            }
            else
            {
                _owner.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                _owner.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                _jump = false;
                _t = 0;
            }
        }
    }
    
    public override void Effect()
    {
        /*
        Vector3 blinkDirection = _owner.Move.normalized;
        if (blinkDirection.x == 0 && blinkDirection.y == 0)
        {
            blinkDirection = _owner.DirectionVector;
        }
        CheckWallsOnBlink(blinkDirection);
        */
        Debug.Log("painallus");
        _jump = true;
        _owner.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        _owner.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        /*
        Quaternion help = _owner.transform.rotation;
        _owner.transform.position = new Vector3(_owner.transform.position.x, _owner.transform.position.y, _jumpSpeed);
        Debug.Log(help);
        _owner.transform.rotation = help;
        */
    }
}
