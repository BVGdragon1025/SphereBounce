using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    [SerializeField]
    protected float bounceForce;
    [SerializeField]
    protected float platformSpeed;

    private void Update()
    {
        gameObject.transform.Translate(platformSpeed * Time.deltaTime * Vector3.left);
    }

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

}
