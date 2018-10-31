using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCooldownReducer : PowerUpBase
{
    [SerializeField]
    private float _reducedCooldown;

    public override void Effect(bool toggle)
    {
        if (toggle)
        {
            _owner.SetAbility1Cooldown(_reducedCooldown);
        }
        else
        {
            _owner.ResetAbility1Cooldown();
        }
    }

}
