using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeller : MonoBehaviour
{

    [SerializeField] private float _pushDistance;
    private int _layerMask;

    private void Awake()
    {
        _layerMask = (1 << 11) | (1 << 12);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("Player"))
        {
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, other.transform.position - transform.position, _pushDistance, _layerMask);

            if (rayHit.collider)
            {
                other.transform.position = (rayHit.point);
            }
            else
            {
                other.transform.position = (Vector2) (other.transform.position - (transform.position - other.transform.position).normalized * _pushDistance);
            }
        }
    }
}
