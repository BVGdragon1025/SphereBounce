using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpeed : Platform
{
    [SerializeField, Tooltip("Speed multiplier by which platforms and background will be moving"), Range(0.1f, 3.0f)]
    private float _speedMultiplier;

    public override void BounceSphere()
    {
        ResetPlatformSpeed();
        playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        GameManager.Instance.CurrentCombo += 2;
        StartCoroutine(ChangePlatformsSpeed());
    }

    private IEnumerator ChangePlatformsSpeed()
    {
        Debug.Log("Coroutine start!");
        PlatformManager.Instance.PlatformSpeed *= 2.0f;
        yield return new WaitForSeconds(0.5f);
        PlatformManager.Instance.PlatformSpeed = PlatformManager.Instance.DefaultSpeed;
        Debug.Log("Coroutine end.");
    }

    private void OnDisable()
    {
        ResetPlatformSpeed();
    }

    private void ResetPlatformSpeed()
    {
        StopCoroutine(ChangePlatformsSpeed());
        PlatformManager.Instance.PlatformSpeed = PlatformManager.Instance.DefaultSpeed;
    }
}
