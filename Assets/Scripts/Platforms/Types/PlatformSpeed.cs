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
        if(PlatformManager.Instance.isSpeedActive)
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
        Debug.Log("Coroutine start: ChangePlatformsSpeed");
        PlatformManager.Instance.isSpeedActive = true;
        PlatformManager.Instance.PlatformSpeed *= _speedMultiplier;
        yield return new WaitForSeconds(_speedTimer);

        PlatformManager.Instance.PlatformSpeed /= _speedMultiplier;
        PlatformManager.Instance.isSpeedActive = false;
        Debug.Log("Coroutine ended: ChangePlatformsSpeed");
        yield return new WaitForSeconds(_speedTimer + 0.2f);

        if(!SphereController.Instance.IsPlayerDead) 
            gameObject.SetActive(false);
    }

    private void ResetPlatformSpeed()
    {
        StopCoroutine(ChangePlatformsSpeed());
        Debug.Log("Coroutine stoped!: ChangePlatformsSpeed");
        PlatformManager.Instance.PlatformSpeed /= _speedMultiplier;
        PlatformManager.Instance.isSpeedActive = false;
        
    }
}
