using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSticky : Platform
{

    public override void BounceSphere()
    {
        if (!SphereController.Instance.touchedStickyPlatform)
        {
            SphereController.Instance.touchedStickyPlatform = true;

        }
        else
        {
            playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            SphereController.Instance.touchedStickyPlatform = false; 
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && SphereController.Instance.touchedStickyPlatform)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                playerRb.AddForce(Vector3.down * bounceForce, ForceMode.Impulse);
            }
        }
    }
}
