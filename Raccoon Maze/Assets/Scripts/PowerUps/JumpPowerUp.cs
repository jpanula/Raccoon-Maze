using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : PowerUpBase {

    private float _t;
    


    private void Awake()
    {
        _t = 0f;
    }
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        _sound = SoundLibrary.Jump;
    }

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();

        if (_owner != null && _owner.GetIsJumping())
        {
            if(_t < 0.5f)
            {
                _t += Time.deltaTime;
            }
            else
            {
                _owner.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                _owner.transform.localScale = new Vector3(1, 1, 1);
                _owner.SetIsJumping(false);
                _t = 0;
            }
        }
    }
    
    public override void Effect()
    {
        base.Effect();
        Debug.Log("painallus");
        _owner.SetIsJumping(true);
        _owner.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        _owner.transform.localScale = new Vector3(1.3f, 1.3f, 1);
    }
}
