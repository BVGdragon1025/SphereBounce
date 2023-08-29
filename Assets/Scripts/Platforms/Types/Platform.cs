using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    [SerializeField]
    protected float bounceForce;
    protected Rigidbody playerRb;
    protected Collider playerColl;
    protected float playerHitLocation;
    protected bool speedPlatformActive;
    [SerializeField]
    protected bool shouldMove;

    private Vector3 _startingPosition;

    private void Start()
    {
        _startingPosition = transform.position;
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        playerColl = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
        speedPlatformActive = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.isGameStarted)
        {
            playerRb.velocity = Vector3.zero;
            playerHitLocation = -transform.localPosition.x;
            Debug.Log($"Player hit on: {transform.position.x}");
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
        if (!SphereController.Instance.IsPlayerDead && GameManager.Instance.isGameStarted && shouldMove)
            transform.Translate(PlatformManager.Instance.PlatformSpeed * Time.deltaTime * Vector3.left);
    }
    public abstract void BounceSphere();

    public void ResetPlatformPosition()
    {
        transform.position = _startingPosition;
    }

}
