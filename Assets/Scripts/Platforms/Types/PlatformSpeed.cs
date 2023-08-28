using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpeed : Platform
{
    [SerializeField, Tooltip("Speed multiplier by which platforms and background will be moving"), Range(0.1f, 5.0f)]
    private float _speedMultiplier;
    [SerializeField, Tooltip("How long increased speed effect will last, 0 - no speed increase.")]
    private float _speedTimer;

    public override void BounceSphere()
    {
        ResetPlatformSpeed();
        if (SphereController.Instance.touchedAntiGravity)
            playerRb.AddForce(Vector3.down * bounceForce, ForceMode.Impulse);
        else
            playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        GameManager.Instance.CurrentCombo += 2;
        StartCoroutine(ChangePlatformsSpeed());
    }

    private IEnumerator ChangePlatformsSpeed()
    {
        Debug.Log("Coroutine start!");
        PlatformManager.Instance.PlatformSpeed *= 2.0f;
        yield return new WaitForSeconds(_speedTimer);

        PlatformManager.Instance.PlatformSpeed = PlatformManager.Instance.DefaultSpeed;
        Debug.Log("Coroutine end.");
        yield return new WaitForSeconds(_speedTimer + 0.2f);

        if(!SphereController.Instance.IsPlayerDead) 
            gameObject.SetActive(false);
    }

    private void ResetPlatformSpeed()
    {
        StopCoroutine(ChangePlatformsSpeed());
        PlatformManager.Instance.PlatformSpeed = PlatformManager.Instance.DefaultSpeed;
    }
}
