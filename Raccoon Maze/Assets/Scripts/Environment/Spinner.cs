using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField, Tooltip("Negative values spin to the other direction")] private float _speed;

    private void Update()
    {
        transform.Rotate(Vector3.forward, _speed * Time.deltaTime);
    }
}
