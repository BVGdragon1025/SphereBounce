using System;
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
            if (!CompareTag("StartingPlatform"))
            {
                GameManager.Instance.AddScore();
            }
            
            SphereController.Instance.isInAir = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            SphereController.Instance.isInAir = true;
    }

    private void Update()
    {
        if (!SphereController.Instance.isDead)
            transform.Translate(PlatformManager.Instance.PlatformSpeed * Time.deltaTime * Vector3.left);
    }

    public abstract void BounceSphere();

}
