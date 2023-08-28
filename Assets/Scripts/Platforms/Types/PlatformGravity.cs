using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGravity : Platform
{
    [SerializeField]
    private float _reverseGravityTime;
    private Vector3 _defaultGravityVector;

    private void OnEnable()
    {
        _defaultGravityVector = Physics.gravity;
    }

    public override void BounceSphere()
    {
        ResetGravity();
        GameManager.Instance.CurrentCombo += 1;
        StartCoroutine(ReverseGravity());

    }

    private IEnumerator ReverseGravity()
    {
        Physics.gravity = new Vector3(0, 9.8f, 0);
        yield return new WaitForSeconds(_reverseGravityTime);
        Physics.gravity = _defaultGravityVector;

        yield return new WaitForSeconds(_reverseGravityTime + 0.2f);
        if(!SphereController.Instance.IsPlayerDead)
            gameObject.SetActive(false);
    }

    private void ResetGravity()
    {
        StopCoroutine(ReverseGravity());
        Physics.gravity = _defaultGravityVector;
    }
}
