using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkPowerUp : PowerUpBase
{
    [SerializeField]
    private float blinkDistance;
    [SerializeField]

    public override void PickUp(GameObject player)
    {
        player.GetComponent<Player>().SetAbilityCooldown(_cooldown, _powerUpType);
        base.PickUp(player);
    }

    public override void Effect()
    {
        Vector3 blinkDirection = _owner.Move.normalized;
        if (blinkDirection.x == 0 && blinkDirection.y == 0)
        {
            blinkDirection = _owner.DirectionVector;
        }
        CheckWallsOnBlink(blinkDirection);
    }

    private void CheckWallsOnBlink(Vector3 blinkDirection)
    {
    
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = (1 << 11) | (1 << 12);

        RaycastHit2D outerHit = Physics2D.Raycast(_owner.transform.position, blinkDirection, blinkDistance, layerMask);

        Vector3 blinkPosition = (_owner.transform.position + (blinkDirection * blinkDistance));

        //blinkPosition = CheckInnerWallsOnBlink(blinkPosition, blinkDirection, blinkDistance);
        Debug.Log("Outer: " + outerHit);

        if (outerHit.collider != null)
        {
            blinkPosition = _owner.transform.position;
            blinkPosition += blinkDirection * (outerHit.distance * 0.8f);
            //blinkPosition = CheckInnerWallsOnBlink(blinkPosition, blinkDirection, blinkDistance);
        }

        _owner.transform.position = blinkPosition;
    }

    private Vector3 CheckInnerWallsOnBlink(Vector3 blinkPosition, Vector3 blinkDirection, float blinkDistance)
    {
        float blinkOffset = 0.51f;
        int layerMask = (1 << 11) | (1 << 12);

        Vector3 result = blinkPosition;

        Collider2D innerHit = Physics2D.OverlapPoint(blinkPosition, layerMask);
        /*
        innerHit = Physics2D.OverlapBox(, new Vector2(transform.localScale.x, transform.localScale.y) *1.1f,);
        bool isEmpty = innerHit == null;
       */
        Debug.Log("Center: " + innerHit);
        Debug.Log(blinkPosition);

        if (innerHit != null)
        {
            innerHit = Physics2D.OverlapPoint(GetSideVector(blinkPosition, blinkDirection, true, blinkOffset), layerMask);
            //Debug.Log("Second: " + innerHit);
            //Debug.Log(GetSideVector(blinkPosition, blinkDirection, true, blinkOffset));
            if (innerHit != null)
            {
                innerHit = Physics2D.OverlapPoint(GetSideVector(blinkPosition, blinkDirection, false, blinkOffset), layerMask);
                //Debug.Log("Third: " + innerHit);
                //Debug.Log(GetSideVector(blinkPosition, blinkDirection, false, blinkOffset));
                if (innerHit != null)
                {
                    blinkOffset = 1f;
                    innerHit = Physics2D.OverlapPoint(new Vector3(blinkPosition.x - (blinkOffset * blinkDirection.x), blinkPosition.y - (blinkOffset * blinkDirection.y), layerMask));
                    if (innerHit == null)
                    {
                        result = new Vector3(blinkPosition.x - (blinkOffset * blinkDirection.x), blinkPosition.y - (blinkOffset * blinkDirection.y));
                    }
                    else
                    {
                        result = _owner.transform.position;
                        //result = new Vector3(blinkPosition.x + (blinkOffset * blinkDirection.x), blinkPosition.y + (blinkOffset * blinkDirection.y));
                    }
                }
                else
                {
                    result = GetSideVector(blinkPosition, blinkDirection, false, blinkOffset);

                }
            }
            else
            {
                result = GetSideVector(blinkPosition, blinkDirection, true, blinkOffset);
            }

        }
        return result;
    }
    private Vector3 GetSideVector(Vector3 position, Vector3 direction, bool right, float offset)
    {
        Vector3 result;

        if (right)
        {
            result = new Vector3(position.x + (offset * direction.y), position.y + (offset * direction.x), 0);
        }
        else
        {
            result = new Vector3(position.x - (offset * direction.y), position.y - (offset * direction.x), 0);
        }

        return result;
    }
}
