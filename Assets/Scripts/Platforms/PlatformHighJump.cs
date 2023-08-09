using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHighJump : Platform
{
    public override void BounceSphere()
    {
        ResetPlatformSpeed();
        SphereController.Instance.sphereRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
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
