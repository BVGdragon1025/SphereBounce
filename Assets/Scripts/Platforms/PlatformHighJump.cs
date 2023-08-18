using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHighJump : Platform
{
    [SerializeField]
    private float _timeInAir;

    public override void BounceSphere()
    {
        playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        GameManager.Instance.CurrentCombo += 1;
    }

}
