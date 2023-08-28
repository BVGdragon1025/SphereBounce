using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformNormal : Platform
{
    public override void BounceSphere()
    {
        playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        if (!CompareTag("StartingPlatform"))
        {
            GameManager.Instance.CurrentCombo += 1;
        }
        
    }
}
