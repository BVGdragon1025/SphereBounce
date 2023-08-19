using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformComboBreaker : Platform
{
    public override void BounceSphere()
    {
        playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        GameManager.Instance.CurrentCombo = 1;
    }
}
