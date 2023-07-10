using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    private Rigidbody _sphereRb;
    [SerializeField, Tooltip("Amount of force impulse the sphere gets on tap or click")]
    private float _downForce;
    private bool _isInAir;

    // Start is called before the first frame update
    void Start()
    {
        _sphereRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isInAir)
        {
            _sphereRb.AddForce(Vector3.down * _downForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _sphereRb.AddForce(Vector3.up * collision.gameObject.GetComponent<PlatformController>().bounceForce, ForceMode.Impulse);
            _sphereRb.velocity = Vector3.zero;
            _isInAir = false;
        }
            
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
            _isInAir = true;
    }

}
