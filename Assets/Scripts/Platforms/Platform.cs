using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    [SerializeField]
    protected float bounceForce;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SphereController.Instance.sphereRb.velocity = Vector3.zero;
            BounceSphere();
            SphereController.Instance.isInAir = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            SphereController.Instance.isInAir = true;
    }

    public abstract void BounceSphere();

    /*
    private void OnEnable()
    {
        Debug.Log($"Platform name: {name}, platform position: {transform.position}");
    }
    */

}
