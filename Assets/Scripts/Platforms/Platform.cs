using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    [SerializeField]
    protected float bounceForce;
    private Vector3 _startingPosition;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.isGameStarted)
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

    private void Start()
    {
        _startingPosition = transform.position;
    }

    private void Update()
    {
        if (!SphereController.Instance.isDead && GameManager.Instance.isGameStarted)
            transform.Translate(PlatformManager.Instance.PlatformSpeed * Time.deltaTime * Vector3.left);
    }

    private void OnDisable()
    {
        ResetPlatformPosition();
    }

    public abstract void BounceSphere();

    private void ResetPlatformPosition()
    {
        _startingPosition = transform.position;
    }

}
