using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformNormal : Platform
{
    public override void BounceSphere()
    {
        if(SphereController.Instance.touchedAntiGravity)
            playerRb.AddForce(Vector3.down * bounceForce, ForceMode.Impulse);
        else
            playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);

        if (!CompareTag("StartingPlatform"))
        {
            GameManager.Instance.CurrentCombo += 1;
        }
        
    }
}
