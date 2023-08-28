using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGravity : Platform
{
    [SerializeField, Tooltip("Determines how long player recovers from gravity going back to normal. \n 0 - No delay.")]
    private float _reverseGravityTime;
    private Vector3 _defaultGravityVector;

    private void OnEnable()
    {
        _defaultGravityVector = Physics.gravity;

        if (!GameManager.Instance.isGravityReversed)
        {
            GameManager.Instance.isGravityReversed = true;
        }
        else
        {
            GameManager.Instance.isGravityReversed = false;
        }
    }

    public override void BounceSphere()
    {
        
        if (!SphereController.Instance.touchedAntiGravity)
        {
            SphereController.Instance.touchedAntiGravity = true;
            StartCoroutine(ReverseGravity());
            playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }
        else
        {
            SphereController.Instance.touchedAntiGravity = false;
            StartCoroutine(RestoreGravity());
            playerRb.AddForce(Vector3.down * bounceForce, ForceMode.Impulse);
        }

        
        GameManager.Instance.CurrentCombo += 1;

    }

    private IEnumerator RestoreGravity()
    {
        yield return new WaitForSeconds(_reverseGravityTime);
        Physics.gravity = _defaultGravityVector;
        yield return StartCoroutine(PrepareToDisable());
    }

    private IEnumerator ReverseGravity()
    {
        yield return new WaitForSeconds(_reverseGravityTime);
        Physics.gravity = Physics.gravity = new Vector3(0, 9.81f, 0);
        yield return StartCoroutine(PrepareToDisable());
    }

    private IEnumerator PrepareToDisable()
    {
        yield return new WaitForSeconds(_reverseGravityTime + 1.0f);
        if (!SphereController.Instance.IsPlayerDead)
        {
            gameObject.SetActive(false);
        }
    }

    private void ResetGravity()
    {
        StopCoroutine(ReverseGravity());
        StopCoroutine(RestoreGravity());
        Physics.gravity = _defaultGravityVector;
    }


}
