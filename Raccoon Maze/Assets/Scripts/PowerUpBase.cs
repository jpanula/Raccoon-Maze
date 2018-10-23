using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : MonoBehaviour {
    private bool _pickUp;

    private void Start()
    {
        _pickUp = false;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 8 && !_pickUp)
        {
            _pickUp = true;
            PickUp();
        }
    }

    public virtual void PickUp()
    {


    }
}
