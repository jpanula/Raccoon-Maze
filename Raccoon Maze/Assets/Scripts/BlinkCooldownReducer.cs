using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkCooldownReducer : PowerUpBase
{
    [SerializeField]
    private float _reducedCooldown;

    public override void Effect(bool toggle)
    {
        if(toggle)
        {
            _owner.SetAbility2Cooldown(_reducedCooldown);
        }
        else
        {
            _owner.ResetAbility2Cooldown();
        }
    }

}
