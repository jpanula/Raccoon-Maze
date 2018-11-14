using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneMissilePowerUp : PowerUpBase
{
    private Projectile _spawnedProjectile;
    public Projectile Projectile;
    [SerializeField]
    private float _cooldown;

    public override void PickUp(GameObject player)
    {
        player.GetComponent<Player>().SetAbilityCooldown(_cooldown, _powerUpType);
        base.PickUp(player);
    }

    public override void Effect()
    {
        _owner.SetSpawnedProjectile(Instantiate(Projectile, _owner.SpawnPoint.transform.position, _owner.SpawnPoint.transform.rotation));
        _owner.GetSpawnedProjectile().GetComponent<Projectile>().Owner = _owner.Name;
        //_owner.GetSpawnedProjectile().GetComponent<Rigidbody2D>().AddForce(_owner.DirectionVector.normalized * 15, ForceMode2D.Impulse);
    }
}
