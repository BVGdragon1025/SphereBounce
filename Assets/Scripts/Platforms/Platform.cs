using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    [SerializeField]
    protected float bounceForce;
    protected Rigidbody playerRb;

    private Vector3 _startingPosition;

    private void Start()
    {
        _startingPosition = transform.position;
        playerRb = SphereController.Instance.sphereRb;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.isGameStarted)
        {
            playerRb.velocity = Vector3.zero;
            BounceSphere();
            Debug.Log($"Player velocity: {playerRb.velocity}");
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
