using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{

    Rigidbody _rb;
    [SerializeField] GameObject _cam;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        _cam.SetActive(false);
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(_rb.velocity);
    }
}
