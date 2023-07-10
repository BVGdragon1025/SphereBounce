using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField]
    private float _bounceForce;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SphereController.Instance.sphereRb.velocity = Vector3.zero;
            SphereController.Instance.sphereRb.AddForce(Vector3.up *  _bounceForce, ForceMode.Impulse);
            SphereController.Instance.isInAir = false;
        }
    }

}
